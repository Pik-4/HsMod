using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
//background: url({webPageBackImg.Value}) no-repeat top left;
background-size: cover;
background-repeat:no-repeat;
background-position:center;
background-attachment:fixed;
opacity:1.0;
//filter:alpha(opacity=100);
//display: flex;
align-items: center;
justify-content: center;
//text-align:center;
//margin: 0;
}}
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
//background: url({webPageBackImg.Value}) no-repeat top left;
background-size: cover;
background-repeat:no-repeat;
background-position:center;
background-attachment:fixed;
opacity:1.0;
//filter:alpha(opacity=100);
//display: flex;
align-items: center;
justify-content: center;
//text-align:center;
//margin: 0;
}}
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
            if (System.IO.File.Exists(mercLogPath.Value)) btn += @"<a href=""/mercmatchlog""><button class=""btn_li"">佣兵对局</button><br/></a><br/>";
            btn += @"<a href=""/test""><button class=""btn_li"">测试界面</button><br/></a><br/>";
            string body = @"<h1 style=""text-align: center; opacity: 0.6;"">HsMod</h1>";
            body += $@"<div style=""text-align: center; width: auto; position: absolute; top: 50%; left: 50%; transform: translate(-50%,-50%);"">{btn}</div>";
            return Template("index", body);
        }

        public static StringBuilder TestPage()
        {
            string body = $@"<div style=""text-align: center;""><p>Test page</p></div>";
            return Template("Test", body);
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
                Hearthstone.DataModels.RewardTrackDataModel trackDataModel = Hearthstone.Progression.RewardTrackManager.Get().TrackDataModel;
                result += $"等级：{trackDataModel.Level}<br />";
                result += "进度：" + ((trackDataModel.Level == 400 && trackDataModel.Xp == 0) ? "已满级！" : trackDataModel.XpProgress) + "<br />";
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
                foreach (Hearthstone.DataModels.QuestDataModel item in Hearthstone.Progression.QuestManager.Get().CreateActiveQuestsDataModel(Assets.QuestPool.QuestPoolType.DAILY, true).Quests)
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

                foreach (Hearthstone.DataModels.QuestDataModel item in Hearthstone.Progression.QuestManager.Get().CreateActiveQuestsDataModel(Assets.QuestPool.QuestPoolType.WEEKLY, true).Quests)
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


                foreach (Hearthstone.DataModels.QuestDataModel item in Hearthstone.Progression.QuestManager.Get().CreateActiveQuestsDataModel(Assets.QuestPool.QuestPoolType.NONE, true).Quests)
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

        public static StringBuilder MercMatchLogPage()
        {
            StringBuilder builder = new StringBuilder();
            if (!System.IO.File.Exists(mercLogPath.Value)) return Template(builder.Append("对局文件不存在！"), "MercMatchLog");
            else builder.Append(@"<h3 style=""text-align: center;"">佣兵对局记录</h3>");

            try
            {
                string temp = @"<table border=0 style=""text-align: center;""><tr>";
                temp += "<th>结束时间</th>";
                temp += "<th>对局结果</th>";
                temp += "<th>当前分数</th>";
                temp += "<th>友方卡组</th>";
                temp += "<th>你的对手</th>";
                temp += "<th>敌方阵容</th>";
                temp += "</tr>";
                builder.Append(temp);

                foreach (string line in System.IO.File.ReadLines(mercLogPath.Value).Reverse())
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
                                if (int.Parse(lineSplit[i]) > 0) temp += $"<td style=\"color:#01FF70\">+{int.Parse(lineSplit[i])}</td>";
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
            return Template(builder, "MercMatchLog").Remove(108, 126);
        }

        public static StringBuilder AlivePage()
        {
            return new StringBuilder().Append(System.Diagnostics.Process.GetCurrentProcess()?.Id.ToString());
        }

    }
}
