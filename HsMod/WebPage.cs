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
            string btn = @" <a href=""/info""><button class=""btn_li"">Main Info</button><br/></a><br />";
            btn += @"<a href=""/pack""><button class=""btn_li"">Pack Info</button><br/></a><br />";
            btn += @"<a href=""/collection""><button class=""btn_li"">Collection</button><br /></a><br />";
            btn += @"<a href=""/skins""><button class=""btn_li"">Skins</button><br /></a><br />";
            btn += @"<a href=""/lettuce""><button class=""btn_li"">Mercenary Bounties</button><br /></a><br />";
            btn += @"<a href=""/mercenaries""><button class=""btn_li"">Mercenaries</button><br /></a><br />";
            if (System.IO.File.Exists(CommandConfig.hsMatchLogPath)) btn += @"<a href=""/matchlog""><button class=""btn_li"">Match Log</button><br /></a><br />";
            string configUrl = File.Exists(Path.Combine(PluginConfig.HsModWebSite, "config", "index.html")) ? "/config/index.html" : "/config";
            btn += $@"<a href=""{configUrl}""><button class=""btn_li"">{LocalizationManager.GetLangValue("config.page.button")}</button><br /></a><br />";
            btn += @"<a href=""/about""><button class=""btn_li"">About</button><br /></a><br />";
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
            builder.AppendLine(@"<h3 style=""text-align: center;"">About HsMod</h3>");
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

            builder.Append(@"<h3 style=""text-align: center;"">Process Info</h3>");
            builder.Append("PID: ");
            builder.Append(System.Diagnostics.Process.GetCurrentProcess()?.Id.ToString());
            builder.Append("<br />");
            builder.Append("<hr />");
            builder.Append(@"<h3 style=""text-align: center;"">Basic Info</h3>");
            NetCache netCache = NetCache.Get();
            try
            {
                builder.Append("Account: ");
                builder.Append(BnetPresenceMgr.Get()?.GetMyPlayer()?.GetBattleTag()?.ToString());
                builder.Append("<br />");
                builder.Append("Gold: ");
                builder.Append(netCache?.GetGoldBalance().ToString());
                builder.Append("<br />");
                builder.Append("Arcane Dust: ");
                builder.Append(netCache?.GetArcaneDustBalance().ToString());
                builder.Append("<br />");
                builder.Append("Arena Tickets: ");
                builder.Append(netCache?.GetArenaTicketBalance().ToString());
                builder.Append("<br />");
            }
            catch (Exception ex)
            {
                builder.Append($@"Error fetching basic info<br /><p style=""white-space: pre-line;"">{ex}</p><br />");
            }
            finally
            {
                builder.Append("<hr />");
            }
            builder.Append(@"<h3 style=""text-align: center;"">Reward Track</h3>");
            try
            {
                Hearthstone.DataModels.RewardTrackDataModel trackDataModel = Hearthstone.Progression.RewardTrackManager.Get().GetRewardTrack(Global.RewardTrackType.GLOBAL).TrackDataModel;
                builder.Append($"Hearthstone: {trackDataModel.Level}&emsp;&emsp;");
                builder.Append("Progress: " + ((trackDataModel.Level == trackDataModel.LevelHardCap && trackDataModel.Xp == 0) ? "Max level!" : trackDataModel.XpProgress) + "<br />");
                trackDataModel = Hearthstone.Progression.RewardTrackManager.Get().GetRewardTrack(Global.RewardTrackType.BATTLEGROUNDS).TrackDataModel;
                builder.Append($"Battlegrounds: {trackDataModel.Level}&emsp;&emsp;");
                builder.Append("Progress: " + ((trackDataModel.Level == trackDataModel.LevelHardCap && trackDataModel.Xp == 0) ? "Max level!" : trackDataModel.XpProgress) + "<br />");
            }
            catch (Exception ex)
            {
                builder.Append($@"Error fetching reward track<br /><p style=""white-space: pre-line;"">{ex}</p><br />");
            }
            finally
            {
                builder.Append("<hr />");
            }
            builder.Append(@"<h3 style=""text-align: center;"">Ladder Info</h3>");
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
                            rankMode = "Classic";
                            break;
                        case 2:
                            currentMedal = localPlayerMedalInfo.GetCurrentMedal(PegasusShared.FormatType.FT_STANDARD);
                            rankMode = "Standard";
                            break;
                        case 3:
                            currentMedal = localPlayerMedalInfo.GetCurrentMedal(PegasusShared.FormatType.FT_WILD);
                            rankMode = "Wild";
                            break;
                        case 4:
                            currentMedal = localPlayerMedalInfo.GetCurrentMedal(PegasusShared.FormatType.FT_TWIST);
                            rankMode = "Twist";
                            break;
                    }
                    string rankName = Utils.RankIdxToString(currentMedal.starLevel);
                    string detail = (rankName == "Legend") ? currentMedal.legendIndex.ToString() + " place" : currentMedal.earnedStars.ToString() + " stars";
                    builder.Append($@"{rankMode}: {rankName}&emsp;{detail}&emsp;&emsp;");
                    builder.Append($@"Season record: {currentMedal.seasonWins}W - {currentMedal.seasonGames}G");
                    builder.Append($@" ({string.Format("{0:P1}", (float)currentMedal.seasonWins / (float)currentMedal.seasonGames)})<br />");
                }
                NetCache.NetCacheMercenariesPlayerInfo mercenariesPlayerInfo = NetCache.Get()?.GetNetObject<NetCache.NetCacheMercenariesPlayerInfo>();
                builder.Append($@"Mercenaries: {mercenariesPlayerInfo.PvpRating} (current) - {mercenariesPlayerInfo.PvpSeasonHighestRating} (best)&emsp;&emsp;");
                builder.Append($@"Chest progress: {mercenariesPlayerInfo.PvpRewardChestWinsProgress}/{mercenariesPlayerInfo.PvpRewardChestWinsRequired} <br />");
            }
            catch (Exception ex)
            {
                builder.Append($@"Error fetching ladder info<br /><p style=""white-space: pre-line;"">{ex}</p><br />");

            }
            finally
            {
                builder.Append("<hr />");
            }
            builder.Append(@"<h3 style=""text-align: center;"">Quests</h3>");
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
                builder.Append(@"<h4>Daily Quests</h4>");
                foreach (Hearthstone.DataModels.QuestDataModel item in dailyQuestListDataModel.Quests)
                {
                    if (item != null)
                    {
                        if (item?.QuestId > 0)
                        {
                            builder.Append("<li>");
                            builder.Append($@"{item?.Status} {item?.Name}: {item?.Description}<br />Progress: {item?.ProgressMessage}<br />");
                            builder.Append($@"XP Reward: {item?.RewardTrackXp}");
                            builder.Append((item?.RerollCount > 0) ? " (Refreshable)" : "");
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
                builder.Append(@"<h4>Weekly Quests</h4>");
                foreach (Hearthstone.DataModels.QuestDataModel item in weeklyQuestListDataModel.Quests)
                {
                    if (item != null)
                    {
                        if (item?.QuestId > 0)
                        {
                            builder.Append("<li>");
                            builder.Append($@"{item?.Status} {item?.Name}: {item?.Description}<br />Progress: {item?.ProgressMessage}<br />");
                            builder.Append($@"XP Reward: {item?.RewardTrackXp}");
                            builder.Append((item?.RerollCount > 0) ? " (Refreshable)" : "");
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
                    builder.Append(@"<h4>Event Quests</h4>");
                    foreach (Hearthstone.DataModels.QuestDataModel item in specialQuestListDataModel.Quests.ToList().Where((x, i) => specialQuestListDataModel.Quests.ToList().FindIndex(z => z.QuestId == x.QuestId) == i).ToList())
                    {
                        if (item != null)
                        {
                            if (item?.QuestId > 0)
                            {
                                builder.Append("<li>");
                                builder.Append($@"{item.PoolType} {item?.Status} {item?.Name}: {item?.Description}<br />");
                                builder.Append($@"Reward: {item?.Rewards?.Description}<br />");
                                builder.Append($@"XP: {item?.RewardTrackXp}<br />Progress: {item?.ProgressMessage}<br />");
                                if (item.NextInChain != 0)
                                {
                                    int nextQuestID = item.NextInChain;
                                    builder.Append("Quest chain:<br />");
                                    while (nextQuestID != 0)
                                    {
                                        var nextQuest = GameDbf.Quest.GetRecord(nextQuestID);
                                        if (nextQuest == null) break;
                                        builder.Append("<li>");
                                        builder.Append($@"{nextQuestID} {nextQuest?.Name?.GetString()}: {nextQuest?.Description?.GetString()}<br />");
                                        builder.Append("</li>");
                                        nextQuestID = nextQuest.NextInChain;
                                    }
                                }
                                builder.Append($@"Time until event ends: " + (!String.IsNullOrEmpty(item.TimeUntilExpiration) ? item.TimeUntilExpiration.ToString() : "Unknown"));
                                builder.Append(" ");
                                builder.Append(item.Abandonable ? "(Abandonable)" : "");
                                builder.Append((item?.RerollCount > 0) ? " (Refreshable)" : "");
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
                builder.Append(@"<h4>Weekly Battlegrounds</h4>");
                foreach (Hearthstone.DataModels.QuestDataModel item in battlegroundsQuestListDataModel.Quests)
                {
                    if (item != null)
                    {
                        if (item?.QuestId > 0)
                        {
                            builder.Append("<li>");
                            builder.Append($@"{item?.Status} {item?.Name}: {item?.Description}<br />Progress: {item?.ProgressMessage}<br />");
                            builder.Append($@"XP Reward: {item?.RewardTrackXp}");
                            builder.Append((item?.RerollCount > 0) ? " (Refreshable)" : "");
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
                builder.Append(@"<h4>Mercenary Tasks</h4>");
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
                        builder.Append($@"Task {mercenaryVillageTaskItemDataModel.TaskChainIndex + 1} - ");
                    }
                    builder.Append($"{mercenaryVillageTaskItemDataModel.Title}<br />{mercenaryVillageTaskItemDataModel.Description}<br />");
                    builder.Append($"Reward: {mercenaryVillageTaskItemDataModel.RewardList.Description}<br />");
                    builder.Append($"Progress: {mercenaryVillageTaskItemDataModel.ProgressMessage}");
                    if (mercenaryVillageTaskItemDataModel.IsTimedEvent)
                        builder.Append($"<br />Time remaining: {mercenaryVillageTaskItemDataModel.RemainingEventTime}<br />");
                    builder.Append("</li><br />");
                }


            }
            catch (Exception ex)
            {
                builder.Append($@"Error fetching quest info<br /><p style=""white-space: pre-line;"">{ex}</p><br />");
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
            body += @"<h3 style=""text-align: center;"">Disenchantable Cards</h3>";
            Utils.CardCount cards = new Utils.CardCount();
            List<Utils.CollectionCard> collectionCards = new List<Utils.CollectionCard>();
            try
            {
                if (SceneMgr.Get().GetMode() != SceneMgr.Mode.COLLECTIONMANAGER)
                {
                    throw new Exception();
                }
                string temp = @"<table border=0 style=""text-align: center;""><tr><th>Card Name</th><th>Quality</th><th>Count</th></tr>";
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

                body += $"<li>Total cards: {cards.total}, golden: {cards.gTotal}</li>";
                body += $"<li>Common cards: {cards.common + cards.gCommon}, golden: {cards.gCommon}</li>";
                body += $"<li>Rare cards: {cards.rare + cards.gRare}, golden: {cards.gRare}</li>";
                body += $"<li>Epic cards: {cards.epic + cards.gEpic}, golden: {cards.gEpic}</li>";
                body += $"<li>Legendary cards: {cards.legendary + cards.gLegendary}, golden: {cards.gLegendary}</li>";
                body += $"<li>Disenchantable Arcane Dust: {cards.totalDust}</li>";
                body += "<br /><hr />";
                //body += temp;
            }
            catch (Exception ex)
            {
                body += $@"Error fetching card info; please re-enter Collection mode.<br /><p style=""white-space: pre-line;"">{ex}</p><br />";
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
            builder.Append(@"<h3 style=""text-align: center;"">Mercenary Bounties</h3>");

            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>ID</th>";
                temp += "<th>Zone</th>";
                temp += "<th>Name</th>";
                temp += "<th>Difficulty</th>";
                temp += "<th>Shard 1</th>";
                temp += "<th>Shard 2</th>";
                temp += "<th>Shard 3</th>";
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
                        temp += (isComplete ? "<td>" : "<td style=\"color:#FF4136\">") + (record.Heroic ? "Heroic" : "Normal") + "</td>";

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
                builder.Append($@"Error fetching mercenary bounties<br /><p style=""white-space: pre-line;"">{ex}</p><br />");
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
            string body = @"<h3 style=""text-align: center;"">Skins</h3>";
            builder.Append(body);

            body = "<h4>Pets</h4>";
            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";

                temp += "<th>ID</th>";
                temp += "<th>Pet</th>";
                temp += "<th>Name</th>";
                temp += "</tr>";

                foreach (var record in GameDbf.PetVariant.GetRecords().OrderBy(x => x.ID).ToList())
                {
                    if (record != null)
                    {
                        temp += "<tr>";
                        temp += $"<td>{record.ID}</td>";
                        temp += $"<td>{record.PetId}</td>";

                        temp += $"<td>{record.Name.GetString()}</td>";
                        temp += "</tr>";
                    }

                }

                temp += "</table>";
                body += temp;
            }
            catch (Exception ex)
            {
                body += $@"Error fetching pet info<br /><p style=""white-space: pre-line;"">{ex}</p>";
            }
            finally
            {
                body += "<br /><hr />";
            }
            builder.Append(body);



            body = "<h4>Coins</h4>";
            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>ID</th>";
                temp += "<th>Name</th>";
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
                body += $@"Error fetching coin info<br /><p style=""white-space: pre-line;"">{ex}</p>";
            }
            finally
            {
                body += "<br /><hr />";
            }
            builder.Append(body);


            body = "<h4>Card Backs</h4>";
            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>ID</th>";
                temp += "<th>Name</th>";
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
                body += $@"Error fetching card back info<br /><p style=""white-space: pre-line;"">{ex}</p>";
            }
            finally
            {
                body += "<br /><hr />";
            }
            builder.Append(body);


            body = "<h4>Battlegrounds Boards</h4>";
            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>ID</th>";
                temp += "<th>Name</th>";
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
                body += $@"Error fetching Battlegrounds board info<br /><p style=""white-space: pre-line;"">{ex}</p>";
            }
            finally
            {
                body += "<br /><hr />";
            }
            builder.Append(body);


            body = "<h4>Battlegrounds Finishers</h4>";
            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>ID</th>";
                temp += "<th>Name</th>";
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
                body += $@"Error fetching Battlegrounds finisher info<br /><p style=""white-space: pre-line;"">{ex}</p>";
            }
            finally
            {
                body += "<br /><hr />";
                //Utils.LeakInfo.Skins();
            }
            builder.Append(body);


            body = "<h4>Heroes</h4>";
            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>ID</th>";
                temp += "<th>Name</th>";
                temp += "<th>Type</th>";
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
                                temp += "<td>Battlegrounds Hero</td>";
                                break;
                            case Assets.CardHero.HeroType.BATTLEGROUNDS_GUIDE:
                                temp += "<td>Battlegrounds Bob</td>";
                                break;
                            default:
                                temp += "<td>Constructed Hero</td>";
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
                body += $@"Error fetching hero info<br /><p style=""white-space: pre-line;"">{ex}</p>";
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
            builder.Append(@"<h3 style=""text-align: center;"">Mercenaries</h3>");
            try
            {
                if (CollectionManager.Get() == null)
                {
                    throw new Exception();
                }
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>Name</th>";
                temp += "<th>Level</th>";
                temp += "<th>Coins</th>";
                temp += "<th>Status</th>";
                temp += "<th>Ability 1</th>";
                temp += "<th>Ability 2</th>";
                temp += "<th>Ability 3</th>";
                temp += "<th>Equip 1</th>";
                temp += "<th>Equip 2</th>";
                temp += "<th>Equip 3</th>";
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
                            temp += "<td>" + ((merc.GetCraftingCost() - merc.m_currencyAmount > 0) ? $"Needs {merc.GetCraftingCost() - merc.m_currencyAmount} coins to craft" : "Craftable!") + "</td>";
                        }
                        //else if (Utils.IsMercenaryFullyUpgraded(merc))
                        else if (merc.m_isFullyUpgraded)
                        {
                            temp += "<td>Fully maxed! (+1+5)</td>";
                        }
                        else
                        {
                            long coinNeed = Utils.CalcMercenaryCoinNeed(merc);
                            if (coinNeed != 8192)
                                temp += $"<td>Coins needed to fully max: {coinNeed}</td>";
                            else
                            {
                                temp += $"<td>Can upgrade to fully max!</td>";
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
                builder.Append($@"Error fetching mercenary collection<br /><p style=""white-space: pre-line;"">{ex}</p>");
            }
            finally
            {
                builder.Append("<br />");
            }
            return Template(builder, "Mercenaries", false);
        }

        public static StringBuilder PackPage()
        {
            string body = @"<h3 style=""text-align: center;"">Pack Info</h3>";
            string temp = @"<table border=0 style=""text-align: center;""><tr>";
            temp += "<th>ID</th>";
            temp += "<th>Name</th>";
            temp += "<th>Opened</th>";
            temp += "<th>Remaining</th>";
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
                                name = "Unknown";
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
                body += $"</table><br /><p>You have opened {totalOpened} packs in total. {totalRemain} packs remain unopened.</p>";
            }
            catch (Exception ex)
            {
                body += $@"Error fetching pack info<br /><p style=""white-space: pre-line;"">{ex}</p>";
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
            if (!System.IO.File.Exists(CommandConfig.hsMatchLogPath)) return Template(builder.Append("Match log file not found!"), "MatchLog");
            else builder.Append(@"<h3 style=""text-align: center;"">Match History</h3>");

            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>End Time</th>";
                temp += "<th>Result</th>";
                temp += "<th>Current Rank</th>";
                temp += "<th>Game Mode</th>";
                temp += "<th>Opponent</th>";
                temp += "<th>Player Info</th>";
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
                                if (lineSplit[i] == "Victory" || lineSplit[i] == "胜利") temp += $"<td style=\"color:#01FF70\">Victory</td>";
                                else if (lineSplit[i] == "Defeat" || lineSplit[i] == "失败") temp += $"<td style=\"color:#FF4136\">Defeat</td>";
                                else if (lineSplit[i] == "Unknown" || lineSplit[i] == "Draw" || lineSplit[i] == "未知" || lineSplit[i] == "平局") temp += $"<td>{lineSplit[i]}</td>";
                                else if (lineSplit[i] == "1st Place" || lineSplit[i] == "第一名") temp += $"<td style=\"color:#01FF70\">1st Place</td>";
                                else if (lineSplit[i] == "2nd Place" || lineSplit[i] == "第二名") temp += $"<td style=\"color:#01FF70\">2nd Place</td>";
                                else if (lineSplit[i] == "3rd Place" || lineSplit[i] == "第三名") temp += $"<td style=\"color:#01FF70\">3rd Place</td>";
                                else if (lineSplit[i] == "4th Place" || lineSplit[i] == "第四名") temp += $"<td style=\"color:#01FF70\">4th Place</td>";
                                else if (lineSplit[i] == "5th Place" || lineSplit[i] == "第五名") temp += $"<td style=\"color:#FF4136\">5th Place</td>";
                                else if (lineSplit[i] == "6th Place" || lineSplit[i] == "第六名") temp += $"<td style=\"color:#FF4136\">6th Place</td>";
                                else if (lineSplit[i] == "7th Place" || lineSplit[i] == "第七名") temp += $"<td style=\"color:#FF4136\">7th Place</td>";
                                else if (lineSplit[i] == "8th Place" || lineSplit[i] == "第八名") temp += $"<td style=\"color:#FF4136\">8th Place</td>";
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
                builder.Append($@"</table>Log parse error<br /><p style=""white-space: pre-line;"">{ex}</p>");
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

        public static StringBuilder ConfigPage()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(@"<h3 style=""text-align: center;"">" + LocalizationManager.GetLangValue("config.page.title") + "</h3>");

            string configHtml = FileManager.ReadEmbeddedFile("./WebResources/config.html");
            configHtml = configHtml
                .Replace("{config.page.language}", LocalizationManager.GetLangValue("config.page.language"))
                .Replace("{config.page.lang.auto}", LocalizationManager.GetLangValue("config.page.lang.auto"))
                .Replace("{config.page.search}", LocalizationManager.GetLangValue("config.page.search"))
                .Replace("{config.page.success}", LocalizationManager.GetLangValue("config.page.success"))
                .Replace("{config.page.error}", LocalizationManager.GetLangValue("config.page.error"))
                .Replace("{config.page.advanced}", LocalizationManager.GetLangValue("config.page.advanced"))
                .Replace("{config.page.warning}", LocalizationManager.GetLangValue("config.page.warning"))
                .Replace("{config.page.warning.desc}", LocalizationManager.GetLangValue("config.page.warning.desc"))
                .Replace("{config.page.cancel}", LocalizationManager.GetLangValue("config.page.cancel"))
                .Replace("{config.page.confirm}", LocalizationManager.GetLangValue("config.page.confirm"));

            builder.AppendLine(configHtml);
            return Template(builder, "Config");
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
