using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace HsMod
{
    public partial class Patcher
    {
        public class PatchAssemble
        {
            private static readonly FieldInfo rarityGemMesh = AccessTools.Field(typeof(Actor), nameof(Actor.m_rarityGemMesh));
            private static readonly FieldInfo rarityFrameMesh = AccessTools.Field(typeof(Actor), nameof(Actor.m_rarityFrameMesh));
            private static readonly MethodInfo getRenderer = AccessTools.Method(typeof(GameObject), nameof(GameObject.GetComponent), Type.EmptyTypes, new[] { typeof(Renderer) });
            private static readonly MethodInfo setRendererEnabled = AccessTools.PropertySetter(typeof(Renderer), nameof(Renderer.enabled));
            private static readonly MethodInfo setRarityMeshEnabled = AccessTools.Method(typeof(PatchAssemble), nameof(SetRarityMeshEnabled));

            [HarmonyPrepare]
            public static bool Prepare()
            {
                // 旧版游戏没有拼装界面；没有可用目标时不能交给 Harmony 处理空列表。
                return TargetMethods().Any();
            }

            [HarmonyTargetMethods]
            public static IEnumerable<MethodBase> TargetMethods()
            {
                Type assembleUIManager = typeof(Actor).Assembly.GetType("AssembleUIManager");
                if (assembleUIManager == null)
                    yield break;

                foreach (string methodName in new[] { "OnShowChoices", "ToggleAssembleUIDisplay" })
                {
                    MethodInfo method = AccessTools.DeclaredMethod(assembleUIManager, methodName);
                    if (method != null)
                        yield return method;
                }
            }

            [HarmonyTranspiler]
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                // CodeMatch 的空操作数会变成通配条件，方法缺失时不应继续匹配。
                if (getRenderer == null || setRendererEnabled == null)
                    return instructions;

                CodeMatcher matcher = new CodeMatcher(instructions)
                    .MatchForward(false,
                        new CodeMatch(instruction => instruction.opcode == OpCodes.Ldfld &&
                            (Equals(instruction.operand, rarityGemMesh) || Equals(instruction.operand, rarityFrameMesh))),
                        new CodeMatch(OpCodes.Callvirt, getRenderer),
                        new CodeMatch(OpCodes.Ldc_I4_0),
                        new CodeMatch(OpCodes.Callvirt, setRendererEnabled));

                if (matcher.IsValid)
                {
                    // 只替换完整的装饰隐藏序列；Set 保留跳转标签和异常块。
                    matcher.Repeat(match => match
                        .Advance(1).Set(OpCodes.Nop, null)
                        .Advance(2).Set(OpCodes.Call, setRarityMeshEnabled));
                }
                return matcher.InstructionEnumeration();
            }

            private static void SetRarityMeshEnabled(GameObject mesh, bool enabled)
            {
                // 异画卡的宝石与边框可以独立缺省，不能由宝石存在推断边框存在（#334）。
                if (mesh == null)
                    return;

                Renderer renderer = mesh.GetComponent<Renderer>();
                if (renderer != null)
                    renderer.enabled = enabled;
            }
        }
    }
}
