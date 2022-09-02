using Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static HsMod.PluginConfig;

namespace HsMod
{
    public class WebPage
    {
        public static StringBuilder Template(string title = "", string body = "")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($@"
<!DOCTYPE html>
<html lang=""zh"">
<head>
<meta charset=""UTF-8"">
<meta name=""theme-color"" content=""#66CCFF""> 
<meta name=""viewport"" content=""width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0"">
<style>
body{{
background: linear-gradient(rgba(255, 255, 255, 0.6), rgba(255, 255, 255, 0.6)), url('{webPageBackImg.Value}') no-repeat 0% 25% / cover;
background-size: cover;
background-repeat:no-repeat;
background-position:center;
background-attachment:fixed;
opacity:1.0;
align-items: center;
justify-content: center;
font-family: ""Lucida Console"", sans-serif;
}}
a{{color:#66CCFF;text-decoration: none;}}
a:hover{{text-decoration:underline}}
hr{{
width: auto;
margin: 0 auto;
border: 0;
height: 0;
border-top: 1px solid rgba(0, 0, 0, 0.1);
border-bottom: 1px solid rgba(255, 255, 255, 0.3);
}}
.btn_li {{
z-index: 9;
width: 100%;
}}
.btn_li {{
-webkit-transition-duration: 0.4s; /* Safari */
transition-duration: 0.4s;
border: 1px solid #66CCFF;
opacity: 0.4;
}}
.btn_li:hover {{
background-color: #66CCFF; 
color: white;
opacity: 0.6;
}}
</style>
<title>{PluginInfo.PLUGIN_GUID} - {title}</title>
</head>
<body>"
).Append(body).Append(@"
</body>
</html>
");
            return builder;
        }

        public static StringBuilder Template(StringBuilder body, string title = "")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($@"
<!DOCTYPE html>
<html lang=""zh"">
<head>
<meta charset=""UTF-8"">
<meta name=""theme-color"" content=""#66CCFF""> 
<meta name=""viewport"" content=""width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0"">
<style>
body{{
background: linear-gradient(rgba(255, 255, 255, 0.6), rgba(255, 255, 255, 0.6)), url('{webPageBackImg.Value}') no-repeat 0% 25% / cover;
background-size: cover;
background-repeat:no-repeat;
background-position:center;
background-attachment:fixed;
opacity:1.0;
align-items: center;
justify-content: center;
font-family: ""Lucida Console"", sans-serif;
}}
a{{color:#66CCFF;text-decoration: none;}}
a:hover{{text-decoration:underline}}
hr{{
width: auto;
margin: 0 auto;
border: 0;
height: 0;
border-top: 1px solid rgba(0, 0, 0, 0.1);
border-bottom: 1px solid rgba(255, 255, 255, 0.3);
}}
.btn_li {{
z-index: 9;
width: 100%;
}}
.btn_li {{
-webkit-transition-duration: 0.4s; /* Safari */
transition-duration: 0.4s;
border: 1px solid #66CCFF;
opacity: 0.4;
}}
.btn_li:hover {{
background-color: #66CCFF; 
color: white;
opacity: 0.6;
}}
</style>
<title>{PluginInfo.PLUGIN_GUID} - {title}</title>
</head>
<body>"
).Append(body).Append(@"
</body>
</html>
");
            return builder;
        }

        public static StringBuilder HomePage()
        {
            string btn = @" <a href=""/info""><button class=""btn_li"">主要信息</button><br/></a><br/>";
            btn += @"<a href=""/pack""><button class=""btn_li"">卡包信息</button><br/></a><br/>";
            btn += @"<a href=""/collection""><button class=""btn_li"">卡牌收藏</button><br/></a><br/>";
            btn += @"<a href=""/skins""><button class=""btn_li"">皮肤信息</button><br/></a><br/>";
            btn += @"<a href=""/lettuce""><button class=""btn_li"">佣兵关卡</button><br/></a><br/>";
            btn += @"<a href=""/mercenaries""><button class=""btn_li"">佣兵收藏</button><br/></a><br/>";
            if (System.IO.File.Exists(hsMatchLogPath.Value)) btn += @"<a href=""/matchlog""><button class=""btn_li"">炉石对局</button><br/></a><br/>";
            btn += @"<a href=""/about""><button class=""btn_li"">关&emsp;&emsp;于</button><br/></a><br/>";
            string body = @"<h1 style=""text-align: center; opacity: 0.6;"">HsMod</h1>";
            body += $@"<div style=""text-align: center; width: auto; position: absolute; top: 50%; left: 50%; transform: translate(-50%,-50%);"">{btn}</div>";
            return Template("index", body);
        }

        public static StringBuilder AboutPage()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(@"<h3 style=""text-align: center;"">关于HsMod</h3>");
            builder.AppendLine($"<p>Author: <a href='https://github.com/Pik-4'>Pik_4</a><br />Page Last Updated: 2022.09.02<br />HsMod Version:{PluginInfo.PLUGIN_VERSION}</p><br />");
            builder.Append("<p><strong>H</strong>earth<strong>s</strong>tone <strong>Mod</strong>ify Based on BepInEx 基于BepInEx的炉石修改，插件源代码位于<a href='https://github.com/Pik-4/HsMod'>github.com/Pik-4/HsMod</a>，插件不会收集您的任何信息；项目遵循<code>AGPL-3.0</code>，仅用作学习研究。</p>\r\n");
            builder.AppendLine("<h3>已实现的功能</h3>\r\n<ol start='' >\r\n<li>支持齿轮快慢8倍速</li>\r\n<li>允许使用VerifyWebCredentials登录（亦支持命令行启动，不需要启动战网）。</li>\r\n<li>屏蔽错误报告，当发生异常时，不会向暴雪报告错误信息。</li>\r\n<li>禁用掉线，允许长时间无操作</li>\r\n<li>允许报错自动退出</li>\r\n<li>允许移除窗口焦点</li>\r\n<li>解除窗口大小化限制</li>\r\n<li>拦截弹窗（如无法匹配等）提示。</li>\r\n<li>移除中国特色提示</li>\r\n<li>支持移除削弱补丁提示，移除广告推销，移除天梯结算奖励等弹窗</li>\r\n<li>允许屏蔽对局结束的升级提示、结算提示</li>\r\n<li>允许屏蔽战令、成就等奖励领取提示</li>\r\n<li>允许快速开包，空格一次开5张</li>\r\n<li>允许在开包时自动分解全额分解的卡牌</li>\r\n<li>允许显示游戏帧率信息</li>\r\n<li>允许修改游戏帧率</li>\r\n<li>支持在收藏、英雄、卡背、打击特效、酒馆面板等场景，右键选中卡牌时显示Dbid</li>\r\n<li>支持收藏显示9+卡牌实际数量</li>\r\n<li>允许在0-0（可以不组卡牌）时放弃对决</li>\r\n<li>允许自动领取竞技场、对决等奖励（结束时点包裹）</li>\r\n<li>允许进入炉石开发者模式</li>\r\n<li>好友观战自动旋转卡牌、自动观战双方</li>\r\n<li>支持炉边聚会模拟定位</li>\r\n<li>允许自动屏蔽对手表情或设置对方表情上限；支持屏蔽思考表情；支持屏蔽鲍勃语音；支持对战跳过英雄介绍</li>\r\n<li>支持表情无冷却（表情发送最小间隔1.5秒）</li>\r\n<li>支持表情快捷键</li>\r\n<li>支持酒馆快速战斗（跳过部分动画，比齿轮更丝滑，开启时屏蔽终结特效）</li>\r\n<li>支持炉石自动金卡、钻石卡</li>\r\n<li>支持标记对手已知卡牌</li>\r\n<li>允许单独屏蔽对手卡牌特效</li>\r\n<li>允许显示对手完整战网昵称</li>\r\n<li>允许点击头像获取酒馆玩家昵称</li>\r\n<li>允许对战中添加对手</li>\r\n<li>允许在传说前显对手示天梯等级</li>\r\n<li>允许使用快捷键静音炉石</li>\r\n<li>允许自动举报对手；当自动举报对手启用时，可以自动生成对局记录</li>\r\n<li>支持模拟拔线（需要开启快捷键）</li>\r\n<li>支持一键自动分解全额分解的卡牌（需要开启快捷键）</li>\r\n<li>支持一键移除<code>新！</code>（需要开启快捷键，可能需要重新进入收藏，佣兵可能重启后失效）</li>\r\n<li>支持修改对战英雄皮肤、酒馆英雄皮肤、终结特效、对战面板、酒馆面板、幸运币等皮肤信息。（需要配置<code>HsSkins.cfg</code>，或在设置中修改，对局中更新需要在按下<code>F4</code>保存后，模拟拔线）</li>\r\n<li>支持修改卡背（对局中自动生效）</li>\r\n<li>支持佣兵随机皮肤，强制钻石皮肤等</li>\r\n<li>支持屏蔽佣兵宝箱、天梯奖励等弹窗</li>\r\n<li>支持屏蔽佣兵对战界面缩放</li>\r\n<li>支持模拟开包（支持结果随机，支持自定义卡包类型、数量、稀有度、品质等信息；支持模拟固定结果）</li>\r\n<li>支持设备模拟（允许领取iOS、Android等设备的卡包卡背，可能需要一局对战）</li>\r\n<li>支持金币购买纳克萨玛斯、黑石山、探险者协会等冒险（也支持卡拉赞，但无法打序章）</li>\r\n<li>允许强开卡拉赞（不能打序章，未通关前不能跳关）</li>\r\n<li>支持信息展示（showinfo，需要启用插件，默认HTTP，端口58744）；支持显示佣兵养成进度、开包历史信息等。</li>\r\n</ol>");
            builder.AppendLine("<h3>补充说明</h3>\r\n<ol start='' >\r\n<li>本插件可能与基于<code>Assembly-CSharp.dll</code>的修改冲突，修改<code>Assembly-CSharp.dll</code>可能导致IL指令定位异常，进而造成相关Patch无法生效；还可能与其他BepInEx插件（例如佣兵、MixMod）冲突，原因是同一个方法可能在两个插件中都存在Patch，当有多个Patch时，运行结果可能会异常，本插件没有检测原方法是否被修改。</li>\r\n<li>皮肤的配置文件在<code>Hearthstone\\BepInEx\\config\\HsSkins.cfg</code>。若无，则在运行游戏后自动创建。</li>\r\n<li><code>F4</code>为固定快捷键，用于获取游戏内部分信息（相关信息存放在<code>Hearthstone\\BepInEx\\</code>目录下）、<strong>更新皮肤配置</strong>、重启Web服务等。其余快捷键均可自定义配置。</li>\r\n<li>本插件在默认状态下，几乎全部的功能均需要手动开启；插件大部分功能能在配置中找到说明，少部分功能只在Patch中提及（如最小化限制）。</li>\r\n<li>对局统计所使用的log文件是<code>BepInEx\\HsMatch.log</code>，可在设置中修改。此外对局统计页面适配的佣兵插件为<a href='https://github.com/HearthstonePlugin/Mercenaries'>MercenariesHelper</a>（当MercenariesHelper配合HsMod编译时，其Build4Public值应为false，以忽略与HsMod的相同Patch）。其他佣兵插件暂未做适配。</li>\r\n<li>出现问题时先尝试删除相关<code>.cfg</code>配置文件（一般位于<code>BepInEx\\config\\</code>），进行重新配置；如果依然存在问题，请带上<code>HsMod.cfg</code>提交<a href='https://github.com/Pik-4/HsMod/issues'>Issues</a>，但不保证及时解答。</li>\r\n<li><code>GetHsLib.py</code>用于更新炉石自有运行库，<code>install.bat</code>用于将编译好的<code>HsMod.dll</code>复制到默认炉石目录（前提是BepInEx已经配置好）。此外，push或提交PR到Github后，Actions会自动生成相关pre-release。</li>\r\n</ol>");
            return Template(builder, "About");
        }

        public static StringBuilder InfoPage()
        {
            string result = "";
            result += @"<h3 style=""text-align: center;"">进程信息</h3>";
            result += "PID：";
            result += System.Diagnostics.Process.GetCurrentProcess()?.Id.ToString();
            result += "<br />";
            result += "<hr />";


            result += @"<h3 style=""text-align: center;"">基本信息</h3>";
            NetCache netCache = NetCache.Get();
            try
            {
                result += "账号：";
                result += BnetPresenceMgr.Get()?.GetMyPlayer()?.GetBattleTag()?.ToString();
                result += "<br />";

                result += "金币：";
                result += netCache?.GetGoldBalance().ToString();
                result += "<br />";

                result += "奥数之尘：";
                result += netCache?.GetArcaneDustBalance().ToString();
                result += "<br />";

                result += "竞技场门票：";
                result += netCache?.GetArenaTicketBalance().ToString();
                result += "<br />";
            }
            catch (Exception ex)
            {
                result += $@"基本信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p><br />";
            }
            finally
            {
                result += "<hr />";
            }

            result += @"<h3 style=""text-align: center;"">战令信息</h3>";
            try
            {
                Hearthstone.DataModels.RewardTrackDataModel trackDataModel = Hearthstone.Progression.RewardTrackManager.Get().GetCurrentRewardTrack(Global.RewardTrackType.GLOBAL).TrackDataModel;
                result += $"炉石：{trackDataModel.Level}&emsp;&emsp;";
                result += "进度：" + ((trackDataModel.Level == trackDataModel.LevelHardCap && trackDataModel.Xp == 0) ? "已满级！" : trackDataModel.XpProgress) + "<br />";
                trackDataModel = Hearthstone.Progression.RewardTrackManager.Get().GetCurrentRewardTrack(Global.RewardTrackType.BATTLEGROUNDS).TrackDataModel;
                result += $"酒馆：{trackDataModel.Level}&emsp;&emsp;";
                result += "进度：" + ((trackDataModel.Level == trackDataModel.LevelHardCap && trackDataModel.Xp == 0) ? "已满级！" : trackDataModel.XpProgress) + "<br />";
            }
            catch (Exception ex)
            {
                result += $@"战令信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p><br />";
            }
            finally
            {
                result += "<hr />";
            }

            result += @"<h3 style=""text-align: center;"">天梯信息</h3>";
            try
            {
                MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
                TranslatedMedalInfo currentMedal = null;
                string rankMode = "";
                for (int i = 1; i <= 3; i++)
                {
                    switch (i)
                    {
                        case 1:
                            currentMedal = localPlayerMedalInfo.GetCurrentMedal(PegasusShared.FormatType.FT_CLASSIC);
                            rankMode = "经典";
                            break;
                        case 2:
                            currentMedal = localPlayerMedalInfo.GetCurrentMedal(PegasusShared.FormatType.FT_STANDARD);
                            rankMode = "标准";
                            break;
                        case 3:
                            currentMedal = localPlayerMedalInfo.GetCurrentMedal(PegasusShared.FormatType.FT_WILD);
                            rankMode = "狂野";
                            break;
                    }
                    string rankName = Utils.RankIdxToString(currentMedal.starLevel);
                    string detail = (rankName == "传说") ? currentMedal.legendIndex.ToString() + " 名" : currentMedal.earnedStars.ToString() + " 星";
                    result += $@"{rankMode}：{rankName}&emsp;{detail}&emsp;&emsp;";
                    result += $@"赛季场次：{currentMedal.seasonWins}胜 - {currentMedal.seasonGames}场";
                    result += $@"（{string.Format("{0:P1}", (float)currentMedal.seasonWins / (float)currentMedal.seasonGames)}）<br />";
                }
                NetCache.NetCacheMercenariesPlayerInfo mercenariesPlayerInfo = NetCache.Get()?.GetNetObject<NetCache.NetCacheMercenariesPlayerInfo>();
                result += $@"佣兵 PvP 分数：{mercenariesPlayerInfo.PvpRating}（当前）- {mercenariesPlayerInfo.PvpSeasonHighestRating}（最高）&emsp;&emsp;";
                result += $@"宝箱进度：{mercenariesPlayerInfo.PvpRewardChestWinsProgress}/{mercenariesPlayerInfo.PvpRewardChestWinsRequired} <br />";
            }
            catch (Exception ex)
            {
                result += $@"天梯信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p><br />";

            }
            finally
            {
                result += "<hr />";
            }

            result += @"<h3 style=""text-align: center;"">任务信息</h3>";
            try
            {
                Hearthstone.DataModels.QuestListDataModel dailyQuestListDataModel = new Hearthstone.DataModels.QuestListDataModel();
                Hearthstone.DataModels.QuestListDataModel weeklyQuestListDataModel = new Hearthstone.DataModels.QuestListDataModel();
                Hearthstone.DataModels.QuestListDataModel specialQuestListDataModel = new Hearthstone.DataModels.QuestListDataModel();
                Hearthstone.DataModels.QuestListDataModel battlegroundsQuestListDataModel = new Hearthstone.DataModels.QuestListDataModel();

                foreach (Hearthstone.DataModels.QuestDataModel item in Hearthstone.Progression.QuestManager.Get().CreateActiveQuestsDataModel(Assets.QuestPool.QuestPoolType.DAILY, QuestPool.RewardTrackType.GLOBAL, true).Quests)
                {
                    if (item == null || dailyQuestListDataModel.Quests.Count > 4)
                    {
                        break;
                    }
                    dailyQuestListDataModel.Quests.Add(item);
                }
                result += @"<h4>日常任务</h4>";
                foreach (Hearthstone.DataModels.QuestDataModel item in dailyQuestListDataModel.Quests)
                {
                    if (item != null)
                    {
                        if (item.QuestId > 0)
                        {
                            result += "<li>";
                            result += $@"{item.Status} {item.Name}：{item.Description}<br />进度：{item.ProgressMessage}<br />";

                            result += $@"经验奖励：{item.RewardTrackXp}";
                            result += (item.RerollCount > 0) ? "（可刷新）" : "";
                            result += "</li><br />";
                        }
                        else
                        {
                            result += "<li>";
                            result += $@"{item.TimeUntilNextQuest}";
                            result += "</li>";
                            break;
                        }
                    }
                }

                foreach (Hearthstone.DataModels.QuestDataModel item in Hearthstone.Progression.QuestManager.Get().CreateActiveQuestsDataModel(Assets.QuestPool.QuestPoolType.WEEKLY, QuestPool.RewardTrackType.GLOBAL, true).Quests)
                {
                    if (item == null || weeklyQuestListDataModel.Quests.Count > 4)
                    {
                        break;
                    }
                    weeklyQuestListDataModel.Quests.Add(item);
                }
                result += @"<h4>每周任务</h4>";
                foreach (Hearthstone.DataModels.QuestDataModel item in weeklyQuestListDataModel.Quests)
                {
                    if (item != null)
                    {
                        if (item.QuestId > 0)
                        {
                            result += "<li>";
                            result += $@"{item.Status} {item.Name}：{item.Description}<br />进度：{item.ProgressMessage}<br />";

                            result += $@"经验奖励：{item.RewardTrackXp}";
                            result += (item.RerollCount > 0) ? "（可刷新）" : "";
                            result += "</li><br />";
                        }
                        else
                        {
                            result += "<li>";
                            result += $@"{item.TimeUntilNextQuest}";
                            result += "</li>";
                            break;
                        }
                    }
                }

                foreach (Hearthstone.DataModels.QuestDataModel item in Hearthstone.Progression.QuestManager.Get().CreateActiveQuestsDataModel(Assets.QuestPool.QuestPoolType.NONE, QuestPool.RewardTrackType.GLOBAL, true).Quests)
                {
                    if (item == null || specialQuestListDataModel.Quests.Count > 4)
                    {
                        break;
                    }
                    specialQuestListDataModel.Quests.Add(item);
                }
                if (specialQuestListDataModel.Quests.Count >= 1 && specialQuestListDataModel.Quests[0].QuestId > 0)
                {

                    result += @"<h4>活动任务</h4>";
                    foreach (Hearthstone.DataModels.QuestDataModel item in specialQuestListDataModel.Quests)
                    {
                        if (item != null)
                        {
                            if (item.QuestId > 0)
                            {
                                result += "<li>";
                                result += $@"{item.Status} {item.Name}：{item.Description}<br />";
                                result += $@"奖励：{item.Rewards.Description}<br />";
                                result += $@"经验：{item.RewardTrackXp}<br />进度：{item.ProgressMessage}<br />";
                                result += $@"距离活动结束还剩：{item.TimeUntilExpiration}";
                                result += item.Abandonable ? "（可放弃）" : "";
                                result += "</li><br />";
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                foreach (Hearthstone.DataModels.QuestDataModel item in Hearthstone.Progression.QuestManager.Get().CreateActiveQuestsDataModel(Assets.QuestPool.QuestPoolType.WEEKLY, QuestPool.RewardTrackType.BATTLEGROUNDS, true).Quests)
                {
                    if (item == null)
                    {
                        break;
                    }
                    battlegroundsQuestListDataModel.Quests.Add(item);
                }
                result += @"<h4>每周酒馆</h4>";
                foreach (Hearthstone.DataModels.QuestDataModel item in battlegroundsQuestListDataModel.Quests)
                {
                    if (item != null)
                    {
                        if (item.QuestId > 0)
                        {
                            result += "<li>";
                            result += $@"{item.Status} {item.Name}：{item.Description}<br />进度：{item.ProgressMessage}<br />";

                            result += $@"经验奖励：{item.RewardTrackXp}";
                            result += (item.RerollCount > 0) ? "（可刷新）" : "";
                            result += "</li><br />";
                        }
                        else
                        {
                            result += "<li>";
                            result += $@"{item.TimeUntilNextQuest}";
                            result += "</li>";
                            break;
                        }
                    }
                }

                result += @"<h4>佣兵任务</h4>";
                foreach (PegasusLettuce.MercenariesVisitorState mercenariesVisitorState in NetCache.Get().GetNetObject<NetCache.NetCacheMercenariesVillageVisitorInfo>().VisitorStates)
                {
                    Hearthstone.DataModels.MercenaryVillageTaskItemDataModel mercenaryVillageTaskItemDataModel = LettuceVillageDataUtil.CreateTaskModelByTaskState(mercenariesVisitorState.ActiveTaskState, null, false, false);

                    result += "<li>";

                    result += $"[{mercenaryVillageTaskItemDataModel.MercenaryName}]&emsp;";
                    if (mercenaryVillageTaskItemDataModel.TaskType == Assets.MercenaryVisitor.VillageVisitorType.STANDARD)
                    {
                        result += $@"任务{mercenaryVillageTaskItemDataModel.TaskChainIndex + 1} - ";
                    }
                    result += $"{mercenaryVillageTaskItemDataModel.Title}<br />{mercenaryVillageTaskItemDataModel.Description}<br />";
                    result += $"任务奖励：{mercenaryVillageTaskItemDataModel.RewardList.Description}<br />";
                    result += $"任务进度：{mercenaryVillageTaskItemDataModel.ProgressMessage}";
                    if (mercenaryVillageTaskItemDataModel.IsTimedEvent)
                        result += $"<br />剩余时间：{mercenaryVillageTaskItemDataModel.RemainingEventTime}<br />";
                    result += "</li><br />";
                }


            }
            catch (Exception ex)
            {
                result += $@"任务信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p><br />";
            }
            finally
            {
                result += "<hr />";
            }


            return Template("Info", result);
        }

        public static StringBuilder CollectionPage()
        {
            string body = "";
            body += @"<h3 style=""text-align: center;"">可分解卡牌信息</h3>";
            Utils.CardCount cards = new Utils.CardCount();
            List<Utils.CollectionCard> collectionCards = new List<Utils.CollectionCard>();
            try
            {
                if (SceneMgr.Get().GetMode() != SceneMgr.Mode.COLLECTIONMANAGER)
                {
                    throw new Exception();
                }
                string temp = @"<table border=0 style=""text-align: center;""><tr><th>卡牌名称</th><th>卡牌质量</th><th>卡牌数量</th></tr>";
                foreach (var record in CollectionManager.Get()?.GetOwnedCards())
                {
                    if (record != null
                        && record.IsCraftable
                        && (!record.IsHeroSkin)
                        && (!record.IsMercenaryAbility)
                        && (record.Set != TAG_CARD_SET.CORE)
                        && (record.OwnedCount > 0))
                    {
                        collectionCards.Add(new Utils.CollectionCard { Name = record.Name, Rarity = record.Rarity, Premium = record.PremiumType, Count = record.OwnedCount });
                    }

                }
                foreach (var card in collectionCards.Distinct().ToList())
                {
                    temp += "<tr>";
                    temp += $"<td>{card.Name}</td>";
                    temp += $@"{Utils.CardsCount(card.Rarity, card.Premium, card.Count, ref cards)}";
                    temp += "</tr>";
                }
                temp += "</table>";

                body += $"<li>全部卡牌数量：{cards.total}，其中金卡数量：{cards.gTotal}</li>";
                body += $"<li>普通卡牌数量：{cards.common + cards.gCommon}，其中金卡数量：{cards.gCommon}</li>";
                body += $"<li>稀有卡牌数量：{cards.rare + cards.gRare}，其中金卡数量：{cards.gRare}</li>";
                body += $"<li>史诗卡牌数量：{cards.epic + cards.gEpic}，其中金卡数量：{cards.gEpic}</li>";
                body += $"<li>传说卡牌数量：{cards.legendary + cards.gLegendary}，其中金卡数量：{cards.gLegendary}</li>";
                body += $"<li>能分解的奥术之尘：{cards.totalDust}</li>";
                body += "<br /><hr />";
                //body += temp;
            }
            catch (Exception ex)
            {
                body += $@"卡牌信息获取异常，请重新进入收藏模式。<br /><p style=""white-space: pre-line;"">{ex}</p><br />";
            }
            finally
            {
                //body += "<hr />";
            }
            return Template("Collection", body);
        }
        public static StringBuilder MercenariesLettucePage()
        {
            string body = "";
            body += @"<h3 style=""text-align: center;"">佣兵关卡</h3>";
            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>索引</th>";
                temp += "<th>位置</th>";
                temp += "<th>名称</th>";
                temp += "<th>难度</th>";
                temp += "</tr>";

                foreach (var record in GameDbf.LettuceBounty.GetRecords().OrderBy(x => x.ID).ToList())
                {
                    if (record != null)
                    {
                        temp += "<tr>";
                        temp += $"<td>{record.ID}</td>";
                        temp += $"<td>{record.BountySetRecord.Name.GetString()}</td>";
                        temp += $"<td>{LettuceVillageDataUtil.GetBountyBossName(record)}</td>";
                        temp += "<td>" + (record.Heroic ? "英雄" : "普通") + "</td>";
                        temp += "</tr>";
                    }

                }
                temp += "</table>";
                body += temp;
            }
            catch (Exception ex)
            {
                body += $@"佣兵关卡信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p><br />";
            }
            finally
            {
                Utils.LeakInfo.Mercenaries();
            }
            return Template("Mercenaries", body);
        }
        public static StringBuilder SkinsPage()
        {

            StringBuilder builder = new StringBuilder();
            string body = @"<h3 style=""text-align: center;"">皮肤信息</h3>";

            body += "<h4>幸运币</h4>";
            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>索引</th>";
                temp += "<th>名称</th>";
                temp += "</tr>";

                foreach (var record in GameDbf.Coin.GetRecords().OrderBy(x => x.ID).ToList())
                {
                    if (record != null)
                    {
                        temp += "<tr>";
                        temp += $"<td>{record.CardId}</td>";
                        temp += $"<td>{record.Name.GetString()}</td>";
                        temp += "</tr>";
                    }

                }
                temp += "</table>";
                body += temp;
            }
            catch (Exception ex)
            {
                body += $@"幸运币信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p>";
            }
            finally
            {
                body += "<br /><hr />";
            }
            builder.Append(body);


            body = "<h4>卡背</h4>";
            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>索引</th>";
                temp += "<th>名称</th>";
                temp += "</tr>";

                foreach (var record in GameDbf.CardBack.GetRecords().OrderBy(x => x.ID).ToList())
                {
                    if (record != null)
                    {
                        temp += "<tr>";
                        temp += $"<td>{record.ID}</td>";
                        temp += $"<td>{record.Name.GetString()}</td>";
                        temp += "</tr>";
                    }
                }
                temp += "</table>";
                body += temp;
            }
            catch (Exception ex)
            {
                body += $@"卡背信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p>";
            }
            finally
            {
                body += "<br /><hr />";
            }
            builder.Append(body);


            body = "<h4>酒馆战斗面板</h4>";
            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>索引</th>";
                temp += "<th>名称</th>";
                temp += "</tr>";

                foreach (var record in GameDbf.BattlegroundsBoardSkin.GetRecords().OrderBy(x => x.ID).ToList())
                {
                    if (record != null)
                    {
                        temp += "<tr>";
                        temp += $"<td>{record.ID}</td>";
                        temp += $"<td>{record.CollectionName.GetString()}</td>";
                        temp += "</tr>";
                    }
                }
                temp += "</table>";
                body += temp;
            }
            catch (Exception ex)
            {
                body += $@"酒馆战斗面板信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p>";
            }
            finally
            {
                body += "<br /><hr />";
            }
            builder.Append(body);


            body = "<h4>酒馆终结特效</h4>";
            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>索引</th>";
                temp += "<th>名称</th>";
                temp += "</tr>";

                foreach (var record in GameDbf.BattlegroundsFinisher.GetRecords().OrderBy(x => x.ID).ToList())
                {
                    if (record != null)
                    {
                        temp += "<tr>";
                        temp += $"<td>{record.ID}</td>";
                        temp += $"<td>{record.CollectionName.GetString()}</td>";
                        temp += "</tr>";
                    }
                }
                temp += "</table>";
                body += temp;
            }
            catch (Exception ex)
            {
                body += $@"酒馆终结特效信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p>";
            }
            finally
            {
                body += "<br /><hr />";
                //Utils.LeakInfo.Skins();
            }
            builder.Append(body);


            body = "<h4>英雄</h4>";
            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>索引</th>";
                temp += "<th>名称</th>";
                temp += "<th>类型</th>";
                temp += "</tr>";

                foreach (var record in GameDbf.CardHero.GetRecords().OrderBy(x => x.HeroType).ToList())
                {
                    if (record != null)
                    {
                        temp += "<tr>";
                        temp += $"<td>{record.CardId}</td>";
                        temp += $"<td>{GameDbf.Card.GetRecord(record.CardId).Name.GetString()}</td>";
                        switch (record.HeroType)
                        {
                            case Assets.CardHero.HeroType.BATTLEGROUNDS_HERO:
                                temp += "<td>酒馆英雄</td>";
                                break;
                            case Assets.CardHero.HeroType.BATTLEGROUNDS_GUIDE:
                                temp += "<td>酒馆鲍勃</td>";
                                break;
                            default:
                                temp += "<td>对战英雄</td>";
                                break;
                        }
                        temp += "</tr>";
                    }
                }
                temp += "</table>";
                body += temp;
            }
            catch (Exception ex)
            {
                body += $@"英雄信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p>";
            }
            finally
            {
                body += "<br /><hr />";
                Utils.LeakInfo.Skins();
            }
            builder.Append(body);

            return Template(builder, "Skins");
        }

        public static StringBuilder MercenariesPage()
        {
            string body = "";
            body += @"<h3 style=""text-align: center;"">佣兵收藏</h3>";
            try
            {
                if (CollectionManager.Get() == null)
                {
                    throw new Exception();
                }
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>名称</th>";
                temp += "<th>等级</th>";
                temp += "<th>硬币</th>";
                temp += "<th>状态</th>";
                temp += "</tr>";

                foreach (var merc in CollectionManager.Get().FindOrderedMercenaries(null, null, null, null, null).m_mercenaries
                                    .OrderByDescending(x => x.m_owned)
                                    .ThenByDescending(x => x.m_isFullyUpgraded)
                                    .ThenByDescending(x => x.m_level)
                                    .ThenByDescending(x => Utils.CalcMercenaryCoinNeed(x))
                                    .ThenByDescending(x => x.m_currencyAmount)
                                    .ToList())
                {
                    if (merc != null)
                    {
                        temp += "<tr>";
                        switch (merc.m_rarity)
                        {
                            case TAG_RARITY.LEGENDARY:
                                temp += $@"<td style=""color:#FFCC00"">{merc.m_mercName}</td>";
                                break;
                            case TAG_RARITY.EPIC:
                                temp += $@"<td style=""color:#CC99CC"">{merc.m_mercName}</td>";
                                break;
                            case TAG_RARITY.RARE:
                                temp += $@"<td style=""color:#99CCFF"">{merc.m_mercName}</td>";
                                break;
                            default:
                                temp += $@"<td>{merc.m_mercName}</td>";
                                break;
                        }

                        temp += $"<td>{merc.m_level}</td>";

                        temp += $"<td>{merc.m_currencyAmount}</td>";
                        //temp += $"<td>{GameStrings.GetRoleName(merc.m_role)}</td>";
                        if (!merc.m_owned)
                        {
                            temp += "<td>" + ((merc.GetCraftingCost() - merc.m_currencyAmount > 0) ? $"制作需要{merc.GetCraftingCost() - merc.m_currencyAmount}硬币" : "可制作！") + "</td>";
                        }
                        else if (merc.m_isFullyUpgraded)
                        {
                            temp += "<td>全满！(+1+5)</td>";
                        }
                        else
                        {
                            long coinNeed = Utils.CalcMercenaryCoinNeed(merc);
                            if (coinNeed != 8192)
                                temp += $"<td>距离全满还需硬币：{coinNeed}</td>";
                            else
                            {
                                temp += $"<td>可升级至全满！</td>";
                            }
                        }

                        temp += "</tr>";
                    }
                }
                temp += "</table>";
                body += temp;
            }
            catch (Exception ex)
            {
                body += $@"佣兵收藏信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p>";
            }
            finally
            {
                body += "<br />";
            }
            return Template("Mercenaries", body);
        }

        public static StringBuilder PackPage()
        {
            string body = @"<h3 style=""text-align: center;"">卡包信息</h3>";
            string temp = @"<table border=0 style=""text-align: center;""><tr>";
            temp += "<th>索引</th>";
            temp += "<th>名称</th>";
            temp += "<th>已开</th>";
            temp += "<th>剩余</th>";
            temp += "</tr>";
            try
            {
                foreach (var booster in GameDbf.Booster.GetRecords().OrderBy(x => x.ID).ToList())
                {
                    if (booster != null)
                    {
                        //Enum.GetValues(typeof(BoosterDbId))
                        string name = booster.Name.GetString();
                        name = (name != "" && name != null) ? name : Enum.GetName(typeof(BoosterDbId), booster.ID);
                        if (name == "" || name == null)
                        {
                            var Dbids = Enum.GetNames(typeof(BoosterDbId)).ToList();
                            if (booster.ID < Dbids.Count)
                            {
                                name = Dbids[booster.ID];
                            }
                            else
                            {
                                name = "未知";
                            }

                        }
                        temp += "<tr>";
                        temp += $"<td>{booster.ID}</td>";
                        temp += $"<td>{name}</td>";
                        int opened = BoosterPackUtils.GetBoosterOpenedCount((int)booster.ID);
                        int remainCount = BoosterPackUtils.GetBoosterCount((int)booster.ID);
                        temp += "<td>" + ((opened == 0) ? "-" : opened.ToString()) + "</td>";
                        temp += "<td>" + ((remainCount == 0) ? "-" : remainCount.ToString()) + "</td>";
                        temp += "</tr>";
                    }
                }
                body += temp;
            }
            catch (Exception ex)
            {
                body += $@"卡包信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p>";
            }
            finally
            {
                body += "<br />";
            }
            return Template("Pack", body);
        }

        public static StringBuilder MatchLogPage()
        {
            StringBuilder builder = new StringBuilder();
            if (!System.IO.File.Exists(hsMatchLogPath.Value)) return Template(builder.Append("对局文件不存在！"), "MatchLog");
            else builder.Append(@"<h3 style=""text-align: center;"">对局记录</h3>");

            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>结束时间</th>";
                temp += "<th>对局结果</th>";
                temp += "<th>当前排名</th>";
                temp += "<th>友方信息</th>";
                temp += "<th>你的对手</th>";
                temp += "<th>对手信息</th>";
                temp += "</tr>";
                builder.Append(temp);

                foreach (string line in System.IO.File.ReadLines(hsMatchLogPath.Value).Reverse())
                {
                    temp = "";
                    if (line != String.Empty)
                    {
                        temp += "<tr>";
                        string[] lineSplit = line.Split(',');
                        for (int i = 0; i < lineSplit.Length; i++)
                        {
                            if (i == 1 && lineSplit[i].Length > 0)
                            {
                                if (lineSplit[i] == "胜利") temp += $"<td style=\"color:#01FF70\">胜利</td>";
                                else if (lineSplit[i] == "失败") temp += $"<td style=\"color:#FF4136\">失败</td>";
                                else if (lineSplit[i] == "未知" || lineSplit[i] == "平局") temp += $"<td>{lineSplit[i]}</td>";
                                else if (int.Parse(lineSplit[i]) > 0) temp += $"<td style=\"color:#01FF70\">+{int.Parse(lineSplit[i])}</td>";
                                else if (int.Parse(lineSplit[i]) < 0) temp += $"<td style=\"color:#FF4136\">{lineSplit[i]}</td>";
                                else temp += $"<td>{lineSplit[i]}</td>";
                            }
                            else temp += $"<td>{lineSplit[i]}</td>";
                        }
                        temp += "</tr>";
                    }
                    builder.Append(temp);
                }
            }
            catch (Exception ex)
            {
                builder.Append($@"</table>日志解析异常<br /><p style=""white-space: pre-line;"">{ex}</p>");
            }
            finally
            {
                builder.Append("</table>");
            }
            return Template(builder, "MatchLog").Remove(108, 126);
        }

        public static StringBuilder AlivePage()
        {
            return new StringBuilder().Append(System.Diagnostics.Process.GetCurrentProcess()?.Id.ToString());
        }

    }
}
