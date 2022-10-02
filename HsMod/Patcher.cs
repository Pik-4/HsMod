using Blizzard.GameService.SDK.Client.Integration;
using Blizzard.T5.Core;
using Blizzard.T5.Core.Time;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using static HsMod.PluginConfig;

namespace HsMod
{
    //public struct AllPatch
    //{
    //    Harmony mHarmony;
    //    Type mType;
    //}
    public static class PatchManager
    {
        public static List<Harmony> AllHarmony = new List<Harmony>();    //保存补丁信息，方便定向卸载。
        public static List<string> AllHarmonyName = new List<string>();

        public static void LoadPatch(Type loadType)
        {
            try
            {
                Harmony harmony;
                int harmonyCount;
                harmony = Harmony.CreateAndPatchAll(loadType);
                //var harmony = new Harmony(PluginInfo.PLUGIN_GUID + ".Patcher");
                harmonyCount = harmony.GetPatchedMethods().Count();
                Utils.MyLogger(BepInEx.Logging.LogLevel.Warning, $"{loadType.Name} => Patched {harmonyCount} methods");
                AllHarmony.Add(harmony);
                AllHarmonyName.Add(loadType.Name);
            }
            catch (Exception ex)
            {
                Utils.MyLogger(BepInEx.Logging.LogLevel.Error, $"{loadType.Name} => {ex.Message}");
            }
        }

        public static bool UnPatch(string name)
        {
            for (int i = 0; i < AllHarmonyName.Count; i++)
            {
                if (AllHarmonyName[i] == name)
                {
                    AllHarmony[i].UnpatchSelf();
                    AllHarmonyName.Remove(AllHarmonyName[i]);
                    AllHarmony.Remove(AllHarmony[i]);
                    Utils.MyLogger(BepInEx.Logging.LogLevel.Warning, $"Unpatched {name}!");
                    return true;
                }
            }
            return false;
        }

        //委托绑定
        public static void PatchSettingDelegate()
        {
            isPluginEnable.SettingChanged += delegate
            {
                if (isPluginEnable.Value)
                {
                    PatchAll();
                }
                else
                {
                    UnPatchAll();
                }
            };
            isTimeGearEnable.SettingChanged += delegate
            {
                TimeScaleMgr.Get().Update();
            };
            timeGear.SettingChanged += delegate
            {
                TimeScaleMgr.Get().Update();
            };

            isShowCardLargeCount.SettingChanged += delegate
            {
                if (isShowCardLargeCount.Value)
                {
                    LoadPatch(typeof(Patcher.PatchRealtimeCardNum));
                }
                else
                {
                    UnPatch("PatchRealtimeCardNum");
                }
            };
            isIdleKickEnable.SettingChanged += delegate
            {
                //Utils.MyLogger(BepInEx.Logging.LogLevel.Warning, $"SetShouldCheckForInactivity: {isIdleKickEnable.Value}");
                InactivePlayerKicker.Get().SetShouldCheckForInactivity(isIdleKickEnable.Value);
            };
            targetFrameRate.SettingChanged += delegate
            {
                graphicsManager = Blizzard.T5.Services.ServiceManager.Get<IGraphicsManager>();
                if (targetFrameRate.Value > 0 && Options.Get().GetInt(Option.GFX_TARGET_FRAME_RATE) != targetFrameRate.Value)
                    graphicsManager.UpdateTargetFramerate(targetFrameRate.Value, isDynamicFpsEnable.Value);
                else if (targetFrameRate.Value <= 0)
                {

                    graphicsManager.UpdateTargetFramerate(30, true);
                }
            };
            isDynamicFpsEnable.SettingChanged += delegate
            {
                graphicsManager = Blizzard.T5.Services.ServiceManager.Get<IGraphicsManager>();
                if (targetFrameRate.Value > 0 && Options.Get().GetInt(Option.GFX_TARGET_FRAME_RATE) != targetFrameRate.Value)
                    graphicsManager.UpdateTargetFramerate(targetFrameRate.Value, isDynamicFpsEnable.Value);
                else if (targetFrameRate.Value <= 0)
                {
                    graphicsManager.UpdateTargetFramerate(30, true);
                }
            };
            isAutoRecvMercenaryRewardEnable.SettingChanged += delegate
            {
                if (isAutoRecvMercenaryRewardEnable.Value)
                {
                    LoadPatch(typeof(Patcher.PatchMercenariesReward));
                }
                else
                {
                    UnPatch("PatchMercenariesReward");
                }
            };
            isAutoOpenBoxesRewardEnable.SettingChanged += delegate
            {
                if (isAutoOpenBoxesRewardEnable.Value)
                {
                    LoadPatch(typeof(Patcher.PatchBoxesReward));
                }
                else
                {
                    UnPatch("PatchBoxesReward");
                }
            };
            isMoveEnemyCardsEnable.SettingChanged += delegate
            {
                if (isMoveEnemyCardsEnable.Value)
                {
                    LoadPatch(typeof(Patcher.PatchDeathOb));
                }
                else
                {
                    UnPatch("PatchDeathOb");
                }
            };
            isFakeOpenEnable.SettingChanged += delegate
            {
                if (isFakeOpenEnable.Value)
                {
                    LoadPatch(typeof(Patcher.PatchFakePackOpening));
                }
                else
                {
                    UnPatch("PatchFakePackOpening");
                }
            };
            isKarazhanFixEnable.SettingChanged += delegate
            {
                if (isKarazhanFixEnable.Value)
                {
                    LoadPatch(typeof(Patcher.PatchKarazhan));
                }
                else
                {
                    UnPatch("PatchKarazhan");
                }
            };
        }

        public static void PatchAll()
        {
            LoadPatch(typeof(Patcher));
            LoadPatch(typeof(Patcher.PatchMisc));
            LoadPatch(typeof(Patcher.PatchFiresideGathering));
            LoadPatch(typeof(Patcher.PatchEmote));
            LoadPatch(typeof(Patcher.PatchIGMMessage));
            LoadPatch(typeof(Patcher.PatchMercenaries));
            LoadPatch(typeof(Patcher.PatchHearthstone));
            LoadPatch(typeof(Patcher.PatchLogArchive));
            LoadPatch(typeof(Patcher.PatchBattlegrounds));
            LoadPatch(typeof(Patcher.PatchFavorite));
            LoadPatch(typeof(Patcher.PatchFakeDevice));
            LoadPatch(typeof(Patcher.PatchDevOptioins));
            if (isShowCardLargeCount.Value)
            {
                LoadPatch(typeof(Patcher.PatchRealtimeCardNum));
            }
            if (isMoveEnemyCardsEnable.Value)
            {
                LoadPatch(typeof(Patcher.PatchDeathOb));
            }
            if (isAutoRecvMercenaryRewardEnable.Value)
            {
                LoadPatch(typeof(Patcher.PatchMercenariesReward));
            }
            if (isAutoOpenBoxesRewardEnable.Value)
            {
                LoadPatch(typeof(Patcher.PatchBoxesReward));
            }
            if (isFakeOpenEnable.Value)
            {
                LoadPatch(typeof(Patcher.PatchFakePackOpening));
            }
            if (isKarazhanFixEnable.Value)
            {
                LoadPatch(typeof(Patcher.PatchKarazhan));
            }
            TimeScaleMgr.Get().Update();

        }
        public static void UnPatchAll()
        {
            for (int i = 0; i < AllHarmony.Count; i++)
            {
                AllHarmony[i].UnpatchSelf();
                Utils.MyLogger(BepInEx.Logging.LogLevel.Warning, $"Unpatched {AllHarmonyName[i]}!");
            }
            AllHarmony.Clear();
            AllHarmonyName.Clear();
        }
        public static void RePatchAll()
        {
            UnPatchAll();
            PatchAll();
        }
    }

    //前置Patch为harmony补丁，后置Patch为反射
    public class Patcher
    {
        public class PatchMisc
        {
            //移除分辨率限制
            [HarmonyPrefix]
            [HarmonyPatch(typeof(GraphicsResolution), "IsAspectRatioWithinLimit")]
            public static bool PatchAspectRatioWithinLimit(ref bool __result, int width, int height, bool isWindowedMode)
            {
                __result = true;
                return false;
            }

            //使用WebToken登录
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(Hearthstone.Login.DesktopLoginTokenFetcher), "GetTokenFromTokenFetcher")]
            public static IEnumerable<CodeInstruction> PatchGetTokenFromTokenFetcher(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                list.RemoveAt(0);
                list.InsertRange(0, new CodeInstruction[4]
                {
                new CodeInstruction(OpCodes.Ldstr, "Aurora.VerifyWebCredentials"),
                new CodeInstruction(OpCodes.Call, new Func<string, Blizzard.T5.Configuration.VarKey>(Blizzard.T5.Configuration.Vars.Key).Method),
                new CodeInstruction(OpCodes.Ldnull),
                new CodeInstruction(OpCodes.Callvirt, typeof(Blizzard.T5.Configuration.VarKey).GetMethod("GetStr", BindingFlags.Instance | BindingFlags.Public))
                });
                return list;
            }
            private static readonly MethodInfo findEntry = typeof(Blizzard.T5.Configuration.ConfigFile).GetMethod("FindEntry", BindingFlags.Instance | BindingFlags.NonPublic);
            [HarmonyPostfix]
            [HarmonyPatch(typeof(Blizzard.T5.Configuration.ConfigFile), "Load", new Type[] { typeof(string), typeof(bool) })]
            public static void PatchLoad(string path, bool ignoreUselessLines, Blizzard.T5.Configuration.ConfigFile __instance)
            {
                Blizzard.T5.Configuration.ConfigFile.Line token = (Blizzard.T5.Configuration.ConfigFile.Line)findEntry.Invoke(__instance, new object[] { "Aurora.VerifyWebCredentials" }); ;

                if (token != null)
                {
                    string pattern = @"(CN|KR|TW|EU|US)\-[a-f0-9]{32}\-\d+";    // Country-Token-AccountID.Low
                    string argv = String.Join(" ", Environment.GetCommandLineArgs());
                    var res = System.Text.RegularExpressions.Regex.Match(argv, pattern);
                    if (res.Success)
                    {
                        __instance.Set("Aurora.VerifyWebCredentials", res.Value);
                        __instance.Set("Aurora.Env", res.Value.Substring(0, 2).ToLower() + ".actual.battle.net");
                    }
                }
            }

            //禁止发送错误报告
            [HarmonyPrefix]
            [HarmonyPostfix]
            [HarmonyPatch(typeof(Hearthstone.ExceptionReporterControl), "ExceptionReportInitialize")]
            public static void PatchExceptionReporterControl()
            {
                Blizzard.BlizzardErrorMobile.ExceptionReporter.Get().SendExceptions = false;
            }

            //屏蔽错误报告
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Blizzard.BlizzardErrorMobile.ExceptionReporter), "ReportCaughtException", new Type[] { typeof(string), typeof(string) })]
            public static bool PatchReportCaughtException(ref string message, ref string stackTrace)
            {
                Utils.MyLogger(BepInEx.Logging.LogLevel.Warning, "message:" + message + "\tstackTrace" + stackTrace);
                return false;
            }

            //禁用掉线
            [HarmonyPrefix]
            [HarmonyPatch(typeof(InactivePlayerKicker), "SetShouldCheckForInactivity")]
            public static void PatchSetShouldCheckForInactivity(ref bool check)
            {
                if (!isIdleKickEnable.Value)
                {
                    check = false;
                }
            }
            [HarmonyPrefix]
            [HarmonyPatch(typeof(InactivePlayerKicker), "Update")]
            public static void PatchInactivePlayerKickerUpdate()
            {
                if (!isIdleKickEnable.Value)
                {
                    InactivePlayerKicker.Get().SetShouldCheckForInactivity(isIdleKickEnable.Value);
                }
            }

            ////自动重启 没有效果
            //[HarmonyPrefix]
            //[HarmonyPatch(typeof(Application), "Quit", new Type[] {  })]
            //public static void PatchApplicationQuit()
            //{
            //    Utils.MyLogger(BepInEx.Logging.LogLevel.Warning, System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            //    //System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //    System.Diagnostics.Process.Start(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            //}

            //报错退出
            [HarmonyPrefix]
            [HarmonyPatch(typeof(DialogManager), "ShowReconnectHelperDialog")]
            [HarmonyPatch(typeof(ReconnectHelperDialog), "Show")]
            [HarmonyPatch(typeof(Network), "OnFatalBnetError")]
            public static bool PatchError()
            {
                if (isAutoExit.Value)
                {
                    Application.Quit();
                    return false;
                }
                else return true;
            }

            //是否拦截弹窗
            [HarmonyPrefix]
            [HarmonyPatch(typeof(AlertPopup), "Show")]
            public static bool PatchAlertPopupShow(ref UIBButton ___m_okayButton, ref UIBButton ___m_confirmButton, ref UIBButton ___m_cancelButton)
            {
                if (isAlertPopupShow.Value) return true;
                else
                {
                    switch (responseAlertPopup.Value)
                    {
                        case Utils.AlertPopupResponse.OK:
                            ___m_okayButton.TriggerPress();
                            ___m_okayButton.TriggerRelease();
                            break;
                        case Utils.AlertPopupResponse.CONFIRM:
                            ___m_confirmButton.TriggerPress();
                            ___m_confirmButton.TriggerRelease();
                            break;
                        case Utils.AlertPopupResponse.CANCEL:
                            ___m_cancelButton.TriggerPress();
                            ___m_cancelButton.TriggerRelease();
                            break;
                        case Utils.AlertPopupResponse.YES:
                            if (___m_confirmButton.gameObject.activeSelf)
                            {
                                ___m_confirmButton.TriggerPress();
                                ___m_confirmButton.TriggerRelease();
                                break;
                            }
                            else
                            {
                                ___m_okayButton.TriggerPress();
                                ___m_okayButton.TriggerRelease();
                                break;
                            }
                    }
                    return false;
                }
            }

            //屏蔽开屏防沉迷提示
            [HarmonyPrefix]
            [HarmonyPatch(typeof(SplashScreen), "GetRatingsScreenRegion")]
            public static bool PatchGetRatingsScreenRegion()
            {
                return false;
            }

            //结算任务、升级提示
            [HarmonyPrefix]
            [HarmonyPatch(typeof(QuestPopups), "ShowNextQuestNotification")]
            [HarmonyPatch(typeof(EndGameScreen), "ShowMercenariesExperienceRewards")]
            public static bool PatchEndGameScreen()
            {
                return isEndGameScreenShow.Value;
            }

            //战令、成就等奖励领取提示
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Hearthstone.Progression.RewardTrack), "UpdateStatus")]
            public static bool OOOOO(int rewardTrackId, int level, Hearthstone.Progression.RewardTrack.RewardStatus status, bool forPaidTrack, List<PegasusUtil.RewardItemOutput> rewardItemOutput)      //隐藏通行证奖励
            {
                if (!isEndGameScreenShow.Value && status == Hearthstone.Progression.RewardTrack.RewardStatus.GRANTED)
                {
                    Hearthstone.Progression.RewardTrackManager.Get().GetCurrentRewardTrack(Assets.Global.RewardTrackType.GLOBAL).AckReward(rewardTrackId, level, forPaidTrack);
                    return false;
                }
                else return true;
            }

            //清空自动分解的Handler
            [HarmonyPrefix]
            [HarmonyPatch(typeof(CollectionManager), "RegisterCollectionNetHandlers")]
            public static void PatchRegisterCollectionNetHandlers()
            {
                try
                {
                    Network.Get().RemoveNetHandler(PegasusUtil.BoughtSoldCard.PacketID.ID, new Network.NetHandler(Utils.TryRefundCardDisenchantCallback));
                }
                catch
                {
                    ;
                }
            }

            //快速开包
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(PackOpening), "AutomaticallyOpenPack")]
            public static IEnumerable<CodeInstruction> PatchAutomaticallyOpenPack(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Ret);
                if (num > 0)
                {
                    Label label = generator.DefineLabel();
                    list[num].labels.Add(label);
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsQuickPackOpeningEnableValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                    list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldfld, typeof(PackOpening).GetField("m_director", BindingFlags.Instance | BindingFlags.NonPublic)));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, new Func<PackOpeningDirector, IEnumerator>(PackOpeningDirectorPatch.ForceRevealAllCards).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, typeof(MonoBehaviour).GetMethod("StartCoroutine", BindingFlags.Instance | BindingFlags.Public, null, new Type[1]
                    {
                    typeof(IEnumerator)
                    }, null)));
                    list.Insert(num, new CodeInstruction(OpCodes.Pop));
                }
                return list;
            }
            [HarmonyPrefix]
            [HarmonyPatch(typeof(PackOpening), "SpaceToOpenPack")]
            public static bool PatchSpaceToOpenPack(ref bool __result)
            {
                if (PackOpeningDirectorPatch.m_WaitingForAllCardsRevealed)
                {
                    __result = false;
                    return false;
                }
                return true;
            }
            [HarmonyPrefix]
            [HarmonyPatch(typeof(PackOpening), "HandleKeyboardInput")]
            public static bool PatchPackOpeningHandleKeyboardInput(ref bool __result)
            {
                if (PackOpeningDirectorPatch.m_WaitingForAllCardsRevealed && isAutoPackOpeningEnable.Value && isQuickPackOpeningEnable.Value)
                {
                    __result = true;
                    return false;
                }
                return true;
            }
            //尝试分解
            [HarmonyPostfix]
            [HarmonyPatch(typeof(PackOpeningDirector), "OnSpellFinished")]
            public static void PatchPackOpeningDirectorOnSpellFinished()
            {
                PackOpeningDirectorPatch.m_WaitingForCards = false;
                //Utils.MyLogger(BepInEx.Logging.LogLevel.Warning, "PackOpeningDirector.OnSpellFinished");
                if (isAutoRefundCardDisenchantEnable.Value)
                {
                    Utils.TryRefundCardDisenchant();
                }
            }
            [HarmonyPostfix]
            [HarmonyPatch(typeof(PackOpeningDirector), "Awake")]
            public static void PatchPackOpeningDirectorAwake()
            {
                PackOpeningDirectorPatch.m_WaitingForCards = true;
            }

            //展示FPS信息
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(ShowFPS), "Awake")]
            public static IEnumerable<CodeInstruction> PatchShowFPSAwake(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                //remove check HearthstoneApplication.IsPublic()
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].opcode == OpCodes.Brfalse_S || list[i].opcode == OpCodes.Brfalse)
                    {
                        num = i;
                        break;
                    }
                }
                num--;
                if (num > 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        list.RemoveAt(num);
                    }

                }
                return list;
            }
            [HarmonyPrefix]
            [HarmonyPatch(typeof(ShowFPS), "OnGUI")]
            public static bool PatchOnGUI(ref Camera[] ___m_cameras, ref string ___m_fpsText)
            {
                int num = 0;
                if (___m_cameras.Length < Camera.allCamerasCount)
                {
                    ___m_cameras = new Camera[Camera.allCamerasCount];
                }
                int allCameras = Camera.GetAllCameras(___m_cameras);
                for (int i = 0; i < allCameras; i++)
                {
                    if (___m_cameras[i].TryGetComponent<FullScreenEffects>(out FullScreenEffects fullScreenEffects) && fullScreenEffects.IsActive)
                    {
                        num++;
                    }
                }
                string text = ___m_fpsText;
                try { text = text.Split('.')[0]; }
                catch { text = "-1"; }
                if (isShowFPSEnable.Value)
                {
                    GUI.Box(new Rect(0f, 0f, 0f, 0f), text, new GUIStyle("box")
                    {
                        clipping = TextClipping.Overflow,
                        stretchHeight = true,
                        stretchWidth = true,
                        wordWrap = true,
                        fontSize = 54,
                        alignment = TextAnchor.MiddleCenter,
                        contentOffset = new UnityEngine.Vector2(64f, 48f)
                    });
                }
                return false;
            }

            //展示CardID
            [HarmonyPrefix]
            [HarmonyPatch(typeof(CollectionCardVisual), "EnterCraftingMode")]
            public static bool PatchEnterCraftingMode(CollectionCardVisual __instance)
            {
                if (isShowCollectionCardIdEnable.Value)
                {
                    int dId = -1;
                    CollectionUtils.ViewMode viewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
                    switch (viewMode)
                    {

                        case CollectionUtils.ViewMode.BATTLEGROUNDS_GUIDE_SKINS:
                        case CollectionUtils.ViewMode.BATTLEGROUNDS_HERO_SKINS:
                        case CollectionUtils.ViewMode.COINS:
                        case CollectionUtils.ViewMode.CARDS:
                        case CollectionUtils.ViewMode.HERO_SKINS:
                            if (__instance.CardId != "") dId = GameUtils.TranslateCardIdToDbId(__instance.CardId);
                            break;
                        case CollectionUtils.ViewMode.CARD_BACKS:
                            dId = __instance.GetActor().GetComponent<CollectionCardBack>().GetCardBackId();
                            break;
                        default:
                            return true;
                    }
                    UIStatus.Get().AddInfo($"ID: {dId}");
                    ClipboardUtils.CopyToClipboard(dId.ToString());
                }
                return true;
            }
            [HarmonyPostfix]
            [HarmonyPatch(typeof(BaconCollectionDisplay), "ShowFinisherDetailsDisplay")]
            public static void PatchShowFinisherDetailsDisplay(ref Hearthstone.DataModels.BattlegroundsFinisherDataModel dataModel, ref Hearthstone.DataModels.BattlegroundsFinisherCollectionPageDataModel pageModel)
            {
                if (isShowCollectionCardIdEnable.Value)
                {
                    UIStatus.Get().AddInfo($"ID: {dataModel.FinisherDbiId}");
                }
            }
            [HarmonyPostfix]
            [HarmonyPatch(typeof(BaconCollectionDisplay), "ShowBoardDetailsDisplay")]
            public static void PatchShowBoardDetailsDisplay(ref Hearthstone.DataModels.BattlegroundsBoardSkinDataModel dataModel, ref Hearthstone.DataModels.BattlegroundsBoardSkinCollectionPageDataModel pageModel)
            {
                if (isShowCollectionCardIdEnable.Value)
                {
                    UIStatus.Get().AddInfo($"ID: {dataModel.BoardDbiId}");
                }
            }

            //允许放弃
            [HarmonyPrefix]
            [HarmonyPatch(typeof(AdventureDungeonCrawlDisplay), "Update")]
            public static void PatchAdventureDungeonCrawlDisplayUpdate(ref GameObject ___m_retireButton)
            {
                if (isShowRetireForever.Value)
                {
                    ___m_retireButton.SetActive(true);
                }
            }

            //OnApplicationFocus
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Hearthstone.HearthstoneApplication), "OnApplicationFocus")]
            public static bool PatchOnApplicationFocus(bool focus)
            {
                return isOnApplicationFocus.Value;
            }

            //齿轮 需要进行delegate委托
            [HarmonyPrefix]
            [HarmonyPatch(typeof(TimeScaleMgr), "Update")]
            public static bool PatchTimeScaleMgr(ref float ___m_timeScaleMultiplier, ref float ___m_gameTimeScale)
            {
                if (isTimeGearEnable.Value)
                {
                    float timeScale = 1f;
                    if (timeGear.Value > 1) timeScale = (float)timeGear.Value;
                    else if (timeGear.Value < -1) timeScale = -1f / (float)timeGear.Value;
                    if (timeScale >= 8) timeScale = 8f;
                    else if (timeScale <= -8) timeScale = 0.125f;    // will not exec
                    Time.timeScale = ((timeScale > ___m_timeScaleMultiplier) ? ((timeScale + (___m_timeScaleMultiplier - 1f) * 0.5f) * ___m_gameTimeScale) : ((___m_timeScaleMultiplier + (timeScale - 1f) * 0.5f) * ___m_gameTimeScale));
                    return false;
                }
                else return true;
            }

            //未知变速修改
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(SocialToastMgr), "AddToast", new Type[]
            {
                    typeof(UserAttentionBlocker),
                    typeof(string),
                    typeof(SocialToastMgr.TOAST_TYPE),
                    typeof(float),
                    typeof(bool)
            })]
            public static IEnumerable<CodeInstruction> PatchSocialToastMgrAddToast(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Ret);
                if (num > 0)
                {
                    num++;
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_3));
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, typeof(Time).GetProperty("timeScale", BindingFlags.Static | BindingFlags.Public).GetGetMethod()));
                    list.Insert(num++, new CodeInstruction(OpCodes.Mul));
                    list.Insert(num++, new CodeInstruction(OpCodes.Starg_S, (byte)3));
                }
                return list;
            }


        }

        public class PatchBoxesReward
        {
            //自动点击包裹
            [HarmonyPostfix]
            [HarmonyPatch(typeof(RewardBoxesDisplay), "RewardPackageOnComplete")]
            public static void PatchRewardPackageOnComplete(RewardBoxesDisplay.RewardBoxData boxData)
            {
                if (isAutoOpenBoxesRewardEnable.Value)
                {
                    boxData.m_RewardPackage.TriggerPress();
                    boxData.m_RewardPackage.TriggerRelease();
                }
            }

            //自动点击完成
            [HarmonyPostfix]
            [HarmonyPatch(typeof(RewardBoxesDisplay), "OnDoneButtonShown")]
            public static void PatchOnDoneButtonShown(Spell spell, object userData)
            {
                if (isAutoOpenBoxesRewardEnable.Value)
                {
                    RewardBoxesDisplay.Get().m_DoneButton.TriggerPress();
                    RewardBoxesDisplay.Get().m_DoneButton.TriggerRelease();
                }
            }

            //额外奖励
            [HarmonyPostfix]
            [HarmonyPatch(typeof(RewardBoxesDisplay), "OnBonusLootButtonShown")]
            public static void PatchOnBonusLootButtonShown(Spell spell, object userData)
            {
                if (isAutoOpenBoxesRewardEnable.Value)
                {
                    RewardBoxesDisplay.Get().m_BonusLootNextButton.TriggerPress();
                    RewardBoxesDisplay.Get().m_BonusLootNextButton.TriggerRelease();
                }
            }
        }

        public class PatchDevOptioins
        {
            //进入内部模式（炉石开发者模式）
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(Hearthstone.HearthstoneApplication), "GetMode")]
            public static IEnumerable<CodeInstruction> PatchHearthstoneApplicationGetMode(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Brtrue);
                if (num > 0)
                {
                    num++;
                    Label label = generator.DefineLabel();
                    list[num].labels.Add(label);
                    if (list[num].opcode == OpCodes.Ldc_I4_2)
                    {
                        list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                        list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsInternalModeEnableValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                        list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
                        list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                        list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsInternalModeEnableValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                        Label label2 = generator.DefineLabel();
                        list.Insert(num, new CodeInstruction(OpCodes.Brtrue_S, label2));
                        num += 3;
                        list.Insert(num, new CodeInstruction(OpCodes.Ldc_I4_1));
                        list[num++].labels.Add(label2);
                        list.Insert(num, new CodeInstruction(OpCodes.Ret));
                    }
                }
                return list;
            }
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(Hearthstone.HearthstoneApplication), "Job_InitializeMode", MethodType.Enumerator)]
            public static IEnumerable<CodeInstruction> PatchJob_InitializeMode(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Brfalse);
                if (num > 0)
                {
                    object operand = list[num].operand;
                    if (operand is Label label)
                    {
                        Label label2 = generator.DefineLabel();
                        list[num].operand = label2;
                        num += 3;
                        if (list[num].opcode == OpCodes.Ldc_I4_2)
                        {
                            list.Insert(num, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                            list[num++].labels.Add(label2);
                            list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsInternalModeEnableValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                            list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
                            list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                            list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsInternalModeEnableValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                            Label label3 = generator.DefineLabel();
                            list.Insert(num, new CodeInstruction(OpCodes.Brtrue_S, label3));
                            num += 2;
                            Label label4 = generator.DefineLabel();
                            list.Insert(num++, new CodeInstruction(OpCodes.Br_S, label4));
                            list.Insert(num, new CodeInstruction(OpCodes.Ldc_I4_1));
                            list[num++].labels.Add(label3);
                            list[num].labels.Add(label4);
                        }
                    }
                }
                return list;
            }




        }

        public class PatchRealtimeCardNum
        {
            //显示卡牌数量 或||控制暂时有问题，需要进行delegate委托
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(CollectionCardCount), "UpdateVisibility")]
            public static IEnumerable<CodeInstruction> PatchCollectionCardCountUpdateVisibility(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].opcode == OpCodes.Ldc_I4_S && (sbyte)list[i].operand == 10)
                    {
                        num = i;
                        break;
                    }
                }
                num--;
                if (num > 0)
                {
                    list[num] = new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method);
                    list[num + 1] = new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsShowCardLargeCountValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod());
                    list[num + 2] = new CodeInstruction(OpCodes.Brfalse_S, list[num + 2].operand);
                }
                return list;
            }
        }


        //好友观战相关
        public class PatchDeathOb
        {
            private static readonly MethodInfo registerCreateGameListenerInfo = typeof(GameState).GetMethod("RegisterCreateGameListener", BindingFlags.Instance | BindingFlags.Public, null, new Type[2]
                                                                        {
                                                                        typeof(GameState.CreateGameCallback),
                                                                        typeof(object)
                                                                        }, null);

            [HarmonyTranspiler]
            [HarmonyPatch(typeof(FriendMgr), "OnSceneLoaded")]
            public static IEnumerable<CodeInstruction> PatchOnSceneLoaded(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Ldftn && (x.operand as MethodInfo).Name == "OnGameOver");
                if (num > 0)
                {
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldftn, new Action<GameState.CreateGamePhase, object>(FriendMgrPatch.OnGameCreated).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Newobj, typeof(GameState.CreateGameCallback).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[2]
                    {
                    typeof(object),
                    typeof(IntPtr)
                    }, null)));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldnull));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, registerCreateGameListenerInfo));
                    list.Insert(num++, new CodeInstruction(OpCodes.Pop));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_0));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
                }
                return list;
            }

            [HarmonyTranspiler]
            [HarmonyPatch(typeof(ZoneHand), "GetCardScale")]
            public static IEnumerable<CodeInstruction> PatchZoneHandGetCardScale(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Ldfld && (x.operand as FieldInfo).Name == "enemyHand");
                if (num > 0)
                {
                    num++;
                    object operand = list[num++].operand;
                    Label label = generator.DefineLabel();
                    list[num].labels.Add(label);
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsMoveEnemyCardsEnableValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                    list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldfld, typeof(ZoneHand).GetField("m_controller", BindingFlags.Instance | BindingFlags.NonPublic)));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(Player).GetMethod("IsRevealed", BindingFlags.Instance | BindingFlags.Public)));
                    list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldc_R4, 0.41f));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldc_R4, 0.085f));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldc_R4, 0.41f));
                    list.Insert(num++, new CodeInstruction(OpCodes.Newobj, typeof(Vector3).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[3]
                    {
                    typeof(float),
                    typeof(float),
                    typeof(float)
                    }, null)));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ret));
                    list.Insert(num, new CodeInstruction(OpCodes.Br_S, operand));
                }
                return list;
            }

            [HarmonyTranspiler]
            [HarmonyPatch(typeof(ZoneHand), "GetCardRotation", new Type[]
            {
                    typeof(int),
                    typeof(int)
            })]
            public static IEnumerable<CodeInstruction> PatchZoneHandGetCardRotation(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Callvirt && (x.operand as MethodInfo).Name == "IsRevealed");
                if (num > 0)
                {
                    num++;
                    object operand = list[num++].operand;
                    Label label = generator.DefineLabel();
                    list[num].labels.Add(label);
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsMoveEnemyCardsEnableValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                    list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_0));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_1));
                    list.Insert(num++, new CodeInstruction(OpCodes.Conv_R4));
                    list.Insert(num++, new CodeInstruction(OpCodes.Mul));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_1));
                    list.Insert(num++, new CodeInstruction(OpCodes.Add));
                    list.Insert(num++, new CodeInstruction(OpCodes.Stloc_3));
                    list.Insert(num, new CodeInstruction(OpCodes.Br_S, operand));
                }
                return list;
            }

            [HarmonyTranspiler]
            [HarmonyPatch(typeof(ZoneHand), "GetCardPosition", new Type[]
            {
                typeof(int),
                typeof(int)
            })]
            public static IEnumerable<CodeInstruction> PatchZoneHandGetCardPosition(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Callvirt && (x.operand as MethodInfo).Name == "IsRevealed");
                if (num > 0)
                {
                    num++;
                    object operand = list[num++].operand;
                    Label label = generator.DefineLabel();
                    list[num].labels.Add(label);
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsMoveEnemyCardsEnableValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                    list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldflda, typeof(ZoneHand).GetField("centerOfHand", BindingFlags.Instance | BindingFlags.NonPublic)));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldfld, typeof(Vector3).GetField("z", BindingFlags.Instance | BindingFlags.Public)));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_1));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_3));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldc_I4_2));
                    list.Insert(num++, new CodeInstruction(OpCodes.Div));
                    list.Insert(num++, new CodeInstruction(OpCodes.Sub));
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<int, int>(Mathf.Abs).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Conv_R4));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldc_R4, 2f));
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<float, float, float>(Mathf.Pow).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldc_I4_4));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_3));
                    list.Insert(num++, new CodeInstruction(OpCodes.Mul));
                    list.Insert(num++, new CodeInstruction(OpCodes.Conv_R4));
                    list.Insert(num++, new CodeInstruction(OpCodes.Div));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_2));
                    list.Insert(num++, new CodeInstruction(OpCodes.Mul));
                    list.Insert(num++, new CodeInstruction(OpCodes.Sub));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_S, (byte)6));
                    list.Insert(num++, new CodeInstruction(OpCodes.Sub));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldc_R4, 0.6f));
                    list.Insert(num++, new CodeInstruction(OpCodes.Sub));
                    list.Insert(num++, new CodeInstruction(OpCodes.Stloc_S, (byte)9));
                    list.Insert(num, new CodeInstruction(OpCodes.Br_S, operand));
                }
                return list;
            }
        }

        public class PatchFiresideGathering
        {

            //炉边聚会
            [HarmonyPrefix]
            [HarmonyPatch(typeof(FiresideGatheringManager), "RequestNearbyFSGs")]
            public static bool PatchRequestNearbyFSGs(ref bool ___m_isRequestNearbyFSGsPending)
            {
                if (isFiresideGatheringEnable.Value)
                {
                    ___m_isRequestNearbyFSGsPending = true;
                    Network.Get().RequestNearbyFSGs(firesideGatheringLatitude.Value, firesideGatheringLongitude.Value, firesideGatheringGpsAccuracy.Value, null);
                    return false;
                }
                return true;
            }
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(FiresideGatheringManager), "OnFSGAllowed")]
            public static IEnumerable<CodeInstruction> PatchOnFSGAllowed(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Callvirt && (x.operand as MethodInfo).Name == "get_GPSOrWifiServicesAvailable");
                if (num > 0)
                {
                    num++;
                    object operand = list[num].operand;
                    list.Insert(num++, new CodeInstruction(OpCodes.Brtrue_S, operand));
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsFiresideGatheringEnableValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                }
                return list;
            }

            private static readonly MethodInfo ChangeStateInfo = typeof(FiresideGatheringLocationHelperDialog).GetMethod("ChangeState", BindingFlags.Instance | BindingFlags.NonPublic);
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(FiresideGatheringLocationHelperDialog), "Start")]
            public static IEnumerable<CodeInstruction> PatchFiresideGatheringLocationHelperDialogStart(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Ldfld && (x.operand as FieldInfo).Name == "m_isCheckInFailure");
                if (num > 0)
                {
                    num--;
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsFiresideGatheringEnableValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                    Label label = generator.DefineLabel();
                    list[num].labels.Add(label);
                    list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldc_I4_3));
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, ChangeStateInfo));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ret));
                }
                return list;
            }
        }

        //表情相关的Patch
        public class PatchEmote
        {
            //思考表情
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(MatchingQueueTab), "Update")]
            [HarmonyPatch(typeof(ThinkEmoteManager), "Update")]
            public static IEnumerable<CodeInstruction> PatchThinkEmoteManagerOrMatchingQueueTabUpdate(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                MethodInfo deltaTimeInfo = typeof(Time).GetProperty("deltaTime", BindingFlags.Static | BindingFlags.Public).GetGetMethod();
                MethodInfo getMethod = typeof(Time).GetProperty("unscaledDeltaTime", BindingFlags.Static | BindingFlags.Public).GetGetMethod();
                int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Call && x.operand as MethodInfo == deltaTimeInfo);
                if (num > 0)
                {
                    list[num].operand = getMethod;
                }
                return list;
            }

            //思考表情
            [HarmonyPrefix]
            [HarmonyPatch(typeof(ThinkEmoteManager), "Update")]
            public static bool PatchThinkEmoteManagerUpdate()
            {
                return isThinkEmotesEnable.Value;
            }

            //表情冷却
            [HarmonyPrefix]
            [HarmonyPatch(typeof(EmoteHandler), "EmoteSpamBlocked")]
            public static bool PatchUpdateAllMutes(ref float ___m_timeSinceLastEmote, ref bool __result)
            {
                if (isExtendedBMEnable.Value)
                {
                    if (___m_timeSinceLastEmote < 1.5f)
                    {
                        __result = true;
                        return false;
                    }
                    __result = false;
                    return false;
                }

                return true;
            }

            //开局自动禁止表情
            [HarmonyPrefix]
            [HarmonyPatch(typeof(EnemyEmoteHandler), "IsSquelched")]
            public static bool PatchIsSquelched(ref int playerId, ref bool __result)
            {
                if (receiveEnemyEmoteLimit.Value == 0)
                {
                    __result = true;
                    return false;
                }
                return true;
            }


            //表情管理，数量限制存在小误差
            [HarmonyPrefix]
            [HarmonyPatch(typeof(RemoteActionHandler), "CanReceiveEnemyEmote")]
            public static bool PatchPreCanReceiveEnemyEmote(ref EmoteType emoteType, ref int playerId, ref bool __result)
            {
                if (receiveEnemyEmoteLimit.Value == 0)
                {
                    __result = false;
                    return false;
                }
                else return true;
            }
            private static int m_lastPlayerId;
            private static float m_lastEnemyEmoteTime;
            private static int m_chainedEnemyEmotes;
            [HarmonyPostfix]
            [HarmonyPatch(typeof(RemoteActionHandler), "CanReceiveEnemyEmote")]
            public static void PatchPostCanReceiveEnemyEmote(ref EmoteType emoteType, ref int playerId, ref bool __result)
            {
                int emotesBeforeBlock;
                if (!__result || (emotesBeforeBlock = receiveEnemyEmoteLimit.Value) <= 0)
                {
                    return;
                }
                float time = Time.time;
                if (m_lastPlayerId == playerId)
                {
                    if (m_lastEnemyEmoteTime != 0f && time - m_lastEnemyEmoteTime < 9f)
                    {
                        m_chainedEnemyEmotes++;
                        if (m_chainedEnemyEmotes > emotesBeforeBlock)
                        {
                            EnemyEmoteHandler.Get().SquelchPlayer(playerId);
                        }
                    }
                    else
                    {
                        m_chainedEnemyEmotes = 1;
                    }
                }
                else
                {
                    m_chainedEnemyEmotes = 1;
                    m_lastPlayerId = playerId;
                }
                m_lastEnemyEmoteTime = time;
            }
        }
        public class PatchBattlegrounds
        {
            //闭了,鲍勃!
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(TB_BaconShop), "PlayBobLineWithoutText", MethodType.Enumerator)]
            public static IEnumerable<CodeInstruction> PatchPlayBobLineWithoutText(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Stfld && (x.operand as FieldInfo).Name == "<>1__state");
                if (num > 0)
                {
                    num++;
                    Label label = generator.DefineLabel();
                    list[num].labels.Add(label);
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsShutUpBobEnableValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                    list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldc_I4_0));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ret));
                }
                return list;
            }
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(TB_BaconShop), "HandleGameOverWithTiming", MethodType.Enumerator)]
            public static IEnumerable<CodeInstruction> PatchHandleGameOverWithTiming(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Callvirt && (x.operand as MethodInfo).Name == "UpdateLayout");
                if (num > 0)
                {
                    num++;
                    Label label = generator.DefineLabel();
                    list[num].labels.Add(label);
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsShutUpBobEnableValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                    list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldc_I4_0));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ret));
                }
                return list;
            }

            //快速战斗 - 理论上可以用于所有模式 现只应用于酒馆战旗
            [HarmonyReversePatch]
            [HarmonyPatch(typeof(SpellController), "OnProcessTaskList")]
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static void AttackSpellControllerBase(AttackSpellController instance) {; }
            [HarmonyPrefix]
            [HarmonyPatch(typeof(AttackSpellController), "OnProcessTaskList")]
            public static bool PatchAttackSpellControllerOnProcessTaskList(AttackSpellController __instance)
            {
                if (isBgsQuickModeEnable.Value && GameMgr.Get().IsBattlegrounds())
                {
                    AttackSpellControllerBase(__instance);
                    return false;
                }
                else return true;
            }
            [HarmonyReversePatch]
            [HarmonyPatch(typeof(SpellController), "OnProcessTaskList")]
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static void DeathSpellControllerBase(DeathSpellController instance) {; }
            [HarmonyPrefix]
            [HarmonyPatch(typeof(DeathSpellController), "OnProcessTaskList")]
            public static bool PatchDeathSpellControllerOnProcessTaskList(DeathSpellController __instance)
            {
                if (isBgsQuickModeEnable.Value && GameMgr.Get().IsBattlegrounds())
                {
                    DeathSpellControllerBase(__instance);
                    return false;
                }
                else return true;
            }
            [HarmonyReversePatch]
            [HarmonyPatch(typeof(SpellController), "OnProcessTaskList")]
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static void TriggerSpellControllerBase(TriggerSpellController instance) {; }
            [HarmonyPrefix]
            [HarmonyPatch(typeof(TriggerSpellController), "OnProcessTaskList")]
            public static bool PatchTriggerSpellControllerOnProcessTaskList(TriggerSpellController __instance)
            {
                if (isBgsQuickModeEnable.Value && GameMgr.Get().IsBattlegrounds())
                {
                    TriggerSpellControllerBase(__instance);
                    return false;
                }
                else return true;
            }
            [HarmonyReversePatch]
            [HarmonyPatch(typeof(SpellController), "OnProcessTaskList")]
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static void SubSpellControllerBase(SubSpellController instance) {; }
            [HarmonyPrefix]
            [HarmonyPatch(typeof(SubSpellController), "OnProcessTaskList")]
            public static bool PatchSubSpellControllerOnProcessTaskList(SubSpellController __instance)
            {
                if (isBgsQuickModeEnable.Value && GameMgr.Get().IsBattlegrounds())
                {
                    SubSpellControllerBase(__instance);
                    return false;
                }
                else return true;
            }
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(SideQuestSpellController), "OnProcessTaskList")]
            public static IEnumerable PatchSubSideQuestSpellControllerOnProcessTaskList(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Callvirt && (x.operand as MethodInfo).Name == "SetSecretTriggered");

                if (num > 0)
                {
                    Label label = generator.DefineLabel();
                    num++;
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsBgsQuickModeEnableValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                    list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, typeof(SpellController).GetMethod("OnProcessTaskList", BindingFlags.Instance | BindingFlags.NonPublic)));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ret));
                    list[num].labels.Add(label);
                }

                return list;
            }

        }

        public class PatchHearthstone
        {
            //金卡钻石卡补丁
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Entity), "GetPremiumType")]
            public static bool PatchGetPremiumType(Entity __instance, ref TAG_PREMIUM __result)
            {
                if (GameMgr.Get() != null && !GameMgr.Get().IsBattlegrounds() && GameState.Get() != null && GameState.Get().IsGameCreatedOrCreating())
                {
                    Utils.CardState mGolden = goldenCardState.Value;
                    Utils.CardState mDiamond = diamondCardState.Value;

                    int dbid = GameUtils.TranslateCardIdToDbId(__instance.GetCardId());
                    bool mercDiamond = false;
                    bool isMerc = false;
                    if (Utils.CheckInfo.IsMercenarySkin(__instance.GetCardId(), out Utils.MercenarySkin skin))
                    {
                        isMerc = true;
                        if (dbid == skin.Diamond)
                        {
                            mercDiamond = true;
                        }
                    }


                    if (__instance.IsControlledByOpposingSidePlayer() && (!isOpponentGoldenCardShow.Value))
                    {
                        __result = TAG_PREMIUM.NORMAL;
                        if (isMerc)
                        {
                            __instance.SetTag(GAME_TAG.PREMIUM, TAG_PREMIUM.NORMAL);
                            __instance.SetTag(GAME_TAG.HAS_DIAMOND_QUALITY, false);
                        }
                        return false;
                    }


                    if (__instance.HasTag(GAME_TAG.HAS_DIAMOND_QUALITY) || mercDiamond)
                    {
                        if (mDiamond == Utils.CardState.All || (mDiamond == Utils.CardState.OnlyMy && __instance.IsControlledByFriendlySidePlayer()))
                        {
                            if (mercDiamond)
                            {
                                __instance.SetTag(GAME_TAG.PREMIUM, TAG_PREMIUM.DIAMOND);
                                __instance.SetTag(GAME_TAG.HAS_DIAMOND_QUALITY, true);
                            }
                            __result = TAG_PREMIUM.DIAMOND;
                            return false;
                        }
                        if ((mDiamond == Utils.CardState.Disabled) && (mGolden == Utils.CardState.Disabled))
                        {
                            __result = TAG_PREMIUM.NORMAL;
                            if (isMerc)
                            {
                                __instance.SetTag(GAME_TAG.PREMIUM, TAG_PREMIUM.NORMAL);
                                __instance.SetTag(GAME_TAG.HAS_DIAMOND_QUALITY, false);
                            }
                            return false;
                        }
                    }
                    if (mGolden == Utils.CardState.All || (mGolden == Utils.CardState.OnlyMy && __instance.IsControlledByFriendlySidePlayer()))
                    {
                        __result = TAG_PREMIUM.GOLDEN;
                        return false;
                    }
                    if (mGolden == Utils.CardState.Disabled)
                    {
                        __result = TAG_PREMIUM.NORMAL;
                        if (isMerc)
                        {
                            __instance.SetTag(GAME_TAG.PREMIUM, TAG_PREMIUM.NORMAL);
                            __instance.SetTag(GAME_TAG.HAS_DIAMOND_QUALITY, false);
                        }
                        return false;
                    }
                }
                return true;
            }


            //设置下个对手、用于获取战网标签
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(PlayerLeaderboardManager), "SetNextOpponent")]
            public static IEnumerable<CodeInstruction> PatchSetNextOpponent(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Ret);
                if (num > 0)
                {
                    num += 2;
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_1, null));
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Action<int>(PlayerLeaderboardManagerPatch.UpdateCurrentOpponent).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0, null));
                }
                return list;
            }

            //设置当前对手、用于获取战网标签
            [HarmonyPostfix]
            [HarmonyPatch(typeof(PlayerLeaderboardManager), "SetCurrentOpponent")]
            public static void PatchSetCurrentOpponent(ref int opponentPlayerId)
            {
                if (opponentPlayerId != -1)
                {
                    PlayerLeaderboardManagerPatch.UpdateCurrentOpponent(opponentPlayerId);
                }
            }


            //显示完整战网ID
            [HarmonyPrefix]
            [HarmonyPatch(typeof(BnetPlayer), "GetBestName")]
            public static bool PatchBnetPlayerGetBestName(BnetPlayer __instance, ref BnetAccount ___m_account, ref Map<BnetGameAccountId, BnetGameAccount> ___m_gameAccounts, ref BnetGameAccount ___m_hsGameAccount, ref string __result)
            {
                if (__instance != BnetPresenceMgr.Get().GetMyPlayer())
                {
                    if (___m_account != null)
                    {
                        string fullName = ___m_account.GetFullName();
                        if (fullName != null)
                        {
                            __result = fullName;
                            return false;
                        }
                        if (___m_account.GetBattleTag() != null)
                        {
                            __result = ___m_account.GetBattleTag().GetName();
                            return false;
                        }
                    }
                    foreach (KeyValuePair<BnetGameAccountId, BnetGameAccount> gameAccount in ___m_gameAccounts)
                    {
                        if (gameAccount.Value.GetBattleTag() != (BnetBattleTag)null)
                        {
                            __result = isFullnameShow.Value ? gameAccount.Value.GetBattleTag().ToString() : gameAccount.Value.GetBattleTag().GetName();
                            Utils.CacheLastOpponentFullName = gameAccount.Value.GetBattleTag().ToString();
                            Utils.CacheLastOpponentAccountID = gameAccount.Value.GetOwnerId();
                            return false;
                        }
                    }
                    __result = null;
                    return false;
                }
                __result = ___m_hsGameAccount.GetBattleTag()?.GetName();
                return false;
            }

            //允许添加对手
            [HarmonyPrefix]
            [HarmonyPatch(typeof(BnetRecentPlayerMgr), "IsCurrentOpponent")]
            public static bool PatchIsCurrentOpponent(BnetPlayer player, ref bool __result)
            {

                if (isFullnameShow.Value || isShortcutsEnable.Value)
                {
                    // var callerMethod = new System.Diagnostics.StackFrame(2, false)?.GetMethod();
                    System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();           // get call stack
                    System.Diagnostics.StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)
                    // find call stack method names
                    foreach (System.Diagnostics.StackFrame stackFrame in stackFrames)
                    {
                        if (stackFrame.GetMethod().Name == "ShouldEnableOption")
                        {
                            __result = false;
                            return false;
                        }
                    }

                }
                return true;
            }


            //显示天梯等级
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(NameBanner), "UpdateMedalWhenReady", MethodType.Enumerator)]
            public static IEnumerable<CodeInstruction> PatchUpdateMedalWhenReady(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Callvirt && (x.operand as MethodInfo).Name == "get_ShowOpponentRankInGame");
                if (num > 0)
                {
                    num++;
                    object operand = list[num].operand;
                    list.Insert(num++, new CodeInstruction(OpCodes.Brtrue_S, operand));
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                    list.Insert(num, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsOpponentRankInGameShowValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                }
                return list;
            }
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(NameBanner), "Initialize")]
            public static IEnumerable<CodeInstruction> PatchNameBannerInitialize(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Call && (x.operand as MethodInfo).Name == "IsGameTypeRanked");
                if (num > 0)
                {
                    num++;
                    Label label = generator.DefineLabel();
                    list[num].labels.Add(label);
                    Label label2 = generator.DefineLabel();
                    list.Insert(num++, new CodeInstruction(OpCodes.Brtrue_S, label2));
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsOpponentRankInGameShowValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                    list.Insert(num++, new CodeInstruction(OpCodes.Br_S, label));
                    list.Insert(num, new CodeInstruction(OpCodes.Ldc_I4_1));
                    list[num].labels.Add(label2);
                }
                return list;
            }

            //跳过英雄介绍
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(MulliganManager), "HandleGameStart")]
            public static IEnumerable<CodeInstruction> PatchHandleGameStart(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Callvirt && (x.operand as MethodInfo).Name == "ShouldSkipMulligan");
                if (num > 0)
                {
                    num++;
                    object operand = list[num].operand;
                    list.Insert(num++, new CodeInstruction(OpCodes.Brtrue_S, operand));
                    list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<ConfigValue>(ConfigValue.Get).Method));
                    list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(ConfigValue).GetProperty("IsSkipHeroIntroValue", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
                }
                return list;
            }

            //设置静音
            [HarmonyPrefix]
            [HarmonyPatch(typeof(SoundManager), "UpdateAllMutes")]
            public static bool PatchUpdateAllMutes(ref bool ___m_mute)
            {
                ___m_mute = SoundManagerPatch.MuteKeyPressed | ___m_mute;
                return true;
            }

            [HarmonyPostfix]
            [HarmonyPatch(typeof(GameEntity), "ShowEndGameScreen")]
            public static void PatchEndGameScreenShow(ref TAG_PLAYSTATE playState, ref Spell enemyBlowUpSpell, ref Spell friendlyBlowUpSpell)
            {
                // 自动举报 + 对局记录
                try
                {
                    if (!GameMgr.Get().IsSpectator() && (Utils.CacheLastOpponentAccountID != null) && (!String.IsNullOrEmpty(Utils.CacheLastOpponentFullName)))
                    {
                        if (isAutoReportEnable.Value)
                        {
                            Utils.TryReportOpponent();

                            string finalResult = "未知";
                            switch (playState)
                            {
                                case TAG_PLAYSTATE.WINNING:
                                case TAG_PLAYSTATE.WON:
                                    finalResult = "胜利";
                                    break;
                                case TAG_PLAYSTATE.CONCEDED:
                                case TAG_PLAYSTATE.LOST:
                                case TAG_PLAYSTATE.LOSING:
                                    finalResult = "失败";
                                    break;
                                case TAG_PLAYSTATE.TIED:
                                    finalResult = "平局";
                                    break;
                                default:
                                    break;
                            }
                            finalResult = $"{String.Join("<br />", DateTime.Now.ToString().Split(' '))},{finalResult},-,{GameMgr.Get().GetGameType()},{Utils.CacheLastOpponentFullName},";
                            finalResult += $"High:{Utils.CacheLastOpponentAccountID.High}+Low:{Utils.CacheLastOpponentAccountID.Low} => 已举报";
                            System.IO.File.AppendAllText(hsMatchLogPath.Value, finalResult + "\n");
                            Utils.CacheLastOpponentAccountID = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utils.MyLogger(BepInEx.Logging.LogLevel.Error, ex);
                    Utils.CacheLastOpponentAccountID = null;
                }

                // 自动退出
                if (autoQuitTimer.Value > 0 && ConfigValue.Get().RunningTime >= autoQuitTimer.Value)
                {
                    Utils.MyLogger(BepInEx.Logging.LogLevel.Warning, "定时重启！即将退出游戏...");
                    Application.Quit();
                }
            }

            // 手牌追踪
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Player), "IsRevealed")]
            public static bool PatchPlayerIsRevealed(ref bool __result)
            {
                if (isCardTrackerEnable.Value)
                {
                    __result = true;
                    return false;
                }
                return true;
            }
            // 剑圣奥卡尼的选择识别
            [HarmonyPrefix]
            [HarmonyPatch(typeof(GameState), "OnPowerHistory")]
            public static void PatchDebugPrintPower(GameState __instance, ref List<Network.PowerHistory> powerList)
            {
                if (isCardTrackerEnable.Value)
                {
                    foreach (var powerHistory in powerList)
                    {
                        if (powerHistory.Type == Network.PowerType.SHOW_ENTITY)
                        {
                            Network.Entity netEntity = ((Network.HistShowEntity)powerHistory).Entity;
                            if (netEntity != null && __instance.GetEntity(netEntity.ID) != null && __instance.GetEntity(netEntity.ID).GetControllerSide() == global::Player.Side.OPPOSING)
                            {
                                if (netEntity.CardID == "TSC_032t")
                                {
                                    UIStatus.Get().AddInfo("注意：反制随从！", 30f);
                                    return;
                                }
                                if (netEntity.CardID == "TSC_032t2")
                                {
                                    UIStatus.Get().AddInfo("注意：反制法术！", 30f);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            // 变装大师识别，40牌识别
            [HarmonyPrefix]
            [HarmonyPatch(typeof(MulliganManager), "SetMulliganBannerText", new Type[] { typeof(string), typeof(string) })]
            public static void PatchSetMulliganBannerText(MulliganManager __instance, ref string title, ref string subtitle, ref bool ___friendlyPlayerGoesFirst)
            {
                if (isCardTrackerEnable.Value && !GameMgr.Get().IsBattlegrounds())
                {
                    int deckCardCount = GameState.Get().GetOpposingPlayer().GetController().GetDeckZone().GetCardCount();
                    int handCardCount = GameState.Get().GetOpposingPlayer().GetController().GetHandZone().GetCardCount();
                    int oppoCardCount = deckCardCount + handCardCount;
                    oppoCardCount = ___friendlyPlayerGoesFirst ? oppoCardCount - 1 : oppoCardCount;

                    int setasideNum = 0;
                    int playNum = 0;
                    Map<int, Entity> entityMap = GameState.Get().GetEntityMap();
                    foreach (KeyValuePair<int, Entity> keyValuePair in entityMap)
                    {
                        Entity value = keyValuePair.Value;
                        if (value != null && value.GetControllerSide() == Player.Side.OPPOSING)
                        {
                            if (value.GetZone() == TAG_ZONE.SETASIDE)
                            {
                                setasideNum++;
                            }
                            if (value.GetZone() == TAG_ZONE.PLAY)
                            {
                                playNum++;
                            }
                        }
                    }
                    bool isRogue = setasideNum == 1 && playNum == 1;

                    string heroClass = isRogue ? GameStrings.GetClassName(TAG_CLASS.ROGUE)
                                               : GameStrings.GetClassName(GameState.Get().GetOpposingPlayer().GetHero().GetClass());
                    title = $"对手职业是{heroClass}，套牌{oppoCardCount}张";
                }
            }
            //// 生成Power.log
            //[HarmonyPrefix]
            //[HarmonyPatch(typeof(Log), "get_ConfigPath")]
            //public static void PatchConfigPath()
            //{
            //    bool isLogConfigExist = false;
            //    string logConfigPath = string.Format("{0}/{1}", PlatformFilePaths.ExternalDataPath, "log.config");
            //    if (!System.IO.File.Exists(logConfigPath))
            //    {
            //        logConfigPath = string.Format("{0}/{1}", PlatformFilePaths.PersistentDataPath, "log.config");
            //        if (!System.IO.File.Exists(logConfigPath))
            //        {
            //            logConfigPath = PlatformFilePaths.GetAssetPath("log.config", false);
            //            isLogConfigExist = System.IO.File.Exists(logConfigPath);
            //        }
            //        else
            //        {
            //            isLogConfigExist = true;
            //        }
            //    }
            //    else
            //    {
            //        isLogConfigExist = true;
            //    }

            //    if (!isLogConfigExist)
            //    {
            //        logConfigPath = string.Format("{0}/{1}", PlatformFilePaths.ExternalDataPath, "log.config");
            //        System.IO.File.WriteAllText(logConfigPath, "[Arena]\r\nLogLevel=1\r\nFilePrinting=True\r\nConsolePrinting=False\r\nScreenPrinting=False\r\nVerbose=False\r\n[Decks]\r\nLogLevel=1\r\nFilePrinting=True\r\nConsolePrinting=False\r\nScreenPrinting=False\r\nVerbose=False\r\n[Power]\r\nLogLevel=1\r\nFilePrinting=True\r\nConsolePrinting=False\r\nScreenPrinting=False\r\nVerbose=True\r\n");
            //    }
            //}


        }

        // 日志文件路径获取 LogArchive.Get().LogPath
        // 无意义Patch，尝试internal与getter的patch
        public class PatchLogArchive
        {
            [HarmonyTargetMethod]
            private static MethodInfo PublicLogArchiveLogPath()
            {
                return AccessTools.TypeByName("LogArchive").GetMethod("get_LogPath"); ;
            }

            [HarmonyPostfix]
            public static void PatchLogPathGetter(string __result)
            {
                if (hsLogPath.Value != __result)
                    hsLogPath.Value = __result;
            }
        }

        public class PatchFavorite
        {
            //加载处理
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Entity), "LoadCard")]
            public static void PatchLoadCard(Entity __instance, ref string cardId, ref Entity.LoadCardData data)
            {
                if (cardId != null
                    && Utils.CheckInfo.IsMercenarySkin(cardId, out Utils.MercenarySkin skin))
                {
                    if ((goldenCardState.Value == Utils.CardState.Disabled) && (diamondCardState.Value == Utils.CardState.Disabled))
                    {
                        cardId = GameUtils.TranslateDbIdToCardId(skin.Default);
                        return;
                    }
                    if ((diamondCardState.Value == Utils.CardState.Disabled) || (mercenaryDiamondCardState.Value == Utils.CardState.Disabled))
                    {
                        if (GameUtils.TranslateCardIdToDbId(cardId) == skin.Diamond)
                        {
                            cardId = GameUtils.TranslateDbIdToCardId(skin.Default);
                            return;
                        }
                    }
                    if (!isOpponentGoldenCardShow.Value)
                    {
                        if (__instance.GetCard().GetControllerSide() == global::Player.Side.OPPOSING)
                        {
                            cardId = GameUtils.TranslateDbIdToCardId(skin.Default);
                            return;
                        }
                    }
                    if ((diamondCardState.Value == Utils.CardState.All) || (mercenaryDiamondCardState.Value == Utils.CardState.All))
                    {
                        if (skin.hasDiamond)
                        {
                            cardId = GameUtils.TranslateDbIdToCardId(skin.Diamond);
                            return;
                        }
                    }
                    if ((diamondCardState.Value == Utils.CardState.OnlyMy) || (mercenaryDiamondCardState.Value == Utils.CardState.OnlyMy))
                    {
                        if (skin.hasDiamond && (__instance.GetCard().GetControllerSide() == global::Player.Side.FRIENDLY))
                        {
                            cardId = GameUtils.TranslateDbIdToCardId(skin.Diamond);
                            return;
                        }
                    }
                    if ((randomMercenarySkinEnable.Value == Utils.CardState.OnlyMy) || (randomMercenarySkinEnable.Value == Utils.CardState.All))
                    {
                        List<int> dbids = new List<int>();
                        dbids.AddRange(skin.Id);
                        dbids.Remove(skin.Diamond);
                        var dbid = dbids[UnityEngine.Random.Range(0, dbids.Count)];
                        if (randomMercenarySkinEnable.Value == Utils.CardState.OnlyMy)
                        {
                            if (__instance.GetCard().GetControllerSide() == global::Player.Side.FRIENDLY)
                            {
                                cardId = GameUtils.TranslateDbIdToCardId(dbid);
                                return;
                            }
                        }
                        cardId = GameUtils.TranslateDbIdToCardId(dbid);
                        return;
                    }
                }
                //string rawCardId = cardId;
                else if (cardId != null && DefLoader.Get()?.GetEntityDef(cardId)?.GetCardType() == TAG_CARDTYPE.HERO_POWER)
                {
                    if (skinHero.Value == 637 && skinOpposingHero.Value == 637)    // 防止技能识别错误，待测试、待验证
                    {
                        return;
                    }
                    else if (skinHero.Value != -1 && __instance.GetCard().GetControllerSide() == global::Player.Side.FRIENDLY)
                    {
                        cardId = GameUtils.GetHeroPowerCardIdFromHero(skinHero.Value);
                    }
                    else if (skinOpposingHero.Value != -1 && __instance.GetCard().GetControllerSide() == global::Player.Side.OPPOSING)
                    {
                        cardId = GameUtils.GetHeroPowerCardIdFromHero(skinOpposingHero.Value);
                    }

                    else if (__instance.GetCard().GetControllerSide() == global::Player.Side.FRIENDLY && HeroesMapping.Count != 0)

                    {
                        // Replace HeroPower
                        //UpdateCardsMappingReal(cardId, Utils.SkinType.HEROPOWER);
                        Utils.UpdateHeroPowerMapping();
                        HeroesPowerMapping.TryGetValue(cardId, out string res);
                        cardId = (res != null && res != "" && res != string.Empty) ? res : cardId;
                        return;
                    }
                }

                else if (Utils.CheckInfo.IsHero(cardId, out Assets.CardHero.HeroType heroType))
                {
                    if (skinBob.Value != -1 && heroType == Assets.CardHero.HeroType.BATTLEGROUNDS_GUIDE)
                    {
                        //UpdateCardsMappingReal(cardId, Utils.SkinType.BOB);
                        if (Utils.CheckInfo.IsHero(skinBob.Value, out Assets.CardHero.HeroType _))
                            cardId = GameUtils.TranslateDbIdToCardId(skinBob.Value);
                    }
                    else if (heroType == Assets.CardHero.HeroType.BATTLEGROUNDS_HERO
                            && CardsMapping.Count != 0
                            && __instance.GetCard().GetControllerSide() == Player.Side.FRIENDLY
                        )
                    {
                        //UpdateCardsMappingReal(cardId, Utils.SkinType.BATTLEGROUNDSHERO);
                        if (skinHero.Value != -1)
                            cardId = GameUtils.TranslateDbIdToCardId(skinHero.Value);
                        else
                        {
                            //LoadSkinsConfigFromFile();
                            if (HeroesMapping.TryGetValue(GameUtils.TranslateCardIdToDbId(cardId), out int dbid))
                            {
                                if (Utils.CheckInfo.IsHero(dbid, out Assets.CardHero.HeroType res))
                                    if (res == Assets.CardHero.HeroType.BATTLEGROUNDS_HERO)
                                        cardId = GameUtils.TranslateDbIdToCardId(dbid);
                            }
                        }
                    }
                    else if (cardId.Substring(0, 5) == "HERO_"
                        && DefLoader.Get().GetEntityDef(cardId).GetCardType() == TAG_CARDTYPE.HERO
                        && CardsMapping.Count != 0
                        )
                    {
                        if (__instance.GetCard().GetControllerSide() == Player.Side.FRIENDLY)
                        {
                            //UpdateCardsMappingReal(cardId, Utils.SkinType.HERO);
                            if (skinHero.Value != -1)
                                cardId = GameUtils.TranslateDbIdToCardId(skinHero.Value);
                            else
                            {
                                //LoadSkinsConfigFromFile();
                                if (HeroesMapping.TryGetValue(GameUtils.TranslateCardIdToDbId(cardId), out int dbid))
                                {
                                    if (Utils.CheckInfo.IsHero(dbid, out Assets.CardHero.HeroType res))
                                        if (res != Assets.CardHero.HeroType.BATTLEGROUNDS_HERO || res != Assets.CardHero.HeroType.BATTLEGROUNDS_GUIDE)
                                            cardId = GameUtils.TranslateDbIdToCardId(dbid);
                                }
                            }
                        }
                        else if (__instance.GetCard().GetControllerSide() == Player.Side.OPPOSING)
                        {
                            if (skinOpposingHero.Value != -1)
                                cardId = GameUtils.TranslateDbIdToCardId(skinOpposingHero.Value);
                        }
                    }
                }
                else if (skinCoin.Value != -1
                        //&& !GameMgr.Get().IsBattlegrounds()
                        && cardId != null && cardId.Length > 4
                        && Utils.CheckInfo.IsCoin(cardId)
                        && __instance.GetCard().GetControllerSide() == Player.Side.FRIENDLY)
                {
                    //int coin = skinCoin.Value;
                    //UpdateCardsMappingReal(cardId, Utils.SkinType.COIN);
                    cardId = GameUtils.TranslateDbIdToCardId(skinCoin.Value);
                }
            }

            //鲍勃替换语音
            [HarmonyPrefix]
            [HarmonyPatch(typeof(TB_BaconShop), "GetFavoriteBattlegroundsGuideSkinCardId")]
            public static bool PatchGetFavoriteBattlegroundsGuideSkinCardId(ref string __result)
            {
                if (skinBob.Value == -1)
                    return true;
                else
                {
                    if (Utils.CheckInfo.IsHero(skinBob.Value, out Assets.CardHero.HeroType _))
                    {
                        __result = GameUtils.TranslateDbIdToCardId(skinBob.Value);
                        return false;
                    }
                    else return true;
                }
            }


            //游戏面板替换
            [HarmonyPrefix]
            [HarmonyPatch(typeof(GameMgr), "ChangeBoardIfNecessary")]
            public static bool PatchChangeBoardIfNecessary(ref Network.GameSetup ___m_gameSetup)
            {
                if ((skinBoard.Value != -1) && Utils.CheckInfo.IsBoard())
                {
                    ___m_gameSetup.Board = skinBoard.Value;
                    return false;
                }
                return true;
            }

            //卡背替换
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Gameplay), "InitCardBacks")]
            public static bool PatchGameplayInitCardBacks()
            {
                if (skinCardBack.Value != -1 && Utils.CheckInfo.IsCardBack())
                {
                    Player friendlySidePlayer = GameState.Get()?.GetFriendlySidePlayer();
                    if (friendlySidePlayer != null)
                        _ = friendlySidePlayer.GetCardBackId();
                    int opponentCardBackID = 0;
                    Player opposingSidePlayer = GameState.Get()?.GetOpposingSidePlayer();
                    if (opposingSidePlayer != null)
                        opponentCardBackID = opposingSidePlayer.GetCardBackId();
                    int friendlyCardBackID = skinCardBack.Value;
                    if (GameMgr.Get().IsBattlegrounds())   // FIXME: 酒馆对战中可能无法正常显示对手卡背
                    {
                        opponentCardBackID = friendlyCardBackID;
                    }

                    CardBackManager.Get().SetGameCardBackIDs(friendlyCardBackID, opponentCardBackID);
                    return false;
                }
                else return true;
            }
            //替换开包卡背
            private static readonly MethodInfo getValidCardBackID = typeof(CardBackManager).GetMethod("GetValidCardBackID", BindingFlags.Instance | BindingFlags.NonPublic);
            private static readonly MethodInfo loadCardBackPrefabIntoSlot = typeof(CardBackManager).GetMethod("LoadCardBackPrefabIntoSlot", BindingFlags.Instance | BindingFlags.NonPublic);
            [HarmonyPrefix]
            [HarmonyPatch(typeof(CardBackManager), "LoadCardBackIdIntoSlot")]
            public static bool PatchGameplayInitCardBacks(ref int cardBackId,
                                                          ref CardBackManager.CardBackSlot slot,
                                                          ref Map<int, CardBackData> ___m_cardBackData,
                                                          CardBackManager __instance
                                                          )
            {
                if (skinCardBack.Value != -1 && Utils.CheckInfo.IsCardBack() && SceneMgr.Get().GetMode() == SceneMgr.Mode.PACKOPENING && slot == CardBackManager.CardBackSlot.FAVORITE)
                {
                    int validCardBackID = (int)getValidCardBackID.Invoke(__instance, new object[] { skinCardBack.Value });
                    //int validCardBackID = skinCardBack.Value;
                    if (___m_cardBackData.TryGetValue(validCardBackID, out CardBackData cardBackData))
                    {
                        loadCardBackPrefabIntoSlot?.Invoke(__instance, new object[] { (AssetReference)cardBackData.PrefabName, slot });
                    }
                    return false;
                }
                else return true;
            }

            //酒馆对战面板
            [HarmonyPrefix]
            [HarmonyPatch(typeof(BaconBoard), "OnBoardSkinChosen")]
            public static void PatchOnBoardSkinChosen(ref int chosenBoardSkinId)
            {
                if (skinBgsBoard.Value != 0 && Utils.CheckInfo.IsBgsBoard())
                    chosenBoardSkinId = skinBgsBoard.Value;
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(FinisherGameplaySettings), "GetFinisherGameplaySettings")]
            public static bool PatchGetFinisherGameplaySettings(ref Entity hero, ref FinisherGameplaySettings __result)
            {
                int num;
                if (skinBgsFinisher.Value != -1
                    && Utils.CheckInfo.IsBgsFinisher()
                    && hero.GetControllerSide() == Player.Side.FRIENDLY
                    )
                {
                    num = skinBgsFinisher.Value;
                }
                else
                {
                    return true;
                }
                if (num <= 0)
                {
                    Log.Spells.PrintError(hero.GetDebugName() + " has no tag BATTLEGROUNDS_FAVORITE_FINISHER. Using Default Finisher.", Array.Empty<object>());
                    num = 1;
                }
                BattlegroundsFinisherDbfRecord battlegroundsFinisherDbfRecord = GameDbf.BattlegroundsFinisher.GetRecord(num);
                if (battlegroundsFinisherDbfRecord == null)
                {
                    Log.Spells.PrintError(string.Format("No Finisher was found for Finisher ID {0}. Using default finisher.", num), Array.Empty<object>());
                    battlegroundsFinisherDbfRecord = GameDbf.BattlegroundsFinisher.GetRecord(1);
                }
                AssetReference assetReference = AssetReference.CreateFromAssetString(battlegroundsFinisherDbfRecord.GameplaySettings);
                Blizzard.T5.AssetManager.AssetHandle<FinisherGameplaySettings> assetHandle = ((assetReference != null) ? AssetLoader.Get().LoadAsset<FinisherGameplaySettings>(assetReference, AssetLoadingOptions.None) : null);
                FinisherGameplaySettings finisherGameplaySettings = (assetHandle ? assetHandle.Asset : null);
                if (finisherGameplaySettings == null)
                {
                    Log.Spells.PrintError(string.Format("Finisher ID {0} is missing its finisher settings entirely in HE2. Using default finisher.", num), Array.Empty<object>());
                    battlegroundsFinisherDbfRecord = GameDbf.BattlegroundsFinisher.GetRecord(1);
                    assetReference = AssetReference.CreateFromAssetString(battlegroundsFinisherDbfRecord.GameplaySettings);
                    assetHandle = AssetLoader.Get().LoadAsset<FinisherGameplaySettings>(assetReference, AssetLoadingOptions.None);
                    finisherGameplaySettings = assetHandle.Asset;
                }
                __result = finisherGameplaySettings;
                return false;
            }

            //偏好硬币修改，不需要patch
            [HarmonyPrefix]
            [HarmonyPatch(typeof(CoinManager), "GetFavoriteCoinId")]
            public static bool PatchGetFavoriteCoinId(ref int __result)
            {
                if (skinCoin.Value == 0) return true;
                if (Utils.CheckInfo.IsCoin())
                {
                    int res = 0;
                    foreach (var record in GameDbf.Coin.GetRecords())
                    {
                        if (record != null)
                        {
                            if (record.CardId == skinCoin.Value)
                            {
                                res = record.ID;
                                break;
                            }
                        }
                    }
                    __result = res;
                    return false;
                }
                return true;
            }
            //[HarmonyPrefix]
            //[HarmonyPatch(typeof(CoinManager), "GetFavoriteCoinCardId")]
            //public static bool PatchGetFavoriteCoinCardId(ref string __result)
            //{
            //    if (skinCoin.Value == 0) return true;
            //    if (Utils.CheckInfo.IsCoin())
            //    {
            //        __result = GameUtils.TranslateDbIdToCardId(skinCoin.Value);
            //        return false;
            //    }
            //    return true;
            //}
        }

        //移除推销;拦截削弱补丁信息
        public class PatchIGMMessage
        {
            //拦截削弱补丁信息
            [HarmonyPostfix]
            [HarmonyPatch(typeof(CardListPopup), "Show")]
            public static void PatchCardListPopupShow(ref UIBButton ___m_okayButton)
            {
                if (!isIGMMessageShow.Value)
                {
                    ___m_okayButton.TriggerPress();
                    ___m_okayButton.TriggerRelease();
                }
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(Hearthstone.InGameMessage.ViewCountController), "GetViewCount")]
            public static bool PatchGetViewCount(ref int __result, string uid)
            {
                if (isIGMMessageShow.Value) return true;
                __result = 0;
                return false;
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(Hearthstone.InGameMessage.UI.MessagePopupDisplay), "GetMessageCount")]
            public static bool PatchGetMessageCount(ref int __result, Hearthstone.InGameMessage.UI.PopupEvent eventID)
            {
                if (isIGMMessageShow.Value) return true;
                __result = 0;
                return false;
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(Hearthstone.InGameMessage.ViewCountController), "Serialize")]
            [HarmonyPatch(typeof(Hearthstone.InGameMessage.ViewCountController), "Deserialize")]
            public static bool PatchViewCountController()
            {
                if (isIGMMessageShow.Value) return true;
                return false;
            }

            //[HarmonyPrefix]
            //[HarmonyPatch(typeof(Hearthstone.InGameMessage.UI.MessagePopupDisplay), "CanDisplayShopMessages")]
            //public static bool PatchCanDisplayShopMessages(ref bool __result)
            //{
            //    if (isIGMMessageShow.Value) return true;
            //    else
            //    {
            //        __result = false;
            //        return false;
            //    }
            //}

            [HarmonyPostfix]
            [HarmonyPatch(typeof(Hearthstone.InGameMessage.UI.MessagePopupDisplay), "DisplayIGMMessage")]
            public static void PatchDisplayIGMMessage(Hearthstone.InGameMessage.UI.MessagePopupDisplay __instance)
            {
                if (isIGMMessageShow.Value) return;
                Traverse.Create(__instance).Method("OnMessageClosed").GetValue();
                Traverse.Create(__instance).Method("OnMessageClosed").GetValue();
            }

            //天梯结算奖励
            [HarmonyPrefix]
            [HarmonyPatch(typeof(SeasonEndDialog), "ShowWhenReady")]
            public static bool PatchSeasonEndDialog(SeasonEndDialog __instance)
            {
                if (isIGMMessageShow.Value) return true;
                Traverse.Create(__instance).Method("Finish").GetValue();
                return false;
            }

        }

        //与佣兵挂机插件可能会冲突 故单独写出
        public class PatchMercenariesReward
        {
            //天梯奖励
            [HarmonyPrefix]
            [HarmonyPatch(typeof(MercenariesSeasonRewardsDialog), "Show")]
            [HarmonyPatch(typeof(MercenariesSeasonRewardsDialog), "ShowWhenReady")]
            public static bool PatchPatchSeasonEndDialog(MercenariesSeasonRewardsDialog __instance)
            {
                if (isAutoRecvMercenaryRewardEnable.Value)
                {
                    Traverse.Create(__instance).Field("AckAndHide").GetValue();
                    return false;
                }
                else return true;
            }

            //天梯奖励
            [HarmonyPrefix]
            [HarmonyPatch(typeof(DialogManager), "ProcessMercenariesSeasonRewardsDialog")]
            public static bool PatchProcessMercenariesSeasonRewardsDialog(ref DialogManager.DialogRequest request,
                                                                          ref MercenariesSeasonRewardsDialog dialog)
            {
                if (isAutoRecvMercenaryRewardEnable.Value)
                {
                    MercenariesSeasonRewardsDialog.Info info = (MercenariesSeasonRewardsDialog.Info)request.m_info;
                    Network.Get().AckNotice(info.m_noticeId);
                    return false;
                }
                else return true;
            }
            //无效Patch
            //[HarmonyPrefix]
            //[HarmonyPatch(typeof(RewardPopups), "ShowNextMercenariesSeasonRewards")]
            //public static bool PatchShowNextMercenariesSeasonRewards(ref bool __result, ref RewardPopups __instance)
            //{
            //    if (isAutoRecvMercenaryRewardEnable.Value)
            //    {
            //        if (SceneMgr.Get().GetMode() != SceneMgr.Mode.LOGIN)
            //        {
            //            __result = false;
            //            return false;
            //        }                    
            //        NetCache.ProfileNoticeMercenariesSeasonRewards nextMercenariesSeasonRewardsNotice = (NetCache.ProfileNoticeMercenariesSeasonRewards)Traverse.Create(__instance).Field("GetNextMercenariesSeasonRewardsNotice").GetValue();
            //        if (nextMercenariesSeasonRewardsNotice == null)
            //        {
            //            __result = false;
            //            return false;
            //        }
            //        Network.Get().AckNotice(nextMercenariesSeasonRewardsNotice.NoticeID);
            //        __result = false;
            //        return false;
            //    }
            //    else return true;
            //}

            // 可以屏蔽天梯等级奖励箱子，但是有异常
            //[HarmonyPrefix]
            //[HarmonyPatch(typeof(RewardPopups), "GetNextMercenariesSeasonRewardsNotice")]
            //public static bool PatchGetNextMercenariesSeasonRewardsNotice(ref NetCache.ProfileNoticeMercenariesSeasonRewards __result)
            //{
            //    if (isAutoRecvMercenaryRewardEnable.Value)
            //    {
            //            foreach (NetCache.ProfileNotice profileNotice in NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>().Notices)
            //            {
            //                if (profileNotice.Type == NetCache.ProfileNotice.NoticeType.MERCENARIES_SEASON_REWARDS)
            //                {
            //                    // NetCache.ProfileNoticeMercenariesSeasonRewards;
            //                    Network.Get().AckNotice(profileNotice.NoticeID);
            //                }
            //            }
            //        __result = null;
            //        return false;
            //    }
            //    return true;
            //}
            //[HarmonyPrefix]
            //[HarmonyPatch(typeof(RewardPopups), "HasUnAckedRewards")]
            //public static bool PatchHasUnAckedRewards(ref List<Reward> ___m_rewards,ref bool __result)
            //{
            //    if (isAutoRecvMercenaryRewardEnable.Value)
            //    {
            //        foreach (Reward reward in ___m_rewards)
            //        {
            //            RewardData rewardData = reward.Data;
            //            Reward.Type rewardType = reward.RewardType;
            //            if(rewardType - Reward.Type.MERCENARY_EXP > 2)
            //            {
            //                rewardData.AcknowledgeNotices();
            //                ___m_rewards.Remove(reward);
            //            }
            //            if(rewardType - Reward.Type.MERCENARY_EXP > 1)
            //            {
            //                rewardData.AcknowledgeNotices();
            //                ___m_rewards.Remove(reward);
            //            }
            //        }
            //        __result = false;
            //        return false;
            //    }
            //    return true;
            //}

            //不显示佣兵宝箱奖励
            [HarmonyPrefix]
            [HarmonyPatch(typeof(RewardPopups), "ShowMercenariesRewards")]
            public static bool PatchShowMercenariesRewards(ref bool autoOpenChest,
                                                           ref NetCache.ProfileNoticeMercenariesRewards rewardNotice,
                                                           ref NetCache.ProfileNoticeMercenariesRewards bonusRewardNotice,
                                                           ref Action doneCallback,
                                                           ref RewardPopups __instance,
                                                           ref bool __result
                                                           )
            {
                if (isAutoRecvMercenaryRewardEnable.Value)
                {
                    autoOpenChest = true;
                    if (rewardNotice != null)
                    {
                        Network.Get().AckNotice(rewardNotice.NoticeID);
                        if (bonusRewardNotice != null)
                        {
                            Network.Get().AckNotice(bonusRewardNotice.NoticeID);
                        }
                        doneCallback?.Invoke();
                        Traverse.Create(__instance).Field("OnPopupClosed").GetValue();
                        __result = false;
                        return false;
                    }
                }
                return true;
            }
        }

        public class PatchMercenaries
        {
            public static readonly float m_FieldOfViewDefault = BoardCameras.Get().m_FieldOfViewDefault;
            public static readonly float m_FieldOfViewZoomed = BoardCameras.Get().m_FieldOfViewZoomed;
            //禁止战斗缩放
            [HarmonyPrefix]
            [HarmonyPatch(typeof(LettuceMissionEntity), "SetFullScreenFXForCombat")]
            public static bool PatchSetFullScreenFXForCombat()
            {
                if (isMercenaryBattleZoom.Value)
                {
                    return true;
                }
                else
                {
                    BoardCameras.Get().m_FieldOfViewDefault = m_FieldOfViewDefault;
                    BoardCameras.Get().m_FieldOfViewZoomed = m_FieldOfViewDefault;
                    return false;
                }
            }


        }

        public class PatchFakePackOpening
        {
            //激活开包模拟
            [HarmonyPrefix]
            [HarmonyPatch(typeof(GameUtils), "IsFakePackOpeningEnabled")]
            public static bool PatchIsFakePackOpeningEnabled(ref bool __result)
            {
                if (isFakeOpenEnable.Value == false)
                {
                    return true;
                }
                __result = true;
                return false;
            }
            //设置模拟卡包数量
            [HarmonyPrefix]
            [HarmonyPatch(typeof(GameUtils), "GetFakePackCount")]
            public static bool PatchGetFakePackCount(ref int __result)
            {
                if (isFakeOpenEnable.Value == false)
                {
                    return true;
                }
                __result = fakePackCount.Value >= 0 ? fakePackCount.Value : 0;
                return false;
            }
            //设置模拟卡包类型
            [HarmonyPrefix]
            [HarmonyPatch(typeof(NetCache), "GetTestData")]
            public static bool PatchGetTestData(ref Type type, ref object __result)
            {
                if (isFakeOpenEnable.Value == false)
                {
                    return true;
                }
                if (type == typeof(NetCache.NetCacheBoosters) && GameUtils.IsFakePackOpeningEnabled())
                {
                    NetCache.NetCacheBoosters netCacheBoosters = new NetCache.NetCacheBoosters();
                    int fakePackCount = GameUtils.GetFakePackCount();
                    NetCache.BoosterStack boosterStack = new NetCache.BoosterStack
                    {
                        Id = (int)fakeBoosterDbId.Value,
                        Count = fakePackCount
                    };
                    netCacheBoosters.BoosterStacks.Add(boosterStack);
                    __result = netCacheBoosters;
                    return false;
                }
                __result = null;
                return false;
            }

            //模拟开包
            [HarmonyReversePatch]
            [HarmonyPatch(typeof(MonoBehaviour), "StopCoroutine", new Type[] { typeof(Coroutine) })]
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static void PackOpeningBaseStopCoroutine(PackOpening instance, Coroutine routine) {; }
            private static readonly MethodInfo onDirectorFinished = typeof(PackOpening).GetMethod("OnDirectorFinished",
                BindingFlags.Instance | BindingFlags.NonPublic);
            private static readonly MethodInfo onBoosterOpened = typeof(PackOpening).GetMethod("OnBoosterOpened",
                BindingFlags.Instance | BindingFlags.NonPublic);
            [HarmonyPrefix]
            [HarmonyPatch(typeof(PackOpening), "OpenBooster")]
            public static bool PatchOpenBooster(ref UnopenedPack pack,
                                                ref PackOpening __instance,
                                                ref float ___m_packOpeningStartTime,
                                                ref int ___m_packOpeningId,
                                                ref GameObject ___m_InputBlocker,
                                                ref Coroutine ___m_autoOpenPackCoroutine,
                                                ref PackOpeningDirector ___m_director,
                                                ref int ___m_lastOpenedBoosterId,
                                                ref UIBScrollable ___m_UnopenedPackScroller
                )
            {
                if (isFakeOpenEnable.Value == false)
                {
                    return true;
                }
                Hearthstone.Progression.AchievementManager.Get().PauseToastNotifications();
                int num = (int)fakeBoosterDbId.Value;
                if (!GameUtils.IsFakePackOpeningEnabled())
                {
                    num = pack.GetBoosterId();
                    ___m_packOpeningStartTime = Time.realtimeSinceStartup;
                    ___m_packOpeningId = num;
                    BoosterPackUtils.OpenBooster(num);
                }
                ___m_InputBlocker.SetActive(true);
                if (___m_autoOpenPackCoroutine != null)
                {
                    PackOpeningBaseStopCoroutine(__instance, ___m_autoOpenPackCoroutine);
                    ___m_autoOpenPackCoroutine = null;
                }

                object target = __instance;
                Delegate myDelegate = Delegate.CreateDelegate(typeof(EventHandler), target, onDirectorFinished);
                EventHandler myMethod = myDelegate as EventHandler;
                ___m_director.OnFinishedEvent += myMethod;
                ___m_lastOpenedBoosterId = num;
                BnetBar.Get().HideCurrencyFrames(false);
                if (GameUtils.IsFakePackOpeningEnabled())
                {
                    onBoosterOpened?.Invoke(__instance, null);
                }
                ___m_UnopenedPackScroller.Pause(true);
                return false;
            }
            // 开包结果替换
            [HarmonyPrefix]
            [HarmonyPatch(typeof(PackOpening), "OnBoosterOpened")]
            public static bool PatchOnBoosterOpened(ref float ___m_packOpeningStartTime,
                                               ref PackOpeningDirector ___m_director,
                                               ref int ___m_lastOpenedBoosterId,
                                               ref int ___m_packOpeningId,
                                               ref bool ___m_autoOpenPending)
            {
                if (isFakeOpenEnable.Value == false)
                {
                    return true;
                }
                float timeToRegisterPackOpening = Time.realtimeSinceStartup - ___m_packOpeningStartTime;
                ___m_director.Play(___m_lastOpenedBoosterId, timeToRegisterPackOpening, ___m_packOpeningId);
                ___m_autoOpenPending = false;
                //List<NetCache.BoosterCard> list = Network.Get().OpenedBooster();
                if (isFakeRandomResult.Value)
                {
                    Utils.GenerateRandomCard(isFakeRandomRarity.Value, isFakeRandomPremium.Value);
                }
                List<NetCache.BoosterCard> cards = new List<NetCache.BoosterCard>
            {
                new NetCache.BoosterCard
                {
                    Def = {
                            Name = GameUtils.TranslateDbIdToCardId(fakeCardID1.Value),
                            Premium = fakeCardPremium1.Value
                        }
                },
                new NetCache.BoosterCard
                {
                    Def = {
                            Name = GameUtils.TranslateDbIdToCardId(fakeCardID2.Value),
                            Premium = fakeCardPremium2.Value
                        }
                },
                new NetCache.BoosterCard
                {
                    Def = {
                            Name = GameUtils.TranslateDbIdToCardId(fakeCardID3.Value),
                            Premium = fakeCardPremium3.Value
                        }
                },
                new NetCache.BoosterCard
                {
                    Def = {
                            Name = GameUtils.TranslateDbIdToCardId(fakeCardID4.Value),
                            Premium = fakeCardPremium4.Value
                        }
                },
                new NetCache.BoosterCard
                {
                    Def = {
                            Name = GameUtils.TranslateDbIdToCardId(fakeCardID5.Value),
                            Premium = fakeCardPremium5.Value
                        }
                }
            };
                ___m_director.OnBoosterOpened(cards);
                return false;
            }
        }

        public class PatchFakeDevice
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(Network), "GetPlatformBuilder")]
            public static void PatchGetPlatformBuilder(Network __instance, ref PegasusShared.Platform __result)
            {
                OSCategory oscategory;
                ScreenCategory screenCategory;
                string model;
                switch (fakeDevicePreset.Value)
                {
                    case Utils.DevicePreset.Default:
                        return;
                    case Utils.DevicePreset.iPad:
                        oscategory = OSCategory.iOS;
                        screenCategory = ScreenCategory.Tablet;
                        model = "iPad13,11";
                        break;
                    case Utils.DevicePreset.iPhone:
                        oscategory = OSCategory.iOS;
                        screenCategory = ScreenCategory.Phone;
                        model = "iPhone13,4";
                        break;
                    case Utils.DevicePreset.Phone:
                        oscategory = OSCategory.Android;
                        screenCategory = ScreenCategory.Phone;
                        model = "SAMSUNG-SM-G930FD";
                        break;
                    case Utils.DevicePreset.Tablet:
                        oscategory = OSCategory.Android;
                        screenCategory = ScreenCategory.Tablet;
                        model = "SAMSUNG-SM-G920F";
                        break;
                    case Utils.DevicePreset.HuaweiPhone:
                        oscategory = OSCategory.Android;
                        screenCategory = ScreenCategory.Phone;
                        model = "Huawei Nova 8";
                        break;
                    case Utils.DevicePreset.Custom:
                        oscategory = fakeDeviceOs.Value;
                        screenCategory = fakeDeviceScreen.Value;
                        model = fakeDeviceName.Value;
                        break;
                    default:
                        return;
                }
                __result.Os = (int)oscategory;
                __result.Screen = (int)screenCategory;
                __result.Name = model;
                __result.UniqueDeviceIdentifier = PatchFakeDevice.GetUniqueDeviceID(oscategory, screenCategory, model);
            }

            private static string GetMD5(string message)
            {
                byte[] array = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(message));
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < array.Length; i++)
                {
                    stringBuilder.Append(array[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }

            private static string GetUniqueDeviceID(OSCategory os, ScreenCategory screen, string deviceName)
            {
                switch (os)
                {
                    case OSCategory.PC:
                        return Crypto.SHA1.Calc(Encoding.Default.GetBytes(string.Format("HsModeD{0}{1}{2}{3}", new object[]
                        {
                                SystemInfo.deviceUniqueIdentifier,
                                os,
                                screen,
                                deviceName
                        })));
                    case OSCategory.Mac:
                    case OSCategory.iOS:
                        return new Guid(PatchFakeDevice.GetMD5(string.Format("HsModeD{0}{1}{2}{3}", new object[]
                        {
                                SystemInfo.deviceUniqueIdentifier,
                                os,
                                screen,
                                deviceName
                        }))).ToString().ToUpper();
                    case OSCategory.Android:
                        return PatchFakeDevice.GetMD5(string.Format("HsModeD_{0}{1}{2}{3}", new object[]
                        {
                                SystemInfo.deviceUniqueIdentifier,
                                os,
                                screen,
                                deviceName
                        }));
                    default:
                        return "n/a";
                }
            }
        }



        public class PatchKarazhan
        {
            // 尝试修复金币购买卡拉赞bug
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(AdventureWing), "Initialize")]
            public static IEnumerable<CodeInstruction> PatchAdventureWingInitialize(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                list.RemoveRange(201, 9);
                return list;
            }
            // 只保留数字8
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(AdventureWing), "Update")]
            public static IEnumerable PatchAdventureWingUpdate(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                List<CodeInstruction> list = new List<CodeInstruction>(instructions);
                for (int i = 0; i <= 112; i++)
                {
                    list[i] = new CodeInstruction(OpCodes.Nop);
                }
                for (int i = 125; i <= 134; i++)
                {
                    list[i] = new CodeInstruction(OpCodes.Nop);
                }
                return list;
            }
        }

        ////测试补丁
        //[HarmonyPrefix]
        //[HarmonyPatch(typeof(UIStatus), "AddInfo", new Type[] { typeof(string) })]
        //public static bool PatchAddInfo(ref string message)
        //{
        //    string dString = isPluginEnable.Value ? "+" : "-";
        //    message = $"{dString}" + message;
        //    return true;
        //}
    }

    //游戏内时间速度更新
    public static class TimeScaleMgrPatch
    {
        private static readonly MethodInfo updateInfo = typeof(TimeScaleMgr).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void Update(this TimeScaleMgr __instance)
        {
            updateInfo?.Invoke(__instance, null);
        }
    }

    //收藏更新，目前测试无效
    public static class CollectionManagerPatch
    {
        private static readonly MethodInfo onCollectionChanged = typeof(CollectionManager).GetMethod("OnCollectionChanged", BindingFlags.Instance | BindingFlags.NonPublic);
        public static void OnCollectionChanged(this CollectionManager __instance)
        {
            onCollectionChanged?.Invoke(__instance, null);
        }
    }

    //静音管理
    public static class SoundManagerPatch
    {
        public static bool MuteKeyPressed = false;

        private static readonly MethodInfo updateAppMuteInfo = typeof(SoundManager).GetMethod("UpdateAppMute", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void OnMuteKeyPressed()
        {
            MuteKeyPressed = !MuteKeyPressed;
            SoundManager soundManager = SoundManager.Get();
            if (soundManager != null)
            {
                updateAppMuteInfo?.Invoke(soundManager, null);
            }
        }
    }

    //获取玩家信息
    public static class SharedPlayerInfoPatch
    {
        private static readonly FieldInfo m_gameAccountIdInfo = typeof(SharedPlayerInfo).GetField("m_gameAccountId", BindingFlags.Instance | BindingFlags.NonPublic);

        public static BnetGameAccountId GetGameAccountId(this SharedPlayerInfo __instance)
        {
            return m_gameAccountIdInfo?.GetValue(__instance) as BnetGameAccountId;
        }
    }

    //点击获取标签
    public static class PlayerLeaderboardManagerPatch
    {
        private static readonly FieldInfo m_currentlyMousedOverTileInfo = typeof(PlayerLeaderboardManager).GetField("m_currentlyMousedOverTile", BindingFlags.Instance | BindingFlags.NonPublic);
        private static BnetPlayer m_currentOpponent;

        public static void UpdateCurrentOpponent(int opponentPlayerId)
        {
            if (GameState.Get() == null || !GameState.Get().GetPlayerInfoMap().ContainsKey(opponentPlayerId))
            {
                PlayerLeaderboardManagerPatch.m_currentOpponent = null;
                return;
            }
            BnetGameAccountId gameAccountId = GameState.Get().GetPlayerInfoMap()[opponentPlayerId].GetGameAccountId();
            if (gameAccountId == null)
            {
                PlayerLeaderboardManagerPatch.m_currentOpponent = null;
                return;
            }
            PlayerLeaderboardManagerPatch.m_currentOpponent = BnetPresenceMgr.Get().GetPlayer(gameAccountId);
        }

        public static BnetPlayer GetCurrentOpponent(this PlayerLeaderboardManager __instance)
        {
            return PlayerLeaderboardManagerPatch.m_currentOpponent;
        }

        public static BnetPlayer GetSelectedOpponent(this PlayerLeaderboardManager __instance)
        {
            if (!(m_currentlyMousedOverTileInfo?.GetValue(__instance) is PlayerLeaderboardCard playerLeaderboardCard) || GameState.Get() == null)
            {
                return null;
            }
            int tag = playerLeaderboardCard.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
            if (!GameState.Get().GetPlayerInfoMap().ContainsKey(tag))
            {
                return null;
            }
            BnetGameAccountId gameAccountId = GameState.Get().GetPlayerInfoMap()[tag].GetGameAccountId();
            if (gameAccountId == null)
            {
                return null;
            }
            return BnetPresenceMgr.Get().GetPlayer(gameAccountId);
        }
    }

    //表情发送
    public static class EmoteHandlerPatch
    {
        private static readonly FieldInfo m_totalEmotesInfo = typeof(EmoteHandler).GetField("m_totalEmotes", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo m_availableEmotesInfo = typeof(EmoteHandler).GetField("m_availableEmotes", BindingFlags.Instance | BindingFlags.NonPublic);
        private static List<EmoteOption> m_FoundedEmotes;
        public static void HandleKeyboardInput(this EmoteHandler __instance, int EmoteIndex, bool useExtended = false)
        {
            if (EmoteHandler.Get().EmoteSpamBlocked())
            {
                return;
            }
            List<EmoteOption> list = EmoteHandlerPatch.m_availableEmotesInfo.GetValue(__instance) as List<EmoteOption>;
            if (useExtended)
            {
                if (!isExtendedBMEnable.Value || EmoteIndex + 1 > EmoteHandlerPatch.m_FoundedEmotes.Count)
                {
                    return;
                }
                EmoteHandlerPatch.m_totalEmotesInfo.SetValue(__instance, (int)EmoteHandlerPatch.m_totalEmotesInfo.GetValue(__instance) + 1);
                //if (MixModConfig.Get().DisableRandomForEmotes || !GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
                if (!GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
                {
                    EmoteHandlerPatch.m_FoundedEmotes[EmoteIndex].DoClick();
                    return;
                }
                List<EmoteOption> list2 = new List<EmoteOption>();
                foreach (EmoteOption emoteOption in list.Concat(__instance.m_HiddenEmotes))
                {
                    if (emoteOption.CanPlayerUseEmoteType(GameState.Get().GetFriendlySidePlayer()))
                    {
                        list2.Add(emoteOption);
                    }
                }
                foreach (EmoteOption emoteOption2 in EmoteHandlerPatch.m_FoundedEmotes)
                {
                    if (emoteOption2 != null)
                    {
                        list2.Add(emoteOption2);
                    }
                }
                if (list2.Count > 0)
                {
                    list2[UnityEngine.Random.Range(0, list2.Count)].DoClick();
                    return;
                }
            }
            else
            {
                if (EmoteIndex + 1 > list.Count)
                {
                    return;
                }
                EmoteHandlerPatch.m_totalEmotesInfo.SetValue(__instance, (int)EmoteHandlerPatch.m_totalEmotesInfo.GetValue(__instance) + 1);
                //if (MixModConfig.Get().DisableRandomForEmotes || !GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
                if (!GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
                {
                    list[EmoteIndex].DoClick();
                    return;
                }
                List<EmoteOption> list3 = new List<EmoteOption>();
                foreach (EmoteOption emoteOption3 in list.Concat(__instance.m_HiddenEmotes))
                {
                    if (emoteOption3.CanPlayerUseEmoteType(GameState.Get().GetFriendlySidePlayer()))
                    {
                        list3.Add(emoteOption3);
                    }
                }
                if (list3.Count > 0)
                {
                    list3[UnityEngine.Random.Range(0, list3.Count)].DoClick();
                }
            }
        }

        public static void DetermineFoundedEmotes(this EmoteHandler __instance)
        {
            if (EmoteHandlerPatch.m_FoundedEmotes == null || EmoteHandlerPatch.m_FoundedEmotes.Count == 0)
            {
                EmoteHandlerPatch.m_FoundedEmotes = new List<EmoteOption>(11);
                EmoteOption emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.HAPPY_NEW_YEAR);
                if (emoteOption == null)
                {
                    emoteOption = new EmoteOption
                    {
                        m_EmoteType = EmoteType.HAPPY_NEW_YEAR,
                        m_StringTag = "GAMEPLAY_EMOTE_LABEL_GREETINGS"
                    };
                }
                EmoteHandlerPatch.m_FoundedEmotes.Add(emoteOption);
                emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.HAPPY_NEW_YEAR_LUNAR);
                if (emoteOption == null)
                {
                    emoteOption = new EmoteOption
                    {
                        m_EmoteType = EmoteType.HAPPY_NEW_YEAR_LUNAR,
                        m_StringTag = "GAMEPLAY_EMOTE_LABEL_GREETINGS"
                    };
                }
                EmoteHandlerPatch.m_FoundedEmotes.Add(emoteOption);
                emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.HAPPY_HOLIDAYS);
                if (emoteOption == null)
                {
                    emoteOption = new EmoteOption
                    {
                        m_EmoteType = EmoteType.HAPPY_HOLIDAYS,
                        m_StringTag = "GAMEPLAY_EMOTE_LABEL_GREETINGS"
                    };
                }
                EmoteHandlerPatch.m_FoundedEmotes.Add(emoteOption);
                emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.HAPPY_HALLOWEEN);
                if (emoteOption == null)
                {
                    emoteOption = new EmoteOption
                    {
                        m_EmoteType = EmoteType.HAPPY_HALLOWEEN,
                        m_StringTag = "GAMEPLAY_EMOTE_LABEL_GREETINGS"
                    };
                }
                EmoteHandlerPatch.m_FoundedEmotes.Add(emoteOption);
                emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.HAPPY_NOBLEGARDEN);
                if (emoteOption == null)
                {
                    emoteOption = new EmoteOption
                    {
                        m_EmoteType = EmoteType.HAPPY_NOBLEGARDEN,
                        m_StringTag = "GAMEPLAY_EMOTE_LABEL_GREETINGS"
                    };
                }
                EmoteHandlerPatch.m_FoundedEmotes.Add(emoteOption);
                emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_ONE);
                if (emoteOption == null)
                {
                    emoteOption = new EmoteOption
                    {
                        m_EmoteType = EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_ONE,
                        m_StringTag = "GAMEPLAY_EMOTE_LABEL_FIREWORKS"
                    };
                }
                EmoteHandlerPatch.m_FoundedEmotes.Add(emoteOption);
                emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_TWO);
                if (emoteOption == null)
                {
                    emoteOption = new EmoteOption
                    {
                        m_EmoteType = EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_TWO,
                        m_StringTag = "GAMEPLAY_EMOTE_LABEL_FIREWORKS"
                    };
                }
                EmoteHandlerPatch.m_FoundedEmotes.Add(emoteOption);
                emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_THREE);
                if (emoteOption == null)
                {
                    emoteOption = new EmoteOption
                    {
                        m_EmoteType = EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_THREE,
                        m_StringTag = "GAMEPLAY_EMOTE_LABEL_FIREWORKS"
                    };
                }
                EmoteHandlerPatch.m_FoundedEmotes.Add(emoteOption);
                emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_ONE);
                if (emoteOption == null)
                {
                    emoteOption = new EmoteOption
                    {
                        m_EmoteType = EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_ONE,
                        m_StringTag = "GAMEPLAY_EMOTE_LABEL_FIREWORKS"
                    };
                }
                EmoteHandlerPatch.m_FoundedEmotes.Add(emoteOption);
                emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_TWO);
                if (emoteOption == null)
                {
                    emoteOption = new EmoteOption
                    {
                        m_EmoteType = EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_TWO,
                        m_StringTag = "GAMEPLAY_EMOTE_LABEL_FIREWORKS"
                    };
                }
                EmoteHandlerPatch.m_FoundedEmotes.Add(emoteOption);
                emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_THREE);
                if (emoteOption == null)
                {
                    emoteOption = new EmoteOption
                    {
                        m_EmoteType = EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_THREE,
                        m_StringTag = "GAMEPLAY_EMOTE_LABEL_FIREWORKS"
                    };
                }
                EmoteHandlerPatch.m_FoundedEmotes.Add(emoteOption);
                foreach (EmoteOption emoteOption2 in EmoteHandlerPatch.m_FoundedEmotes)
                {
                    emoteOption2.UpdateEmoteType();
                }
            }
        }
    }

    //表情静默
    public static class EnemyEmoteHandlerPatch
    {
        private static readonly MethodInfo doSquelchClickInfo = typeof(EnemyEmoteHandler).GetMethod("DoSquelchClick", BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly FieldInfo m_squelchedInfo = typeof(EnemyEmoteHandler).GetField("m_squelched", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void DoSquelchClick(this EnemyEmoteHandler __instance)
        {
            doSquelchClickInfo?.Invoke(__instance, null);
        }

        public static void SquelchPlayer(this EnemyEmoteHandler __instance, int playerId)
        {
            if (m_squelchedInfo.GetValue(__instance) is Map<int, bool> map)
            {
                map[playerId] = true;
            }
        }
    }

    //开包
    public static class PackOpeningDirectorPatch
    {
        public static bool m_WaitingForCards;

        public static bool m_WaitingForAllCardsRevealed;

        private static readonly FieldInfo m_hiddenCardsInfo = typeof(PackOpeningDirector).GetField("m_hiddenCards", BindingFlags.Instance | BindingFlags.NonPublic);

        public static IEnumerator ForceRevealAllCards(this PackOpeningDirector __instance)
        {
            object value = m_hiddenCardsInfo.GetValue(__instance);
            if (value is Game.PackOpening.HiddenCards m_hiddenCards)
            {
                m_WaitingForAllCardsRevealed = true;
                while (m_WaitingForCards)
                {
                    yield return new WaitForSeconds(0.05f);
                }
                m_hiddenCards.ForceRevealAllCards();
                m_WaitingForAllCardsRevealed = false;
            }
        }
    }
    public static class FriendMgrPatch
    {
        private static BnetPlayer m_currentOpponent;
        public static BnetPlayer GetCurrentOpponent(this FriendMgr __instance)
        {
            return m_currentOpponent;
        }
        private static void UpdateCurrentOpponent()
        {
            if (GameState.Get() == null)
            {
                m_currentOpponent = null;
                return;
            }
            Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
            if (opposingSidePlayer == null)
            {
                FriendMgrPatch.m_currentOpponent = null;
                return;
            }
            FriendMgrPatch.m_currentOpponent = BnetPresenceMgr.Get().GetPlayer(opposingSidePlayer.GetGameAccountId());
            //if (opposingSidePlayer == null)
            //{
            //    m_currentOpponent = null;
            //}
            //else
            //{
            //    m_currentOpponent = BnetPresenceMgr.Get().GetPlayer(opposingSidePlayer.GetGameAccountId());
            //}
        }
        public static void OnGameCreated(GameState.CreateGamePhase phase, object userData)
        {
            GameState.Get().UnregisterCreateGameListener(new GameState.CreateGameCallback(FriendMgrPatch.OnGameCreated), null);

            FriendMgrPatch.UpdateCurrentOpponent();
        }
    }

    //模拟开包
    public static class PackOpeningPatch
    {
        private static readonly MethodInfo onReloginComplete = typeof(PackOpening).GetMethod("OnReloginComplete", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly MethodInfo updatePacks = typeof(PackOpening).GetMethod("UpdatePacks", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void OnReloginComplete(this PackOpening __instance)
        {
            onReloginComplete?.Invoke(__instance, null);
        }

        public static void UpdatePacks(this PackOpening __instance)
        {
            updatePacks?.Invoke(__instance, null);
        }
    }

}

