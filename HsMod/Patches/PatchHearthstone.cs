using Blizzard.GameService.SDK.Client.Integration;
using Blizzard.T5.Core;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using static HsMod.PluginConfig;

namespace HsMod
{
    public partial class Patcher
    {
        public class PatchHearthstone
        {


            // fix #131



            [HarmonyPostfix]
            [HarmonyPatch(typeof(TagMap), "GetMap")]
            public static void PatchTagMapGetMap(ref Dictionary<int, int> __result)
            {
                // todo: check call from;
                // return a copy to prevent modification in foreach
                __result = __result.ToList().ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }

            //金卡钻石卡补丁
            [HarmonyPrefix]
            [HarmonyPatch(typeof(EntityBase), nameof(EntityBase.GetPremiumType))]
            public static bool PatchGetPremiumType(EntityBase __instance, ref TAG_PREMIUM __result)
            {

                return Utils.GetPremiumType(ref __instance, ref __result);
            }

            //屏蔽主播模式：读取时强制返回关闭
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Options), "GetBool", new Type[] { typeof(Option) })]
            public static bool PatchOptionsGetBool(Option __0, ref bool __result)
            {
                if (isBlockStreamerMode.Value && __0 == Option.STREAMER_MODE)
                {
                    __result = false;
                    return false;
                }
                return true;
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(Options), "GetBool", new Type[] { typeof(Option), typeof(bool) })]
            public static bool PatchOptionsGetBoolDefault(Option __0, ref bool __result)
            {
                if (isBlockStreamerMode.Value && __0 == Option.STREAMER_MODE)
                {
                    __result = false;
                    return false;
                }
                return true;
            }

            //屏蔽主播模式：拦截 Ctrl+Shift+S 等途径的开启写入
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Options), "SetBool", new Type[] { typeof(Option), typeof(bool) })]
            public static bool PatchOptionsSetBool(Option __0, bool __1)
            {
                if (isBlockStreamerMode.Value && __0 == Option.STREAMER_MODE && __1)
                {
                    return false;
                }
                return true;
            }
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Actor), nameof(Actor.GetPremium))]
            public static bool PatchActorGetPremiumType(ref TAG_PREMIUM __result, ref Entity ___m_entity, ref Actor __instance)
            {
                return Utils.GetPremiumType(ref ___m_entity, ref __result);
            }

            // 收藏预览卡牌时播放入场音效
            [HarmonyPrefix]
            [HarmonyPatch(typeof(CraftingManager), "EnterCraftMode")]
            private static void PatchEnterCraftMode(CraftingManager __instance, Actor collectionCardActor)
            {
                if (previewCardPlaySounds.Value && collectionCardActor != null && collectionCardActor.HasCardDef &&
                    collectionCardActor.PlayEffectDef != null)
                {
                    GameUtils.PlayCardEffectDefSounds(collectionCardActor.PlayEffectDef);
                }
            }

            [HarmonyPostfix]
            [HarmonyPatch(typeof(ActorNames), "Initialize")]
            private static void PatchActorNames(ref Dictionary<string, int> ___m_signatureFrames)
            {
                if (oldSignatureSave.Value)
                {
                    Dictionary<string, int> result = new Dictionary<string, int>();
                    foreach (KeyValuePair<string, int> pair in ___m_signatureFrames)
                    {
                        if (pair.Value == 1)
                        {
                            // 9 新版随从的异画样式
                            result[pair.Key] = 9;
                        }
                        else
                        {
                            result[pair.Key] = pair.Value;
                        }
                    }
                }
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
                        LoadSkinsConfigFromFile(); // 更新皮肤映射
                        Utils.CacheRawHeroCardId = null;
                        if (!System.IO.File.Exists(CommandConfig.hsMatchLogPath))
                        {
                            var f = System.IO.File.Create(CommandConfig.hsMatchLogPath);
                            f?.Close();
                        }
                        if (System.IO.File.Exists(CommandConfig.hsMatchLogPath))
                        {
                            if (isAutoReportEnable.Value)
                            {
                                Utils.TryReportOpponent();
                                Utils.TryAutoReport();
                            }
                            string finalResult = "未知";
                            if (GameMgr.Get().GetGameType() != PegasusShared.GameType.GT_BATTLEGROUNDS)
                            {
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
                            }
                            else
                            {
                                switch (GameState.Get()?.GetFriendlySidePlayer()?.GetHero()?.GetRealTimePlayerLeaderboardPlace())
                                {
                                    case 1: finalResult = "第一名"; break;
                                    case 2: finalResult = "第二名"; break;
                                    case 3: finalResult = "第三名"; break;
                                    case 4: finalResult = "第四名"; break;
                                    case 5: finalResult = "第五名"; break;
                                    case 6: finalResult = "第六名"; break;
                                    case 7: finalResult = "第七名"; break;
                                    case 8: finalResult = "第八名"; break;
                                    default: break;
                                }
                            }

                            string gameType = (GameMgr.Get().GetGameType() == PegasusShared.GameType.GT_RANKED) ? GameMgr.Get().GetFormatType().ToString() : GameMgr.Get().GetGameType().ToString();

                            string gameRank = "-";
                            if ((GameMgr.Get().GetGameType() == PegasusShared.GameType.GT_RANKED) && (GameMgr.Get().GetFormatType() != PegasusShared.FormatType.FT_UNKNOWN))
                            {
                                var currentMedal = RankMgr.Get()?.GetLocalPlayerMedalInfo()?.GetCurrentMedal(GameMgr.Get().GetFormatType());
                                if (currentMedal != null)
                                {
                                    gameRank = Utils.RankIdxToString(currentMedal.starLevel);
                                    gameRank = (gameRank == "传说") ? "传说" + currentMedal.legendIndex.ToString() : (currentMedal.earnedStars > 0 ? gameRank + "-" + currentMedal.earnedStars.ToString() : "-");
                                }
                            }
                            else if (GameMgr.Get().GetGameType() == PegasusShared.GameType.GT_BATTLEGROUNDS)
                            {
                                gameRank = BaconLobbyMgr.Get()?.GetBattlegroundsActiveGameModeRating().ToString();
                            }
                            finalResult = $"{String.Join("<br />", DateTime.Now.ToString().Split(' '))},{finalResult},{gameRank},{gameType},{Utils.CacheLastOpponentFullName},";
                            if (Utils.CacheLastOpponentAccountID.High == 72057594037927936)
                            {
                                finalResult += $"OPPOSING: {Utils.CacheLastOpponentAccountID.Low}";
                            }
                            else
                            {
                                finalResult += $"OPPOSING: Low:{Utils.CacheLastOpponentAccountID.Low}+High:{Utils.CacheLastOpponentAccountID.High}";
                            }
                            if (isAutoReportEnable.Value)
                            {
                                finalResult += " => 已举报";
                            }
                            finalResult += $"<br />FRIENDLY: {BnetPresenceMgr.Get()?.GetMyPlayer()?.GetBestName()?.ToString()}";
                            System.IO.File.AppendAllText(CommandConfig.hsMatchLogPath, finalResult + "\n");
                            Utils.CacheLastOpponentAccountID = null;
                        }

                        if (autoRefershQuestTimer.Value > 0 && ConfigValue.Get().RunningTime >= autoRefershQuestTimer.Value)
                        {
                            Utils.MyLogger(BepInEx.Logging.LogLevel.Warning, "定时重启！即将退出游戏...");
                            Utils.TryAutoRefreshQuest();
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
                    Utils.Quit();
                }
            }

            // 手牌追踪
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Player), "IsRevealed")]
            public static bool PatchPlayerIsRevealed(ref bool __result)
            {
                if (isCardRevealedEnable.Value)
                {
                    __result = true;
                    return false;
                }
                return true;
            }
            // 选择识别（对手抉择提示）
            [HarmonyPrefix]
            [HarmonyPatch(typeof(GameState), "OnPowerHistory")]
            public static void PatchDebugPrintPower(GameState __instance, ref List<Network.PowerHistory> powerList)
            {
                try
                {
                    if (isCardTrackerEnable.Value && !GameMgr.Get().IsBattlegrounds() && !GameMgr.Get().IsMercenaries())
                    {
                        if (powerList == null) return;
                        List<string> hintList = new List<string>();
                        int i = 0;
                        foreach (Network.PowerHistory pl in powerList)
                        {
                            i++;
                            if (pl == null) continue;
                            Network.PowerHistory powerHistory = pl;
                            if (powerHistory.Type == Network.PowerType.SHOW_ENTITY)
                            {
                                Network.Entity netEntity = ((Network.HistShowEntity)powerHistory).Entity;
                                Entity entity = __instance?.GetEntity(netEntity.ID);
                                if (entity != null && entity.GetControllerSide() == global::Player.Side.OPPOSING && entity.GetZone() == TAG_ZONE.SETASIDE && entity.GetCardType() != TAG_CARDTYPE.ENCHANTMENT)
                                {
                                    EntityDef entityDef = DefLoader.Get()?.GetEntityDef(netEntity.CardID);
                                    if (entityDef != null && entityDef.GetCardType() != TAG_CARDTYPE.ENCHANTMENT && !entityDef.IsQuestline())
                                    {
                                        string hintText = entityDef.GetName();
                                        if (hintText != null)
                                        {
                                            hintText = hintText + "\n" + entityDef.GetCardTextInHand();
                                            UIStatus.Get().AddInfo($"{LocalizationManager.GetLangValue("info.notice")}{hintText}", 15f);
                                        }
                                    }
                                }
                            }
                            if (powerHistory.Type == Network.PowerType.FULL_ENTITY)
                            {
                                Network.Entity entity3 = ((Network.HistFullEntity)powerHistory).Entity;
                                int j = 0;
                                foreach (Network.Entity.Tag tg in entity3.Tags)
                                {
                                    j++;
                                    if (tg.Name == 49 && tg.Value == 4)
                                    {
                                        EntityDef entityDef2 = DefLoader.Get().GetEntityDef(entity3.CardID);
                                        hintList.Add(entityDef2?.GetName());
                                    }
                                }
                            }
                        }
                        string hintText2 = string.Join(" ", hintList);
                        if (hintText2 != "")
                        {
                            UIStatus.Get().AddInfo($"{LocalizationManager.GetLangValue("info.notice")}{hintText2}", 15f);
                        }
                    }


                }
                catch (Exception ex)
                {
                    Utils.MyLogger(BepInEx.Logging.LogLevel.Error, ex);
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
                    bool isRogue = setasideNum == 1 && playNum == 4;

                    string heroClass = isRogue ? GameStrings.GetClassName(TAG_CLASS.ROGUE)
                                               : GameStrings.GetClassName(GameState.Get().GetOpposingPlayer().GetHero().GetClass());
                    title = $"对手职业是{heroClass}，套牌{oppoCardCount}张";
                }
            }

            // 屏蔽暗月宝藏
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Hub), "PreloadLuckyDraw")]
            public static bool PatchNotifySceneLoadedWhenReady(Hub __instance)
            {
                if (shieldMainBoxLuckyDraw.Value)
                {
                    return false;
                }
                return true;
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(Actor), "SetPortraitTexture")]
            private static void PatchSetPortraitTexture(Actor __instance, ref Texture texture)
            {
                try
                {
                    if (SaveCardTextures.Value)
                    {
                        __instance.StartCoroutine(Utils.SaveLoadCardLocalTextures(__instance, texture));
                    }
                }
                catch (Exception e)
                {
                    Utils.MyLogger(BepInEx.Logging.LogLevel.Error, e);
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
    }
}
