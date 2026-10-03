using System;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using static HsMod.PluginConfig;

namespace HsMod
{
    public partial class Patcher
    {
        public class PatchActorMod
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Actor), "SetPortraitTexture")]
            private static bool PatchSetPortraitTexture(Actor __instance, ref Texture texture,
                ref TAG_PREMIUM ___m_premiumType, ref CardDefHandle ___m_cardDefHandle)
            {
                try
                {
                    if (!ModTextures.Value)
                    {
                        return true;
                    }
                    
                    string cardIdInit = __instance?.GetEntity()?.GetCardId() ?? __instance?.GetEntityDef()?.GetCardId();
                    if (string.IsNullOrEmpty(cardIdInit))
                    {
                        return true;
                    }

                    Texture localCardTexture =
                        (Texture)(object)Utils.GetLocalCardTexture(__instance, __instance, ___m_premiumType);
                    if ((object)localCardTexture != null)
                    {
                        texture = localCardTexture;

                        Material portraitMaterial = __instance.GetPortraitMaterial();
                        if ((UnityEngine.Object)portraitMaterial == (UnityEngine.Object)null)
                        {
                            return true;
                        }

                        portraitMaterial.mainTexture = texture;
                        __instance.UpdateCustomFrameDiamondMaterial();
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Utils.MyLogger(LogLevel.Error, $"SetPortraitTexture {e.Message}");
                    return true;
                }

                return true;
            }
        }
    }
}