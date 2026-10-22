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
                List<CodeInstruction> result = new List<CodeInstruction>(instructions);
                for (int i = 0; i + 3 < result.Count; i++)
                {
                    if (result[i].opcode != OpCodes.Ldfld ||
                        (!Equals(result[i].operand, rarityGemMesh) && !Equals(result[i].operand, rarityFrameMesh)))
                        continue;

                    MethodInfo getComponent = result[i + 1].operand as MethodInfo;
                    if (result[i + 1].opcode != OpCodes.Callvirt || getComponent == null ||
                        getComponent.DeclaringType != typeof(GameObject) || getComponent.Name != nameof(GameObject.GetComponent) ||
                        !getComponent.IsGenericMethod || getComponent.GetGenericArguments()[0] != typeof(Renderer) ||
                        result[i + 2].opcode != OpCodes.Ldc_I4_0 ||
                        result[i + 3].opcode != OpCodes.Callvirt || !Equals(result[i + 3].operand, setRendererEnabled))
                        continue;

                    // 原地替换以保留跳转标签与异常块，仅保护拼装界面的稀有度装饰。
                    result[i + 1].opcode = OpCodes.Nop;
                    result[i + 1].operand = null;
                    result[i + 3].opcode = OpCodes.Call;
                    result[i + 3].operand = setRarityMeshEnabled;
                }
                return result;
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
