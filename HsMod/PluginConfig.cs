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
        public static ConfigEntry<bool> isFakeOpenEnable;
        public static ConfigEntry<Utils.ConfigTemplate> configTemplate;
        public static ConfigEntry<bool> isTimeGearEnable;
        public static ConfigEntry<bool> isShortcutsEnable;
        public static ConfigEntry<int> targetFrameRate;
        public static ConfigEntry<bool> isDynamicFpsEnable;

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
        //public static ConfigEntry<KeyboardShortcut> keyRuin;    //Destroy ithurry up
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
        public static ConfigEntry<long> autoQuitTimer;    // Exit regularly

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


        public static class CommandConfig
        {
            public static int webServerPort = -1;
            public static string hsMatchLogPath = "";
            public static int width = -1;
            public static int height = -1;
        }

        public static long timeKeeper = DateTime.Now.Ticks;

        public static List<Utils.CardMapping> CardsMapping = new List<Utils.CardMapping>();    //Card replacement mapping，Not currently in use
        public static IGraphicsManager graphicsManager;
        public static void ConfigBind(ConfigFile config)
        {
            config.Clear();
            isPluginEnable = config.Bind("overall situation", "HsModstate", false, "Whether to enable plug-ins（After modifying this option, it is recommended to restart Hearthstone）");
            configTemplate = config.Bind("overall situation", "Set template", Utils.ConfigTemplate.DoNothing, "ConfigurationRun template，When the option isDoNothinghour，Do not modify anyConfiguration。Configuration修改完成后automaticreplace回DoNothing");
            isShortcutsEnable = config.Bind("overall situation", "shortcut keystate", false, "Whether to enableshortcut key");
            isTimeGearEnable = config.Bind("overall situation", "speed change gearstate", false, "Whether to enable transmission gear");
            timeGear = config.Bind("overall situation", "Variable speed ratio", 0, new ConfigDescription("Speed ​​change gear，1and0Same magnification，Negative numbers mean slower", new AcceptableValueRange<int>(-32, 32)));
            isShowFPSEnable = config.Bind("overall situation", "showFPS", false, "Whether to displayFPSinformation（shortcut key：leftCrtl+P）");
            targetFrameRate = config.Bind("overall situation", "game frame rate", -1, new ConfigDescription("Game frame rate settings，-1Indicates no modification or restoration to default value（The default value may be30）", new AcceptableValueRange<int>(-1, 2333)));

            isIGMMessageShow = config.Bind("optimization", "In-game news", true, "（When the store cannot be opened，You can try setting this option to onstate）Whether to displayIn-game news（advertising promotion、nerf patch、Ladder settlementinformationwait）");
            isAlertPopupShow = config.Bind("optimization", "popup message", true, "Whether to display a pop-up window");
            responseAlertPopup = config.Bind("optimization", "popup response", Utils.AlertPopupResponse.DONOTHING, "existshieldpopup messagehour，How to respond to pop-ups");
            isOnApplicationFocus = config.Bind("optimization", "Application focus", true, "isOnApplicationFocus");
            isRewardToastShow = config.Bind("optimization", "Settlement display", true, "Whether to display settlement tasks、Achievement rewards、Upgrade tips, etc.（This may result in no prompt when claiming rewards.）");
            isAutoOpenBoxesRewardEnable = config.Bind("optimization", "Automatic box opening", false, "Whether to automatically open the arena（duel、mercenarywait）Settlement chest");
            isAutoExit = config.Bind("optimization", "Exit with error", false, "Whether to automatically exit when encountering an error");
            //isAutoRestart = config.Bind("optimization", "Restart on exit", false, "（May not be valid）Whether to automatically restart when an error occurs");
            isShowCardLargeCount = config.Bind("optimization", "collectcardquantity", false, "Whether to displaycollectcardquantitymore than thewait于10number of hours（Temporarily available when selectedBug）");
            isShowCollectionCardIdEnable = config.Bind("optimization", "showcardID", false, "Whether to right-click to select a card（skin）hour，Show and copy selectionsCardID");
            isBypassDeckShareCodeCheckEnable = config.Bind("optimization", "Card group sharing code detection", false, "Whether to removeCard group sharing code detection");
            isShowRetireForever = config.Bind("optimization", "showgive up", false, "allowexist0-0give up deck");
            isIdleKickEnable = config.Bind("optimization", "Allow disconnection", true, "（Not tested yet）Whether to allow disconnection without operation for a long time（start up游戏hourUnable to loadConfiguration）");


            isQuickPackOpeningEnable = config.Bind("Unpack", "Unpackaccelerate", false, "Unpackaccelerate，usespaceUnpackhourShow results directly");
            isAutoPackOpeningEnable = config.Bind("Unpack", "automaticUnpack", false, "（Use with caution，haveBUG！）Open all card packs，No distinction between card package categories（based onUnpackaccelerate）");
            isAutoRefundCardDisenchantEnable = config.Bind("Unpack", "Automatic decomposition", false, "whetherexistUnpackhourAutomatic decompositionFull anti-dust card");

            isAutoReportEnable = config.Bind("friends", "Automatic reporting", false, "After the game is overAutomatic reportingopponent昵称违规、Cheats and Scripts、malicioussurrender");
            // isAutoReportEnable = config.Bind("friends", "Automatic reporting", true, new ConfigDescription("After the game is overAutomatic reportingopponent昵称违规、Cheats and Scripts、malicioussurrender", null, new object[] { "Advanced" }));
            isMoveEnemyCardsEnable = config.Bind("friends", "spectating displaycard", false, "（Not tested yet）existObDisplay in(rotate)opponent's cards");


            isQuickModeEnable = config.Bind("Hearthstone", "Quick fight", false, "Whether to enableTavernormercenaryAIQuick battle mode");
            isFullnameShow = config.Bind("Hearthstone", "showfull name", false, "Whether to displayFull name of opponent's battle network；If this option is enabled，Also allows adding current opponents(start upshortcut keyhour，Also allows adding opponents)。");
            isOpponentRankInGameShow = config.Bind("Hearthstone", "Show ladder level", false, "Whether to display the opponent's ladder level before the legend");
            isCardTrackerEnable = config.Bind("Hearthstone", "cardtrack", false, "推测opponentcard，and give hints（For example：Decision etc.，There is a probability of recognition error）");
            isCardRevealedEnable = config.Bind("Hearthstone", "cardreveal", false, "Show what is known in a bright lightcard（There is a chance that Hearthstone will automatically disconnect and reconnect.）");
            isSkipHeroIntro = config.Bind("Hearthstone", "jump overherointroduce", false, "whetherjump overherointroduce(ShouldSkipMulligan)");
            isExtendedBMEnable = config.Bind("Hearthstone", "Expression without cooling", false, "Whether to allow unlimited emoticons(lowest latency1.5s)");
            isThinkEmotesEnable = config.Bind("Hearthstone", "thinking expression", true, "Whether to allow thinking emoticons to be displayed");
            receiveEnemyEmoteLimit = config.Bind("Hearthstone", "Number of expressions", -1, new ConfigDescription("In gameNumber of expressionsReceipt restrictions，More than automatic blocking of opponent expressions，0time opening shield，-1not limited（There are smallbug）", new AcceptableValueRange<int>(-1, 100)));
            isOpponentGoldenCardShow = config.Bind("Hearthstone", "opponentcardspecial effects", true, "Whether to displayopponentcardspecial effects(coverALLConfiguration)");
            isSignatureCardStateEnable = config.Bind("Hearthstone", "Different painting special effects", true, "whetherexistcardHighest special effects中showStrange paintings（only影响cardHighest special effects）");
            goldenCardState = config.Bind("Hearthstone", "gold cardspecial effects", Utils.CardState.Default, "Forced gold card effect");
            maxCardState = config.Bind("Hearthstone", "cardHighest special effects", Utils.CardState.Default, "forcecardHighest special effectsspecial effects（Current priority：diamond、Strange paintings、gold card、usually）");

            isAutoRecvMercenaryRewardEnable = config.Bind("mercenary", "Automatically collect prizes", false, "whetherautomatic领取mercenarymercenaryaward（Shield treasure chest）");
            isMercenaryBattleZoom = config.Bind("mercenary", "Allow scaling", true, "（That may existBUG）Is it allowed?mercenaryfightinghourZoom screen");
            mercenaryDiamondCardState = config.Bind("mercenary", "diamondskinreplace", Utils.CardState.Default, "if it is possible，whetherreplace成diamondskin（Lower priority than Hearthstone-cardHighest special effects）");
            randomMercenarySkinEnable = config.Bind("mercenary", "Random skin", Utils.CardState.Default, "Random skin（Does not include diamond skin and hearthstone-diamondThe card effect cannot bedisabled）");

            isShutUpBobEnable = config.Bind("Tavern", "silent bob", false, "whether to letbobShut up");
            isBgsGoldenEnable = config.Bind("Tavern", "Taverngold plated", false, "（test，You need to turn on the gold card effect in Hearthstone Card Effects）whethergold platedTavern。该gold plated不meetinggold plated随从andquest line。");
            //Consider exporting separatelyConfiguration
            skinCoin = config.Bind("skin", "coin", -1, "Lucky Coin PreferenceID，-1Indicates no modifications will be made（In gameSimulate unpluggingCan be implementedhourrenew）");
            skinCardBack = config.Bind("skin", "card back", -1, "Card back preferenceID，-1Indicates no modifications will be made（Effective in real time）");
            skinBoard = config.Bind("skin", "Battlepanel", -1, "（test function，可能meetingexistTavernstuck），BattlePanel replacement，-1Indicates no modifications will be made）");
            skinBgsBoard = config.Bind("skin", "Tavernfightingpanel", -1, "TavernfightingpanelPreferencesID，-1Indicates no modifications will be made");
            skinBgsFinisher = config.Bind("skin", "Tavern kill effects", -1, "Tavern击杀PreferencesID，-1Indicates no modifications will be made");
            skinBob = config.Bind("skin", "bob", -1, "Bob's preferencesID，-1Indicates no modifications will be made");
            isSkinDefalutHeroEnable = config.Bind("skin", "defaulthero", false, "if it is possible，WillBattleheroskin都replace为defaultskin");
            skinHero = config.Bind("skin", "hero", -1, "（Use with caution，Not recommended for non-hook use，优先级低于defaulthero。generallyDownIt is recommended to load from a fileheroskin，After modificationF4renew；If you are in a match again，You also need to simulate unplugging）heroPreferencesID，-1Indicates no modifications will be made");
            skinOpposingHero = config.Bind("skin", "opponenthero", -1, "（Use with caution，Not recommended for non-hook use，优先级低于defaulthero）opponentheroPreferencesID，-1Indicates no modifications will be made");

            keyTimeGearUp = config.Bind("shortcut key", "Gear magnification+1", new KeyboardShortcut(KeyCode.UpArrow), "Gear magnificationIncrease1，in default direction");
            keyTimeGearDown = config.Bind("shortcut key", "Gear magnification-1", new KeyboardShortcut(KeyCode.DownArrow), "gear ratio reduction1，In default orientation");
            keyTimeGearDefault = config.Bind("shortcut key", "Gear magnificationReturn to zero", new KeyboardShortcut(KeyCode.LeftArrow), "Restore default gear ratio，Default direction left");
            keyTimeGearMax = config.Bind("shortcut key", "Maximum gear ratio", new KeyboardShortcut(KeyCode.RightArrow), "Gear magnificationless than4time becomes4，more than the4time becomes8，Default direction right");
            keySimulateDisconnect = config.Bind("shortcut key", "Simulate unplugging", new KeyboardShortcut(KeyCode.D, KeyCode.LeftControl), "Simulate offline reconnection，Pay attention to needsDisableExit with errorand allowpopup message，defaultleftCtrl+D");
            keyCopyBattleTag = config.Bind("shortcut key", "Copy an opponent's Battle.net tag", new KeyboardShortcut(KeyCode.C, KeyCode.LeftControl), "Copy in-game rival battle networkID，defaultleftCtrl+C");
            keyCopySelectBattleTag = config.Bind("shortcut key", "Copy selected opponent Battle.net tag", new KeyboardShortcut(KeyCode.Mouse0), "copyTavernSelected opponent in Battle.netID，Default left mouse button");
            keyConcede = config.Bind("shortcut key", "surrender", new KeyboardShortcut(KeyCode.Space, KeyCode.LeftControl), "surrender，defaultleftCtrl+space");
            keyContinueMulligan = config.Bind("shortcut key", "end turn", new KeyboardShortcut(KeyCode.Space), "End turn or replace card confirmation，defaultspace");
            keySquelch = config.Bind("shortcut key", "silence opponents", new KeyboardShortcut(KeyCode.Q, KeyCode.LeftControl), "Block your opponent’s expression，defaultleftCtrl+Q");
            keySoundMute = config.Bind("shortcut key", "mute/restore volume", new KeyboardShortcut(KeyCode.S, KeyCode.LeftControl), "mute/restore volume，defaultleftCtrl+S");
            keyShutUpBob = config.Bind("shortcut key", "closed，bob", new KeyboardShortcut(KeyCode.B, KeyCode.LeftControl), "Disable/recoverbobvoice，defaultleftCtrl+B");
            keyRefund = config.Bind("shortcut key", "One-click full decomposition", new KeyboardShortcut(KeyCode.Z, KeyCode.LeftControl), "Fully decomposed with one clickcard（onlyexistUnpack界面与collect界面haveeffect），defaultleftCtrl+Z");
            //keyRuin = config.Bind("shortcut key", "Destroy it，hurry up", new KeyboardShortcut(KeyCode.R, KeyCode.LeftControl), "One-click exit，defaultleftCtrl+R");
            keyReadNewCards = config.Bind("shortcut key", "ERROR，Read！", new KeyboardShortcut(KeyCode.R, KeyCode.LeftControl), "Eliminate all new！mark（onlyexistUnpack界面与collect界面haveeffect；mercenarymodel（havebug，Invalid after restarting）Down，You may need to re-enter the collection after execution），defaultleftCtrl+R");
            keyShowFPS = config.Bind("shortcut key", "show/Hide frame rate", new KeyboardShortcut(KeyCode.P, KeyCode.LeftControl), "Show or hide game frame rate information，defaultleftCtrl+P");

            keyEmoteGreetings = config.Bind("shortcut key", "greeting", new KeyboardShortcut(KeyCode.Alpha1), "expressiongreeting，Default numeric keys1");
            keyEmoteWellPlayed = config.Bind("shortcut key", "commend", new KeyboardShortcut(KeyCode.Alpha2), "Expression of praise，Default numeric keys2");
            keyEmoteThanks = config.Bind("shortcut key", "grateful", new KeyboardShortcut(KeyCode.Alpha3), "expressiongrateful，Default numeric keys3");
            keyEmoteWow = config.Bind("shortcut key", "marvel", new KeyboardShortcut(KeyCode.Alpha4), "expressionmarvel，Default numeric keys4");
            keyEmoteOops = config.Bind("shortcut key", "Mistake", new KeyboardShortcut(KeyCode.Alpha5), "Expression error，Default numeric keys5");
            keyEmoteThreaten = config.Bind("shortcut key", "threaten", new KeyboardShortcut(KeyCode.Alpha6), "expressionthreaten，Default numeric keys6");

            hsLogPath = config.Bind("develop", "Hearthstone Log", "", new ConfigDescription("HearthstoneProcess log file location（relative toHearthstone）", null, new object[] { "Advanced" }));
            hsMatchLogPath = config.Bind("develop", "Game log", @"BepInEx/HsMatch.log", "Hearthstone game log file location（relative toHearthstone），The parameters are first selected from the command line.");
            autoQuitTimer = config.Bind("develop", "Exit regularly", (long)0, "When the game is runningxseconds later（at the end of the game）Automatic withdrawal，x<=0This option is invalid when。");
            isFakeOpenEnable = config.Bind("develop", "simulationUnpackstate", false, "Whether to enablesimulationUnpack（After modifying this option, it is recommended to restart Hearthstone，enablehourMay cause jamminginformationstatistical anomaly）");
            buyAdventure = config.Bind("develop", "risk buying", Utils.BuyAdventureTemplate.DoNothing, "（It is not recommended to buy Karazhan）Choose an adventure to try with purchase（Possibility of account ban，Consider using as appropriate）");
            isKarazhanFixEnable = config.Bind("develop", "Karazhan Repair", false, "（Please close after typing，Unable to write prologue at the moment）Karazhan Crow's Soar Repair，Can also be used as an adventure jump level。（Possibility of account ban，Consider using as appropriate）");
            webServerPort = config.Bind("develop", "websiteport", 58744, new ConfigDescription("WebServerport，The parameters are first selected from the command line.", new AcceptableValueRange<int>(1, 65535)));
            webPageBackImg = config.Bind("develop", "Web page background image", "", new ConfigDescription("Web page background image", null, new object[] { "Advanced" }));
            isWebshellEnable = config.Bind("develop", "Webshell", false, "Webshellswitch");
            isInternalModeEnable = config.Bind("develop", "internal mode", false, "whether to switch tointernal mode（Need to restart Hearthstone）");

            fakeDevicePreset = config.Bind("simulation", "Device Simulation Template", Utils.DevicePreset.Default, "（It will take effect after restarting Hearthstone.）Analog device，Used to receive card packscard back");
            fakeDeviceOs = config.Bind("simulation", "Equipment simulation system", OSCategory.PC, "Analog deviceoperating system，When the device simulation template isCustomvalid when。");
            fakeDeviceScreen = config.Bind("simulation", "Device screen size", ScreenCategory.PC, "Analog size（Screentype），When the device simulation template isCustomvalid when。");
            fakeDeviceName = config.Bind("simulation", "Equipment equipment model", "HsMod", "Analog devicemodel，When the device simulation template isCustomvalid when。");

            fakePackCount = config.Bind("simulation", "quantity", 233, "Number of simulation card packs");
            fakeBoosterDbId = config.Bind("simulation", "type", BoosterDbId.GOLDEN_CLASSIC_PACK, "simulationcard holdertype。(Replace card pack icon)");
            isFakeRandomResult = config.Bind("simulation", "Random results", false, "Whether to enable random results");
            isFakeRandomRarity = config.Bind("simulation", "Random rarity", false, "Whether to randomize rarity（based onRandom results）");
            isFakeRandomPremium = config.Bind("simulation", "random quality", false, "whetherrandom quality（based onRandom results）");
            isFakeAtypicalRandomPremium = config.Bind("simulation", "Random other special effects", false, "random qualityIncludeddiamondorStrange paintingswait（based onrandom quality）");
            fakeRandomRarity = config.Bind("simulation", "rarity type", Utils.CardRarity.LEGENDARY, "Specify random rarity（Based on random rarity）");
            fakeRandomPremium = config.Bind("simulation", "qualitytype", TAG_PREMIUM.GOLDEN, "designationquality（based onrandom quality）");

            fakeCatchupCount = config.Bind("simulation", "catch up packagecardquantity，less than5hour，Random quantity", -1, new ConfigDescription("Catchup card num", null, new object[] { "Advanced" }));
            fakeCardID1 = config.Bind("simulation", "card1", 71984, new ConfigDescription("Card 1 DbID.", null, new object[] { "Advanced" }));
            fakeCardPremium1 = config.Bind("simulation", "card1quality", TAG_PREMIUM.GOLDEN, new ConfigDescription("Card 1 Premium.", null, new object[] { "Advanced" }));
            fakeCardID2 = config.Bind("simulation", "card2", 71945, new ConfigDescription("Card 2 DbID.", null, new object[] { "Advanced" }));
            fakeCardPremium2 = config.Bind("simulation", "card2quality", TAG_PREMIUM.GOLDEN, new ConfigDescription("Card 2 Premium.", null, new object[] { "Advanced" }));
            fakeCardID3 = config.Bind("simulation", "card3", 73446, new ConfigDescription("Card 3 DbID.", null, new object[] { "Advanced" }));
            fakeCardPremium3 = config.Bind("simulation", "card3quality", TAG_PREMIUM.GOLDEN, new ConfigDescription("Card 3 Premium.", null, new object[] { "Advanced" }));
            fakeCardID4 = config.Bind("simulation", "card4", 71781, new ConfigDescription("Card 4 DbID.", null, new object[] { "Advanced" }));
            fakeCardPremium4 = config.Bind("simulation", "card4quality", TAG_PREMIUM.GOLDEN, new ConfigDescription("Card 4 Premium.", null, new object[] { "Advanced" }));
            fakeCardID5 = config.Bind("simulation", "card5", 67040, new ConfigDescription("Card 5 DbID.", null, new object[] { "Advanced" }));
            fakeCardPremium5 = config.Bind("simulation", "card5quality", TAG_PREMIUM.GOLDEN, new ConfigDescription("Card 5 Premium.", null, new object[] { "Advanced" }));

            InitCardsMapping();
            LoadSkinsConfigFromFile();
            ConfigValueDelegate();
            ConfigTemplateSettingChanged(configTemplate.Value);
            timeKeeper = DateTime.Now.Ticks;


            if (CommandConfig.hsMatchLogPath == string.Empty) CommandConfig.hsMatchLogPath = hsMatchLogPath.Value;
            if (CommandConfig.webServerPort == -1) CommandConfig.webServerPort = webServerPort.Value;

        }

        public static void ConfigValueDelegate()
        {
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
                    isIdleKickEnable.Value = true;
                    isQuickPackOpeningEnable.Value = true;
                    isAutoRefundCardDisenchantEnable.Value = true;
                    isAutoRecvMercenaryRewardEnable.Value = true;
                    isMercenaryBattleZoom.Value = false;
                    isSkipHeroIntro.Value = true;
                    isThinkEmotesEnable.Value = false;
                    receiveEnemyEmoteLimit.Value = 0;
                    isOpponentGoldenCardShow.Value = false;

                    skinCoin.Value = 1746;   // Initial lucky coins
                    isSkinDefalutHeroEnable.Value = true;

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
                    isIdleKickEnable.Value = false;
                    isQuickPackOpeningEnable.Value = true;
                    isAutoRefundCardDisenchantEnable.Value = false;
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

        public static void LoadSkinsConfigFromFile(string file = "BepInEx/config/HsSkins.cfg")
        {
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
                            if (!HeroesMapping.ContainsKey(int.Parse(parts[0])))
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
                string newConfigFile = "# skin map\n";
                newConfigFile += "# illustrate：Mainly used forhero（Tavern、Battle）kindskinreplace；pressF4meetingexistBepInExTable of contentsDownGenerate all currentskininformation；\n";
                newConfigFile += "# Format：original skin:Replacement skin（:as half-width characters）；The next line is an example(Malfurion·怒风replace成大导师Malfurion)，can delete\n";
                newConfigFile += "#      Also supportsa:b,c,dThis multi-value mapping，accomplishRandom skin。\n";
                newConfigFile += "274:57761\n\n";
                File.WriteAllText(file, newConfigFile);
            }
        }

        public static ConfigValue configValue = new ConfigValue();
    }



    //External Interface，
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
            get { return (DateTime.Now.Ticks - PluginConfig.timeKeeper) / 10000000; }    // Return seconds
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
