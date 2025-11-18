using BepInEx.Logging;
using PegasusUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using static HsMod.PluginConfig;

namespace HsMod
{
    public partial class Utils
    {
        public enum CardState
        {
            //[Description("默认（不做修改）")]
            Default,
            //[Description("仅友方有效")]
            OnlyMy,
            //[Description("全部生效")]
            All,
            //[Description("禁用特效")]
            Disabled
        }
        //public enum QuickMode
        //{
        //    [Description("默认（禁用）")]
        //    Default,
        //    [Description("酒馆战棋")]
        //    Battlegrounds,
        //    [Description("佣兵战纪")]
        //    Mercenaries,
        //    [Description("全部模式")]
        //    AllMode,
        //    [Description("禁用")]
        //    Disabled
        //}
        public enum SkinType
        {
            [Description("卡背")]
            CARDBACK,
            [Description("卡牌")]
            CARD,
            [Description("硬币")]
            COIN,
            [Description("英雄皮肤")]
            HERO,
            [Description("酒馆鲍勃")]
            BOB,
            [Description("酒馆终结特效")]
            BATTLEGROUNDSFINISHER,
            [Description("酒馆战场")]
            BATTLEGROUNDSBOARD,
            [Description("酒馆英雄皮肤")]
            BATTLEGROUNDSHERO,
            [Description("英雄技能")]
            HEROPOWER,
        }
        public enum AlertPopupResponse
        {
            OK = AlertPopup.Response.OK,
            CONFIRM = AlertPopup.Response.CONFIRM,
            CANCEL = AlertPopup.Response.CANCEL,
            YES,
            DONOTHING
        }
        public enum ConfigTemplate
        {
            [Description("默认")]
            DoNothing,
            [Description("挂机")]
            AwayFromKeyboard,
            [Description("反挂机")]
            AntiAwayFromKeyboard
        }
        public enum BuyAdventureTemplate
        {
            [Description("默认")]
            DoNothing,
            [Description("纳克萨玛斯的诅咒")]
            BuyNAX,
            [Description("黑石山的火焰")]
            BuyBRM,
            [Description("探险者协会")]
            BuyLOE,
            [Description("卡拉赞之夜")]
            BuyKara
        }
        public enum CardRarity    // 卡牌稀有度
        {
            COMMON = TAG_RARITY.COMMON,
            RARE = TAG_RARITY.RARE,
            EPIC = TAG_RARITY.EPIC,
            LEGENDARY = TAG_RARITY.LEGENDARY
        }
        public enum DevicePreset
        {
            Default,
            iPad,
            iPhone,
            Phone,
            Tablet,
            HuaweiPhone,
            Custom
        }


        public class CardCount
        {
            public int legendary = 0;
            public int epic = 0;
            public int rare = 0;
            public int common = 0;
            public int total = 0;
            public int gLegendary = 0;
            public int gEpic = 0;
            public int gRare = 0;
            public int gCommon = 0;
            public int gTotal = 0;
            public int totalDust = 0;
        };

        public struct CollectionCard
        {
            public string Name;
            public int Count;
            public TAG_RARITY Rarity;
            public TAG_PREMIUM Premium;
        }

        public struct MercenarySkin
        {
            public string Name;
            public List<int> Id;
            public bool hasDiamond;
            public int Diamond;
            public int Default;
        }

        public static string CardsCount(TAG_RARITY Rarity, TAG_PREMIUM premium, int count, ref CardCount cardCount)
        {
            bool golden = (premium == TAG_PREMIUM.GOLDEN);
            switch (Rarity)
            {
                case TAG_RARITY.COMMON:
                    if (golden)
                    {
                        cardCount.gCommon += count;
                        cardCount.gTotal += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 50;
                        return $"<td>金色普通</td><td>{count}</td>";
                    }
                    else
                    {
                        cardCount.common += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 5;
                        return $"<td>普通</td><td>{count}</td>";
                    }
                case TAG_RARITY.RARE:
                    if (golden)
                    {
                        cardCount.gRare += count;
                        cardCount.gTotal += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 100;
                        return $"<td>金色稀有</td><td>{count}</td>";
                    }
                    else
                    {
                        cardCount.rare += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 20;
                        return $"<td>稀有</td><td>{count}</td>";
                    }
                case TAG_RARITY.EPIC:
                    if (golden)
                    {
                        cardCount.gEpic += count;
                        cardCount.gTotal += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 400;
                        return $"<td>金色史诗</td><td>{count}</td>";
                    }
                    else
                    {
                        cardCount.epic += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 100;
                        return $"<td>史诗</td><td>{count}</td>";
                    }
                case TAG_RARITY.LEGENDARY:
                    if (golden)
                    {
                        cardCount.gLegendary += count;
                        cardCount.gTotal += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 1600;
                        return $"<td>金色传说</td><td>{count}</td>";
                    }
                    else
                    {
                        cardCount.legendary += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 400;
                        return $"<td>传说</td><td>{count}</td>";
                    }
                default:
                    return "<td>未知</td>";
            }
        }

        public static string RankIdxToString(int rank)
        {
            string text;
            switch ((rank - 1) / 10)
            {
                case 0:
                    text = "青铜";
                    break;
                case 1:
                    text = "白银";
                    break;
                case 2:
                    text = "黄金";
                    break;
                case 3:
                    text = "铂金";
                    break;
                case 4:
                    text = "钻石";
                    break;
                case 5:
                    return "传说";
                default:
                    text = "未知";
                    break;
            }
            return text + (11 - (rank - (rank - 1) / 10 * 10)).ToString();
        }


        public struct CardMapping
        {
            public int RealDbID { get; set; }
            public int FakeDbID { get; set; }
            public string RealCardID { get; set; }
            public string FakeCardID { get; set; }
            public SkinType ThisSkinType { get; set; }
        };

        public static void MyLogger(LogLevel level, object message)
        {
            var myLogSource = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_GUID + ".MyLogger");
            myLogSource.Log(level, message);
            BepInEx.Logging.Logger.Sources.Remove(myLogSource);
        }

        public static void TryReportOpponent()
        {
            List<Blizzard.GameService.SDK.Client.Integration.ReportType.SubcomplaintType> subcomplaintTypes = new List<Blizzard.GameService.SDK.Client.Integration.ReportType.SubcomplaintType>
            {
                Blizzard.GameService.SDK.Client.Integration.ReportType.SubcomplaintType.BATTLETAG
            };

            Blizzard.GameService.SDK.Client.Integration.BattleNet.Get().SubmitReport(Utils.CacheLastOpponentAccountID, Blizzard.GameService.SDK.Client.Integration.ReportType.ComplaintType.INAPPROPRIATE_NAME, subcomplaintTypes);
            subcomplaintTypes.Clear();

            subcomplaintTypes.Add(Blizzard.GameService.SDK.Client.Integration.ReportType.SubcomplaintType.HACKING);
            subcomplaintTypes.Add(Blizzard.GameService.SDK.Client.Integration.ReportType.SubcomplaintType.BOTTING);
            Blizzard.GameService.SDK.Client.Integration.BattleNet.Get().SubmitReport(Utils.CacheLastOpponentAccountID, Blizzard.GameService.SDK.Client.Integration.ReportType.ComplaintType.CHEATING, subcomplaintTypes);
            subcomplaintTypes.Clear();

            subcomplaintTypes.Add(Blizzard.GameService.SDK.Client.Integration.ReportType.SubcomplaintType.BOOSTING_DERANKING);
            Blizzard.GameService.SDK.Client.Integration.BattleNet.Get().SubmitReport(Utils.CacheLastOpponentAccountID, Blizzard.GameService.SDK.Client.Integration.ReportType.ComplaintType.CHEATING, subcomplaintTypes);
            subcomplaintTypes.Clear();

            Utils.MyLogger(BepInEx.Logging.LogLevel.Warning, Utils.CacheLastOpponentFullName + Utils.CacheLastOpponentAccountID.EntityId.ToString() + "已举报");
        }

        public static void TryAutoReport()
        {
            var myPlayer = BnetPresenceMgr.Get()?.GetMyPlayer()?.GetAccountId();
            List<Blizzard.GameService.SDK.Client.Integration.ReportType.SubcomplaintType> subcomplaintTypes = new List<Blizzard.GameService.SDK.Client.Integration.ReportType.SubcomplaintType>
            {
                Blizzard.GameService.SDK.Client.Integration.ReportType.SubcomplaintType.BATTLETAG
            };

            Blizzard.GameService.SDK.Client.Integration.BattleNet.Get().SubmitReport(myPlayer, Blizzard.GameService.SDK.Client.Integration.ReportType.ComplaintType.INAPPROPRIATE_NAME, subcomplaintTypes);
            subcomplaintTypes.Clear();

            subcomplaintTypes.Add(Blizzard.GameService.SDK.Client.Integration.ReportType.SubcomplaintType.HACKING);
            subcomplaintTypes.Add(Blizzard.GameService.SDK.Client.Integration.ReportType.SubcomplaintType.BOTTING);
            Blizzard.GameService.SDK.Client.Integration.BattleNet.Get().SubmitReport(myPlayer, Blizzard.GameService.SDK.Client.Integration.ReportType.ComplaintType.CHEATING, subcomplaintTypes);
            subcomplaintTypes.Clear();

            subcomplaintTypes.Add(Blizzard.GameService.SDK.Client.Integration.ReportType.SubcomplaintType.BOOSTING_DERANKING);
            Blizzard.GameService.SDK.Client.Integration.BattleNet.Get().SubmitReport(myPlayer, Blizzard.GameService.SDK.Client.Integration.ReportType.ComplaintType.CHEATING, subcomplaintTypes);
            subcomplaintTypes.Clear();

            Utils.MyLogger(BepInEx.Logging.LogLevel.Warning, "举报完成。");
        }

        public static void TryRefundCardDisenchantCallback()
        {
            Network.CardSaleResult cardSaleResult = Network.Get().GetCardSaleResult();
            if (cardSaleResult.Action == Network.CardSaleResult.SaleResult.CARD_WAS_BOUGHT)
            {
                ;
            }
            else if (cardSaleResult.Action != Network.CardSaleResult.SaleResult.CARD_WAS_SOLD)
            {
                MyLogger(LogLevel.Warning, $"分解失败：{cardSaleResult.Action}");
                UIStatus.Get().AddInfo("分解失败");
            }
            else
            {
                MyLogger(LogLevel.Warning, "分解成功");
                CollectionManager.Get().OnCollectionChanged();
            }
        }

        //毁灭吧
        //public static void TryRuinCardDisenchant()
        //{
        //    int totalSell = 0;
        //    Network network = Network.Get();
        //    network.RegisterNetHandler(PegasusUtil.BoughtSoldCard.PacketID.ID, new Network.NetHandler(TryRefundCardDisenchantCallback), null);
        //    foreach (var record in CollectionManager.Get().GetOwnedCards())
        //    {
        //        if (record != null && record.IsCraftable && (record.OwnedCount > 0))
        //        {
        //            CraftingManager.Get().TryGetCardSellValue(record.CardId, record.PremiumType, out int value);
        //            network.SellCard(record.CardDbId, record.PremiumType, record.OwnedCount, value, record.OwnedCount);
        //            totalSell += record.OwnedCount * value;
        //        }
        //    }
        //    MyLogger(LogLevel.Warning, "尝试分解粉尘：" + totalSell);
        //    UIStatus.Get().AddInfo("尝试分解粉尘：" + totalSell);
        //}

        public static void TryReadNewCards()
        {
            if ((SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER) || (SceneMgr.Get().GetMode() == SceneMgr.Mode.PACKOPENING))
            {
                foreach (var record in CollectionManager.Get().GetOwnedCards())
                {
                    if (record != null && record.IsNewCard)
                    {
                        CollectionManager.Get().MarkAllInstancesAsSeen(record.CardId, record.PremiumType);
                    }
                }
            }
            if ((SceneMgr.Get()?.GetMode() == SceneMgr.Mode.LETTUCE_COLLECTION) || (SceneMgr.Get().GetMode() == SceneMgr.Mode.LETTUCE_PACK_OPENING)) // 执行后，需要重新进入佣兵收藏
            {
                List<PegasusLettuce.MercenaryAcknowledgeData> m_mercenaryAcknowledgements = new List<PegasusLettuce.MercenaryAcknowledgeData>();
                foreach (var merc in CollectionManager.Get().FindMercenaries(null, true, null, null, null).m_mercenaries)
                {
                    foreach (var ability in merc.m_abilityList)
                    {
                        if (!ability.IsAcknowledged(merc))
                        {
                            PegasusLettuce.MercenaryAcknowledgeData mercenaryAcknowledgeData = new PegasusLettuce.MercenaryAcknowledgeData
                            {
                                Type = PegasusLettuce.MercenaryAcknowledgeData.AcknowledgeType.ACKNOWLEDGE_MERC_ABILITY_ALL,
                                AssetId = ability.ID,
                                Acknowledged = true,
                                MercenaryId = merc.ID
                            };
                            //m_mercenaryAcknowledgements.Add(mercenaryAcknowledgeData);
                            //m_mercenaryAcknowledgements.Append(mercenaryAcknowledgeData);
                            CollectionManager.Get().MarkMercenaryAsAcknowledgedinCollection(mercenaryAcknowledgeData);
                        }
                    }
                    foreach (var equipment in merc.m_equipmentList)
                    {
                        if (!equipment.IsAcknowledged(merc))
                        {
                            PegasusLettuce.MercenaryAcknowledgeData mercenaryAcknowledgeData = new PegasusLettuce.MercenaryAcknowledgeData
                            {
                                Type = PegasusLettuce.MercenaryAcknowledgeData.AcknowledgeType.ACKNOWLEDGE_MERC_EQUIPMENT_ALL,
                                AssetId = equipment.ID,
                                Acknowledged = true,
                                MercenaryId = merc.ID
                            };
                            //m_mercenaryAcknowledgements.Add(mercenaryAcknowledgeData);
                            CollectionManager.Get().MarkMercenaryAsAcknowledgedinCollection(mercenaryAcknowledgeData);
                        }
                    }

                    foreach (var artVariation in merc.m_artVariations)
                    {
                        if (!artVariation.m_acknowledged)
                        {
                            PegasusLettuce.MercenaryAcknowledgeData mercenaryAcknowledgeData = new PegasusLettuce.MercenaryAcknowledgeData
                            {
                                Type = PegasusLettuce.MercenaryAcknowledgeData.AcknowledgeType.ACKNOWLEDGE_MERC_PORTRAIT_ACQUIRED,
                                AssetId = artVariation.m_record.ID,
                                Acknowledged = true,
                                MercenaryId = merc.ID
                            };
                            //m_mercenaryAcknowledgements.Add(mercenaryAcknowledgeData);
                            CollectionManager.Get().MarkMercenaryAsAcknowledgedinCollection(mercenaryAcknowledgeData);
                        }
                    }
                }
                Network.Get().RegisterNetHandler(PegasusLettuce.MercenariesCollectionAcknowledgeResponse.PacketID.ID, new Network.NetHandler(OnMercCollectionAcknowledgeResponse), null);
                Network.Get().AcknowledgeMercenaryCollection(m_mercenaryAcknowledgements);
                m_mercenaryAcknowledgements.Clear();
            }

        }

        public static void OnMercCollectionAcknowledgeResponse()
        {
            Network.Get().RemoveNetHandler(PegasusLettuce.MercenariesCollectionAcknowledgeResponse.PacketID.ID, new Network.NetHandler(OnMercCollectionAcknowledgeResponse));
            if (!Network.Get().AcknowledgeMercenaryCollectionResponse().Success)
            {
                MyLogger(LogLevel.Error, "Error acknowledging collection");
            }
        }


        public static void TryRefundCardDisenchant()    //TellServerAboutWhatUserDid
        {
            int totalSell = 0;
            Network network = Network.Get();
            network.RegisterNetHandler(PegasusUtil.BoughtSoldCard.PacketID.ID, new Network.NetHandler(TryRefundCardDisenchantCallback), null);
            Dictionary<string, CraftingPendingTransaction> pendingClientTransactionMaps = new Dictionary<string, CraftingPendingTransaction>();
            foreach (var record in CollectionManager.Get().GetOwnedCards())
            {
                if (record != null && record.IsCraftable && record.IsRefundable && (record.OwnedCount > 0))    // fixme: 需要强制刷新卡牌数量缓存，无法一次性全部分解。
                {
                    CraftingManager.Get().TryGetCardSellValue(record.CardId, record.PremiumType, out int sellValue);
                    CraftingManager.Get().TryGetCardSellValue(record.CardId, TAG_PREMIUM.NORMAL, out int normalSellValue);
                    CraftingManager.Get().TryGetCardSellValue(record.CardId, TAG_PREMIUM.GOLDEN, out int goldenSellValue);

                    CraftingManager.Get().TryGetCardBuyValue(record.CardId, record.PremiumType, out int buyValue);
                    CraftingManager.Get().TryGetCardBuyValue(record.CardId, TAG_PREMIUM.NORMAL, out int normalBuyValue);
                    CraftingManager.Get().TryGetCardBuyValue(record.CardId, TAG_PREMIUM.GOLDEN, out int goldenBuyValue);

                    if ((sellValue != buyValue) || (normalBuyValue != normalSellValue) || (goldenBuyValue != goldenSellValue)) continue;

                    int numNormalCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(record.CardId, TAG_PREMIUM.NORMAL);
                    int numGoldenCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(record.CardId, TAG_PREMIUM.GOLDEN);
                    int numSignatureCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(record.CardId, TAG_PREMIUM.SIGNATURE);
                    int numDiamondCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(record.CardId, TAG_PREMIUM.DIAMOND);

                    if (!pendingClientTransactionMaps.ContainsKey(record.CardId))
                    {
                        CraftingPendingTransaction pendingClientTransaction = new CraftingPendingTransaction
                        {
                            CardID = record.CardId,
                            Premium = record.PremiumType,
                            NormalDisenchantCount = 0,
                            GoldenDisenchantCount = 0,
                            SignatureDisenchantCount = 0,
                            DiamondDisenchantCount = 0
                        };
                        pendingClientTransactionMaps[record.CardId] = pendingClientTransaction;
                    }
                    CraftingPendingTransaction m_pendingClientTransaction = pendingClientTransactionMaps[record.CardId];
                    switch (record.PremiumType)
                    {
                        case TAG_PREMIUM.NORMAL:
                            m_pendingClientTransaction.NormalDisenchantCount = numNormalCopiesInCollection;
                            break;
                        case TAG_PREMIUM.GOLDEN:
                            m_pendingClientTransaction.GoldenDisenchantCount = numGoldenCopiesInCollection;
                            break;
                    }
                }

            }
            foreach (var record in pendingClientTransactionMaps.Values)
            {
                if (record == null) continue;
                int numNormalCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(record.CardID, TAG_PREMIUM.NORMAL);
                int numGoldenCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(record.CardID, TAG_PREMIUM.GOLDEN);
                int numSignatureCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(record.CardID, TAG_PREMIUM.SIGNATURE);
                int numDiamondCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(record.CardID, TAG_PREMIUM.DIAMOND);

                int normalSells = record.NormalDisenchantCount;
                int goldenSells = record.GoldenDisenchantCount;
                CraftingManager.Get().TryGetCardSellValue(record.CardID, TAG_PREMIUM.NORMAL, out int normalSellValue);
                CraftingManager.Get().TryGetCardSellValue(record.CardID, TAG_PREMIUM.GOLDEN, out int goldenSellValue);
                int sellValue = -(normalSellValue * normalSells + goldenSellValue * goldenSells);
                totalSell += -sellValue;
                network.CraftingTransaction(record, sellValue, numNormalCopiesInCollection, numGoldenCopiesInCollection, numSignatureCopiesInCollection, numDiamondCopiesInCollection);
                MyLogger(LogLevel.Warning, $"尝试分解卡牌：{record.CardID}，普通{normalSells}，金卡{goldenSells}。");

            }
            MyLogger(LogLevel.Warning, "尝试分解粉尘：" + totalSell);
            UIStatus.Get().AddInfo("尝试分解粉尘：" + totalSell);
        }

        public static void TryGetSafeImg()
        {
            webPageBackImg.Value = "";
        }


        //虚假开包数据范围
        public static List<int> GetCardsDbId()
        {
            List<int> cardsDbId = new List<int>();
            foreach (int dbid in GameDbf.GetIndex().GetCollectibleCardDbIds())
            {
                var entitydef = DefLoader.Get().GetEntityDef(dbid, false);
                if (entitydef != null)
                {
                    if (entitydef.GetRarity() != TAG_RARITY.FREE
                        && entitydef.GetRarity() != TAG_RARITY.INVALID)
                    {
                        if (entitydef.GetCardType() != TAG_CARDTYPE.HERO)
                            cardsDbId.Add(dbid);
                        else if (entitydef.GetCost() != 0)    // 忽略英雄皮肤
                            cardsDbId.Add(dbid);
                    }
                }
            }
            return cardsDbId;
        }

        //虚假结果，指定稀有度
        public static int GetRandomCardID(TAG_RARITY rarity)
        {
            int dbid;
            List<int> dbids = GetCardsDbId();
            while (true)
            {
                dbid = dbids[UnityEngine.Random.Range(0, dbids.Count)];
                if (DefLoader.Get().GetEntityDef(dbid, false).GetRarity() == rarity)
                {
                    break;
                }
            }
            return dbid;
        }


        public static NetCache.BoosterCard GenerateRandomACard(bool rarityRandom = false, bool premiumRandom = false, TAG_RARITY rarity = TAG_RARITY.LEGENDARY, TAG_PREMIUM premium = TAG_PREMIUM.GOLDEN)
        {
            if (!rarityRandom) rarity = (TAG_RARITY)fakeRandomRarity.Value;
            if (!premiumRandom) premium = fakeRandomPremium.Value;
            if (fakeBoosterDbId.Value.ToString().Substring(0, 7) == "GOLDEN_")
            {
                premiumRandom = false;
                premium = TAG_PREMIUM.GOLDEN;
            }
            List<int> dbids = GetCardsDbId();
            if (premiumRandom)
            {
                if (!isFakeAtypicalRandomPremium.Value)
                {
                    premium = (TAG_PREMIUM)UnityEngine.Random.Range(0, 2);
                }
                else premium = (TAG_PREMIUM)UnityEngine.Random.Range(0, Enum.GetValues(typeof(TAG_PREMIUM)).Length);
            }
            NetCache.BoosterCard card = new NetCache.BoosterCard();
            card.Def.Name = GameUtils.TranslateDbIdToCardId(rarityRandom ? dbids[UnityEngine.Random.Range(0, dbids.Count)] : GetRandomCardID(rarity));
            card.Def.Premium = premium;
            return card;
        }


        //虚假结果
        public static void GenerateRandomCard(bool rarityRandom = false, bool premiumRandom = false, TAG_RARITY rarity = TAG_RARITY.LEGENDARY, TAG_PREMIUM premium = TAG_PREMIUM.GOLDEN)
        {
            if (!rarityRandom) rarity = (TAG_RARITY)fakeRandomRarity.Value;
            if (!premiumRandom) premium = fakeRandomPremium.Value;
            if (fakeBoosterDbId.Value.ToString().Substring(0, 7) == "GOLDEN_")
            {
                premiumRandom = false;
                premium = TAG_PREMIUM.GOLDEN;
            }
            List<int> dbids = GetCardsDbId();
            for (int i = 1; i <= 5; i++)
            {
                if (premiumRandom)
                {
                    if (!isFakeAtypicalRandomPremium.Value)
                    {
                        premium = (TAG_PREMIUM)UnityEngine.Random.Range(0, 2);
                    }
                    else premium = (TAG_PREMIUM)UnityEngine.Random.Range(0, Enum.GetValues(typeof(TAG_PREMIUM)).Length);
                }
                switch (i)
                {
                    case 1:
                        fakeCardID1.Value = rarityRandom ? dbids[UnityEngine.Random.Range(0, dbids.Count)] : GetRandomCardID(rarity);
                        fakeCardPremium1.Value = premium;
                        break;
                    case 2:
                        fakeCardID2.Value = rarityRandom ? dbids[UnityEngine.Random.Range(0, dbids.Count)] : GetRandomCardID(rarity);
                        fakeCardPremium2.Value = premium;
                        break;
                    case 3:
                        fakeCardID3.Value = rarityRandom ? dbids[UnityEngine.Random.Range(0, dbids.Count)] : GetRandomCardID(rarity);
                        fakeCardPremium3.Value = premium;
                        break;
                    case 4:
                        fakeCardID4.Value = rarityRandom ? dbids[UnityEngine.Random.Range(0, dbids.Count)] : GetRandomCardID(rarity);
                        fakeCardPremium4.Value = premium;
                        break;
                    case 5:
                        fakeCardID5.Value = rarityRandom ? dbids[UnityEngine.Random.Range(0, dbids.Count)] : GetRandomCardID(rarity);
                        fakeCardPremium5.Value = premium;
                        break;
                }
            }
        }

        public static void BuyAdventure(BuyAdventureTemplate adventure)
        {
            if (adventure == BuyAdventureTemplate.DoNothing)
            {
                return;
            }
            if (SceneMgr.Get().GetMode() == SceneMgr.Mode.STARTUP || SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
            {
                UIStatus.Get().AddInfo("未初始化！");
                return;
            }
            if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
            {
                UIStatus.Get().AddInfo("不能在游戏内购买！");
                return;
            }
            try
            {
                int wingID = -1;
                ProductType productType = ProductType.PRODUCT_TYPE_UNKNOWN;
                switch (adventure)
                {
                    case BuyAdventureTemplate.BuyKara:
                        productType = ProductType.PRODUCT_TYPE_WING;
                        wingID = 16;
                        break;
                    case BuyAdventureTemplate.BuyNAX:
                        productType = ProductType.PRODUCT_TYPE_NAXX;
                        wingID = 1;
                        break;
                    case BuyAdventureTemplate.BuyBRM:
                        productType = ProductType.PRODUCT_TYPE_BRM;
                        wingID = 6;
                        break;
                    case BuyAdventureTemplate.BuyLOE:
                        productType = ProductType.PRODUCT_TYPE_LOE;
                        wingID = 11;
                        break;
                    default:
                        return;
                }

                if (adventure == BuyAdventureTemplate.BuyKara)
                {
                    for (int i = 16; i <= 20; i++)
                    {
                        wingID = i;
                        if (StoreManager.GetStaticProductItemOwnershipStatus(productType, wingID, out string _) != ItemOwnershipStatus.OWNED)
                        {
                            break;
                        }
                    }
                    if (wingID == 20)
                    {
                        wingID = 15;
                    }
                }

                if (StoreManager.GetStaticProductItemOwnershipStatus(productType, wingID, out string failReason) == ItemOwnershipStatus.OWNED)
                {
                    Utils.MyLogger(LogLevel.Warning, $"{adventure}：冒险已拥有！");
                    UIStatus.Get().AddInfo("所选冒险已拥有！");
                }
                else
                {
                    StoreManager.Get().StartAdventureTransaction(productType, wingID, null, null, global::ShopType.ADVENTURE_STORE, 1, false, null, 0);

                    //ProductDataModel productByPmtId = StoreManager.Get().Catalog.GetProductByPmtId(ProductId.CreateFromValidated((long)0));     // 购买经典卡包
                    //PriceDataModel priceDataModel = productByPmtId.Prices.FirstOrDefault((PriceDataModel p) => p.Currency == CurrencyType.GOLD);
                    //Shop.Get().AttemptToPurchaseProduct(productByPmtId, priceDataModel, 1);
                }

            }
            catch (Exception ex)
            {
                Utils.MyLogger(LogLevel.Warning, ex);
            }
        }


        public static void ZeroDollarShopping()
        {
            if (SceneMgr.Get().GetMode() == SceneMgr.Mode.STARTUP || SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
            {
                UIStatus.Get().AddInfo("未初始化！");
                return;
            }
            if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
            {
                UIStatus.Get().AddInfo("不能在游戏内购买！");
                return;
            }
            if (!StoreManager.Get().IsOpen())
            {
                UIStatus.Get().AddInfo("商店初始化失败！");
                return;
            }
            try
            {
                foreach (PegasusUtil.ProductType t in Enum.GetValues(typeof(PegasusUtil.ProductType)))
                {
                    foreach (var bundle in StoreManager.Get().GetAvailableBundlesForProduct(t, true))
                    {
                        Utils.MyLogger(LogLevel.Info, $"[{StoreManager.Get().CanBuyBundle(bundle)}]{t.ToString()}[true] type={bundle?.GetFirstVirtualCurrencyPriceType()} id={bundle?.Id} title={bundle?.Title} des={bundle?.Description}");
                        if (bundle?.Id.Value == 1888902) //战旗代币
                        {
                            continue;
                        }
                        if (bundle?.GetFirstVirtualCurrencyPriceType() == CurrencyType.NONE)
                        {
                            if (!bundle.TryGetBundlePrice(CurrencyType.GOLD, out _))
                            {
                                Utils.MyLogger(LogLevel.Info, $"Found {bundle?.Title}.");
                                StoreManager.Get().StartStoreBuy(new BuyPmtProductEventArgs(bundle, CurrencyType.GOLD, 1));
                                UIStatus.Get().AddInfo("请等待购买完成，如果UI卡住，请重进游戏。", 60);
                                return;
                            }
                        }
                        foreach (CurrencyType pt in Enum.GetValues(typeof(CurrencyType)))
                        {

                            if (null != bundle)
                            {
                                //if (bundle.Id.Value == 1914499)
                                //{
                                //    Utils.MyLogger(LogLevel.Info, $"found.");
                                //    StoreManager.Get().StartStoreBuy(new BuyPmtProductEventArgs(bundle, CurrencyType.GOLD, 1));
                                //    UIStatus.Get().AddInfo("请等待购买完成，如果UI卡住，请重进游戏。", 60);
                                //    return;
                                //}
                                var pd = bundle?.GetPriceDataModel(pt);
                                double totalPrice;
                                if (!bundle.TryGetBundlePrice(pt, out totalPrice))
                                {
                                    //Utils.MyLogger(LogLevel.Warning, string.Format("Product {0} does not have requested cost for {1}", bundle.Id, pd));
                                    continue;
                                }
                                Utils.MyLogger(LogLevel.Info, $"type={pt} Currency/FirstVirtualCurrency={pd?.Currency}/{bundle?.GetFirstVirtualCurrencyPriceType()} price={totalPrice} Amount={pd?.Amount} OriginalAmount={pd?.OriginalAmount} OriginalDisplayText={pd?.OriginalDisplayText}");
                                if (totalPrice == 0)
                                {
                                    Utils.MyLogger(LogLevel.Warning, $"{t.ToString()}[true] id={bundle?.Id} title={bundle?.Title} price=0!!!");
                                    //StoreManager.Get().StartStoreBuy(new BuyPmtProductEventArgs(bundle, pt, 1));
                                }

                            }
                        }
                        foreach (var id in bundle?.SaleIds)
                        {
                            Utils.MyLogger(LogLevel.Info, $"saleid={id}");
                        }
                        foreach (var item in bundle?.Items)
                        {
                            Utils.MyLogger(LogLevel.Info, $"ShopAvailableDate={item.ShopAvailableDate} data={item.ProductData} type={item.ItemType.ToString()}  quantity={item.Quantity}");
                        }
                    }
                }
                UIStatus.Get().AddInfo("未发现零元购商品！");
                if (targetFrameRate.Value >= 144) // test code
                {
                    foreach (PegasusUtil.ProductType t in Enum.GetValues(typeof(PegasusUtil.ProductType)))
                    {
                        foreach (var bundle in StoreManager.Get().GetAvailableBundlesForProduct(t, false))
                        {
                            // ！！！
                            Utils.MyLogger(LogLevel.Info, $"[{StoreManager.Get().CanBuyBundle(bundle)}]{t.ToString()}[false] type={bundle?.GetFirstVirtualCurrencyPriceType()} id={bundle?.Id} title={bundle?.Title} des={bundle?.Description}");

                            if (bundle?.GetFirstVirtualCurrencyPriceType() == CurrencyType.NONE)
                            {
                                if (!bundle.TryGetBundlePrice(CurrencyType.GOLD, out _))
                                {
                                    //if (bundle.Id.Value == 1851720)
                                    //{
                                    //    Utils.MyLogger(LogLevel.Info, $"Found {bundle?.Title}.");
                                    //    StoreManager.Get().StartStoreBuy(new BuyPmtProductEventArgs(bundle, CurrencyType.GOLD, 1));
                                    //    UIStatus.Get().AddInfo("请等待购买完成，如果UI卡住，请重进游戏。", 60);
                                    //    return;
                                    //}
                                    Utils.MyLogger(LogLevel.Error, $"[{StoreManager.Get().CanBuyBundle(bundle)}]{t.ToString()}[false] id={bundle?.Id} title={bundle?.Title} des={bundle?.Description}");
                                    //continue;
                                    //
                                    Utils.MyLogger(LogLevel.Info, $"Found {bundle?.Title}.");
                                    StoreManager.Get().StartStoreBuy(new BuyPmtProductEventArgs(bundle, (CurrencyType)(targetFrameRate.Value - 180), 1));
                                    UIStatus.Get().AddInfo("请等待购买完成，如果UI卡住，请重进游戏。", 60);
                                    return;
                                }
                            }
                            //foreach (CurrencyType pt in Enum.GetValues(typeof(CurrencyType)))
                            //{

                            //    if (null != bundle)
                            //    {
                            //        var pd = bundle?.GetPriceDataModel(pt);
                            //        double totalPrice;
                            //        if (!bundle.TryGetBundlePrice(pt, out totalPrice))
                            //        {
                            //            //Utils.MyLogger(LogLevel.Warning, string.Format("Product {0} does not have requested cost for {1}", bundle.Id, pd));.
                            //            continue;
                            //        }
                            //        Utils.MyLogger(LogLevel.Info, $"type={pt} Currency/FirstVirtualCurrency={pd?.Currency}/{bundle?.GetFirstVirtualCurrencyPriceType()} price={totalPrice} Amount={pd?.Amount} OriginalAmount={pd?.OriginalAmount} OriginalDisplayText={pd?.OriginalDisplayText}");
                            //        if (totalPrice == 0)
                            //        {
                            //            Utils.MyLogger(LogLevel.Warning, $"{t.ToString()}[true] id={bundle?.Id} title={bundle?.Title} price=0!!!");
                            //            //StoreManager.Get().StartStoreBuy(new BuyPmtProductEventArgs(gz, pt, 1));
                            //        }
                            //    }
                            //}
                            //foreach (var id in bundle?.SaleIds)
                            //{
                            //    Utils.MyLogger(LogLevel.Info, $"saleid={id}");
                            //}
                            //foreach (var item in bundle?.Items)
                            //{
                            //    Utils.MyLogger(LogLevel.Info, $"ShopAvailableDate={item.ShopAvailableDate} data={item.ProductData} type={item.ItemType.ToString()}  quantity={item.Quantity}");
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.MyLogger(LogLevel.Warning, ex);
            }

        }

        public static List<int> CacheCoin = new List<int>();
        public static List<int> CacheCoinCard = new List<int>();
        public static List<int> CacheGameBoard = new List<int>();
        public static List<int> CacheBgsBoard = new List<int>();
        public static List<int> CacheCardBack = new List<int>();
        public static List<int> CacheBgsFinisher = new List<int>();
        public static Dictionary<int, Assets.CardHero.HeroType> CacheHeroes = new Dictionary<int, Assets.CardHero.HeroType>();
        public static string CacheLastOpponentFullName;
        public static string CacheRawHeroCardId;
        public static Blizzard.GameService.SDK.Client.Integration.BnetAccountId CacheLastOpponentAccountID;
        public static List<MercenarySkin> CacheMercenarySkin = new List<MercenarySkin>();
        public static bool CacheLoginStatus = false;

        public static class CacheInfo
        {

            public static void UpdateCoin()
            {
                CacheCoin.Clear();
                foreach (var record in GameDbf.CosmeticCoin.GetRecords())
                {
                    if (record != null)
                    {
                        CacheCoin.Add(record.CardId);
                    }
                }
            }
            public static void UpdateCoinCard()
            {
                CacheCoinCard.Clear();
                foreach (var record in GameDbf.CosmeticCoin.GetRecords())
                {
                    if (record != null)
                    {
                        CacheCoinCard.Add(record.CardId);
                    }
                }
            }

            public static void UpdateGameBoard()
            {
                CacheGameBoard.Clear();
                foreach (var record in GameDbf.Board.GetRecords())
                {
                    if (record != null)
                    {
                        CacheGameBoard.Add(record.ID);
                    }
                }
            }
            public static void UpdateBgsBoard()
            {
                CacheBgsBoard.Clear();
                foreach (var record in GameDbf.BattlegroundsBoardSkin.GetRecords())
                {
                    if (record != null)
                    {
                        CacheBgsBoard.Add(record.ID);
                    }
                }
            }
            public static void UpdateHeroes()
            {
                CacheHeroes.Clear();
                foreach (var record in GameDbf.CardHero.GetRecords())
                {
                    if (record != null)
                    {
                        CacheHeroes.Add(record.CardId, record.HeroType);
                    }
                }
            }
            public static void UpdateCardBack()
            {
                CacheCardBack.Clear();
                foreach (var record in GameDbf.CardBack.GetRecords())
                {
                    if (record != null)
                    {
                        CacheCardBack.Add(record.ID);
                    }
                }
            }
            public static void UpdateBgsFinisher()
            {
                CacheBgsFinisher.Clear();
                foreach (var record in GameDbf.BattlegroundsFinisher.GetRecords())
                {
                    if (record != null)
                    {
                        CacheBgsFinisher.Add(record.ID);
                    }
                }
            }

            public static void UpdateMercenarySkin()
            {
                CacheMercenarySkin.Clear();
                foreach (var merc in GameDbf.LettuceMercenary.GetRecords())
                {

                    if (merc != null && merc.MercenaryArtVariations.Count > 0)
                    {
                        MercenarySkin mercSkin = new MercenarySkin
                        {
                            Name = merc.MercenaryArtVariations.First().CardRecord.Name.GetString(),
                            Id = new List<int>(),
                            hasDiamond = false
                        };
                        foreach (var art in merc.MercenaryArtVariations.OrderBy(x => x.ID).ToList())
                        {
                            if (art != null)
                            {
                                mercSkin.Id.Add(art.CardId);
                                if (art.DefaultVariation)
                                {
                                    mercSkin.Default = art.CardId;
                                }
                                foreach (var premiums in art.MercenaryArtVariationPremiums)
                                {
                                    if (premiums != null && premiums.Premium == Assets.MercenaryArtVariationPremium.MercenariesPremium.PREMIUM_DIAMOND)
                                    {
                                        mercSkin.hasDiamond = true;
                                        mercSkin.Diamond = art.CardId;
                                    }
                                }
                            }
                        }
                        CacheMercenarySkin.Add(mercSkin);
                    }
                }
            }

        }

        public static void UpdateHeroPowerMapping()
        {
            HeroesPowerMapping.Clear();
            //HeroesPowerMapping.Add("CS2_083b_H1", "CS2_102_H1"); 求解存在问题，如，玛维的技能无法正确列出。
            foreach (var hero in HeroesMapping)
            {
                string rawHeroPower = GameUtils.GetHeroPowerCardIdFromHero(hero.Key);
                string aimHeroPower = GameUtils.GetHeroPowerCardIdFromHero(hero.Value);

                if (rawHeroPower != string.Empty && aimHeroPower != string.Empty && (!HeroesPowerMapping.ContainsKey(rawHeroPower)))
                {
                    HeroesPowerMapping.Add(rawHeroPower, aimHeroPower);
                    //MyLogger(LogLevel.Debug, "HeroPowerMapping: " + rawHeroPower + " -> " + aimHeroPower);
                }
            }
        }


        public static bool IsMercenaryFullyUpgraded(LettuceMercenary merc)
        {
            if (merc == null || !merc.m_owned || !merc.IsMaxLevel())
            {
                return false;
            }
            else
            {
                foreach (var ability in merc.m_abilityList)
                {
                    if (ability.GetNextUpgradeCost() <= 0)
                    {
                        continue;
                    }
                    else return false;
                }
                foreach (var equipment in merc.m_equipmentList)
                {
                    if (equipment.GetNextUpgradeCost() <= 0)
                    {
                        continue;
                    }
                    else return false;
                }
                return true;
            }
        }

        public static long CalcMercenaryCoinNeed(LettuceMercenary merc)
        {
            long coinNeed = 0;
            if (!merc.m_owned)
            {
                coinNeed = merc.GetCraftingCost() - merc.m_currencyAmount;
                return (coinNeed > 0) ? coinNeed : 4096;
            }

            foreach (var ability in merc.m_abilityList)
            {
                if (ability.GetNextUpgradeCost() <= 0)
                {
                    continue;
                }
                int tier = ability.GetNextTier() - 1;
                switch (tier)
                {
                    case 1:
                        coinNeed += 50 + 125 + 150 + 150;
                        break;
                    case 2:
                        coinNeed += 125 + 150 + 150;
                        break;
                    case 3:
                        coinNeed += 150 + 150; ;
                        break;
                    case 4:
                        coinNeed += 150;
                        break;
                    case 5:
                        coinNeed += 0;
                        break;
                }
            }
            foreach (var equipment in merc.m_equipmentList)
            {
                if (equipment.GetNextUpgradeCost() <= 0)
                {
                    continue;
                }
                int tier = equipment.GetNextTier() - 1;
                switch (tier)
                {
                    case 1:
                        coinNeed += 100 + 150 + 175;
                        break;
                    case 2:
                        coinNeed += 150 + 175;
                        break;
                    case 3:
                        coinNeed += 175; ;
                        break;
                    case 4:
                        coinNeed += 0;
                        break;
                }
            }
            coinNeed -= merc.m_currencyAmount;
            return (coinNeed > 0) ? coinNeed : 8192;
        }

        public static void EnhanceBepInExSetting()
        {
            var coreConfigProp = typeof(BepInEx.Configuration.ConfigFile).GetProperty("CoreConfig", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            if (coreConfigProp == null) throw new ArgumentNullException(nameof(coreConfigProp));
            var coreConfig = (BepInEx.Configuration.ConfigFile)coreConfigProp.GetValue(null, null);

            //var bepinMeta = new BepInEx.BepInPlugin("BepInEx", "BepInEx", typeof(BepInEx.Bootstrap.Chainloader).Assembly.GetName().Version.ToString());
            //var bepinexConfig = new BepInEx.Configuration.ConfigFile(Path.Combine(BepInEx.Paths.ConfigPath, "BepInEx.cfg"), true, bepinMeta);
            bool restart = false;

            BepInEx.Configuration.ConfigEntry<bool> configHideManagerGameObject;
            BepInEx.Configuration.ConfigEntry<bool> configUnityLogListening;
            BepInEx.Configuration.ConfigEntry<bool> configWriteUnityLog;
            BepInEx.Configuration.ConfigEntry<bool> configAppendLog;
            BepInEx.Configuration.ConfigEntry<bool> configEnabled;
            BepInEx.Configuration.ConfigEntry<LogLevel> configLogLevels;
            BepInEx.Configuration.ConfigEntry<LogLevel> configUnityLogLevels;
            if (coreConfig.TryGetEntry("Chainloader", "HideManagerGameObject", out configHideManagerGameObject))
            {
                if (configHideManagerGameObject.Value == false)
                {
                    restart = true;
                }
                configHideManagerGameObject.Value = true;
            }
            else
            {
                MyLogger(LogLevel.Error, "Chainloader.HideManagerGameObject not found");
            }
            if (coreConfig.TryGetEntry("Logging", "UnityLogListening", out configUnityLogListening))
            {
                configUnityLogListening.Value = true;
            }
            else
            {
                MyLogger(LogLevel.Error, "Logging.UnityLogListening not found");
            }
            if (coreConfig.TryGetEntry("Logging.Disk", "WriteUnityLog", out configWriteUnityLog))
            {
                configWriteUnityLog.Value = true;
            }
            else
            {
                MyLogger(LogLevel.Error, "Logging.Disk.WriteUnityLog not found");
            }
            if (coreConfig.TryGetEntry("Logging.Disk", "AppendLog", out configAppendLog))
            {
                configAppendLog.Value = false;
            }
            else
            {
                MyLogger(LogLevel.Error, "Logging.Disk.AppendLog not found");
            }
            if (coreConfig.TryGetEntry("Logging.Disk", "Enabled", out configEnabled))
            {
                configEnabled.Value = true;
            }
            else
            {
                MyLogger(LogLevel.Error, "Logging.DiskEnabled  not found");
            }
            if (coreConfig.TryGetEntry("Logging.Disk", "LogLevels", out configLogLevels))
            {
                configLogLevels.Value = LogLevel.All;
            }
            else
            {
                MyLogger(LogLevel.Error, "Logging.Disk.LogLevels not found");
            }
            if (coreConfig.TryGetEntry("Logging.Unity", "LogLevels", out configUnityLogLevels))
            {
                configUnityLogLevels.Value = LogLevel.All;
            }
            else
            {
                MyLogger(LogLevel.Warning, $"{BepInEx.Paths.BepInExConfigPath}: Logging.Unity.LogLevels not found.");
            }
            if (restart)
            {
                Utils.MyLogger(BepInEx.Logging.LogLevel.Error, "BepInEx config update. Please restart Heartstone!");
                System.Threading.Thread.Sleep(1919);
                Utils.Quit(1919810);
            }
        }


        public static string ReadLastLine(string path, int lines, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            try
            {
                var lastLines = new LinkedList<string>();

                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(stream, encoding))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lastLines.AddLast(line);
                        if (lastLines.Count >= lines)
                        {
                            lastLines.RemoveFirst();
                        }
                    }
                }

                return string.Join(Environment.NewLine, lastLines);
            }
            catch (Exception ex)
            {

                return ex.Message; // 出现异常时返回空字符串
            }
        }

        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir))
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d);
                    else
                        DeleteFolder(d);
                }
                Directory.Delete(dir, true);
            }
        }

        public static void ShowEula()
        {
            if (!isEulaRead.Value)
            {
                UIStatus.Get()?.AddInfo(LocalizationManager.GetLangValue("hsmod.declaration"), 23.333f);
                AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
                popupInfo.m_headerText = "HsMod End-User License Agreement";
                popupInfo.m_text = LocalizationManager.GetLangValue("hsmod.declaration");
                popupInfo.m_showAlertIcon = false;
                popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
                popupInfo.m_attentionCategory = UserAttentionBlocker.NONE;
                DialogManager.Get()?.ShowPopup(popupInfo);
                isEulaRead.Value = true;
            }
            else
            {
                if ((Localization.GetLocale() == Locale.zhCN) || (pluginInitLanague.Value == "zhCN") || (string.Compare(System.Globalization.CultureInfo.CurrentCulture.Name, "zh-CN") == 0))
                    UIStatus.Get()?.AddInfo(LocalizationManager.GetLangValue("hsmod.declaration"), 6.666f);
            }
        }

        public static void OnHsLoginCompleted()
        {
            Utils.CacheLoginStatus = true;
            try
            {
                ShowEula();
                if (!isIdleKickEnable.Value)
                {
                    InactivePlayerKicker.Get()?.SetShouldCheckForInactivity(isIdleKickEnable.Value);
                }
                LeakInfo.Mercenaries();
                LeakInfo.Skins();
            }
            catch (Exception ex)
            {
                MyLogger(LogLevel.Error, ex.Message);
                MyLogger(LogLevel.Error, ex.StackTrace);
            }
        }

        public static void Quit(int exitCode = 0)
        {
            try
            {
                UnityEngine.Application.Quit(exitCode);
            }
            finally
            {
                System.Threading.Thread.Sleep(11451);
                Process.GetCurrentProcess().Kill();
            }
        }

        public static class LeakInfo
        {
            public static void Mercenaries()
            {
                string savePath = Path.Combine(BepInEx.Paths.BepInExRootPath, "HsMod", "mercenaries.log");
                //List<LettuceTeam> teams = CollectionManager.Get().GetTeams();
                //System.IO.File.WriteAllText(savePath, DateTime.Now.ToLocalTime().ToString() + "\t获取到您的队伍如下：\n");
                //foreach (LettuceTeam team in teams)
                //{
                //    System.IO.File.AppendAllText(savePath, team.Name + "\n");
                //}
                System.IO.File.WriteAllText(savePath, "# " + DateTime.Now.ToLocalTime().ToString() + "\t获取到关卡信息如下：\n");
                System.IO.File.AppendAllText(savePath, "# [ID]\t[Heroic?]\t[Bounty]\t[BossName]\n");
                foreach (var record in GameDbf.LettuceBounty.GetRecords())     // 生成关卡名称
                {
                    string saveString;
                    if (record != null)
                    {
                        saveString = record.ID.ToString() + (record.Heroic ? " Heroic " : " ") + record.BountySetRecord.Name.GetString() + " " + GameDbf.Card.GetRecord(record.FinalBossCardId).Name.GetString();
                        System.IO.File.AppendAllText(savePath, saveString + "\n");
                    }
                }
            }
            public static void MyCards()
            {
                string savePath = Path.Combine(BepInEx.Paths.BepInExRootPath, "HsMod", "refundcards.log");
                System.IO.File.AppendAllText(savePath, "# " + DateTime.Now.ToLocalTime().ToString() + "\t获取到全额分解卡牌情况如下：\n");
                System.IO.File.AppendAllText(savePath, "# [Name]\t[PremiumType]\t[Rarity]\t[CardId]\t[CardDbId]\t[OwnedCount]\n");
                //Filter<CollectibleCard> filter3 = new Filter<CollectibleCard>((CollectibleCard card) => card.IsRefundable);
                foreach (var record in CollectionManager.Get().GetOwnedCards())
                {
                    string saveString;
                    if (record != null && record.IsCraftable && record.IsRefundable && (record.OwnedCount > 0))
                    {
                        // GameUtils.TranslateDbIdToCardId(record.CardId, false);
                        saveString = $"{record.Name}\t{record.PremiumType}\t{record.Rarity}\t{record.CardId}\t{record.CardDbId}\t{record.OwnedCount}\t";
                        System.IO.File.AppendAllText(savePath, saveString + "\n");
                    }
                }
            }
            public static void Skins()
            {
                string savePath = Path.Combine(BepInEx.Paths.BepInExRootPath, "HsMod", "skins.log");
                System.IO.File.WriteAllText(savePath, "# " + DateTime.Now.ToLocalTime().ToString() + "\t获取到硬币皮肤如下：\n");
                System.IO.File.AppendAllText(savePath, "# [CARD_ID]\t[Name]\n");
                foreach (var record in GameDbf.CosmeticCoin.GetRecords())
                {
                    string saveString;
                    if (record != null)
                    {
                        saveString = $"{record.CardId}\t{record.Name.GetString()}";
                        System.IO.File.AppendAllText(savePath, saveString + "\n");
                    }
                }
                System.IO.File.AppendAllText(savePath, "# " + DateTime.Now.ToLocalTime().ToString() + "\t获取到卡背信息如下：\n");
                System.IO.File.AppendAllText(savePath, "# [ID]\t[Name]\n");
                foreach (var record in GameDbf.CardBack.GetRecords())
                {
                    string saveString;
                    if (record != null)
                    {
                        // GameUtils.TranslateDbIdToCardId(record.CardId, false);
                        saveString = $"{record.ID}\t{record.Name.GetString()}";
                        System.IO.File.AppendAllText(savePath, saveString + "\n");
                    }
                }

                System.IO.File.AppendAllText(savePath, "# " + DateTime.Now.ToLocalTime().ToString() + "\t获取到游戏面板信息如下：\n");
                System.IO.File.AppendAllText(savePath, "# [ID]\t[NOTE_DESC]\n");
                foreach (var record in GameDbf.Board.GetRecords())
                {
                    string saveString;
                    if (record != null)
                    {
                        // GameUtils.TranslateDbIdToCardId(record.CardId, false);
                        saveString = $"{record.ID}\t{record.NoteDesc.ToString()}";
                        System.IO.File.AppendAllText(savePath, saveString + "\n");
                    }
                }

                System.IO.File.AppendAllText(savePath, "# " + DateTime.Now.ToLocalTime().ToString() + "\t获取到酒馆战斗面板如下：\n");
                System.IO.File.AppendAllText(savePath, "# [ID]\t[CollectionShortName]\t[CollectionName]\n");
                foreach (var record in GameDbf.BattlegroundsBoardSkin.GetRecords())
                {
                    string saveString;
                    if (record != null)
                    {
                        // GameUtils.TranslateDbIdToCardId(record.CardId, false);
                        saveString = $"{record.ID}\t{record.CollectionShortName.GetString()}\t{record.CollectionName.GetString()}";
                        System.IO.File.AppendAllText(savePath, saveString + "\n");
                    }
                }

                System.IO.File.AppendAllText(savePath, "# " + DateTime.Now.ToLocalTime().ToString() + "\t获取到酒馆终结特效如下：\n");
                System.IO.File.AppendAllText(savePath, "# [ID]\t[CollectionShortName]\t[CollectionName]\n");
                foreach (var record in GameDbf.BattlegroundsFinisher.GetRecords())
                {
                    string saveString;
                    if (record != null)
                    {
                        // GameUtils.TranslateDbIdToCardId(record.CardId, false);
                        saveString = $"{record.ID}\t{record.CollectionShortName.GetString()}\t{record.CollectionName.GetString()}";
                        System.IO.File.AppendAllText(savePath, saveString + "\n");
                    }
                }

                System.IO.File.AppendAllText(savePath, "# " + DateTime.Now.ToLocalTime().ToString() + "\t获取到英雄皮肤（包括酒馆）如下：\n");
                System.IO.File.AppendAllText(savePath, "# [CARD_ID]\t[Name]\t[HeroType]\n");
                foreach (var record in GameDbf.CardHero.GetRecords())
                {
                    string saveString;
                    if (record != null)
                    {
                        string name = GameDbf.Card.GetRecord(record.CardId).Name.GetString();
                        // GameUtils.TranslateDbIdToCardId(record.CardId, false);
                        saveString = $"{record.CardId}\t{name}\t{record.HeroType}\t";
                        System.IO.File.AppendAllText(savePath, saveString + "\n");
                    }
                }


                System.IO.File.AppendAllText(savePath, "# " + DateTime.Now.ToLocalTime().ToString() + "\t获取到宠物皮肤如下：\n");
                System.IO.File.AppendAllText(savePath, "# [PET_ID]\t[Name]\n");
                foreach (var record in GameDbf.PetVariant.GetRecords())
                {
                    string saveString;
                    if (record != null)
                    {
                        saveString = $"{record.ID}\t{record.Name.GetString()}\t";
                        System.IO.File.AppendAllText(savePath, saveString + "\n");
                    }
                }
                
            }
        }

        public static IEnumerator SaveLoadCardLocalTextures(Actor __instance, Texture texture)
        {
            try
            {
                if (null != texture)
                {
                    string cardId = __instance?.GetEntityDef()?.GetCardId();

                    if (!string.IsNullOrEmpty(cardId))
                    {
                        string cardName = __instance?.GetEntityDef()?.GetName();

                        string type = "";
                        if (TAG_PREMIUM.SIGNATURE == __instance.GetPremium())
                        {
                            type = "-SIGNATURE";
                        }

                        string filePath = Path.Combine(
                            Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/")),
                            "CardTexturesDownload", cardName + type + "-" + cardId + ".png");

                        if (File.Exists(filePath))
                        {
                            yield break;
                        }

                        RenderTexture rt = RenderTexture.GetTemporary(
                            texture.width,
                            texture.height,
                            0,
                            RenderTextureFormat.ARGBFloat,
                            RenderTextureReadWrite.Linear,
                            8,
                            RenderTextureMemoryless.None);

                        Material copyMat = new Material(Shader.Find("Unlit/Texture"));
                        Graphics.Blit(texture, rt, copyMat);
                        UnityEngine.Object.Destroy(copyMat);

                        RenderTexture previous = RenderTexture.active;
                        RenderTexture.active = rt;

                        Texture2D readableTexture = new Texture2D(
                            texture.width,
                            texture.height,
                            TextureFormat.RGBAFloat,
                            false,
                            true);

                        readableTexture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
                        readableTexture.Apply();

                        RenderTexture.active = previous;
                        RenderTexture.ReleaseTemporary(rt);

                        File.WriteAllBytes(filePath, readableTexture.EncodeToPNG());
                        UnityEngine.Object.Destroy(readableTexture);
                    }
                }
            }
            catch (Exception e)
            {
                if (!Directory.Exists(Path.Combine("CardTexturesDownload")))
                {
                    try
                    {
                        Directory.CreateDirectory(Path.Combine("CardTexturesDownload"));
                    }
                    catch (Exception e2)
                    {
                        MyLogger(LogLevel.Error, e2.Message);
                    }
                }

                MyLogger(LogLevel.Error, e);
            }

            yield break;
        }
    }
}
