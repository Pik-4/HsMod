﻿using BepInEx.Logging;
using PegasusUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using static HsMod.PluginConfig;

namespace HsMod
{
    public class Utils
    {
        public enum CardState
        {
            [Description("Default（Никаких модификаций）")]
            Default,
            [Description("Действует только для дружеских вечеринок")]
            OnlyMy,
            [Description("Все вступает в силу")]
            All,
            [Description("Запрещать特效")]
            Disabled
        }
        //public enum QuickMode
        //{
        //    [Description("Default（Запрещать）")]
        //    Default,
        //    [Description("Поля боя")]
        //    Battlegrounds,
        //    [Description("Mercenary record")]
        //    Mercenaries,
        //    [Description("Все режимы")]
        //    AllMode,
        //    [Description("Запрещать")]
        //    Disabled
        //}
        public enum SkinType
        {
            [Description("рубашка назад")]
            CARDBACK,
            [Description("Cards")]
            CARD,
            [Description("монета")]
            COIN,
            [Description("скин героя")]
            HERO,
            [Description("Таверна Боб")]
            BOB,
            [Description("Спецэффекты концовки таверны")]
            BATTLEGROUNDSFINISHER,
            [Description("Таверна Поле битвы")]
            BATTLEGROUNDSBOARD,
            [Description("酒馆скин героя")]
            BATTLEGROUNDSHERO,
            [Description("Силы героя")]
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
            [Description("Default")]
            DoNothing,
            [Description("Вешать трубку")]
            AwayFromKeyboard,
            [Description("Анти-крючок")]
            AntiAwayFromKeyboard
        }
        public enum BuyAdventureTemplate
        {
            [Description("Default")]
            DoNothing,
            [Description("Проклятие Наксрамаса")]
            BuyNAX,
            [Description("Огонь Черной горы")]
            BuyBRM,
            [Description("Общество исследователей")]
            BuyLOE,
            [Description("Каражан Ночь")]
            BuyKara
        }
        public enum CardRarity    // CardsRare度
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
                        return $"<td>золотой ординарный</td><td>{count}</td>";
                    }
                    else
                    {
                        cardCount.common += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 5;
                        return $"<td>обычно</td><td>{count}</td>";
                    }
                case TAG_RARITY.RARE:
                    if (golden)
                    {
                        cardCount.gRare += count;
                        cardCount.gTotal += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 100;
                        return $"<td>золотой редкий</td><td>{count}</td>";
                    }
                    else
                    {
                        cardCount.rare += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 20;
                        return $"<td>Rare</td><td>{count}</td>";
                    }
                case TAG_RARITY.EPIC:
                    if (golden)
                    {
                        cardCount.gEpic += count;
                        cardCount.gTotal += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 400;
                        return $"<td>золотая эпопея</td><td>{count}</td>";
                    }
                    else
                    {
                        cardCount.epic += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 100;
                        return $"<td>Epic</td><td>{count}</td>";
                    }
                case TAG_RARITY.LEGENDARY:
                    if (golden)
                    {
                        cardCount.gLegendary += count;
                        cardCount.gTotal += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 1600;
                        return $"<td>金色легенда</td><td>{count}</td>";
                    }
                    else
                    {
                        cardCount.legendary += count;
                        cardCount.total += count;
                        cardCount.totalDust += count * 400;
                        return $"<td>легенда</td><td>{count}</td>";
                    }
                default:
                    return "<td>неизвестный</td>";
            }
        }

        public static string RankIdxToString(int rank)
        {
            string text;
            switch ((rank - 1) / 10)
            {
                case 0:
                    text = "бронза";
                    break;
                case 1:
                    text = "серебро";
                    break;
                case 2:
                    text = "золото";
                    break;
                case 3:
                    text = "платина";
                    break;
                case 4:
                    text = "алмаз";
                    break;
                case 5:
                    return "легенда";
                default:
                    text = "неизвестный";
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

            Utils.MyLogger(BepInEx.Logging.LogLevel.Warning, Utils.CacheLastOpponentFullName + Utils.CacheLastOpponentAccountID.EntityId.ToString() + "Сообщено");
        }

        public static void TryRefundCardDisenchantCallback()
        {
            Network.CardSaleResult cardSaleResult = Network.Get().GetCardSaleResult();
            if (cardSaleResult.Action != Network.CardSaleResult.SaleResult.CARD_WAS_SOLD)
            {
                MyLogger(LogLevel.Warning, $"Разложение не удалось：{cardSaleResult.Action}");
                UIStatus.Get().AddInfo("Разложение не удалось");
            }
            else
            {
                MyLogger(LogLevel.Warning, "Разложение успешно");
                CollectionManager.Get().OnCollectionChanged();
            }
        }

        //Уничтожьте это
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
        //    MyLogger(LogLevel.Warning, "Попробуй разбить пыль：" + totalSell);
        //    UIStatus.Get().AddInfo("Попробуй разбить пыль：" + totalSell);
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
            if ((SceneMgr.Get()?.GetMode() == SceneMgr.Mode.LETTUCE_COLLECTION) || (SceneMgr.Get().GetMode() == SceneMgr.Mode.LETTUCE_PACK_OPENING)) // после исполнения，Нужно заново войти в коллекцию наемников
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
                            m_mercenaryAcknowledgements.Append(mercenaryAcknowledgeData);
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
                            m_mercenaryAcknowledgements.Append(mercenaryAcknowledgeData);
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
                            m_mercenaryAcknowledgements.Append(mercenaryAcknowledgeData);
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
            foreach (var record in CollectionManager.Get().GetOwnedCards())
            {
                if (record != null && record.IsCraftable && record.IsRefundable && (record.OwnedCount > 0))    // Золотые карты и обычные карты будут активироваться по одному разу.，Но все сразу
                {
                    CraftingManager.Get().TryGetCardSellValue(record.CardId, record.PremiumType, out int value);
                    totalSell += record.OwnedCount * value;

                    CraftingManager.Get().TryGetCardSellValue(record.CardId, TAG_PREMIUM.NORMAL, out int normalValue);
                    CraftingManager.Get().TryGetCardSellValue(record.CardId, TAG_PREMIUM.GOLDEN, out int goldenValue);

                    int numNormalCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(record.CardId, TAG_PREMIUM.NORMAL);
                    int numGoldenCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(record.CardId, TAG_PREMIUM.GOLDEN);
                    int numSignatureCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(record.CardId, TAG_PREMIUM.SIGNATURE);

                    CraftingPendingTransaction m_pendingClientTransaction = new CraftingPendingTransaction
                    {
                        CardID = record.CardId,
                        Premium = record.PremiumType,
                        NormalDisenchantCount = numNormalCopiesInCollection,
                        GoldenDisenchantCount = numGoldenCopiesInCollection,
                        SignatureDisenchantCount = numSignatureCopiesInCollection
                    };

                    value = -(normalValue * numNormalCopiesInCollection + goldenValue * numGoldenCopiesInCollection);
                    network.CraftingTransaction(m_pendingClientTransaction, value, numNormalCopiesInCollection, numGoldenCopiesInCollection, numSignatureCopiesInCollection);
                    m_pendingClientTransaction = null;
                }
            }
            MyLogger(LogLevel.Warning, "Попробуй разбить пыль：" + totalSell);
            UIStatus.Get().AddInfo("Попробуй разбить пыль：" + totalSell);
        }

        public static void TryGetSafeImg()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://qndxx.youth54.cn/SmartLA/dxxjfgl.w?method=getNewestVersionInfo");
                request.Timeout = 2333;
                request.ReadWriteTimeout = 2333;
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                request.ServerCertificateValidationCallback = (_s, _x509s, _x509c, _ssl) => { return (true); };
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                var res = System.Text.RegularExpressions.Regex.Match(retString, "https?://h5.cyol.com/special/daxuexi/.*?/");
                if (res.Success)
                {
                    Utils.MyLogger(LogLevel.Warning, res + "images/end.jpg");
                    request = (HttpWebRequest)WebRequest.Create("http://qndxx.youth54.cn/SmartLA/dxxjfgl.w?method=getNewestVersionInfo");
                    request.Timeout = 2333;
                    request.ReadWriteTimeout = 2333;
                    request.Method = "GET";
                    request.ContentType = "text/html;charset=UTF-8";
                    request.ServerCertificateValidationCallback = (_s, _x509s, _x509c, _ssl) => { return (true); };
                    var statusCode = Convert.ToInt32(((HttpWebResponse)request.GetResponse()).StatusCode);
                    myResponseStream.Close();
                    if (statusCode != 200)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        webPageBackImg.Value = res + "images/end.jpg?safeimg";
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Utils.MyLogger(LogLevel.Warning, ex);
                Utils.MyLogger(LogLevel.Warning, ex.StackTrace);
                webPageBackImg.Value = "";
            }
        }


        //Диапазон данных ложного открытия пакета
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
                        else if (entitydef.GetCost() != 0)    // Игнорировать скины героев
                            cardsDbId.Add(dbid);
                    }
                }
            }
            return cardsDbId;
        }

        //ложные результаты，指定Rare度
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


        //ложные результаты
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
                UIStatus.Get().AddInfo("Неинициализированный！");
                return;
            }
            if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
            {
                UIStatus.Get().AddInfo("Невозможно приобрести в игре.！");
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
                    Utils.MyLogger(LogLevel.Warning, $"{adventure}：Приключение уже принадлежит！");
                    UIStatus.Get().AddInfo("Выбранное приключение уже принадлежит！");
                }
                else
                {
                    StoreManager.Get().StartAdventureTransaction(productType, wingID, null, null, global::ShopType.ADVENTURE_STORE, 1, false, null, 0);

                    //ProductDataModel productByPmtId = StoreManager.Get().Catalog.GetProductByPmtId(ProductId.CreateFromValidated((long)0));     // Купить классические наборы карт
                    //PriceDataModel priceDataModel = productByPmtId.Prices.FirstOrDefault((PriceDataModel p) => p.Currency == CurrencyType.GOLD);
                    //Shop.Get().AttemptToPurchaseProduct(productByPmtId, priceDataModel, 1);
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
            //HeroesPowerMapping.Add("CS2_083b_H1", "CS2_102_H1"); Решить существующие проблемы，нравиться，Навыки Майев указаны неправильно.。
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

        public static class CheckInfo
        {
            public static bool IsCoin()
            {
                if (CacheCoin.Count == 0) CacheInfo.UpdateCoin();
                if (CacheCoin.Contains(skinCoin.Value)) return true;
                else return false;
            }
            public static bool IsCoin(string cardId)
            {
                int dbId = GameUtils.TranslateCardIdToDbId(cardId);
                if (CacheCoinCard.Count == 0) CacheInfo.UpdateCoinCard();
                if (CacheCoinCard.Contains(dbId)) return true;
                else return false;
            }
            public static bool IsBoard()
            {
                if (CacheGameBoard.Count == 0) CacheInfo.UpdateGameBoard();
                if (CacheGameBoard.Contains(skinBoard.Value)) return true;
                else return false;
            }
            public static bool IsBgsBoard()
            {
                if (CacheBgsBoard.Count == 0) CacheInfo.UpdateBgsBoard();
                if (CacheBgsBoard.Contains(skinBgsBoard.Value)) return true;
                else return false;
            }
            public static bool IsHero(int DbID, out Assets.CardHero.HeroType heroType)
            {
                if (CacheHeroes.Count == 0) CacheInfo.UpdateHeroes();
                if (CacheHeroes.ContainsKey(DbID))
                {
                    heroType = CacheHeroes[DbID];
                    return true;
                }
                else
                {
                    heroType = Assets.CardHero.HeroType.UNKNOWN;
                    return false;
                }
            }
            public static bool IsCardBack()
            {
                if (CacheCardBack.Count == 0) CacheInfo.UpdateCardBack();
                if (CacheCardBack.Contains(skinCardBack.Value)) return true;
                else return false;
            }
            public static bool IsBgsFinisher()
            {
                if (CacheBgsFinisher.Count == 0) CacheInfo.UpdateBgsFinisher();
                if (CacheBgsFinisher.Contains(skinBgsFinisher.Value)) return true;
                else return false;
            }

            public static bool IsHero(string cardID, out Assets.CardHero.HeroType heroType)
            {
                if (CacheHeroes.Count == 0) CacheInfo.UpdateHeroes();
                int dbid = GameUtils.TranslateCardIdToDbId(cardID);
                if (CacheHeroes.ContainsKey(dbid))
                {
                    heroType = CacheHeroes[dbid];
                    return true;
                }
                else
                {
                    heroType = Assets.CardHero.HeroType.UNKNOWN;
                    return false;
                }
            }

            public static bool IsMercenarySkin(string cardID, out MercenarySkin skin)
            {
                if (CacheMercenarySkin.Count == 0) CacheInfo.UpdateMercenarySkin();
                int dbid = GameUtils.TranslateCardIdToDbId(cardID);

                foreach (var mercSkin in CacheMercenarySkin)
                {
                    if (mercSkin.Id.Contains(dbid))
                    {
                        skin = mercSkin;
                        return true;
                    }
                }
                skin = new MercenarySkin();
                return false;
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

        public static class LeakInfo
        {
            public static void Mercenaries(string savePath = @"BepInEx/HsMercenaries.log")
            {
                //List<LettuceTeam> teams = CollectionManager.Get().GetTeams();
                //System.IO.File.WriteAllText(savePath, DateTime.Now.ToLocalTime().ToString() + "\tСоберите свою команду следующим образом：\n");
                //foreach (LettuceTeam team in teams)
                //{
                //    System.IO.File.AppendAllText(savePath, team.Name + "\n");
                //}
                System.IO.File.WriteAllText(savePath, DateTime.Now.ToLocalTime().ToString() + "\t获取到关卡信息нравиться下：\n");
                System.IO.File.AppendAllText(savePath, "[ID]\t[Heroic?]\t[Bounty]\t[BossName]\n");
                foreach (var record in GameDbf.LettuceBounty.GetRecords())     // Создать название уровня
                {
                    string saveString;
                    if (record != null)
                    {
                        saveString = record.ID.ToString() + (record.Heroic ? " Heroic " : " ") + record.BountySetRecord.Name.GetString() + " " + GameDbf.Card.GetRecord(record.FinalBossCardId).Name.GetString();
                        System.IO.File.AppendAllText(savePath, saveString + "\n");
                    }
                }
            }
            public static void MyCards(string savePath = @"BepInEx/HsRefundCards.log")
            {
                System.IO.File.AppendAllText(savePath, DateTime.Now.ToLocalTime().ToString() + "\tСитуация с получением полностью разложенных карт следующая:：\n");
                System.IO.File.AppendAllText(savePath, "[Name]\t[PremiumType]\t[Rarity]\t[CardId]\t[CardDbId]\t[OwnedCount]\n");
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
            public static void Skins(string savePath = "BepInEx/HsSkins.log")
            {
                System.IO.File.WriteAllText(savePath, DateTime.Now.ToLocalTime().ToString() + "\t获取到монета皮肤нравиться下：\n");
                System.IO.File.AppendAllText(savePath, "[CARD_ID]\t[Name]\n");
                foreach (var record in GameDbf.CosmeticCoin.GetRecords())
                {
                    string saveString;
                    if (record != null)
                    {
                        saveString = $"{record.CardId}\t{record.Name.GetString()}";
                        System.IO.File.AppendAllText(savePath, saveString + "\n");
                    }
                }
                System.IO.File.AppendAllText(savePath, DateTime.Now.ToLocalTime().ToString() + "\t获取到рубашка назад信息нравиться下：\n");
                System.IO.File.AppendAllText(savePath, "[ID]\t[Name]\n");
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

                System.IO.File.AppendAllText(savePath, DateTime.Now.ToLocalTime().ToString() + "\tИнформация об игровой панели получается следующим образом:：\n");
                System.IO.File.AppendAllText(savePath, "[ID]\t[NOTE_DESC]\n");
                foreach (var record in GameDbf.Board.GetRecords())
                {
                    string saveString;
                    if (record != null)
                    {
                        // GameUtils.TranslateDbIdToCardId(record.CardId, false);
                        saveString = $"{record.ID}\t{record.GetVar("NOTE_DESC")}";
                        System.IO.File.AppendAllText(savePath, saveString + "\n");
                    }
                }

                System.IO.File.AppendAllText(savePath, DateTime.Now.ToLocalTime().ToString() + "\t获取到酒馆战斗面板нравиться下：\n");
                System.IO.File.AppendAllText(savePath, "[ID]\t[CollectionShortName]\t[CollectionName]\n");
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

                System.IO.File.AppendAllText(savePath, DateTime.Now.ToLocalTime().ToString() + "\tПолучите специальные эффекты концовки таверны следующим образом:：\n");
                System.IO.File.AppendAllText(savePath, "[ID]\t[CollectionShortName]\t[CollectionName]\n");
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

                System.IO.File.AppendAllText(savePath, DateTime.Now.ToLocalTime().ToString() + "\t获取到скин героя（включая паб）нравиться下：\n");
                System.IO.File.AppendAllText(savePath, "[CARD_ID]\t[Name]\t[HeroType]\n");
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
            }
        }
    }
}
