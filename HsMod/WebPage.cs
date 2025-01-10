using Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static HsMod.PluginConfig;

namespace HsMod
{
    public class WebPage
    {
        public const string Viewport = "<meta name=\"viewport\" content=\"width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0\">";
        private static string GenerateBtn()
        {
            string btn = @" <a href=""/info""><button class=""btn_li"">主要信息</button><br/></a><br />";
            btn += @"<a href=""/pack""><button class=""btn_li"">卡包信息</button><br/></a><br />";
            btn += @"<a href=""/collection""><button class=""btn_li"">卡牌收藏</button><br/></a><br />";
            btn += @"<a href=""/skins""><button class=""btn_li"">皮肤信息</button><br/></a><br />";
            btn += @"<a href=""/lettuce""><button class=""btn_li"">佣兵关卡</button><br/></a><br />";
            btn += @"<a href=""/mercenaries""><button class=""btn_li"">佣兵收藏</button><br/></a><br />";
            if (System.IO.File.Exists(CommandConfig.hsMatchLogPath)) btn += @"<a href=""/matchlog""><button class=""btn_li"">炉石对局</button><br/></a><br />";
            if (File.Exists(Path.Combine(PluginConfig.HsModWebSite, "config", "index.html"))) btn += @"<a href=""/config/index.html""><button class=""btn_li"">配置修改</button><br/></a><br />";
            btn += @"<a href=""/about""><button class=""btn_li"">关&emsp;&emsp;于</button><br/></a><br />";
            return btn;
        }

        private static string GenerateNav(string title)
        {
            string nav = string.Empty;
            if (title != "index")
            {
                nav = "<center><ul class=\"nav_ui\">";
                var btns = GenerateBtn().Replace("<br/>", "").Split("<br />");
                foreach (string btn in btns)
                {
                    nav += $@"<li class=""nav_li"">{btn}</li>";
                }
                nav += "</ul></center><br />";
            }

            return nav;
        }

        public static StringBuilder Template(string title = "", string body = "", bool useViewport = true)
        {
            StringBuilder builder = new StringBuilder();

            string templateContent = FileManager.ReadEmbeddedFile("./WebResources/HsMod.template.html");
            string nav = GenerateNav(title);

            templateContent = templateContent
                .Replace("{PluginInfo.PLUGIN_GUID}", PluginInfo.PLUGIN_GUID)
                .Replace("{useViewport}", useViewport ? Viewport : "")
                .Replace("{title}", title)
                .Replace("{nav}", nav)
                .Replace("{body}", body)
                .Replace("{webPageBackImg}", webPageBackImg.Value);
            builder.Append(templateContent);

            return builder;
        }

        public static StringBuilder Template(StringBuilder body, string title = "", bool useViewport = true)
        {
            StringBuilder builder = new StringBuilder();

            string templateContent = FileManager.ReadEmbeddedFile("./WebResources/HsMod.template.html");

            string nav = GenerateNav(title);

            templateContent = templateContent
                .Replace("{PluginInfo.PLUGIN_GUID}", PluginInfo.PLUGIN_GUID)
                .Replace("{useViewport}", useViewport ? Viewport : "")
                .Replace("{title}", title)
                .Replace("{nav}", nav)
                .Replace("{body}", body.ToString())
                .Replace("{webPageBackImg}", webPageBackImg.Value);
            builder.Append(templateContent);
            return builder;
        }

        public static StringBuilder HomePage()
        {
            var btn = GenerateBtn();
            string body = @"<h1 style=""text-align: center; opacity: 0.6;"">HsMod</h1>";
            body += $@"<div style=""text-align: center; width: auto; position: absolute; top: 50%; left: 50%; transform: translate(-50%,-50%);"">{btn}</div>";
            return Template("index", body);
        }



        public static StringBuilder AboutPage()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(@"<h3 style=""text-align: center;"">关于HsMod</h3>");
            builder.AppendLine($"<p>Author: <a href='https://github.com/Pik-4'>Pik_4</a><br />Page Last Updated: 2024.11.05<br />HsMod Version:{PluginInfo.PLUGIN_VERSION}</p><br />");
            builder.AppendLine(FileManager.ReadEmbeddedFile($"./WebResources/about.{pluginInitLanague.Value}.html"));
            return Template(builder, "About");
        }

        public static StringBuilder ShellPage()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(@"<h3 style=""text-align: center;"">WebShell</h3>");
            builder.AppendLine(FileManager.ReadEmbeddedFile("./WebResources/shell.html"));
            return Template(builder, "WebShell");
        }

        public static StringBuilder InfoPage()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(@"<h3 style=""text-align: center;"">进程信息</h3>");
            builder.Append("PID：");
            builder.Append(System.Diagnostics.Process.GetCurrentProcess()?.Id.ToString());
            builder.Append("<br />");
            builder.Append("<hr />");
            builder.Append(@"<h3 style=""text-align: center;"">基本信息</h3>");
            NetCache netCache = NetCache.Get();
            try
            {
                builder.Append("账号：");
                builder.Append(BnetPresenceMgr.Get()?.GetMyPlayer()?.GetBattleTag()?.ToString());
                builder.Append("<br />");
                builder.Append("金币：");
                builder.Append(netCache?.GetGoldBalance().ToString());
                builder.Append("<br />");
                builder.Append("奥数之尘：");
                builder.Append(netCache?.GetArcaneDustBalance().ToString());
                builder.Append("<br />");
                builder.Append("竞技场门票：");
                builder.Append(netCache?.GetArenaTicketBalance().ToString());
                builder.Append("<br />");
            }
            catch (Exception ex)
            {
                builder.Append($@"基本信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p><br />");
            }
            finally
            {
                builder.Append("<hr />");
            }
            builder.Append(@"<h3 style=""text-align: center;"">战令信息</h3>");
            try
            {
                Hearthstone.DataModels.RewardTrackDataModel trackDataModel = Hearthstone.Progression.RewardTrackManager.Get().GetRewardTrack(Global.RewardTrackType.GLOBAL).TrackDataModel;
                builder.Append($"炉石：{trackDataModel.Level}&emsp;&emsp;");
                builder.Append("进度：" + ((trackDataModel.Level == trackDataModel.LevelHardCap && trackDataModel.Xp == 0) ? "已满级！" : trackDataModel.XpProgress) + "<br />");
                trackDataModel = Hearthstone.Progression.RewardTrackManager.Get().GetRewardTrack(Global.RewardTrackType.BATTLEGROUNDS).TrackDataModel;
                builder.Append($"酒馆：{trackDataModel.Level}&emsp;&emsp;");
                builder.Append("进度：" + ((trackDataModel.Level == trackDataModel.LevelHardCap && trackDataModel.Xp == 0) ? "已满级！" : trackDataModel.XpProgress) + "<br />");
            }
            catch (Exception ex)
            {
                builder.Append($@"战令信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p><br />");
            }
            finally
            {
                builder.Append("<hr />");
            }
            builder.Append(@"<h3 style=""text-align: center;"">天梯信息</h3>");
            try
            {
                MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
                TranslatedMedalInfo currentMedal = null;
                string rankMode = "";
                for (int i = 1; i <= 4; i++)
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
                        case 4:
                            currentMedal = localPlayerMedalInfo.GetCurrentMedal(PegasusShared.FormatType.FT_TWIST);
                            rankMode = "幻变";
                            break;
                    }
                    string rankName = Utils.RankIdxToString(currentMedal.starLevel);
                    string detail = (rankName == "传说") ? currentMedal.legendIndex.ToString() + " 名" : currentMedal.earnedStars.ToString() + " 星";
                    builder.Append($@"{rankMode}：{rankName}&emsp;{detail}&emsp;&emsp;");
                    builder.Append($@"赛季场次：{currentMedal.seasonWins}胜 - {currentMedal.seasonGames}场");
                    builder.Append($@"（{string.Format("{0:P1}", (float)currentMedal.seasonWins / (float)currentMedal.seasonGames)}）<br />");
                }
                NetCache.NetCacheMercenariesPlayerInfo mercenariesPlayerInfo = NetCache.Get()?.GetNetObject<NetCache.NetCacheMercenariesPlayerInfo>();
                builder.Append($@"佣兵：{mercenariesPlayerInfo.PvpRating}（当前）- {mercenariesPlayerInfo.PvpSeasonHighestRating}（最高）&emsp;&emsp;");
                builder.Append($@"宝箱进度：{mercenariesPlayerInfo.PvpRewardChestWinsProgress}/{mercenariesPlayerInfo.PvpRewardChestWinsRequired} <br />");
            }
            catch (Exception ex)
            {
                builder.Append($@"天梯信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p><br />");

            }
            finally
            {
                builder.Append("<hr />");
            }
            builder.Append(@"<h3 style=""text-align: center;"">任务信息</h3>");
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
                builder.Append(@"<h4>日常任务</h4>");
                foreach (Hearthstone.DataModels.QuestDataModel item in dailyQuestListDataModel.Quests)
                {
                    if (item != null)
                    {
                        if (item?.QuestId > 0)
                        {
                            builder.Append("<li>");
                            builder.Append($@"{item?.Status} {item?.Name}：{item?.Description}<br />进度：{item?.ProgressMessage}<br />");
                            builder.Append($@"经验奖励：{item?.RewardTrackXp}");
                            builder.Append((item?.RerollCount > 0) ? "（可刷新）" : "");
                            builder.Append("</li><br />");
                        }
                        else
                        {
                            builder.Append("<li>");
                            builder.Append($@"{item?.TimeUntilNextQuest}");
                            builder.Append("</li>");
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
                builder.Append(@"<h4>每周任务</h4>");
                foreach (Hearthstone.DataModels.QuestDataModel item in weeklyQuestListDataModel.Quests)
                {
                    if (item != null)
                    {
                        if (item?.QuestId > 0)
                        {
                            builder.Append("<li>");
                            builder.Append($@"{item?.Status} {item?.Name}：{item?.Description}<br />进度：{item?.ProgressMessage}<br />");
                            builder.Append($@"经验奖励：{item?.RewardTrackXp}");
                            builder.Append((item?.RerollCount > 0) ? "（可刷新）" : "");
                            builder.Append("</li><br />");
                        }
                        else
                        {
                            builder.Append("<li>");
                            builder.Append($@"{item?.TimeUntilNextQuest}");
                            builder.Append("</li>");
                            break;
                        }
                    }
                }

                foreach (var questsType in (QuestPool.RewardTrackType[])Enum.GetValues(typeof(QuestPool.RewardTrackType)))
                {
                    foreach (Hearthstone.DataModels.QuestDataModel item in Hearthstone.Progression.QuestManager.Get().CreateActiveQuestsDataModel(Assets.QuestPool.QuestPoolType.NONE, questsType, true)?.Quests)
                    {
                        if (item == null)
                        {
                            continue;
                        }
                        specialQuestListDataModel.Quests.Add(item);
                    }
                    foreach (Hearthstone.DataModels.QuestDataModel item in Hearthstone.Progression.QuestManager.Get().CreateActiveQuestsDataModel(Assets.QuestPool.QuestPoolType.EVENT, questsType, true)?.Quests)
                    {
                        if (item == null)
                        {
                            continue;
                        }
                        specialQuestListDataModel.Quests.Add(item);
                    }
                }

                if (specialQuestListDataModel.Quests.Count >= 1 && specialQuestListDataModel.Quests[0].QuestId > 0)
                {
                    builder.Append(@"<h4>活动任务</h4>");
                    foreach (Hearthstone.DataModels.QuestDataModel item in specialQuestListDataModel.Quests.ToList().Where((x, i) => specialQuestListDataModel.Quests.ToList().FindIndex(z => z.QuestId == x.QuestId) == i).ToList())
                    {
                        if (item != null)
                        {
                            if (item?.QuestId > 0)
                            {
                                builder.Append("<li>");
                                builder.Append($@"{item.PoolType} {item?.Status} {item?.Name}：{item?.Description}<br />");
                                builder.Append($@"奖励：{item?.Rewards?.Description}<br />");
                                builder.Append($@"经验：{item?.RewardTrackXp}<br />进度：{item?.ProgressMessage}<br />");
                                if (item.NextInChain != 0)
                                {
                                    int nextQuestID = item.NextInChain;
                                    builder.Append("任务链：<br />");
                                    while (nextQuestID != 0)
                                    {
                                        var nextQuest = GameDbf.Quest.GetRecord(nextQuestID);
                                        if (nextQuest == null) break;
                                        builder.Append("<li>");
                                        builder.Append($@"{nextQuestID} {nextQuest?.Name?.GetString()}：{nextQuest?.Description?.GetString()}<br />");
                                        builder.Append("</li>");
                                        nextQuestID = nextQuest.NextInChain;
                                    }
                                }
                                builder.Append($@"距离活动结束还剩：" + (!String.IsNullOrEmpty(item.TimeUntilExpiration) ? item.TimeUntilExpiration.ToString() : "未知"));
                                builder.Append(" ");
                                builder.Append(item.Abandonable ? "（可放弃）" : "");
                                builder.Append((item?.RerollCount > 0) ? "(可刷新）" : "");
                                builder.Append("</li><br />");
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
                builder.Append(@"<h4>每周酒馆</h4>");
                foreach (Hearthstone.DataModels.QuestDataModel item in battlegroundsQuestListDataModel.Quests)
                {
                    if (item != null)
                    {
                        if (item?.QuestId > 0)
                        {
                            builder.Append("<li>");
                            builder.Append($@"{item?.Status} {item?.Name}：{item?.Description}<br />进度：{item?.ProgressMessage}<br />");
                            builder.Append($@"经验奖励：{item?.RewardTrackXp}");
                            builder.Append((item?.RerollCount > 0) ? "（可刷新）" : "");
                            builder.Append("</li><br />");
                        }
                        else
                        {
                            builder.Append("<li>");
                            builder.Append($@"{item?.TimeUntilNextQuest}");
                            builder.Append("</li>");
                            break;
                        }
                    }
                }
                builder.Append(@"<h4>佣兵任务</h4>");
                foreach (PegasusLettuce.MercenariesVisitorState mercenariesVisitorState in NetCache.Get().GetNetObject<NetCache.NetCacheMercenariesVillageVisitorInfo>().VisitorStates
                                                                                           .OrderByDescending(x => LettuceVillageDataUtil.CreateTaskModelFromTaskState(x.ActiveTaskState, null).TaskType)
                                                                                           .ThenBy(x => LettuceVillageDataUtil.CreateTaskModelFromTaskState(x.ActiveTaskState, null).MercenaryId)
                                                                                           .ToList())
                {
                    Hearthstone.DataModels.MercenaryVillageTaskItemDataModel mercenaryVillageTaskItemDataModel = LettuceVillageDataUtil.CreateTaskModelFromTaskState(mercenariesVisitorState.ActiveTaskState, null);
                    builder.Append("<li>");
                    builder.Append($"[{mercenaryVillageTaskItemDataModel.TaskType}] [{mercenaryVillageTaskItemDataModel.MercenaryName}]&emsp;");
                    if (mercenaryVillageTaskItemDataModel.TaskType == Assets.MercenaryVisitor.VillageVisitorType.STANDARD)
                    {
                        builder.Append($@"任务{mercenaryVillageTaskItemDataModel.TaskChainIndex + 1} - ");
                    }
                    builder.Append($"{mercenaryVillageTaskItemDataModel.Title}<br />{mercenaryVillageTaskItemDataModel.Description}<br />");
                    builder.Append($"任务奖励：{mercenaryVillageTaskItemDataModel.RewardList.Description}<br />");
                    builder.Append($"任务进度：{mercenaryVillageTaskItemDataModel.ProgressMessage}");
                    if (mercenaryVillageTaskItemDataModel.IsTimedEvent)
                        builder.Append($"<br />剩余时间：{mercenaryVillageTaskItemDataModel.RemainingEventTime}<br />");
                    builder.Append("</li><br />");
                }


            }
            catch (Exception ex)
            {
                builder.Append($@"任务信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p><br />");
            }
            finally
            {
                builder.Append("<hr />");
            }

            return Template(builder, "Info");
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
            StringBuilder builder = new StringBuilder();
            builder.Append(@"<h3 style=""text-align: center;"">佣兵关卡</h3>");

            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>索引</th>";
                temp += "<th>位置</th>";
                temp += "<th>名称</th>";
                temp += "<th>难度</th>";
                temp += "<th>碎片1</th>";
                temp += "<th>碎片2</th>";
                temp += "<th>碎片3</th>";
                temp += "</tr>";
                builder.Append(temp);

                foreach (var record in GameDbf.LettuceBounty.GetRecords().OrderBy(x => x.ID).ToList())
                {
                    if (record != null)
                    {
                        bool isComplete = MercenariesDataUtil.IsBountyComplete(record.ID);
                        temp = "<tr>";
                        temp += (isComplete ? "<td>" : "<td style=\"color:#FF4136\">") + $"{record.ID}</td>";
                        temp += (isComplete ? "<td>" : "<td style=\"color:#FF4136\">") + $"{record.BountySetRecord.Name.GetString()}</td>";
                        temp += (isComplete ? "<td>" : "<td style=\"color:#FF4136\">") + $"{GameDbf.Card.GetRecord(record.FinalBossCardId).Name.GetString()}</td>";
                        temp += (isComplete ? "<td>" : "<td style=\"color:#FF4136\">") + (record.Heroic ? "英雄" : "普通") + "</td>";

                        foreach (var finalReward in record.FinalBossRewards)
                        {
                            foreach (var merc in GameDbf.LettuceMercenary.GetRecords())
                            {

                                if (merc != null && merc.ID == finalReward.RewardMercenaryId)
                                {
                                    switch ((TAG_RARITY)merc.Rarity)
                                    {
                                        case TAG_RARITY.LEGENDARY:
                                            temp += $@"<td style=""color:#FFCC00"">{merc.MercenaryArtVariations[0].CardRecord.Name.GetString()}</td>";
                                            break;
                                        case TAG_RARITY.EPIC:
                                            temp += $@"<td style=""color:#CC99CC"">{merc.MercenaryArtVariations[0].CardRecord.Name.GetString()}</td>";
                                            break;
                                        case TAG_RARITY.RARE:
                                            temp += $@"<td style=""color:#99CCFF"">{merc.MercenaryArtVariations[0].CardRecord.Name.GetString()}</td>";
                                            break;
                                        default:
                                            temp += $@"<td>{merc.MercenaryArtVariations[0].CardRecord.Name.GetString()}</td>";
                                            break;
                                    }
                                    break;
                                }
                            }

                        }

                        temp += "</tr>";
                        builder.Append(temp);
                    }

                }
                builder.Append("</table>");
            }
            catch (Exception ex)
            {
                builder.Append($@"佣兵关卡信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p><br />");
            }
            finally
            {
                Utils.LeakInfo.Mercenaries();
            }
            return Template(builder, "Mercenaries");
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

                foreach (var record in GameDbf.CosmeticCoin.GetRecords().OrderBy(x => x.ID).ToList())
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
            StringBuilder builder = new StringBuilder();
            builder.Append(@"<h3 style=""text-align: center;"">佣兵收藏</h3>");
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
                temp += "<th>技能1</th>";
                temp += "<th>技能2</th>";
                temp += "<th>技能3</th>";
                temp += "<th>装备1</th>";
                temp += "<th>装备2</th>";
                temp += "<th>装备3</th>";
                temp += "</tr>";
                builder.Append(temp);

                foreach (var merc in CollectionManager.Get().FindMercenaries(null, null, null, null, null).m_mercenaries
                                    .OrderByDescending(x => x.m_owned)
                                    //.ThenByDescending(x => Utils.IsMercenaryFullyUpgraded(x))
                                    .ThenByDescending(x => x.m_isFullyUpgraded)
                                    .ThenByDescending(x => x.m_level)
                                    .ThenByDescending(x => Utils.CalcMercenaryCoinNeed(x))
                                    .ThenByDescending(x => x.m_currencyAmount)
                                    .ToList())
                {
                    if (merc != null)
                    {
                        temp = "<tr>";
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
                        //else if (Utils.IsMercenaryFullyUpgraded(merc))
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

                        foreach (var ability in merc.m_abilityList)
                        {
                            if (ability != null)
                                temp += "<td>" + ability.GetCardName() + "</td>";
                        }
                        foreach (var equipment in merc.m_equipmentList)
                        {
                            if (equipment != null)
                                temp += (equipment.Owned ? "<td>" : "<td style=\"color:#FF4136\">") + equipment.GetCardName() + "</td>";
                        }

                        builder.Append(temp);

                        builder.Append("</tr>");
                    }
                }
                builder.Append("</table>");
            }
            catch (Exception ex)
            {
                builder.Append($@"佣兵收藏信息获取异常<br /><p style=""white-space: pre-line;"">{ex}</p>");
            }
            finally
            {
                builder.Append("<br />");
            }
            return Template(builder, "Mercenaries", false);
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

            int totalOpened = 0;
            int totalRemain = 0;

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
                        totalOpened += opened;
                        totalRemain += remainCount;
                        temp += "<td>" + ((opened == 0) ? "-" : opened.ToString()) + "</td>";
                        temp += "<td>" + ((remainCount == 0) ? "-" : remainCount.ToString()) + "</td>";
                        temp += "</tr>";
                    }
                }
                body += temp;
                body += $"</table><br /><p>您一共开了{totalOpened}包，目前还有{totalRemain}包未开。</p>";
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
            if (!System.IO.File.Exists(CommandConfig.hsMatchLogPath)) return Template(builder.Append("对局文件不存在！"), "MatchLog");
            else builder.Append(@"<h3 style=""text-align: center;"">对局记录</h3>");

            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>结束时间</th>";
                temp += "<th>对局结果</th>";
                temp += "<th>当前排名</th>";
                temp += "<th>游戏模式</th>";
                temp += "<th>你的对手</th>";
                temp += "<th>玩家信息</th>";
                temp += "</tr>";
                builder.Append(temp);

                foreach (string line in System.IO.File.ReadLines(CommandConfig.hsMatchLogPath).Reverse())
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
                                else if (lineSplit[i] == "第一名") temp += $"<td style=\"color:#01FF70\">第一名</td>";
                                else if (lineSplit[i] == "第二名") temp += $"<td style=\"color:#01FF70\">第二名</td>";
                                else if (lineSplit[i] == "第三名") temp += $"<td style=\"color:#01FF70\">第三名</td>";
                                else if (lineSplit[i] == "第四名") temp += $"<td style=\"color:#01FF70\">第四名</td>";
                                else if (lineSplit[i] == "第五名") temp += $"<td style=\"color:#FF4136\">第五名</td>";
                                else if (lineSplit[i] == "第六名") temp += $"<td style=\"color:#FF4136\">第六名</td>";
                                else if (lineSplit[i] == "第七名") temp += $"<td style=\"color:#FF4136\">第七名</td>";
                                else if (lineSplit[i] == "第八名") temp += $"<td style=\"color:#FF4136\">第八名</td>";
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
            return Template(builder, "MatchLog", false);
        }

        public static StringBuilder AlivePage()
        {
            return new StringBuilder().Append($"{{\"pid\":{System.Diagnostics.Process.GetCurrentProcess()?.Id},\"login\":\"{Utils.CacheLoginStatus}\"}}");
        }

        public static StringBuilder BepInExLogPage(int lines = -1)
        {
            string logPath = Path.Combine(BepInEx.Paths.BepInExRootPath, "LogOutput.log");
            StringBuilder output = new StringBuilder().Append("");

            if (File.Exists(logPath))
            {
                try
                {
                    if (lines <= 0)
                    {
                        // 以共享模式打开文件
                        using (FileStream fs = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            using (StreamReader reader = new StreamReader(fs))
                            {
                                string content = reader.ReadToEnd();
                                output.Append(content);
                            }
                        }
                    }
                    else
                    {
                        output.Append(Utils.ReadLastLine(logPath, 666));
                    }
                }
                catch (Exception ex)
                {
                    output.Append(ex.Message);
                }
            }
            return output;
        }
        public static StringBuilder HsModCfgPage(string cfg)
        {
            string cfgPath = Path.Combine(BepInEx.Paths.ConfigPath, CommandConfig.GlobalHSUnitID, cfg);
            StringBuilder output = new StringBuilder().Append("");

            if (File.Exists(cfgPath))
            {
                try
                {
                    // 以共享模式打开文件
                    using (FileStream fs = new FileStream(cfgPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader reader = new StreamReader(fs))
                        {
                            string content = reader.ReadToEnd();
                            output.Append(content);
                        }
                    }
                }
                catch (Exception ex)
                {
                    output.Append(ex.Message);
                }
            }
            return output;
        }
    }
}
