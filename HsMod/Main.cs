using BepInEx;
using System;
using System.Linq;
using UnityEngine;
using static HsMod.PluginConfig;


namespace HsMod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // 处理命令行参数
            string hsUnitID = "";
            if (UtilsArgu.Instance.Exists("hsunitid"))
                hsUnitID = UtilsArgu.Instance.Single("hsunitid");
            if (hsUnitID.Length <= 0)
                ConfigBind(base.Config);
            else
                ConfigBind(new BepInEx.Configuration.ConfigFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BepInEx/config", hsUnitID, PluginInfo.PLUGIN_GUID + ".cfg"), false,
                    new BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)));

            if (UtilsArgu.Instance.Exists("port"))
                if (int.TryParse(UtilsArgu.Instance.Single("port"), out int port))
                    if (port > 0 && port < 65535)
                        CommandConfig.webServerPort = port;

            if (UtilsArgu.Instance.Exists("width"))
                if (int.TryParse(UtilsArgu.Instance.Single("width"), out int width))
                    if (width > 0 && width < 65535)
                        CommandConfig.width = width;

            if (UtilsArgu.Instance.Exists("height"))
                if (int.TryParse(UtilsArgu.Instance.Single("height"), out int height))
                    if (height > 0 && height < 65535)
                        CommandConfig.height = height;

            if (UtilsArgu.Instance.Exists("matchPath")) CommandConfig.hsMatchLogPath = UtilsArgu.Instance.Single("matchPath");

            if (UtilsArgu.Instance.Exists("afk"))
                if (int.TryParse(UtilsArgu.Instance.Single("afk"), out int afk))
                {
                    timeGear.Value = 0;    //齿轮置零
                    if (afk == 1)
                    {
                        isPluginEnable.Value = true;    //启用插件
                        isTimeGearEnable.Value = true;  //启用齿轮
                        configTemplate.Value = Utils.ConfigTemplate.AwayFromKeyboard;    //设置挂机模板
                    }
                    else if (afk == 0)
                    {
                        isPluginEnable.Value = false;
                        isTimeGearEnable.Value = false;
                    }
                    else if (afk == 2)
                    {
                        configTemplate.Value = Utils.ConfigTemplate.AntiAwayFromKeyboard;
                        isShortcutsEnable.Value = false;
                    }
                    else if (afk == 3)
                    {
                        isPluginEnable.Value = true;
                        isTimeGearEnable.Value = false;
                    }
                }

            // 处理插件状态
            //Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            if (isPluginEnable.Value)
            {
                Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
                PatchManager.PatchSettingDelegate();
                PatchManager.PatchAll();
            }
            else
            {
                OnDestroy();
                return;
            }
        }

        private void Start()
        {
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is started!");
            if (!isPluginEnable.Value)
            {
                OnDestroy();
                return;
            }
            //暂时无法移动showFPS相关代码
            showFPS = new GameObject("ShowFPSSceneObject", new Type[] { typeof(HSDontDestroyOnLoad) }).AddComponent<ShowFPS>();
            showFPS.enabled = false;
            showFPS.StartFrameCount();
            showFPS.StopFrameCount();
            showFPS.ClearFrameCount();
            isShowFPSEnable.SettingChanged += delegate
            {
                showFPS.enabled = isShowFPSEnable.Value;
            };
            if (isShowFPSEnable.Value)
            {
                showFPS.enabled = true;
            }
            if (targetFrameRate.Value > 0 && Options.Get().GetInt(Option.GFX_TARGET_FRAME_RATE) != targetFrameRate.Value)
            {
                graphicsManager = Blizzard.T5.Services.ServiceManager.Get<IGraphicsManager>();
                graphicsManager.UpdateTargetFramerate(targetFrameRate.Value, isDynamicFpsEnable.Value);
            }

            //设置命令行的分辨率 位于patch之后，防止炉石自动修改
            if (CommandConfig.width > 0 && CommandConfig.height > 0)
            {
                Screen.SetResolution(CommandConfig.width, CommandConfig.height, false);
            }

            //启动web服务
            WebServer.Start();
            //InactivePlayerKicker.Get().SetShouldCheckForInactivity(isIdleKickEnable.Value);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F4))
            {
                int allPatchNum = 0;
                foreach (var tempatch in PatchManager.AllHarmony)
                {
                    allPatchNum += tempatch.GetPatchedMethods().Count();
                }
                LoadSkinsConfigFromFile();
                UIStatus.Get().AddInfo($"[{allPatchNum}]插件状态：" + (isPluginEnable.Value ? "运行" : "停止"));
                InactivePlayerKicker.Get().SetShouldCheckForInactivity(isIdleKickEnable.Value);
                WebServer.Restart();
            }

            if (!isPluginEnable.Value) return;
            if (!isShortcutsEnable.Value || !Input.anyKey) return;
            else
            {
                if (keyTimeGearUp.Value.IsDown())
                {
                    if (timeGear.Value == 8) return;
                    if (timeGear.Value <= -2 || timeGear.Value >= 2) timeGear.Value += 1;
                    else timeGear.Value = 2;
                    return;
                }
                else if (keyTimeGearDown.Value.IsDown())
                {
                    if (timeGear.Value == -8) return;
                    if (timeGear.Value <= -2 || timeGear.Value >= 2) timeGear.Value -= 1;
                    else timeGear.Value = -2;
                    return;
                }
                else if (keyTimeGearMax.Value.IsDown())
                {
                    timeGear.Value = timeGear.Value >= 4 ? 8 : 4;
                    return;
                }
                else if (keyTimeGearDefault.Value.IsDown())
                {
                    timeGear.Value = 0;
                    return;
                }
                else if (keySimulateDisconnect.Value.IsDown())
                {
                    Network.Get().SimulateUncleanDisconnectFromGameServer();
                    return;
                }
                else if (keyShowFPS.Value.IsDown())
                {
                    isShowFPSEnable.Value = !isShowFPSEnable.Value;
                    return;
                }
                else if (SoundManager.Get() != null && keySoundMute.Value.IsDown())
                {
                    SoundManagerPatch.OnMuteKeyPressed();
                    return;
                }
                else
                {
                    if (keyReadNewCards.Value.IsDown())
                    {
                        Utils.TryReadNewCards();
                    }
                    if (keyRefund.Value.IsDown()
                        && (SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
                        && (
                            (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
                            || (SceneMgr.Get().GetMode() == SceneMgr.Mode.PACKOPENING)
                        ))
                    {
                        Utils.LeakInfo.MyCards();
                        Utils.TryRefundCardDisenchant();
                        return;
                    }

                    if (GameState.Get() == null || GameMgr.Get() == null) return;
                    if (GameMgr.Get().IsBattlegrounds() && keyShutUpBob.Value.IsDown())
                    {
                        isShutUpBobEnable.Value = !isShutUpBobEnable.Value;
                        return;
                    }
                    else
                    {
                        if (!GameState.Get().IsGameCreated())
                        {
                            return;
                        }
                        if (!GameMgr.Get().IsSpectator())
                        {
                            if (keyConcede.Value.IsDown())
                            {
                                GameState.Get().Concede();
                                return;
                            }
                            if (GameState.Get().IsMulliganManagerActive() && MulliganManager.Get().GetMulliganButton() != null && keyContinueMulligan.Value.IsDown())
                            {
                                MulliganManager.Get().AutomaticContinueMulligan();
                                return;
                            }
                        }
                        if (GameMgr.Get().IsBattlegrounds() && keyCopySelectBattleTag.Value.IsDown() && PlayerLeaderboardManager.Get() != null && PlayerLeaderboardManager.Get().IsMousedOver())
                        {
                            BnetPlayer selectedOpponent = PlayerLeaderboardManager.Get().GetSelectedOpponent();
                            if (selectedOpponent != null)
                            {
                                BnetBattleTag battleTag = selectedOpponent.GetBattleTag();
                                if (battleTag != null)
                                {
                                    string @battleTagString = battleTag.GetString();
                                    ClipboardUtils.CopyToClipboard(@battleTagString);
                                    UIStatus.Get().AddInfo(@battleTagString);
                                    return;
                                }
                            }
                        }
                        else if (keyCopyBattleTag.Value.IsDown())
                        {
                            BnetPlayer bnetPlayer = null;
                            if (GameMgr.Get().IsBattlegrounds())
                            {
                                if (PlayerLeaderboardManager.Get() != null)
                                {
                                    bnetPlayer = PlayerLeaderboardManager.Get()?.GetCurrentOpponent();
                                }
                            }
                            else if (FriendMgr.Get() != null)
                            {
                                bnetPlayer = FriendMgr.Get()?.GetCurrentOpponent();
                            }
                            try
                            {
                                BnetBattleTag bnetTag = bnetPlayer?.GetBattleTag();
                                if (bnetTag != null)
                                {
                                    ClipboardUtils.CopyToClipboard(bnetTag.GetString());
                                    UIStatus.Get().AddInfo(bnetTag.GetString());
                                }
                                else if (!GameMgr.Get().IsBattlegrounds())
                                {
                                    bnetPlayer = BnetPresenceMgr.Get().GetPlayer(GameState.Get().GetOpposingSidePlayer().GetGameAccountId());
                                    string tempFullName = bnetPlayer?.GetBattleTag()?.ToString();
                                    if (tempFullName != null)
                                    {
                                        tempFullName = Utils.CacheLastOpponentFullName.StartsWith(tempFullName) ? Utils.CacheLastOpponentFullName : tempFullName;
                                    }
                                    else tempFullName = Utils.CacheLastOpponentFullName;
                                    ClipboardUtils.CopyToClipboard(@tempFullName);
                                    UIStatus.Get().AddInfo(@tempFullName);
                                }
                                return;
                            }
                            catch (Exception ex)
                            {
                                Utils.MyLogger(BepInEx.Logging.LogLevel.Error, ex);
                                return;
                            }
                        }
                        else
                        {
                            if (!GameState.Get().IsMainPhase())
                            {
                                return;
                            }
                            if (keySquelch.Value.IsDown())
                            {
                                EnemyEmoteHandler.Get().DoSquelchClick();
                            }
                            else
                            {
                                if (GameMgr.Get().IsSpectator())
                                {
                                    return;
                                }
                                if (keyContinueMulligan.Value.IsDown())
                                {
                                    InputManager.Get().DoEndTurnButton();
                                }
                                else if (!(EmoteHandler.Get() == null))
                                {
                                    if (keyEmoteGreetings.Value.IsDown())
                                    {
                                        EmoteHandler.Get().HandleKeyboardInput(0);
                                    }
                                    else if (keyEmoteWellPlayed.Value.IsDown())
                                    {
                                        EmoteHandler.Get().HandleKeyboardInput(1);
                                    }
                                    else if (keyEmoteThanks.Value.IsDown())
                                    {
                                        EmoteHandler.Get().HandleKeyboardInput(2);
                                    }
                                    else if (keyEmoteWow.Value.IsDown())
                                    {
                                        EmoteHandler.Get().HandleKeyboardInput(3);
                                    }
                                    else if (keyEmoteOops.Value.IsDown())
                                    {
                                        EmoteHandler.Get().HandleKeyboardInput(4);
                                    }
                                    else if (keyEmoteThreaten.Value.IsDown())
                                    {
                                        EmoteHandler.Get().HandleKeyboardInput(5);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void OnDestroy()
        {
            PatchManager.UnPatchAll();
        }

    }

}