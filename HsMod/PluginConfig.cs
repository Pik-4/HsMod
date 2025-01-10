using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace HsMod
{
    public static class PluginConfig
    {
        public static ConfigEntry<bool> isPluginEnable;
        public static ConfigEntry<string> pluginInitLanague;
        public static ConfigEntry<Locale> pluginLanague;
        public static ConfigEntry<bool> isFakeOpenEnable;
        public static ConfigEntry<Utils.ConfigTemplate> configTemplate;
        public static ConfigEntry<bool> isTimeGearEnable;
        public static ConfigEntry<bool> isShortcutsEnable;
        public static ConfigEntry<int> targetFrameRate;
        public static ConfigEntry<bool> isDynamicFpsEnable;
        public static ConfigEntry<bool> isEulaRead;

        public static ConfigEntry<int> timeGear;
        public static ConfigEntry<int> receiveEnemyEmoteLimit;

        public static ConfigEntry<bool> isIGMMessageShow;
        public static ConfigEntry<bool> isOnApplicationFocus;
        public static ConfigEntry<bool> isAutoExit;
        //public static ConfigEntry<bool> isAutoRestart;
        public static ConfigEntry<bool> isAlertPopupShow;
        public static ConfigEntry<Utils.AlertPopupResponse> responseAlertPopup;
        public static ConfigEntry<bool> isRewardToastShow;
        public static ConfigEntry<bool> isAutoOpenBoxesRewardEnable;
        public static ConfigEntry<bool> isFullnameShow;
        public static ConfigEntry<bool> isOpponentRankInGameShow;
        public static ConfigEntry<bool> isSkipHeroIntro;
        public static ConfigEntry<bool> isThinkEmotesEnable;
        public static ConfigEntry<bool> isExtendedBMEnable;
        public static ConfigEntry<bool> isQuickPackOpeningEnable;
        public static ConfigEntry<bool> isAutoPackOpeningEnable;
        public static ConfigEntry<bool> isShowCardLargeCount;
        public static ConfigEntry<bool> isAutoRefundCardDisenchantEnable;
        public static ConfigEntry<bool> isShowCollectionCardIdEnable;
        public static ConfigEntry<bool> isShowRetireForever;
        public static ConfigEntry<bool> isIdleKickEnable;
        public static ConfigEntry<bool> isBypassDeckShareCodeCheckEnable;

        //public static ConfigEntry<Utils.QuickMode> quickModeState;
        public static ConfigEntry<bool> isQuickModeEnable;
        public static ConfigEntry<bool> isCardTrackerEnable;
        public static ConfigEntry<bool> isCardRevealedEnable;
        public static ConfigEntry<bool> isMoveEnemyCardsEnable;
        public static ConfigEntry<bool> isAutoReportEnable;

        public static ConfigEntry<bool> isAutoRecvMercenaryRewardEnable;
        public static ConfigEntry<bool> isMercenaryBattleZoom;
        public static ConfigEntry<Utils.CardState> mercenaryDiamondCardState;
        public static ConfigEntry<Utils.CardState> randomMercenarySkinEnable;

        public static ConfigEntry<bool> isShutUpBobEnable;
        public static ConfigEntry<bool> isBgsGoldenEnable;

        public static ConfigEntry<bool> isOpponentGoldenCardShow;
        public static ConfigEntry<bool> isSignatureCardStateEnable;
        public static ConfigEntry<Utils.CardState> goldenCardState;
        public static ConfigEntry<Utils.CardState> maxCardState;

        public static ConfigEntry<KeyboardShortcut> keyTimeGearUp;
        public static ConfigEntry<KeyboardShortcut> keyTimeGearDown;
        public static ConfigEntry<KeyboardShortcut> keyTimeGearDefault;
        public static ConfigEntry<KeyboardShortcut> keyTimeGearMax;
        public static ConfigEntry<KeyboardShortcut> keySimulateDisconnect;
        public static ConfigEntry<KeyboardShortcut> keyCopyBattleTag;
        public static ConfigEntry<KeyboardShortcut> keyCopySelectBattleTag;
        public static ConfigEntry<KeyboardShortcut> keyConcede;
        public static ConfigEntry<KeyboardShortcut> keyContinueMulligan;
        public static ConfigEntry<KeyboardShortcut> keySquelch;
        public static ConfigEntry<KeyboardShortcut> keySoundMute;
        public static ConfigEntry<KeyboardShortcut> keyShutUpBob;
        public static ConfigEntry<KeyboardShortcut> keyRefund;
        public static ConfigEntry<KeyboardShortcut> keyReadNewCards;
        //public static ConfigEntry<KeyboardShortcut> keyRuin;    //毁灭吧赶紧的
        public static ConfigEntry<KeyboardShortcut> keyShowFPS;

        public static ConfigEntry<KeyboardShortcut> keyEmoteGreetings;
        public static ConfigEntry<KeyboardShortcut> keyEmoteWellPlayed;
        public static ConfigEntry<KeyboardShortcut> keyEmoteThanks;
        public static ConfigEntry<KeyboardShortcut> keyEmoteWow;
        public static ConfigEntry<KeyboardShortcut> keyEmoteOops;
        public static ConfigEntry<KeyboardShortcut> keyEmoteThreaten;

        public static ConfigEntry<int> skinCoin;
        public static ConfigEntry<int> skinCardBack;
        public static ConfigEntry<int> skinBoard;
        public static ConfigEntry<int> skinBgsBoard;
        public static ConfigEntry<int> skinBgsFinisher;
        public static ConfigEntry<int> skinBob;
        public static ConfigEntry<int> skinHero;
        public static ConfigEntry<int> skinOpposingHero;
        public static ConfigEntry<bool> isSkinDefalutHeroEnable;

        public static ConfigEntry<bool> isShowFPSEnable;
        public static ConfigEntry<bool> isInternalModeEnable;
        public static ConfigEntry<int> webServerPort;
        public static ConfigEntry<string> webPageBackImg;
        public static ConfigEntry<bool> isWebshellEnable;

        public static ConfigEntry<string> hsMatchLogPath;
        public static ConfigEntry<string> hsLogPath;
        public static ConfigEntry<long> autoQuitTimer;    // 定时退出

        public static ConfigEntry<Utils.DevicePreset> fakeDevicePreset;
        public static ConfigEntry<OSCategory> fakeDeviceOs;
        public static ConfigEntry<ScreenCategory> fakeDeviceScreen;
        public static ConfigEntry<string> fakeDeviceName;

        public static ConfigEntry<int> fakePackCount;
        public static ConfigEntry<BoosterDbId> fakeBoosterDbId;
        public static ConfigEntry<bool> isFakeRandomResult;
        public static ConfigEntry<bool> isFakeRandomRarity;
        public static ConfigEntry<bool> isFakeRandomPremium;
        public static ConfigEntry<bool> isFakeAtypicalRandomPremium;
        public static ConfigEntry<TAG_PREMIUM> fakeRandomPremium;
        public static ConfigEntry<Utils.CardRarity> fakeRandomRarity;
        public static ConfigEntry<int> fakeCatchupCount;
        public static ConfigEntry<int> fakeCardID1;
        public static ConfigEntry<TAG_PREMIUM> fakeCardPremium1;
        public static ConfigEntry<int> fakeCardID2;
        public static ConfigEntry<TAG_PREMIUM> fakeCardPremium2;
        public static ConfigEntry<int> fakeCardID3;
        public static ConfigEntry<TAG_PREMIUM> fakeCardPremium3;
        public static ConfigEntry<int> fakeCardID4;
        public static ConfigEntry<TAG_PREMIUM> fakeCardPremium4;
        public static ConfigEntry<int> fakeCardID5;
        public static ConfigEntry<TAG_PREMIUM> fakeCardPremium5;


        public static ConfigEntry<Utils.BuyAdventureTemplate> buyAdventure;
        public static ConfigEntry<bool> isKarazhanFixEnable;
        public static ShowFPS showFPS;
        public static Dictionary<int, int> HeroesMapping = new Dictionary<int, int>();
        public static Dictionary<string, string> HeroesPowerMapping = new Dictionary<string, string>();

        public static ConfigEntry<bool> isAutoRedundantNDE;

        public static string HsModWebSite;

        public static class CommandConfig
        {
            public static int webServerPort = -1;
            public static string hsMatchLogPath = "";
            public static int width = -1;
            public static int height = -1;
            public static string GlobalHSUnitID = "";
        }

        public static long timeKeeper = DateTime.Now.Ticks;

        public static List<Utils.CardMapping> CardsMapping = new List<Utils.CardMapping>();    //卡片替换映射，目前暂未使用
        public static IGraphicsManager graphicsManager;
        public static void ConfigBind(ConfigFile config)
        {
            config.Clear();
            pluginInitLanague = config.Bind("HsMod", "HsMod.Init.Language", "UNKNOWN", new ConfigDescription("(!!! DON'T EDIT IT, unless you know what you are doing) HsMod Init Language", null, new object[] { "Advanced" }));
            isEulaRead = config.Bind("HsMod", "HsMod.Init.Eula", false, new ConfigDescription("End-User License Agreement", null, new object[] { "Advanced" }));

            if (pluginInitLanague.Value == "UNKNOWN")
            {
                pluginInitLanague.Value = LocalizationManager.GetCurrentLang();
            }
            /*
             * ^(.*?)( =.*?\()(".*?")(.*?)(".*?")(,.*?,.*?)(".*?")(.*?)$
             * \1\2LocalizationManager\.GetLangValue\("\1.lable"\)\4LocalizationManager\.GetLangValue\("\1.name"\)\6LocalizationManager\.GetLangValue\("\1.description"\)\8
             */

            CreateHsModWorkDir();


            isPluginEnable = config.Bind(LocalizationManager.GetLangValue("isPluginEnable.label"), LocalizationManager.GetLangValue("isPluginEnable.name"), true, LocalizationManager.GetLangValue("isPluginEnable.description"));
            pluginLanague = config.Bind(LocalizationManager.GetLangValue("pluginLanague.label"), LocalizationManager.GetLangValue("pluginLanague.name"), LocalizationManager.StrToLocale(pluginInitLanague.Value), LocalizationManager.GetLangValue("pluginLanague.description"));

            configTemplate = config.Bind(LocalizationManager.GetLangValue("configTemplate.label"), LocalizationManager.GetLangValue("configTemplate.name"), Utils.ConfigTemplate.DoNothing, LocalizationManager.GetLangValue("configTemplate.description"));
            isShortcutsEnable = config.Bind(LocalizationManager.GetLangValue("isShortcutsEnable.label"), LocalizationManager.GetLangValue("isShortcutsEnable.name"), false, LocalizationManager.GetLangValue("isShortcutsEnable.description"));
            isTimeGearEnable = config.Bind(LocalizationManager.GetLangValue("isTimeGearEnable.label"), LocalizationManager.GetLangValue("isTimeGearEnable.name"), false, LocalizationManager.GetLangValue("isTimeGearEnable.description"));
            timeGear = config.Bind(LocalizationManager.GetLangValue("timeGear.label"), LocalizationManager.GetLangValue("timeGear.name"), 0, new ConfigDescription(LocalizationManager.GetLangValue("timeGear.description"), new AcceptableValueRange<int>(-32, 32)));
            isShowFPSEnable = config.Bind(LocalizationManager.GetLangValue("isShowFPSEnable.label"), LocalizationManager.GetLangValue("isShowFPSEnable.name"), false, LocalizationManager.GetLangValue("isShowFPSEnable.description"));
            targetFrameRate = config.Bind(LocalizationManager.GetLangValue("targetFrameRate.label"), LocalizationManager.GetLangValue("targetFrameRate.name"), -1, new ConfigDescription(LocalizationManager.GetLangValue("targetFrameRate.description"), new AcceptableValueRange<int>(-1, 2333)));

            isIGMMessageShow = config.Bind(LocalizationManager.GetLangValue("isIGMMessageShow.label"), LocalizationManager.GetLangValue("isIGMMessageShow.name"), true, LocalizationManager.GetLangValue("isIGMMessageShow.description"));
            isAlertPopupShow = config.Bind(LocalizationManager.GetLangValue("isAlertPopupShow.label"), LocalizationManager.GetLangValue("isAlertPopupShow.name"), true, LocalizationManager.GetLangValue("isAlertPopupShow.description"));
            responseAlertPopup = config.Bind(LocalizationManager.GetLangValue("responseAlertPopup.label"), LocalizationManager.GetLangValue("responseAlertPopup.name"), Utils.AlertPopupResponse.DONOTHING, LocalizationManager.GetLangValue("responseAlertPopup.description"));
            isOnApplicationFocus = config.Bind(LocalizationManager.GetLangValue("isOnApplicationFocus.label"), LocalizationManager.GetLangValue("isOnApplicationFocus.name"), true, LocalizationManager.GetLangValue("isOnApplicationFocus.description"));
            isRewardToastShow = config.Bind(LocalizationManager.GetLangValue("isRewardToastShow.label"), LocalizationManager.GetLangValue("isRewardToastShow.name"), true, LocalizationManager.GetLangValue("isRewardToastShow.description"));
            isAutoOpenBoxesRewardEnable = config.Bind(LocalizationManager.GetLangValue("isAutoOpenBoxesRewardEnable.label"), LocalizationManager.GetLangValue("isAutoOpenBoxesRewardEnable.name"), false, LocalizationManager.GetLangValue("isAutoOpenBoxesRewardEnable.description"));
            isAutoExit = config.Bind(LocalizationManager.GetLangValue("isAutoExit.label"), LocalizationManager.GetLangValue("isAutoExit.name"), false, LocalizationManager.GetLangValue("isAutoExit.description"));
            //isAutoRestart = config.Bind(LocalizationManager.GetLangValue("//isAutoRestart.label"), LocalizationManager.GetLangValue("//isAutoRestart.name"), false, LocalizationManager.GetLangValue("//isAutoRestart.description"));
            isShowCardLargeCount = config.Bind(LocalizationManager.GetLangValue("isShowCardLargeCount.label"), LocalizationManager.GetLangValue("isShowCardLargeCount.name"), false, LocalizationManager.GetLangValue("isShowCardLargeCount.description"));
            isShowCollectionCardIdEnable = config.Bind(LocalizationManager.GetLangValue("isShowCollectionCardIdEnable.label"), LocalizationManager.GetLangValue("isShowCollectionCardIdEnable.name"), false, LocalizationManager.GetLangValue("isShowCollectionCardIdEnable.description"));
            isBypassDeckShareCodeCheckEnable = config.Bind(LocalizationManager.GetLangValue("isBypassDeckShareCodeCheckEnable.label"), LocalizationManager.GetLangValue("isBypassDeckShareCodeCheckEnable.name"), false, LocalizationManager.GetLangValue("isBypassDeckShareCodeCheckEnable.description"));
            isShowRetireForever = config.Bind(LocalizationManager.GetLangValue("isShowRetireForever.label"), LocalizationManager.GetLangValue("isShowRetireForever.name"), false, LocalizationManager.GetLangValue("isShowRetireForever.description"));
            isIdleKickEnable = config.Bind(LocalizationManager.GetLangValue("isIdleKickEnable.label"), LocalizationManager.GetLangValue("isIdleKickEnable.name"), true, LocalizationManager.GetLangValue("isIdleKickEnable.description"));


            isQuickPackOpeningEnable = config.Bind(LocalizationManager.GetLangValue("isQuickPackOpeningEnable.label"), LocalizationManager.GetLangValue("isQuickPackOpeningEnable.name"), false, LocalizationManager.GetLangValue("isQuickPackOpeningEnable.description"));
            isAutoPackOpeningEnable = config.Bind(LocalizationManager.GetLangValue("isAutoPackOpeningEnable.label"), LocalizationManager.GetLangValue("isAutoPackOpeningEnable.name"), false, LocalizationManager.GetLangValue("isAutoPackOpeningEnable.description"));
            isAutoRefundCardDisenchantEnable = config.Bind(LocalizationManager.GetLangValue("isAutoRefundCardDisenchantEnable.label"), LocalizationManager.GetLangValue("isAutoRefundCardDisenchantEnable.name"), false, LocalizationManager.GetLangValue("isAutoRefundCardDisenchantEnable.description"));

            isAutoReportEnable = config.Bind(LocalizationManager.GetLangValue("isAutoReportEnable.label"), LocalizationManager.GetLangValue("isAutoReportEnable.name"), false, LocalizationManager.GetLangValue("isAutoReportEnable.description"));
            // isAutoReportEnable = config.Bind(LocalizationManager.GetLangValue("// isAutoReportEnable.label"), LocalizationManager.GetLangValue("// isAutoReportEnable.name"), true, new ConfigDescription(LocalizationManager.GetLangValue("// isAutoReportEnable.description"), null, new object[] { "Advanced" }));
            isMoveEnemyCardsEnable = config.Bind(LocalizationManager.GetLangValue("isMoveEnemyCardsEnable.label"), LocalizationManager.GetLangValue("isMoveEnemyCardsEnable.name"), false, LocalizationManager.GetLangValue("isMoveEnemyCardsEnable.description"));


            isQuickModeEnable = config.Bind(LocalizationManager.GetLangValue("isQuickModeEnable.label"), LocalizationManager.GetLangValue("isQuickModeEnable.name"), false, LocalizationManager.GetLangValue("isQuickModeEnable.description"));
            isFullnameShow = config.Bind(LocalizationManager.GetLangValue("isFullnameShow.label"), LocalizationManager.GetLangValue("isFullnameShow.name"), false, LocalizationManager.GetLangValue("isFullnameShow.description"));
            isOpponentRankInGameShow = config.Bind(LocalizationManager.GetLangValue("isOpponentRankInGameShow.label"), LocalizationManager.GetLangValue("isOpponentRankInGameShow.name"), false, LocalizationManager.GetLangValue("isOpponentRankInGameShow.description"));
            isCardTrackerEnable = config.Bind(LocalizationManager.GetLangValue("isCardTrackerEnable.label"), LocalizationManager.GetLangValue("isCardTrackerEnable.name"), false, LocalizationManager.GetLangValue("isCardTrackerEnable.description"));
            isCardRevealedEnable = config.Bind(LocalizationManager.GetLangValue("isCardRevealedEnable.label"), LocalizationManager.GetLangValue("isCardRevealedEnable.name"), false, LocalizationManager.GetLangValue("isCardRevealedEnable.description"));
            isSkipHeroIntro = config.Bind(LocalizationManager.GetLangValue("isSkipHeroIntro.label"), LocalizationManager.GetLangValue("isSkipHeroIntro.name"), false, LocalizationManager.GetLangValue("isSkipHeroIntro.description"));
            isExtendedBMEnable = config.Bind(LocalizationManager.GetLangValue("isExtendedBMEnable.label"), LocalizationManager.GetLangValue("isExtendedBMEnable.name"), false, LocalizationManager.GetLangValue("isExtendedBMEnable.description"));
            isThinkEmotesEnable = config.Bind(LocalizationManager.GetLangValue("isThinkEmotesEnable.label"), LocalizationManager.GetLangValue("isThinkEmotesEnable.name"), true, LocalizationManager.GetLangValue("isThinkEmotesEnable.description"));
            receiveEnemyEmoteLimit = config.Bind(LocalizationManager.GetLangValue("receiveEnemyEmoteLimit.label"), LocalizationManager.GetLangValue("receiveEnemyEmoteLimit.name"), -1, new ConfigDescription(LocalizationManager.GetLangValue("receiveEnemyEmoteLimit.description"), new AcceptableValueRange<int>(-1, 100)));
            isOpponentGoldenCardShow = config.Bind(LocalizationManager.GetLangValue("isOpponentGoldenCardShow.label"), LocalizationManager.GetLangValue("isOpponentGoldenCardShow.name"), true, LocalizationManager.GetLangValue("isOpponentGoldenCardShow.description"));
            isSignatureCardStateEnable = config.Bind(LocalizationManager.GetLangValue("isSignatureCardStateEnable.label"), LocalizationManager.GetLangValue("isSignatureCardStateEnable.name"), true, LocalizationManager.GetLangValue("isSignatureCardStateEnable.description"));
            goldenCardState = config.Bind(LocalizationManager.GetLangValue("goldenCardState.label"), LocalizationManager.GetLangValue("goldenCardState.name"), Utils.CardState.Default, LocalizationManager.GetLangValue("goldenCardState.description"));
            maxCardState = config.Bind(LocalizationManager.GetLangValue("maxCardState.label"), LocalizationManager.GetLangValue("maxCardState.name"), Utils.CardState.Default, LocalizationManager.GetLangValue("maxCardState.description"));

            isAutoRecvMercenaryRewardEnable = config.Bind(LocalizationManager.GetLangValue("isAutoRecvMercenaryRewardEnable.label"), LocalizationManager.GetLangValue("isAutoRecvMercenaryRewardEnable.name"), false, LocalizationManager.GetLangValue("isAutoRecvMercenaryRewardEnable.description"));
            isMercenaryBattleZoom = config.Bind(LocalizationManager.GetLangValue("isMercenaryBattleZoom.label"), LocalizationManager.GetLangValue("isMercenaryBattleZoom.name"), true, LocalizationManager.GetLangValue("isMercenaryBattleZoom.description"));
            mercenaryDiamondCardState = config.Bind(LocalizationManager.GetLangValue("mercenaryDiamondCardState.label"), LocalizationManager.GetLangValue("mercenaryDiamondCardState.name"), Utils.CardState.Default, LocalizationManager.GetLangValue("mercenaryDiamondCardState.description"));
            randomMercenarySkinEnable = config.Bind(LocalizationManager.GetLangValue("randomMercenarySkinEnable.label"), LocalizationManager.GetLangValue("randomMercenarySkinEnable.name"), Utils.CardState.Default, LocalizationManager.GetLangValue("randomMercenarySkinEnable.description"));

            isShutUpBobEnable = config.Bind(LocalizationManager.GetLangValue("isShutUpBobEnable.label"), LocalizationManager.GetLangValue("isShutUpBobEnable.name"), false, LocalizationManager.GetLangValue("isShutUpBobEnable.description"));
            isBgsGoldenEnable = config.Bind(LocalizationManager.GetLangValue("isBgsGoldenEnable.label"), LocalizationManager.GetLangValue("isBgsGoldenEnable.name"), false, LocalizationManager.GetLangValue("isBgsGoldenEnable.description"));
            //考虑导出单独配置
            skinCoin = config.Bind(LocalizationManager.GetLangValue("skinCoin.label"), LocalizationManager.GetLangValue("skinCoin.name"), -1, LocalizationManager.GetLangValue("skinCoin.description"));
            skinCardBack = config.Bind(LocalizationManager.GetLangValue("skinCardBack.label"), LocalizationManager.GetLangValue("skinCardBack.name"), -1, LocalizationManager.GetLangValue("skinCardBack.description"));
            skinBoard = config.Bind(LocalizationManager.GetLangValue("skinBoard.label"), LocalizationManager.GetLangValue("skinBoard.name"), -1, LocalizationManager.GetLangValue("skinBoard.description"));
            skinBgsBoard = config.Bind(LocalizationManager.GetLangValue("skinBgsBoard.label"), LocalizationManager.GetLangValue("skinBgsBoard.name"), -1, LocalizationManager.GetLangValue("skinBgsBoard.description"));
            skinBgsFinisher = config.Bind(LocalizationManager.GetLangValue("skinBgsFinisher.label"), LocalizationManager.GetLangValue("skinBgsFinisher.name"), -1, LocalizationManager.GetLangValue("skinBgsFinisher.description"));
            skinBob = config.Bind(LocalizationManager.GetLangValue("skinBob.label"), LocalizationManager.GetLangValue("skinBob.name"), -1, LocalizationManager.GetLangValue("skinBob.description"));
            isSkinDefalutHeroEnable = config.Bind(LocalizationManager.GetLangValue("isSkinDefalutHeroEnable.label"), LocalizationManager.GetLangValue("isSkinDefalutHeroEnable.name"), false, LocalizationManager.GetLangValue("isSkinDefalutHeroEnable.description"));
            skinHero = config.Bind(LocalizationManager.GetLangValue("skinHero.label"), LocalizationManager.GetLangValue("skinHero.name"), -1, LocalizationManager.GetLangValue("skinHero.description"));
            skinOpposingHero = config.Bind(LocalizationManager.GetLangValue("skinOpposingHero.label"), LocalizationManager.GetLangValue("skinOpposingHero.name"), -1, LocalizationManager.GetLangValue("skinOpposingHero.description"));

            keyTimeGearUp = config.Bind(LocalizationManager.GetLangValue("keyTimeGearUp.label"), LocalizationManager.GetLangValue("keyTimeGearUp.name"), new KeyboardShortcut(KeyCode.UpArrow), LocalizationManager.GetLangValue("keyTimeGearUp.description"));
            keyTimeGearDown = config.Bind(LocalizationManager.GetLangValue("keyTimeGearDown.label"), LocalizationManager.GetLangValue("keyTimeGearDown.name"), new KeyboardShortcut(KeyCode.DownArrow), LocalizationManager.GetLangValue("keyTimeGearDown.description"));
            keyTimeGearDefault = config.Bind(LocalizationManager.GetLangValue("keyTimeGearDefault.label"), LocalizationManager.GetLangValue("keyTimeGearDefault.name"), new KeyboardShortcut(KeyCode.LeftArrow), LocalizationManager.GetLangValue("keyTimeGearDefault.description"));
            keyTimeGearMax = config.Bind(LocalizationManager.GetLangValue("keyTimeGearMax.label"), LocalizationManager.GetLangValue("keyTimeGearMax.name"), new KeyboardShortcut(KeyCode.RightArrow), LocalizationManager.GetLangValue("keyTimeGearMax.description"));
            keySimulateDisconnect = config.Bind(LocalizationManager.GetLangValue("keySimulateDisconnect.label"), LocalizationManager.GetLangValue("keySimulateDisconnect.name"), new KeyboardShortcut(KeyCode.D, KeyCode.LeftControl), LocalizationManager.GetLangValue("keySimulateDisconnect.description"));
            keyCopyBattleTag = config.Bind(LocalizationManager.GetLangValue("keyCopyBattleTag.label"), LocalizationManager.GetLangValue("keyCopyBattleTag.name"), new KeyboardShortcut(KeyCode.C, KeyCode.LeftControl), LocalizationManager.GetLangValue("keyCopyBattleTag.description"));
            keyCopySelectBattleTag = config.Bind(LocalizationManager.GetLangValue("keyCopySelectBattleTag.label"), LocalizationManager.GetLangValue("keyCopySelectBattleTag.name"), new KeyboardShortcut(KeyCode.Mouse0), LocalizationManager.GetLangValue("keyCopySelectBattleTag.description"));
            keyConcede = config.Bind(LocalizationManager.GetLangValue("keyConcede.label"), LocalizationManager.GetLangValue("keyConcede.name"), new KeyboardShortcut(KeyCode.Space, KeyCode.LeftControl), LocalizationManager.GetLangValue("keyConcede.description"));
            keyContinueMulligan = config.Bind(LocalizationManager.GetLangValue("keyContinueMulligan.label"), LocalizationManager.GetLangValue("keyContinueMulligan.name"), new KeyboardShortcut(KeyCode.Space), LocalizationManager.GetLangValue("keyContinueMulligan.description"));
            keySquelch = config.Bind(LocalizationManager.GetLangValue("keySquelch.label"), LocalizationManager.GetLangValue("keySquelch.name"), new KeyboardShortcut(KeyCode.Q, KeyCode.LeftControl), LocalizationManager.GetLangValue("keySquelch.description"));
            keySoundMute = config.Bind(LocalizationManager.GetLangValue("keySoundMute.label"), LocalizationManager.GetLangValue("keySoundMute.name"), new KeyboardShortcut(KeyCode.S, KeyCode.LeftControl), LocalizationManager.GetLangValue("keySoundMute.description"));
            keyShutUpBob = config.Bind(LocalizationManager.GetLangValue("keyShutUpBob.label"), LocalizationManager.GetLangValue("keyShutUpBob.name"), new KeyboardShortcut(KeyCode.B, KeyCode.LeftControl), LocalizationManager.GetLangValue("keyShutUpBob.description"));
            keyRefund = config.Bind(LocalizationManager.GetLangValue("keyRefund.label"), LocalizationManager.GetLangValue("keyRefund.name"), new KeyboardShortcut(KeyCode.Z, KeyCode.LeftControl), LocalizationManager.GetLangValue("keyRefund.description"));
            //keyRuin = config.Bind(LocalizationManager.GetLangValue("//keyRuin.label"), LocalizationManager.GetLangValue("//keyRuin.name"), new KeyboardShortcut(KeyCode.R, KeyCode.LeftControl), LocalizationManager.GetLangValue("//keyRuin.description"));
            keyReadNewCards = config.Bind(LocalizationManager.GetLangValue("keyReadNewCards.label"), LocalizationManager.GetLangValue("keyReadNewCards.name"), new KeyboardShortcut(KeyCode.R, KeyCode.LeftControl), LocalizationManager.GetLangValue("keyReadNewCards.description"));
            keyShowFPS = config.Bind(LocalizationManager.GetLangValue("keyShowFPS.label"), LocalizationManager.GetLangValue("keyShowFPS.name"), new KeyboardShortcut(KeyCode.P, KeyCode.LeftControl), LocalizationManager.GetLangValue("keyShowFPS.description"));

            keyEmoteGreetings = config.Bind(LocalizationManager.GetLangValue("keyEmoteGreetings.label"), LocalizationManager.GetLangValue("keyEmoteGreetings.name"), new KeyboardShortcut(KeyCode.Alpha1), LocalizationManager.GetLangValue("keyEmoteGreetings.description"));
            keyEmoteWellPlayed = config.Bind(LocalizationManager.GetLangValue("keyEmoteWellPlayed.label"), LocalizationManager.GetLangValue("keyEmoteWellPlayed.name"), new KeyboardShortcut(KeyCode.Alpha2), LocalizationManager.GetLangValue("keyEmoteWellPlayed.description"));
            keyEmoteThanks = config.Bind(LocalizationManager.GetLangValue("keyEmoteThanks.label"), LocalizationManager.GetLangValue("keyEmoteThanks.name"), new KeyboardShortcut(KeyCode.Alpha3), LocalizationManager.GetLangValue("keyEmoteThanks.description"));
            keyEmoteWow = config.Bind(LocalizationManager.GetLangValue("keyEmoteWow.label"), LocalizationManager.GetLangValue("keyEmoteWow.name"), new KeyboardShortcut(KeyCode.Alpha4), LocalizationManager.GetLangValue("keyEmoteWow.description"));
            keyEmoteOops = config.Bind(LocalizationManager.GetLangValue("keyEmoteOops.label"), LocalizationManager.GetLangValue("keyEmoteOops.name"), new KeyboardShortcut(KeyCode.Alpha5), LocalizationManager.GetLangValue("keyEmoteOops.description"));
            keyEmoteThreaten = config.Bind(LocalizationManager.GetLangValue("keyEmoteThreaten.label"), LocalizationManager.GetLangValue("keyEmoteThreaten.name"), new KeyboardShortcut(KeyCode.Alpha6), LocalizationManager.GetLangValue("keyEmoteThreaten.description"));

            hsLogPath = config.Bind(LocalizationManager.GetLangValue("hsLogPath.label"), LocalizationManager.GetLangValue("hsLogPath.name"), "", new ConfigDescription(LocalizationManager.GetLangValue("hsLogPath.description"), null, new object[] { "Advanced" }));
            hsMatchLogPath = config.Bind(LocalizationManager.GetLangValue("hsMatchLogPath.label"), LocalizationManager.GetLangValue("hsMatchLogPath.name"), Path.Combine(BepInEx.Paths.BepInExRootPath, "HsMod", "match.log"), LocalizationManager.GetLangValue("hsMatchLogPath.description"));
            autoQuitTimer = config.Bind(LocalizationManager.GetLangValue("autoQuitTimer.label"), LocalizationManager.GetLangValue("autoQuitTimer.name"), (long)0, LocalizationManager.GetLangValue("autoQuitTimer.description"));
            isFakeOpenEnable = config.Bind(LocalizationManager.GetLangValue("isFakeOpenEnable.label"), LocalizationManager.GetLangValue("isFakeOpenEnable.name"), false, LocalizationManager.GetLangValue("isFakeOpenEnable.description"));
            buyAdventure = config.Bind(LocalizationManager.GetLangValue("buyAdventure.label"), LocalizationManager.GetLangValue("buyAdventure.name"), Utils.BuyAdventureTemplate.DoNothing, LocalizationManager.GetLangValue("buyAdventure.description"));
            isKarazhanFixEnable = config.Bind(LocalizationManager.GetLangValue("isKarazhanFixEnable.label"), LocalizationManager.GetLangValue("isKarazhanFixEnable.name"), false, LocalizationManager.GetLangValue("isKarazhanFixEnable.description"));
            webServerPort = config.Bind(LocalizationManager.GetLangValue("webServerPort.label"), LocalizationManager.GetLangValue("webServerPort.name"), 58744, new ConfigDescription(LocalizationManager.GetLangValue("webServerPort.description"), new AcceptableValueRange<int>(1, 65535)));
            webPageBackImg = config.Bind(LocalizationManager.GetLangValue("webPageBackImg.label"), LocalizationManager.GetLangValue("webPageBackImg.name"), "https://imgapi.cn/cos.php", new ConfigDescription(LocalizationManager.GetLangValue("webPageBackImg.description"), null, new object[] { "Advanced" }));
            isWebshellEnable = config.Bind(LocalizationManager.GetLangValue("isWebshellEnable.label"), LocalizationManager.GetLangValue("isWebshellEnable.name"), false, LocalizationManager.GetLangValue("isWebshellEnable.description"));
            isInternalModeEnable = config.Bind(LocalizationManager.GetLangValue("isInternalModeEnable.label"), LocalizationManager.GetLangValue("isInternalModeEnable.name"), false, LocalizationManager.GetLangValue("isInternalModeEnable.description"));

            fakeDevicePreset = config.Bind(LocalizationManager.GetLangValue("fakeDevicePreset.label"), LocalizationManager.GetLangValue("fakeDevicePreset.name"), Utils.DevicePreset.Default, LocalizationManager.GetLangValue("fakeDevicePreset.description"));
            fakeDeviceOs = config.Bind(LocalizationManager.GetLangValue("fakeDeviceOs.label"), LocalizationManager.GetLangValue("fakeDeviceOs.name"), OSCategory.PC, LocalizationManager.GetLangValue("fakeDeviceOs.description"));
            fakeDeviceScreen = config.Bind(LocalizationManager.GetLangValue("fakeDeviceScreen.label"), LocalizationManager.GetLangValue("fakeDeviceScreen.name"), ScreenCategory.PC, LocalizationManager.GetLangValue("fakeDeviceScreen.description"));
            fakeDeviceName = config.Bind(LocalizationManager.GetLangValue("fakeDeviceName.label"), LocalizationManager.GetLangValue("fakeDeviceName.name"), "HsMod", LocalizationManager.GetLangValue("fakeDeviceName.description"));

            fakePackCount = config.Bind(LocalizationManager.GetLangValue("fakePackCount.label"), LocalizationManager.GetLangValue("fakePackCount.name"), 233, LocalizationManager.GetLangValue("fakePackCount.description"));
            fakeBoosterDbId = config.Bind(LocalizationManager.GetLangValue("fakeBoosterDbId.label"), LocalizationManager.GetLangValue("fakeBoosterDbId.name"), BoosterDbId.GOLDEN_CLASSIC_PACK, LocalizationManager.GetLangValue("fakeBoosterDbId.description"));
            isFakeRandomResult = config.Bind(LocalizationManager.GetLangValue("isFakeRandomResult.label"), LocalizationManager.GetLangValue("isFakeRandomResult.name"), false, LocalizationManager.GetLangValue("isFakeRandomResult.description"));
            isFakeRandomRarity = config.Bind(LocalizationManager.GetLangValue("isFakeRandomRarity.label"), LocalizationManager.GetLangValue("isFakeRandomRarity.name"), false, LocalizationManager.GetLangValue("isFakeRandomRarity.description"));
            isFakeRandomPremium = config.Bind(LocalizationManager.GetLangValue("isFakeRandomPremium.label"), LocalizationManager.GetLangValue("isFakeRandomPremium.name"), false, LocalizationManager.GetLangValue("isFakeRandomPremium.description"));
            isFakeAtypicalRandomPremium = config.Bind(LocalizationManager.GetLangValue("isFakeAtypicalRandomPremium.label"), LocalizationManager.GetLangValue("isFakeAtypicalRandomPremium.name"), false, LocalizationManager.GetLangValue("isFakeAtypicalRandomPremium.description"));
            fakeRandomRarity = config.Bind(LocalizationManager.GetLangValue("fakeRandomRarity.label"), LocalizationManager.GetLangValue("fakeRandomRarity.name"), Utils.CardRarity.LEGENDARY, LocalizationManager.GetLangValue("fakeRandomRarity.description"));
            fakeRandomPremium = config.Bind(LocalizationManager.GetLangValue("fakeRandomPremium.label"), LocalizationManager.GetLangValue("fakeRandomPremium.name"), TAG_PREMIUM.GOLDEN, LocalizationManager.GetLangValue("fakeRandomPremium.description"));

            fakeCatchupCount = config.Bind(LocalizationManager.GetLangValue("fakeCatchupCount.label"), LocalizationManager.GetLangValue("fakeCatchupCount.name"), -1, new ConfigDescription(LocalizationManager.GetLangValue("fakeCatchupCount.description"), null, new object[] { "Advanced" }));
            fakeCardID1 = config.Bind(LocalizationManager.GetLangValue("fakeCardID1.label"), LocalizationManager.GetLangValue("fakeCardID1.name"), 71984, new ConfigDescription(LocalizationManager.GetLangValue("fakeCardID1.description"), null, new object[] { "Advanced" }));
            fakeCardPremium1 = config.Bind(LocalizationManager.GetLangValue("fakeCardPremium1.label"), LocalizationManager.GetLangValue("fakeCardPremium1.name"), TAG_PREMIUM.GOLDEN, new ConfigDescription(LocalizationManager.GetLangValue("fakeCardPremium1.description"), null, new object[] { "Advanced" }));
            fakeCardID2 = config.Bind(LocalizationManager.GetLangValue("fakeCardID2.label"), LocalizationManager.GetLangValue("fakeCardID2.name"), 71945, new ConfigDescription(LocalizationManager.GetLangValue("fakeCardID2.description"), null, new object[] { "Advanced" }));
            fakeCardPremium2 = config.Bind(LocalizationManager.GetLangValue("fakeCardPremium2.label"), LocalizationManager.GetLangValue("fakeCardPremium2.name"), TAG_PREMIUM.GOLDEN, new ConfigDescription(LocalizationManager.GetLangValue("fakeCardPremium2.description"), null, new object[] { "Advanced" }));
            fakeCardID3 = config.Bind(LocalizationManager.GetLangValue("fakeCardID3.label"), LocalizationManager.GetLangValue("fakeCardID3.name"), 73446, new ConfigDescription(LocalizationManager.GetLangValue("fakeCardID3.description"), null, new object[] { "Advanced" }));
            fakeCardPremium3 = config.Bind(LocalizationManager.GetLangValue("fakeCardPremium3.label"), LocalizationManager.GetLangValue("fakeCardPremium3.name"), TAG_PREMIUM.GOLDEN, new ConfigDescription(LocalizationManager.GetLangValue("fakeCardPremium3.description"), null, new object[] { "Advanced" }));
            fakeCardID4 = config.Bind(LocalizationManager.GetLangValue("fakeCardID4.label"), LocalizationManager.GetLangValue("fakeCardID4.name"), 71781, new ConfigDescription(LocalizationManager.GetLangValue("fakeCardID4.description"), null, new object[] { "Advanced" }));
            fakeCardPremium4 = config.Bind(LocalizationManager.GetLangValue("fakeCardPremium4.label"), LocalizationManager.GetLangValue("fakeCardPremium4.name"), TAG_PREMIUM.GOLDEN, new ConfigDescription(LocalizationManager.GetLangValue("fakeCardPremium4.description"), null, new object[] { "Advanced" }));
            fakeCardID5 = config.Bind(LocalizationManager.GetLangValue("fakeCardID5.label"), LocalizationManager.GetLangValue("fakeCardID5.name"), 67040, new ConfigDescription(LocalizationManager.GetLangValue("fakeCardID5.description"), null, new object[] { "Advanced" }));
            fakeCardPremium5 = config.Bind(LocalizationManager.GetLangValue("fakeCardPremium5.label"), LocalizationManager.GetLangValue("fakeCardPremium5.name"), TAG_PREMIUM.GOLDEN, new ConfigDescription(LocalizationManager.GetLangValue("fakeCardPremium5.description"), null, new object[] { "Advanced" }));
			isAutoRedundantNDE = config.Bind(LocalizationManager.GetLangValue("isAutoRedundantNDE.label"), LocalizationManager.GetLangValue("isAutoRedundantNDE.name"), false, LocalizationManager.GetLangValue("isAutoRedundantNDE.description"));

            InitCardsMapping();
            LoadSkinsConfigFromFile();
            ConfigValueDelegate();
            ConfigTemplateSettingChanged(configTemplate.Value);
            timeKeeper = DateTime.Now.Ticks;


            if (CommandConfig.hsMatchLogPath == string.Empty) CommandConfig.hsMatchLogPath = hsMatchLogPath.Value;
            if (CommandConfig.webServerPort == -1) CommandConfig.webServerPort = webServerPort.Value;

            isAutoRefundCardDisenchantEnable.Value = false;  // 自动禁用自动分解，使用时，必须手动开启

        }
        public static void CreateHsModWorkDir()
        {
            HsModWebSite = System.IO.Path.Combine(BepInEx.Paths.BepInExRootPath, "HsMod");
            try
            {
                if (!Directory.Exists(PluginConfig.HsModWebSite))
                {
                    Directory.CreateDirectory(PluginConfig.HsModWebSite);
                }
            }
            catch (Exception ex)
            {

                Utils.MyLogger(BepInEx.Logging.LogLevel.Error, $"{ex.Message} \n{ex.InnerException} \n{ex.StackTrace}");
            }
        }
        public static void ConfigValueDelegate()
        {
            pluginLanague.SettingChanged += delegate
            {
                pluginInitLanague.Value = pluginLanague.Value.ToString();
            };
            configTemplate.SettingChanged += delegate
            {
                ConfigTemplateSettingChanged(configTemplate.Value);
            };
            skinCardBack.SettingChanged += delegate
            {
                GameState gameState = GameState.Get();
                if (gameState != null)
                {
                    Player friendlySidePlayer = GameState.Get()?.GetFriendlySidePlayer();
                    if (friendlySidePlayer != null)
                        _ = friendlySidePlayer.GetCardBackId();
                    int opponentCardBackID = 0;
                    Player opposingSidePlayer = GameState.Get()?.GetOpposingSidePlayer();
                    if (opposingSidePlayer != null)
                        opponentCardBackID = opposingSidePlayer.GetCardBackId();
                    int friendlyCardBackID = skinCardBack.Value;
                    CardBackManager.Get().SetGameCardBackIDs(friendlyCardBackID, opponentCardBackID);
                }
            };
            buyAdventure.SettingChanged += delegate
            {
                if (buyAdventure.Value != Utils.BuyAdventureTemplate.DoNothing)
                {
                    Utils.BuyAdventure(buyAdventure.Value);
                    buyAdventure.Value = Utils.BuyAdventureTemplate.DoNothing;
                }
            };
        }

        public static void ConfigTemplateSettingChanged(Utils.ConfigTemplate cTemplate)
        {
            switch (cTemplate)
            {
                case Utils.ConfigTemplate.DoNothing:
                    return;
                case Utils.ConfigTemplate.AwayFromKeyboard:
                    isShortcutsEnable.Value = false;
                    isIGMMessageShow.Value = false;
                    isAlertPopupShow.Value = false;
                    responseAlertPopup.Value = Utils.AlertPopupResponse.YES;
                    isOnApplicationFocus.Value = false;
                    isRewardToastShow.Value = false;
                    isAutoOpenBoxesRewardEnable.Value = true;
                    isAutoExit.Value = true;
                    isIdleKickEnable.Value = false;
                    isQuickPackOpeningEnable.Value = true;
                    //isAutoRefundCardDisenchantEnable.Value = true;
                    isAutoRecvMercenaryRewardEnable.Value = true;
                    isMercenaryBattleZoom.Value = false;
                    isSkipHeroIntro.Value = true;
                    isThinkEmotesEnable.Value = false;
                    receiveEnemyEmoteLimit.Value = 0;
                    isOpponentGoldenCardShow.Value = false;
                    skinCoin.Value = 1746;   // 初始幸运币
                    //isSkinDefalutHeroEnable.Value = true;
                    mercenaryDiamondCardState.Value = Utils.CardState.Disabled;
                    randomMercenarySkinEnable.Value = Utils.CardState.Disabled;
                    goldenCardState.Value = Utils.CardState.Disabled;
                    maxCardState.Value = Utils.CardState.Disabled;
                    configTemplate.Value = Utils.ConfigTemplate.DoNothing;
                    return;
                case Utils.ConfigTemplate.AntiAwayFromKeyboard:
                    isShortcutsEnable.Value = true;
                    isIGMMessageShow.Value = true;
                    isAlertPopupShow.Value = true;
                    responseAlertPopup.Value = Utils.AlertPopupResponse.DONOTHING;
                    isOnApplicationFocus.Value = false;
                    isRewardToastShow.Value = true;
                    isAutoOpenBoxesRewardEnable.Value = false;
                    isAutoExit.Value = false;
                    isIdleKickEnable.Value = true;
                    isQuickPackOpeningEnable.Value = true;
                    //isAutoRefundCardDisenchantEnable.Value = false;
                    isAutoRecvMercenaryRewardEnable.Value = true;
                    isMercenaryBattleZoom.Value = false;
                    isSkipHeroIntro.Value = true;
                    isThinkEmotesEnable.Value = false;
                    receiveEnemyEmoteLimit.Value = 3;
                    isOpponentGoldenCardShow.Value = true;
                    skinCoin.Value = -1;
                    isSkinDefalutHeroEnable.Value = false;
                    goldenCardState.Value = Utils.CardState.Default;
                    maxCardState.Value = Utils.CardState.Default;
                    mercenaryDiamondCardState.Value = Utils.CardState.Default;
                    randomMercenarySkinEnable.Value = Utils.CardState.Default;
                    configTemplate.Value = Utils.ConfigTemplate.DoNothing;
                    return;
            }
        }

        public static void InitCardsMapping()
        {
            CardsMapping.Clear();
            Utils.CardMapping cardMapping = new Utils.CardMapping
            {
                ThisSkinType = Utils.SkinType.COIN,
                RealDbID = -1,
                FakeDbID = skinCoin.Value,
                RealCardID = "",
                FakeCardID = ""
                //FakeCardID = GameUtils.TranslateDbIdToCardId(skinCoin.Value)
            };
            if (cardMapping.FakeDbID != -1)
                CardsMapping.Add(cardMapping);
        }
        public static void UpdateCardsMapping()
        {
            for (int i = 0; i < CardsMapping.Count; i++)
            {
                if (CardsMapping[i].FakeDbID != -1 && CardsMapping[i].FakeCardID == "")
                {
                    Utils.CardMapping cardMapping = CardsMapping[i];
                    cardMapping.FakeCardID = GameUtils.TranslateDbIdToCardId(cardMapping.FakeDbID);
                    CardsMapping[i] = cardMapping;
                }
            }
        }
        public static void UpdateCardsMappingReal(string realCardID, Utils.SkinType skinType)
        {
            UpdateCardsMapping();
            for (int i = 0; i < CardsMapping.Count; i++)
            {
                if (CardsMapping[i].ThisSkinType == skinType)
                {
                    Utils.CardMapping cardMapping = CardsMapping[i];
                    cardMapping.RealCardID = realCardID;
                    cardMapping.RealDbID = GameUtils.TranslateCardIdToDbId(realCardID);
                    CardsMapping[i] = cardMapping;
                    break;
                }
            }
        }

        public static void LoadSkinsConfigFromFile()
        {
            string file = Path.Combine(BepInEx.Paths.ConfigPath, "HsSkins.cfg");
            HeroesMapping.Clear();
            if (File.Exists(file))
            {
                foreach (string line in File.ReadLines(file))
                {
                    if (line.StartsWith("#"))
                        continue;
                    else
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length == 2)
                        {
                            if (!HeroesMapping.ContainsKey(int.Parse(parts[0].Trim())))
                            {
                                string[] skins = parts[1].Split(',');
                                HeroesMapping.Add(int.Parse(parts[0].Trim()), int.Parse(skins[new System.Random().Next(skins.Length)].Trim()));
                            }
                        }
                    }
                }
            }
            else
            {
                string newConfigFile = LocalizationManager.GetLangValue("HsSkins.cfg");
                File.WriteAllText(file, newConfigFile);
            }
        }

        public static ConfigValue configValue = new ConfigValue();
    }



    //对外接口，
    public class ConfigValue
    {
        public bool IsOpponentRankInGameShowValue
        {
            get
            {
                if (GameUtils.IsGameTypeRanked()) return PluginConfig.isOpponentRankInGameShow.Value;
                else return false;
            }
            set { PluginConfig.isOpponentRankInGameShow.Value = value; }
        }
        public bool IsSkipHeroIntroValue
        {
            get { return PluginConfig.isSkipHeroIntro.Value; }
            set { PluginConfig.isSkipHeroIntro.Value = value; }
        }
        public bool IsShutUpBobEnableValue
        {
            get { return PluginConfig.isShutUpBobEnable.Value; }
            set { PluginConfig.isShutUpBobEnable.Value = value; }
        }
        public bool IsQuickPackOpeningEnableValue
        {
            get { return PluginConfig.isQuickPackOpeningEnable.Value; }
            set { PluginConfig.isQuickPackOpeningEnable.Value = value; }
        }
        public bool IsShowCardLargeCountValue
        {
            get { return PluginConfig.isShowCardLargeCount.Value; }
            set { PluginConfig.isShowCardLargeCount.Value = value; }
        }
        public bool IsMoveEnemyCardsEnableValue
        {
            get { return PluginConfig.isMoveEnemyCardsEnable.Value; }
            set { PluginConfig.isMoveEnemyCardsEnable.Value = value; }
        }
        public bool IsShowFPSEnableValue
        {
            get { return PluginConfig.isShowFPSEnable.Value; }
            set { PluginConfig.isShowFPSEnable.Value = value; }
        }
        public bool IsInternalModeEnableValue
        {
            get { return PluginConfig.isInternalModeEnable.Value; }
            set { PluginConfig.isInternalModeEnable.Value = value; }
        }
        public bool IsAlertPopupShowValue
        {
            get { return PluginConfig.isAlertPopupShow.Value; }
            set { PluginConfig.isAlertPopupShow.Value = value; }
        }
        public Utils.ConfigTemplate ConfigTemplateValue
        {
            set { PluginConfig.configTemplate.Value = value; }
        }
        public bool IsQuickModeEnableValue
        {
            get
            {
                return PluginConfig.isQuickModeEnable.Value && (GameMgr.Get().IsBattlegrounds() || (GameMgr.Get().IsMercenaries() && (GameMgr.Get().IsAI() || GameMgr.Get().IsLettuceTutorial() || GameMgr.Get().GetGameType() == PegasusShared.GameType.GT_VS_AI || GameMgr.Get().GetGameType() == PegasusShared.GameType.GT_MERCENARIES_AI_VS_AI || GameMgr.Get().GetGameType() == PegasusShared.GameType.GT_MERCENARIES_PVE)));
            }
            set { PluginConfig.isQuickModeEnable.Value = value; }
        }

        public bool IsTimeGearEnableValue
        {
            get { return PluginConfig.isTimeGearEnable.Value; }
            set { PluginConfig.isTimeGearEnable.Value = value; }
        }

        public bool TimeGearEnable
        {
            get { return PluginConfig.isTimeGearEnable.Value; }
            set { PluginConfig.isTimeGearEnable.Value = value; }
        }

        public int TimeGearValue
        {
            get { return PluginConfig.timeGear.Value; }
            set { PluginConfig.timeGear.Value = value; }
        }

        public long RunningTime
        {
            get { return (DateTime.Now.Ticks - PluginConfig.timeKeeper) / 10000000; }    // 返回秒
        }
        public bool IsBypassDeckShareCodeCheckEnable
        {
            get { return PluginConfig.isBypassDeckShareCodeCheckEnable.Value; }
            set { PluginConfig.isBypassDeckShareCodeCheckEnable.Value = value; }
        }
        public string HsMatchLogPathValue
        {
            get { return PluginConfig.CommandConfig.hsMatchLogPath; }
            set
            {
                PluginConfig.hsMatchLogPath.Value = value;
                PluginConfig.CommandConfig.hsMatchLogPath = value;
            }
        }

        public string CacheOpponentFullName
        {
            get
            {
                if (!String.IsNullOrEmpty(Utils.CacheLastOpponentFullName))
                    return Utils.CacheLastOpponentFullName;
                else if (!String.IsNullOrEmpty(BnetPresenceMgr.Get()?.GetPlayer(GameState.Get()?.GetOpposingSidePlayer()?.GetGameAccountId())?.GetFullName()))
                {
                    return BnetPresenceMgr.Get()?.GetPlayer(GameState.Get()?.GetOpposingSidePlayer()?.GetGameAccountId())?.GetFullName();
                }

                return "";
            }
        }

        public static ConfigValue Get()
        {
            return PluginConfig.configValue;
        }

    }
}
