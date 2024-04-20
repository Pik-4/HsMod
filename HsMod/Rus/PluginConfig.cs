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
        //public static ConfigEntry<KeyboardShortcut> keyRuin;    //Уничтожьте этоторопиться
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
        public static ConfigEntry<long> autoQuitTimer;    // 定час退出

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

        public static List<Utils.CardMapping> CardsMapping = new List<Utils.CardMapping>();    //Сопоставление замены карты，В настоящее время не используется
        public static IGraphicsManager graphicsManager;
        public static void ConfigBind(ConfigFile config)
        {
            config.Clear();
            isPluginEnable = config.Bind("общая ситуация", "HsModсостояние", false, "Включать ли плагины（修改该选项后建议重启домашний очаг）");
            configTemplate = config.Bind("общая ситуация", "Установить шаблон", Utils.ConfigTemplate.DoNothing, "Configuration运行模板，Когда есть вариантDoNothingчас，不修改任何Configuration。Автоматически заменять конфигурацию после завершения изменения конфигурации.DoNothing");
            isShortcutsEnable = config.Bind("общая ситуация", "быстрая клавишасостояние", false, "是否启用быстрая клавиша");
            isTimeGearEnable = config.Bind("общая ситуация", "Статус трансмиссии", false, "Включить ли передачу");
            timeGear = config.Bind("общая ситуация", "Переменное передаточное число", 0, new ConfigDescription("Переключатель скоростей，1и0То же увеличение，Отрицательные числа означают медленнее", new AcceptableValueRange<int>(-32, 32)));
            isShowFPSEnable = config.Bind("общая ситуация", "показыватьFPS", false, "是否показыватьFPSInformation（быстрая клавиша：левыйCrtl+P）");
            targetFrameRate = config.Bind("общая ситуация", "Game frame rate", -1, new ConfigDescription("Настройки частоты кадров в игре，-1Указывает, что никаких изменений не будет.或恢复默认值（Значение по умолчанию может быть30）", new AcceptableValueRange<int>(-1, 2333)));

            isIGMMessageShow = config.Bind("оптимизация", "Внутриигровые новости", true, "（牌店无法打开час，Вы можете попробовать включить эту опцию）Отображать ли внутриигровые сообщения（рекламное продвижение、нерф патч、天梯结算Information等）");
            isAlertPopupShow = config.Bind("оптимизация", "всплывающее сообщение", true, "Отображать ли всплывающее окно");
            responseAlertPopup = config.Bind("оптимизация", "всплывающий ответ", Utils.AlertPopupResponse.DONOTHING, "существовать屏蔽всплывающее сообщениечас，Как реагировать на всплывающие окна");
            isOnApplicationFocus = config.Bind("оптимизация", "Фокус приложения", true, "isOnApplicationFocus");
            isRewardToastShow = config.Bind("оптимизация", "Отображение населенного пункта", true, "Отображать ли задачи расчета、Награды за достижения、Советы по обновлению и т. д.（Это может привести к тому, что при получении вознаграждения не будет подсказки.）");
            isAutoOpenBoxesRewardEnable = config.Bind("оптимизация", "Автоматическое открытие ящика", false, "Стоит ли автоматически открывать арену（дуэль、Наемники и т.д.）Расчетный сундук");
            isAutoExit = config.Bind("оптимизация", "Выйти с ошибкой", false, "Следует ли автоматически завершать работу при возникновении ошибки");
            //isAutoRestart = config.Bind("оптимизация", "退出час重启", false, "（Может быть недействительным）Следует ли автоматически перезапускать компьютер при возникновении ошибки");
            isShowCardLargeCount = config.Bind("оптимизация", "收藏Cardsколичество", false, "是否показывать收藏Cardsколичествобольше, чем等于10Количество часов（Временно доступно при выбореBug）");
            isShowCollectionCardIdEnable = config.Bind("оптимизация", "показыватьCardsID", false, "是否существовать右键选择Cards（Skin）час，показывать并复制所选内容的CardID");
            isBypassDeckShareCodeCheckEnable = config.Bind("оптимизация", "Обнаружение кода совместного использования группы карт", false, "Удалить ли обнаружение кода совместного использования группы карт");
            isShowRetireForever = config.Bind("оптимизация", "показать, что сдаюсь", false, "允许существовать0-0час放弃套牌");
            isIdleKickEnable = config.Bind("оптимизация", "Разрешить отключение", true, "（尚未Translation: Test）是否允许长час间无操作掉线（启动游戏час无法加载Configuration）");


            isQuickPackOpeningEnable = config.Bind("Распаковать", "Ускорить открытие упаковки", false, "Ускорить открытие упаковки，Отображение результата напрямую при использовании пробелов для открытия пакета.");
            isAutoPackOpeningEnable = config.Bind("Распаковать", "Автоматически открыть пакет", false, "（Используйте с осторожностью，иметьBUG！）Открыть все наборы карт，Нет различия между категориями карточных пакетов.（基于Ускорить открытие упаковки）");
            isAutoRefundCardDisenchantEnable = config.Bind("Распаковать", "Автоматическое разложение", false, "是否существоватьРаспаковатьчасАвтоматическое разложение全额反尘的卡");

            isAutoReportEnable = config.Bind("друзья", "Автоматическая отчетность", false, "Автоматически сообщать о нарушении никнейма соперника после окончания игры、Читы и скрипты、Злонамеренная капитуляция");
            // isAutoReportEnable = config.Bind("друзья", "Автоматическая отчетность", true, new ConfigDescription("Автоматически сообщать о нарушении никнейма соперника после окончания игры、Читы и скрипты、Злонамеренная капитуляция", null, new object[] { "Advanced" }));
            isMoveEnemyCardsEnable = config.Bind("друзья", "Следите за игровыми карточками", false, "（尚未Translation: Test）существоватьObОтображать в(вращать)карты противника");


            isQuickModeEnable = config.Bind("домашний очаг", "Быстрый бой", false, "Включить ли таверны или наемниковAIРежим быстрого боя");
            isFullnameShow = config.Bind("домашний очаг", "показывать全名", false, "是否показывать对手战网全名；Если эта опция включена，Также позволяет добавлять текущих противников.(启动быстрая клавишачас，Также позволяет добавлять противников)。");
            isOpponentRankInGameShow = config.Bind("домашний очаг", "Показать уровень лестницы", false, "是否существовать传说前показывать对手天梯等级");
            isCardTrackerEnable = config.Bind("домашний очаг", "Cards追踪", false, "推测对手Cards，и давать подсказки（For example.：Решение и т. д.，Существует вероятность ошибки распознавания）");
            isCardRevealedEnable = config.Bind("домашний очаг", "Cards揭示", false, "Раскройте известные карты как лицевые карты.（иметь概率导致домашний очаг自动断线重连）");
            isSkipHeroIntro = config.Bind("домашний очаг", "Пропустить представление героя", false, "Пропускать ли представление героя(ShouldSkipMulligan)");
            isExtendedBMEnable = config.Bind("домашний очаг", "Сцеживание без охлаждения", false, "Разрешить ли неограниченное количество смайлов(самая низкая задержка1.5s)");
            isThinkEmotesEnable = config.Bind("домашний очаг", "выражение мышления", true, "是否允许показыватьвыражение мышления");
            receiveEnemyEmoteLimit = config.Bind("домашний очаг", "表情количество", -1, new ConfigDescription("游戏内表情количество接收限制，Больше, чем просто автоматическая блокировка выражений оппонента，0час开局屏蔽，-1не ограничен（Есть маленькиеbug）", new AcceptableValueRange<int>(-1, 100)));
            isOpponentGoldenCardShow = config.Bind("домашний очаг", "Эффекты карт противника", true, "是否показыватьЭффекты карт противника(крышкаALLConfiguration)");
            isSignatureCardStateEnable = config.Bind("домашний очаг", "Различные спецэффекты рисования", true, "是否существоватьВысочайшие спецэффекты карт中показыватьСтранные картины（仅影响Высочайшие спецэффекты карт）");
            goldenCardState = config.Bind("домашний очаг", "золотая карта特效", Utils.CardState.Default, "强制золотая карта特效");
            maxCardState = config.Bind("домашний очаг", "Высочайшие спецэффекты карт", Utils.CardState.Default, "强制Высочайшие спецэффекты карт特效（Текущий приоритет：алмаз、Странные картины、золотая карта、обычно）");

            isAutoRecvMercenaryRewardEnable = config.Bind("наемник", "Автоматически собирайте призы", false, "Нужно ли автоматически получать награды наемникам（Сундук с сокровищами щита）");
            isMercenaryBattleZoom = config.Bind("наемник", "Разрешить масштабирование", true, "（可能存существоватьBUG）Разрешить ли наемникам увеличивать и уменьшать масштаб во время сражений");
            mercenaryDiamondCardState = config.Bind("наемник", "алмазSkin替换", Utils.CardState.Default, "если это возможно，是否替换成алмазSkin（优先级低于домашний очаг-Высочайшие спецэффекты карт）");
            randomMercenarySkinEnable = config.Bind("наемник", "随机Skin", Utils.CardState.Default, "随机Skin（Не включает алмазный скин и камень возвращения.-алмаз卡特效值不能为disabled）");

            isShutUpBobEnable = config.Bind("Таверна", "沉默Боб", false, "是否让Боб闭嘴");
            isBgsGoldenEnable = config.Bind("Таверна", "Таверна镀金", false, "（Translation: Test，需要существоватьдомашний очагCards特效开启золотая карта特效）是否镀金Таверна。该镀金不会镀金随从и任务线。");
            //考虑导出单独Configuration
            skinCoin = config.Bind("Skin", "монета", -1, "Предпочтение счастливой монетыID，-1Указывает, что никаких изменений не будет.（Имитацию отключения в игре можно обновлять в режиме реального времени.）");
            skinCardBack = config.Bind("Skin", "рубашка назад", -1, "Предпочтение рубашки картыID，-1Указывает, что никаких изменений не будет.（实час生效）");
            skinBoard = config.Bind("Skin", "Battle面板", -1, "（Translation: Test功能，可能Будет вТаверна卡住），Замена боевой панели，-1Указывает, что никаких изменений не будет.）");
            skinBgsBoard = config.Bind("Skin", "Боевая панель таверны", -1, "Боевая панель таверны的偏好ID，-1Указывает, что никаких изменений не будет.");
            skinBgsFinisher = config.Bind("Skin", "Эффекты убийства в таверне", -1, "Предпочтения убийства в тавернеID，-1Указывает, что никаких изменений не будет.");
            skinBob = config.Bind("Skin", "Боб", -1, "Боб的偏好ID，-1Указывает, что никаких изменений не будет.");
            isSkinDefalutHeroEnable = config.Bind("Skin", "默认герой", false, "если это возможно，将BattleгеройSkin都替换为默认Skin");
            skinHero = config.Bind("Skin", "герой", -1, "（Используйте с осторожностью，Не рекомендуется использовать без крючка.，Более низкий приоритет, чем у героя по умолчанию。Обычно рекомендуется загружать скины героев из файлов.，После модификацииF4возобновлять；Если вы снова участвуете в матче，则还需要Имитировать отключение）предпочтение герояID，-1Указывает, что никаких изменений не будет.");
            skinOpposingHero = config.Bind("Skin", "Противник-герой", -1, "（Используйте с осторожностью，Не рекомендуется использовать без крючка.，Более низкий приоритет, чем у героя по умолчанию）Противник-герой的偏好ID，-1Указывает, что никаких изменений не будет.");

            keyTimeGearUp = config.Bind("быстрая клавиша", "Увеличение шестерни+1", new KeyboardShortcut(KeyCode.UpArrow), "Увеличенное передаточное число1，в направлении по умолчанию");
            keyTimeGearDown = config.Bind("быстрая клавиша", "Увеличение шестерни-1", new KeyboardShortcut(KeyCode.DownArrow), "Увеличение шестерни减少1，В ориентации по умолчанию");
            keyTimeGearDefault = config.Bind("быстрая клавиша", "Увеличение шестерни сброшено на ноль.", new KeyboardShortcut(KeyCode.LeftArrow), "Восстановить передаточное число по умолчанию，Направление по умолчанию влево");
            keyTimeGearMax = config.Bind("быстрая клавиша", "Максимальное передаточное число", new KeyboardShortcut(KeyCode.RightArrow), "Увеличение зубчатого колеса меньше4время становится4，больше, чем4время становится8，Направление по умолчанию вправо");
            keySimulateDisconnect = config.Bind("быстрая клавиша", "Имитировать отключение", new KeyboardShortcut(KeyCode.D, KeyCode.LeftControl), "моделирование掉线重连，注意需要ЗапрещатьВыйти с ошибкой并允许всплывающее сообщение，默认левыйCtrl+D");
            keyCopyBattleTag = config.Bind("быстрая клавиша", "Скопируйте тег Battle.net противника.", new KeyboardShortcut(KeyCode.C, KeyCode.LeftControl), "Копирование внутриигровой боевой сети соперниковID，默认левыйCtrl+C");
            keyCopySelectBattleTag = config.Bind("быстрая клавиша", "Скопировать тег выбранного противника Battle.net", new KeyboardShortcut(KeyCode.Mouse0), "Скопируйте боевую сеть выбранного противника в таверне.ID，默认鼠标левый键");
            keyConcede = config.Bind("быстрая клавиша", "сдаваться", new KeyboardShortcut(KeyCode.Space, KeyCode.LeftControl), "сдаваться，默认левыйCtrl+космос");
            keyContinueMulligan = config.Bind("быстрая клавиша", "Конечный поворот", new KeyboardShortcut(KeyCode.Space), "Конечный поворот或替换Cards确认，Пространство по умолчанию");
            keySquelch = config.Bind("быстрая клавиша", "заставить оппонентов замолчать", new KeyboardShortcut(KeyCode.Q, KeyCode.LeftControl), "Блокируйте выражение лица вашего противника，默认левыйCtrl+Q");
            keySoundMute = config.Bind("быстрая клавиша", "немой/восстановить громкость", new KeyboardShortcut(KeyCode.S, KeyCode.LeftControl), "немой/восстановить громкость，默认левыйCtrl+S");
            keyShutUpBob = config.Bind("быстрая клавиша", "закрыто，Боб", new KeyboardShortcut(KeyCode.B, KeyCode.LeftControl), "Запрещать/恢复Боб语音，默认левыйCtrl+B");
            keyRefund = config.Bind("быстрая клавиша", "Полная декомпозиция в один клик", new KeyboardShortcut(KeyCode.Z, KeyCode.LeftControl), "Разложите полностью разложенные карты одним щелчком мыши（仅существоватьРаспаковать界面与收藏界面иметь效），默认левыйCtrl+Z");
            //keyRuin = config.Bind("быстрая клавиша", "Уничтожьте это，торопиться", new KeyboardShortcut(KeyCode.R, KeyCode.LeftControl), "Выход в один клик，默认левыйCtrl+R");
            keyReadNewCards = config.Bind("быстрая клавиша", "ERROR，Читать！", new KeyboardShortcut(KeyCode.R, KeyCode.LeftControl), "Устраните все новое！отметка（仅существоватьРаспаковать界面与收藏界面иметь效；Режим наемника（иметьbug，Недействительно после перезапуска）Вниз，Возможно, вам придется повторно ввести коллекцию после выполнения.），默认левыйCtrl+R");
            keyShowFPS = config.Bind("быстрая клавиша", "показывать/Скрыть частоту кадров", new KeyboardShortcut(KeyCode.P, KeyCode.LeftControl), "Показать или скрыть информацию о частоте кадров в игре，默认левыйCtrl+P");

            keyEmoteGreetings = config.Bind("быстрая клавиша", "приветствие", new KeyboardShortcut(KeyCode.Alpha1), "смайлик приветствие，Цифровые клавиши по умолчанию1");
            keyEmoteWellPlayed = config.Bind("быстрая клавиша", "хвалить", new KeyboardShortcut(KeyCode.Alpha2), "Выражение похвалы，Цифровые клавиши по умолчанию2");
            keyEmoteThanks = config.Bind("быстрая клавиша", "благодарный", new KeyboardShortcut(KeyCode.Alpha3), "表情благодарный，Цифровые клавиши по умолчанию3");
            keyEmoteWow = config.Bind("быстрая клавиша", "удивляться", new KeyboardShortcut(KeyCode.Alpha4), "表情удивляться，Цифровые клавиши по умолчанию4");
            keyEmoteOops = config.Bind("быстрая клавиша", "Ошибка", new KeyboardShortcut(KeyCode.Alpha5), "Ошибка выражения，Цифровые клавиши по умолчанию5");
            keyEmoteThreaten = config.Bind("быстрая клавиша", "угрожать", new KeyboardShortcut(KeyCode.Alpha6), "угрожающее выражение，Цифровые клавиши по умолчанию6");

            hsLogPath = config.Bind("развивать", "Журнал Hearthstone", "", new ConfigDescription("домашний очаг进程日志文件位置（относительноHearthstone）", null, new object[] { "Advanced" }));
            hsMatchLogPath = config.Bind("развивать", "Журнал игры", @"BepInEx/HsMatch.log", "домашний очагЖурнал игры文件位置（относительноHearthstone），Параметры сначала выбираются из командной строки.");
            autoQuitTimer = config.Bind("развивать", "定час退出", (long)0, "Когда игра запущенаxсекунды спустя（существовать对局结束час）Автоматический вывод，x<=0Эта опция недействительна, если。");
            isFakeOpenEnable = config.Bind("развивать", "Имитировать статус открытия посылки", false, "Включить ли имитацию открытия упаковки（修改该选项后建议重启домашний очаг，启用час可能会导致卡包Information统计异常）");
            buyAdventure = config.Bind("развивать", "рискованная покупка", Utils.BuyAdventureTemplate.DoNothing, "（Не рекомендуется покупать Каражан.）Выберите приключение, чтобы попробовать его при покупке（Возможность бана аккаунта，Рассмотрите возможность использования по мере необходимости）");
            isKarazhanFixEnable = config.Bind("развивать", "Ремонт Каражана", false, "（Пожалуйста, закройте после ввода，На данный момент не могу написать пролог）Ремонт восхождения Каражана Ворона，Также может использоваться в качестве уровня приключенческого прыжка.。（Возможность бана аккаунта，Рассмотрите возможность использования по мере необходимости）");
            webServerPort = config.Bind("развивать", "порт веб-сайта", 58744, new ConfigDescription("WebServerпорт，Параметры сначала выбираются из командной строки.", new AcceptableValueRange<int>(1, 65535)));
            webPageBackImg = config.Bind("развивать", "Фоновое изображение веб-страницы", "", new ConfigDescription("Фоновое изображение веб-страницы片", null, new object[] { "Advanced" }));
            isWebshellEnable = config.Bind("развивать", "Webshell", false, "Webshellвыключатель");
            isInternalModeEnable = config.Bind("развивать", "внутренний режим", false, "是否切换到внутренний режим（Нужно перезапустить Hearthstone）");

            fakeDevicePreset = config.Bind("моделирование", "Шаблон моделирования устройства", Utils.DevicePreset.Default, "（重启домашний очаг后生效）Аналоговое устройство，Используется для получения обратной стороны карты.");
            fakeDeviceOs = config.Bind("моделирование", "Система моделирования оборудования", OSCategory.PC, "Имитировать операционную систему устройства，当Шаблон моделирования устройства为Customдействителен, когда。");
            fakeDeviceScreen = config.Bind("моделирование", "Размер экрана устройства", ScreenCategory.PC, "Аналоговый размер（屏幕тип），当Шаблон моделирования устройства为Customдействителен, когда。");
            fakeDeviceName = config.Bind("моделирование", "Модель оборудования оборудования", "HsMod", "Модель аналогового устройства，当Шаблон моделирования устройства为Customдействителен, когда。");

            fakePackCount = config.Bind("моделирование", "количество", 233, "Количество комплектов симуляционных карточек");
            fakeBoosterDbId = config.Bind("моделирование", "тип", BoosterDbId.GOLDEN_CLASSIC_PACK, "моделирование卡包тип。(Заменить значок пакета карт)");
            isFakeRandomResult = config.Bind("моделирование", "Случайные результаты", false, "Включить ли случайные результаты");
            isFakeRandomRarity = config.Bind("моделирование", "随机稀иметь度", false, "Стоит ли рандомизировать редкость（На основе случайных результатов）");
            isFakeRandomPremium = config.Bind("моделирование", "随机качество", false, "是否随机качество（На основе случайных результатов）");
            isFakeAtypicalRandomPremium = config.Bind("моделирование", "Другие случайные спецэффекты", false, "Случайное качество включает бриллианты или экзотические картины и т. д.（基于随机качество）");
            fakeRandomRarity = config.Bind("моделирование", "稀иметь度тип", Utils.CardRarity.LEGENDARY, "Укажите случайную редкость（На основе случайной редкости）");
            fakeRandomPremium = config.Bind("моделирование", "качествотип", TAG_PREMIUM.GOLDEN, "指定качество（基于随机качество）");

            fakeCatchupCount = config.Bind("моделирование", "追赶包Cardsколичество，меньше, чем5час，Случайное количество", -1, new ConfigDescription("Catchup card num", null, new object[] { "Advanced" }));
            fakeCardID1 = config.Bind("моделирование", "Cards1", 71984, new ConfigDescription("Card 1 DbID.", null, new object[] { "Advanced" }));
            fakeCardPremium1 = config.Bind("моделирование", "Cards1качество", TAG_PREMIUM.GOLDEN, new ConfigDescription("Card 1 Premium.", null, new object[] { "Advanced" }));
            fakeCardID2 = config.Bind("моделирование", "Cards2", 71945, new ConfigDescription("Card 2 DbID.", null, new object[] { "Advanced" }));
            fakeCardPremium2 = config.Bind("моделирование", "Cards2качество", TAG_PREMIUM.GOLDEN, new ConfigDescription("Card 2 Premium.", null, new object[] { "Advanced" }));
            fakeCardID3 = config.Bind("моделирование", "Cards3", 73446, new ConfigDescription("Card 3 DbID.", null, new object[] { "Advanced" }));
            fakeCardPremium3 = config.Bind("моделирование", "Cards3качество", TAG_PREMIUM.GOLDEN, new ConfigDescription("Card 3 Premium.", null, new object[] { "Advanced" }));
            fakeCardID4 = config.Bind("моделирование", "Cards4", 71781, new ConfigDescription("Card 4 DbID.", null, new object[] { "Advanced" }));
            fakeCardPremium4 = config.Bind("моделирование", "Cards4качество", TAG_PREMIUM.GOLDEN, new ConfigDescription("Card 4 Premium.", null, new object[] { "Advanced" }));
            fakeCardID5 = config.Bind("моделирование", "Cards5", 67040, new ConfigDescription("Card 5 DbID.", null, new object[] { "Advanced" }));
            fakeCardPremium5 = config.Bind("моделирование", "Cards5качество", TAG_PREMIUM.GOLDEN, new ConfigDescription("Card 5 Premium.", null, new object[] { "Advanced" }));

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
                    skinCoin.Value = 1746;   // Начальные счастливые монеты
                    isSkinDefalutHeroEnable.Value = true;
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
                string newConfigFile = "# карта кожи\n";
                newConfigFile += "# иллюстрировать：В основном используется как герой（Таверна、Battle）类Skin替换；PressF4Будет вBepInEx目录Вниз生成当前全部SkinInformation；\n";
                newConfigFile += "# Формат：原始Skin:Замена скина（:как символы половинной ширины）；Следующая строка является примером(Малфурион·Ярость Бури заменена Великим Магистром Малфурионом.)，могу удалить\n";
                newConfigFile += "#      Также поддерживаетa:b,c,dЭто многозначное отображение，Реализовать случайный скин。\n";
                newConfigFile += "274:57761\n\n";
                File.WriteAllText(file, newConfigFile);
            }
        }

        public static ConfigValue configValue = new ConfigValue();
    }



    //Внешний интерфейс，
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
            get { return (DateTime.Now.Ticks - PluginConfig.timeKeeper) / 10000000; }    // Вернуть секунды
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
