using BepInEx.Configuration;
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
        public static ConfigEntry<bool> isEndGameScreenShow;
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

        public static ConfigEntry<bool> isMoveEnemyCardsEnable;
        public static ConfigEntry<bool> isAutoReportEnable;
        public static ConfigEntry<bool> isFiresideGatheringEnable;
        public static ConfigEntry<double> firesideGatheringLatitude, firesideGatheringLongitude, firesideGatheringGpsAccuracy;

        public static ConfigEntry<bool> isAutoRecvMercenaryRewardEnable;
        public static ConfigEntry<bool> isMercenaryBattleZoom;
        public static ConfigEntry<Utils.CardState> mercenaryDiamondCardState;
        public static ConfigEntry<Utils.CardState> randomMercenarySkinEnable;

        public static ConfigEntry<bool> isShutUpBobEnable;
        public static ConfigEntry<bool> isBgsQuickModeEnable;

        public static ConfigEntry<bool> isOpponentGoldenCardShow;
        public static ConfigEntry<Utils.CardState> goldenCardState;
        public static ConfigEntry<Utils.CardState> diamondCardState;

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

        public static ConfigEntry<bool> isShowFPSEnable;
        public static ConfigEntry<bool> isInternalModeEnable;
        public static ConfigEntry<int> webServerPort;
        public static ConfigEntry<string> webPageBackImg;

        public static ConfigEntry<string> hsMatchLogPath;


        public static ConfigEntry<Utils.DevicePreset> fakeDevicePreset;
        public static ConfigEntry<OSCategory> fakeDeviceOs;
        public static ConfigEntry<ScreenCategory> fakeDeviceScreen;
        public static ConfigEntry<string> fakeDeviceName;

        public static ConfigEntry<int> fakePackCount;
        public static ConfigEntry<BoosterDbId> fakeBoosterDbId;
        public static ConfigEntry<bool> isFakeRandomResult;
        public static ConfigEntry<bool> isFakeRandomRarity;
        public static ConfigEntry<bool> isFakeRandomPremium;
        public static ConfigEntry<bool> isFakeRandomDiamond;
        public static ConfigEntry<TAG_PREMIUM> fakeRandomPremium;
        public static ConfigEntry<Utils.CardRarity> fakeRandomRarity;
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


        public static List<Utils.CardMapping> CardsMapping = new List<Utils.CardMapping>();    //卡片替换映射，目前暂未使用
        public static IGraphicsManager graphicsManager;
        public static void ConfigBind(ConfigFile config)
        {
            config.Clear();
            isPluginEnable = config.Bind("全局", "HsMod状态", false, "是否启用插件（修改该选项后建议重启炉石）");
            isFakeOpenEnable = config.Bind("全局", "模拟开包状态", false, "是否启用模拟开包（修改该选项后建议重启炉石，启用时可能会导致卡包信息统计异常）");
            configTemplate = config.Bind("全局", "设置模板", Utils.ConfigTemplate.DoNothing, "配置运行模板，当选项为DoNothing时，不修改任何配置。配置修改完成后自动替换回DoNothing");
            isShortcutsEnable = config.Bind("全局", "快捷键状态", false, "是否启用快捷键");
            isTimeGearEnable = config.Bind("全局", "变速齿轮状态", false, "是否启用变速齿轮");
            timeGear = config.Bind("全局", "变速倍率", 0, new ConfigDescription("变速齿轮倍速，1和0倍率相同，负数表示变慢", new AcceptableValueRange<int>(-8, 8)));
            targetFrameRate = config.Bind("全局", "游戏帧率", -1, new ConfigDescription("游戏帧率设置，-1表示不做修改", new AcceptableValueRange<int>(-1, 2333)));
            isDynamicFpsEnable = config.Bind("全局", "动态帧率", true, "是否启用动态帧率，修改游戏帧率时，建议禁用该选项");

            isIGMMessageShow = config.Bind("优化", "游戏内消息", true, "（牌店无法打开时，可以尝试关闭该选项）是否显示游戏内消息（广告推销、削弱补丁、天梯结算信息等）");
            isAlertPopupShow = config.Bind("优化", "弹出消息", true, "是否显示弹窗");
            responseAlertPopup = config.Bind("优化", "弹出响应", Utils.AlertPopupResponse.DONOTHING, "在屏蔽弹出消息时，如何回应弹窗");
            isOnApplicationFocus = config.Bind("优化", "应用焦点", true, "isOnApplicationFocus");
            isEndGameScreenShow = config.Bind("优化", "结算展示", true, "是否展示结算任务、升级提示（可能导致领取奖励时无提示）");
            isAutoOpenBoxesRewardEnable = config.Bind("优化", "自动开盒", false, "是否自动打开竞技场（对决、佣兵等）结算宝箱");
            isAutoExit = config.Bind("优化", "报错退出", false, "遇到错误是否自动退出");
            //isAutoRestart = config.Bind("优化", "退出时重启", false, "（可能无效）遇到错误是否自动重启");
            isShowCardLargeCount = config.Bind("优化", "收藏卡牌数量", false, "是否显示收藏卡牌数量大于等于10时的数量（选中时暂有Bug）");
            isShowCollectionCardIdEnable = config.Bind("优化", "显示卡牌ID", false, "是否在右键选择卡牌（皮肤）时，显示并复制所选内容的CardID");
            isShowRetireForever = config.Bind("优化", "显示放弃", false, "允许在0-0时放弃套牌");
            isIdleKickEnable = config.Bind("优化", "允许掉线", true, "（尚未测试）是否允许长时间无操作掉线（启动游戏时无法加载配置）");

            isQuickPackOpeningEnable = config.Bind("开包", "开包加速", false, "开包加速，使用空格开包时直接展示结果");
            isAutoPackOpeningEnable = config.Bind("开包", "自动开包", false, "（慎用，有BUG！）开完全部卡包，不区分卡包品类（基于开包加速）");
            isAutoRefundCardDisenchantEnable = config.Bind("开包", "自动分解", false, "是否在开包时自动分解全额反尘的卡");

            isAutoReportEnable = config.Bind("好友", "自动举报", false, "对局结束后自动举报对手昵称违规、作弊和脚本、恶意投降");
            isMoveEnemyCardsEnable = config.Bind("好友", "观战展示卡牌", false, "（尚未测试）在Ob中展示(旋转)对手手中的牌");
            isFiresideGatheringEnable = config.Bind("好友", "炉边聚会虚拟定位", false, "（尚未测试）是否启用炉边聚会虚拟定位");
            firesideGatheringLatitude = config.Bind("好友", "炉边聚会纬度", 0.0, "炉边聚会纬度");
            firesideGatheringLongitude = config.Bind("好友", "炉边聚会经度", 0.0, "炉边聚会经度");
            firesideGatheringGpsAccuracy = config.Bind("好友", "炉边聚会Gps定位精度", 54.0, "炉边聚会定位精度");

            isFullnameShow = config.Bind("炉石", "显示全名", false, "是否显示对手战网全名；如果启用该选项，还会允许添加当前对手(启动快捷键时，也允许添加对手)。");
            isOpponentRankInGameShow = config.Bind("炉石", "显示天梯等级", false, "是否在传说前显示对手天梯等级");
            isSkipHeroIntro = config.Bind("炉石", "跳过英雄介绍", false, "是否跳过英雄介绍(ShouldSkipMulligan)");
            isExtendedBMEnable = config.Bind("炉石", "表情无冷却", false, "是否允许无限制表情(延迟最低1.5s)");
            isThinkEmotesEnable = config.Bind("炉石", "思考表情", true, "是否允许显示思考表情");
            receiveEnemyEmoteLimit = config.Bind("炉石", "表情数量", -1, new ConfigDescription("游戏内表情数量接收限制，超过自动屏蔽对手表情，0时开局屏蔽，-1不限制（有小bug）", new AcceptableValueRange<int>(-1, 100)));
            isOpponentGoldenCardShow = config.Bind("炉石", "对手卡牌特效", true, "是否显示对手卡牌特效(覆盖ALL配置)");
            goldenCardState = config.Bind("炉石", "金卡特效", Utils.CardState.Default, "强制金卡特效");
            diamondCardState = config.Bind("炉石", "钻石卡特效", Utils.CardState.Default, "强制钻石卡特效");

            isAutoRecvMercenaryRewardEnable = config.Bind("佣兵", "自动领奖", false, "是否自动领取佣兵佣兵奖励（屏蔽宝箱）");
            isMercenaryBattleZoom = config.Bind("佣兵", "允许缩放", true, "（可能存在BUG）是否允许佣兵战斗时缩放画面");
            mercenaryDiamondCardState = config.Bind("佣兵", "钻石皮肤替换", Utils.CardState.Default, "如果可以，是否替换成钻石皮肤（优先级低于炉石-钻石卡特效）");
            randomMercenarySkinEnable = config.Bind("佣兵", "随机皮肤", Utils.CardState.Default, "随机皮肤（不包含钻皮且钻石特效值不能为disabled）");

            isBgsQuickModeEnable = config.Bind("酒馆", "酒馆快速战斗", false, "是否启用酒馆快速战斗模式");
            isShutUpBobEnable = config.Bind("酒馆", "沉默鲍勃", false, "是否让鲍勃闭嘴");
            //考虑导出单独配置
            skinCoin = config.Bind("皮肤", "硬币", -1, "幸运币的偏好ID，-1表示不做修改（游戏内模拟拔线可以实时更新）");
            skinCardBack = config.Bind("皮肤", "卡背", -1, "卡背的偏好ID，-1表示不做修改（实时生效）");
            skinBoard = config.Bind("皮肤", "对战面板", -1, "（测试功能，可能会在酒馆卡住），对战面板替换，-1表示不做修改）");
            skinBgsBoard = config.Bind("皮肤", "酒馆战斗面板", -1, "酒馆战斗面板的偏好ID，-1表示不做修改");
            skinBgsFinisher = config.Bind("皮肤", "酒馆击杀特效", -1, "酒馆击杀的偏好ID，-1表示不做修改");
            skinBob = config.Bind("皮肤", "鲍勃", -1, "鲍勃的偏好ID，-1表示不做修改");
            skinHero = config.Bind("皮肤", "英雄", -1, "（废弃，不建议使用，建议从文件加载英雄皮肤，修改完后F4更新；如果再对局中，则还需要模拟拔线）英雄的偏好ID，-1表示不做修改");

            keyTimeGearUp = config.Bind("快捷键", "齿轮倍率+1", new KeyboardShortcut(KeyCode.UpArrow), "齿轮倍率增加1，默认方向上");
            keyTimeGearDown = config.Bind("快捷键", "齿轮倍率-1", new KeyboardShortcut(KeyCode.DownArrow), "齿轮倍率减少1，默认方向下");
            keyTimeGearDefault = config.Bind("快捷键", "齿轮倍率归零", new KeyboardShortcut(KeyCode.LeftArrow), "恢复默认齿轮倍率，默认方向左");
            keyTimeGearMax = config.Bind("快捷键", "齿轮倍率最大", new KeyboardShortcut(KeyCode.RightArrow), "齿轮倍率小于4时变为4，大于4时变为8，默认方向右");
            keySimulateDisconnect = config.Bind("快捷键", "模拟拔线", new KeyboardShortcut(KeyCode.D, KeyCode.LeftControl), "模拟掉线重连，默认左Ctrl+D");
            keyCopyBattleTag = config.Bind("快捷键", "复制对手战网标签", new KeyboardShortcut(KeyCode.C, KeyCode.LeftControl), "复制游戏内对手战网ID，默认左Ctrl+C");
            keyCopySelectBattleTag = config.Bind("快捷键", "复制所选对手战网标签", new KeyboardShortcut(KeyCode.Mouse0), "复制酒馆内所选对手战网ID，默认鼠标左键");
            keyConcede = config.Bind("快捷键", "投降", new KeyboardShortcut(KeyCode.Space, KeyCode.LeftControl), "投降，默认左Ctrl+空格");
            keyContinueMulligan = config.Bind("快捷键", "结束回合", new KeyboardShortcut(KeyCode.Space), "结束回合或替换卡牌确认，默认空格");
            keySquelch = config.Bind("快捷键", "沉默对手", new KeyboardShortcut(KeyCode.Q, KeyCode.LeftControl), "屏蔽你对手的表情，默认左Ctrl+Q");
            keySoundMute = config.Bind("快捷键", "静音/恢复音量", new KeyboardShortcut(KeyCode.S, KeyCode.LeftControl), "静音/恢复音量，默认左Ctrl+S");
            keyShutUpBob = config.Bind("快捷键", "闭了，鲍勃", new KeyboardShortcut(KeyCode.B, KeyCode.LeftControl), "禁用/恢复鲍勃语音，默认左Ctrl+B");
            keyRefund = config.Bind("快捷键", "一键全额分解", new KeyboardShortcut(KeyCode.Z, KeyCode.LeftControl), "一键分解全额分解的卡牌（仅在开包界面与收藏界面有效），默认左Ctrl+Z");
            //keyRuin = config.Bind("快捷键", "毁灭吧，赶紧的", new KeyboardShortcut(KeyCode.R, KeyCode.LeftControl), "一键退坑，默认左Ctrl+R");
            keyReadNewCards = config.Bind("快捷键", "朕，已阅！", new KeyboardShortcut(KeyCode.R, KeyCode.LeftControl), "消除所有新！标记（仅在开包界面与收藏界面有效；佣兵模式（有bug，重启后失效）下，执行后可能需要重新进入收藏），默认左Ctrl+R");
            keyShowFPS = config.Bind("快捷键", "显示/隐藏帧率", new KeyboardShortcut(KeyCode.P, KeyCode.LeftControl), "展示或隐藏游戏帧率信息，默认左Ctrl+P");

            keyEmoteGreetings = config.Bind("快捷键", "问候", new KeyboardShortcut(KeyCode.Alpha1), "表情问候，默认数字键1");
            keyEmoteWellPlayed = config.Bind("快捷键", "称赞", new KeyboardShortcut(KeyCode.Alpha2), "表情称赞，默认数字键2");
            keyEmoteThanks = config.Bind("快捷键", "感谢", new KeyboardShortcut(KeyCode.Alpha3), "表情感谢，默认数字键3");
            keyEmoteWow = config.Bind("快捷键", "惊叹", new KeyboardShortcut(KeyCode.Alpha4), "表情惊叹，默认数字键4");
            keyEmoteOops = config.Bind("快捷键", "失误", new KeyboardShortcut(KeyCode.Alpha5), "表情失误，默认数字键5");
            keyEmoteThreaten = config.Bind("快捷键", "威胁", new KeyboardShortcut(KeyCode.Alpha6), "表情威胁，默认数字键6");

            isInternalModeEnable = config.Bind("开发", "内部模式", false, "是否切换到内部模式（需要重启炉石）");
            isShowFPSEnable = config.Bind("开发", "显示FPS", false, "是否显示FPS信息");
            buyAdventure = config.Bind("开发", "冒险购买", Utils.BuyAdventureTemplate.DoNothing, "（不建议购买卡拉赞）选择一个冒险进行购买尝试（有概率封号，酌情考虑使用）");
            isKarazhanFixEnable = config.Bind("开发", "卡拉赞修复", false, "（请打完后请关闭，目前无法打序章）卡拉赞黑鸦翱翔修复，也可以用作冒险跳关。（有概率封号，酌情考虑使用）");
            webServerPort = config.Bind("开发", "网站端口", 58744, new ConfigDescription("WebServer端口", new AcceptableValueRange<int>(1, 65535)));
            webPageBackImg = config.Bind("开发", "网页背景图", "", "网页背景图片");

            hsMatchLogPath = config.Bind("开发", "对局日志", @"BepInEx\HsMatch.log", "炉石对局日志文件位置（相对于Hearthstone）");

            fakeDevicePreset = config.Bind("模拟", "设备模拟模板", Utils.DevicePreset.Default, "（重启炉石后生效）模拟设备，用于领取卡包卡背");
            fakeDeviceOs = config.Bind("模拟", "设备模拟系统", OSCategory.PC, "模拟设备操作系统，当设备模拟模板为Custom时有效。");
            fakeDeviceScreen = config.Bind("模拟", "设备屏幕大小", ScreenCategory.PC, "模拟尺寸（屏幕类型），当设备模拟模板为Custom时有效。");
            fakeDeviceName = config.Bind("模拟", "设备设备型号", "HsMod", "模拟设备型号，当设备模拟模板为Custom时有效。");

            fakePackCount = config.Bind("模拟", "数量", 233, "模拟卡包数量");
            fakeBoosterDbId = config.Bind("模拟", "类型", BoosterDbId.GOLDEN_CLASSIC_PACK, "模拟卡包类型。(替换卡包图标)");
            isFakeRandomResult = config.Bind("模拟", "随机结果", false, "是否启用随机结果");
            isFakeRandomRarity = config.Bind("模拟", "随机稀有度", false, "是否随机稀有度（基于随机结果）");
            isFakeRandomPremium = config.Bind("模拟", "随机品质", false, "是否随机品质（基于随机结果）");
            isFakeRandomDiamond = config.Bind("模拟", "随机钻石", false, "随机品质中包括钻石（基于随机品质）");
            fakeRandomRarity = config.Bind("模拟", "稀有度类型", Utils.CardRarity.LEGENDARY, "指定随机稀有度（基于随机稀有度）");
            fakeRandomPremium = config.Bind("模拟", "品质类型", TAG_PREMIUM.GOLDEN, "指定品质（基于随机品质）");

            fakeCardID1 = config.Bind("模拟", "卡牌1", 71984, "Card 1 DbID.");
            fakeCardPremium1 = config.Bind("模拟", "卡牌1品质", TAG_PREMIUM.GOLDEN, "Card 1 Premium.");
            fakeCardID2 = config.Bind("模拟", "卡牌2", 71945, "Card 2 DbID.");
            fakeCardPremium2 = config.Bind("模拟", "卡牌2品质", TAG_PREMIUM.GOLDEN, "Card 2 Premium.");
            fakeCardID3 = config.Bind("模拟", "卡牌3", 73446, "Card 3 DbID.");
            fakeCardPremium3 = config.Bind("模拟", "卡牌3品质", TAG_PREMIUM.GOLDEN, "Card 3 Premium.");
            fakeCardID4 = config.Bind("模拟", "卡牌4", 71781, "Card 4 DbID.");
            fakeCardPremium4 = config.Bind("模拟", "卡牌4品质", TAG_PREMIUM.GOLDEN, "Card 4 Premium.");
            fakeCardID5 = config.Bind("模拟", "卡牌5", 67040, "Card 5 DbID.");
            fakeCardPremium5 = config.Bind("模拟", "卡牌5品质", TAG_PREMIUM.GOLDEN, "Card 5 Premium.");

            InitCardsMapping();
            LoadSkinsConfigFromFile();
            ConfigValueDelegate();
            ConfigTemplateSettingChanged(configTemplate.Value);
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
                    responseAlertPopup.Value = Utils.AlertPopupResponse.DONOTHING;
                    isOnApplicationFocus.Value = false;
                    isEndGameScreenShow.Value = false;
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
                    goldenCardState.Value = Utils.CardState.Disabled;
                    diamondCardState.Value = Utils.CardState.Disabled;
                    configTemplate.Value = Utils.ConfigTemplate.DoNothing;
                    return;
                case Utils.ConfigTemplate.AntiAwayFromKeyboard:
                    isShortcutsEnable.Value = true;
                    isIGMMessageShow.Value = true;
                    isAlertPopupShow.Value = true;
                    responseAlertPopup.Value = Utils.AlertPopupResponse.DONOTHING;
                    isOnApplicationFocus.Value = false;
                    isEndGameScreenShow.Value = false;
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
                    goldenCardState.Value = Utils.CardState.OnlyMy;
                    diamondCardState.Value = Utils.CardState.OnlyMy;
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

        public static void LoadSkinsConfigFromFile(string file = @"BepInEx\config\HsSkins.cfg")
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
                                HeroesMapping.Add(int.Parse(parts[0]), int.Parse(parts[1]));
                            }
                        }
                    }
                }
            }
            else
            {
                string newConfigFile = "# 皮肤映射表\n";
                newConfigFile += "# 说明：主要用作英雄（酒馆、对战）类皮肤替换；按下F4会在BepInEx目录下生成当前全部皮肤信息；\n";
                newConfigFile += "# 格式：原始皮肤:替换皮肤（:为半角字符）；下一行是一个样例(玛法里奥·怒风替换成大导师玛法里奥)，可以删除\n";
                newConfigFile += "274:57761\n\n";
                File.WriteAllText(file, newConfigFile);
            }
        }

        public static ConfigValue configValue = new ConfigValue();
    }



    //用作生成IL指令，似乎可以优化
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
        public bool IsFiresideGatheringEnableValue
        {
            get { return PluginConfig.isFiresideGatheringEnable.Value; }
            set { PluginConfig.isFiresideGatheringEnable.Value = value; }
        }
        public double FiresideGatheringLatitudeValue
        {
            get { return PluginConfig.firesideGatheringLatitude.Value; }
            set { PluginConfig.firesideGatheringLatitude.Value = value; }
        }
        public double FiresideGatheringLongitudeValue
        {
            get { return PluginConfig.firesideGatheringLongitude.Value; }
            set { PluginConfig.firesideGatheringLongitude.Value = value; }
        }
        public double FiresideGatheringGpsAccuracyValue
        {
            get { return PluginConfig.firesideGatheringGpsAccuracy.Value; }
            set { PluginConfig.firesideGatheringGpsAccuracy.Value = value; }
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


        public string CacheOpponentFullName
        {
            get { return Utils.CacheLastOpponentFullName; }
        }

        public static ConfigValue Get()
        {
            return PluginConfig.configValue;
        }

    }
}
