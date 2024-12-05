using System;
using static HsMod.PluginConfig;

namespace HsMod
{
    public partial class Utils
    {
        public static bool GetPremiumType(ref Entity ___m_entity, ref TAG_PREMIUM __result)
        {
            try
            {
                if (GameMgr.Get() != null && GameState.Get() != null && ___m_entity != null && GameState.Get().IsGameCreatedOrCreating())
                {
                    //跳过酒馆随从
                    if (GameMgr.Get().IsBattlegrounds())
                    {
                        if (!isBgsGoldenEnable.Value || ___m_entity.IsMinion() || ___m_entity.IsQuest())
                            return true;
                    }

                    Utils.CardState mGolden = goldenCardState.Value;
                    Utils.CardState mMaxState = maxCardState.Value;

                    //佣兵镀金
                    int dbid = GameUtils.TranslateCardIdToDbId(___m_entity.GetCardId());
                    bool mercDiamond = false;
                    bool isMerc = false;
                    if (Utils.CheckInfo.IsMercenarySkin(___m_entity.GetCardId(), out Utils.MercenarySkin skin))
                    {
                        isMerc = true;
                        if (dbid == skin.Diamond)
                        {
                            mercDiamond = true;
                        }
                    }

                    //屏蔽对手特效
                    if (___m_entity.IsControlledByOpposingSidePlayer() && (!isOpponentGoldenCardShow.Value) && (!GameMgr.Get().IsBattlegrounds()))
                    {
                        __result = TAG_PREMIUM.NORMAL;
                        if (isMerc)
                        {
                            ___m_entity.SetTag(GAME_TAG.PREMIUM, TAG_PREMIUM.NORMAL);
                            ___m_entity.SetTag(GAME_TAG.HAS_DIAMOND_QUALITY, false);
                        }
                        return false;
                    }


                    //其他品质
                    if (___m_entity.HasTag(GAME_TAG.HAS_DIAMOND_QUALITY) || ___m_entity.HasTag(GAME_TAG.HAS_SIGNATURE_QUALITY) || mercDiamond)
                    {
                        if (___m_entity.HasTag(GAME_TAG.HAS_DIAMOND_QUALITY) || mercDiamond)
                        {
                            if (mMaxState == Utils.CardState.All || (mMaxState == Utils.CardState.OnlyMy && ___m_entity.IsControlledByFriendlySidePlayer()))
                            {
                                if (mercDiamond)
                                {
                                    ___m_entity.SetTag(GAME_TAG.PREMIUM, TAG_PREMIUM.DIAMOND);
                                    ___m_entity.SetTag(GAME_TAG.HAS_DIAMOND_QUALITY, true);
                                }
                                __result = TAG_PREMIUM.DIAMOND;
                                return false;
                            }
                        }

                        if (___m_entity.HasTag(GAME_TAG.HAS_SIGNATURE_QUALITY) && isSignatureCardStateEnable.Value)
                        {
                            if (mMaxState == Utils.CardState.All || (mMaxState == Utils.CardState.OnlyMy && ___m_entity.IsControlledByFriendlySidePlayer()))
                            {
                                __result = TAG_PREMIUM.SIGNATURE;
                                ___m_entity.SetTag(GAME_TAG.PREMIUM, TAG_PREMIUM.SIGNATURE);
                                return false;
                            }
                        }

                        if ((mMaxState == Utils.CardState.Disabled) && (mGolden == Utils.CardState.Disabled))
                        {
                            __result = TAG_PREMIUM.NORMAL;
                            if (isMerc)
                            {
                                ___m_entity.SetTag(GAME_TAG.PREMIUM, TAG_PREMIUM.NORMAL);
                                ___m_entity.SetTag(GAME_TAG.HAS_DIAMOND_QUALITY, false);
                            }
                            return false;
                        }
                    }
                    //金卡特效
                    if (mGolden == Utils.CardState.All || (mGolden == Utils.CardState.OnlyMy && ___m_entity.IsControlledByFriendlySidePlayer()))
                    {
                        ___m_entity.SetTag(GAME_TAG.PREMIUM, TAG_PREMIUM.GOLDEN);
                        __result = TAG_PREMIUM.GOLDEN;
                        return false;
                    }
                    //禁用特效
                    if (mGolden == Utils.CardState.Disabled)
                    {
                        __result = TAG_PREMIUM.NORMAL;
                        if (isMerc)
                        {
                            ___m_entity.SetTag(GAME_TAG.PREMIUM, TAG_PREMIUM.NORMAL);
                            ___m_entity.SetTag(GAME_TAG.HAS_DIAMOND_QUALITY, false);
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.MyLogger(BepInEx.Logging.LogLevel.Error, ex);
                Utils.MyLogger(BepInEx.Logging.LogLevel.Error, ex.StackTrace);
            }
            return true;
        }

        public static void UpdateHeroTag(string cardId)
        {
            if (!Utils.CheckInfo.IsHero(cardId, out _))
            {
                return;
            }
            try
            {
                var defLoader = DefLoader.Get();
                if (defLoader == null)
                {
                    return;
                }
                //if (!defLoader.HasLoadedEntityDefs())
                //{
                //    defLoader.LoadAllEntityDefs();
                //}
                EntityDef entityDef = defLoader.GetEntityDef(cardId);
                if (entityDef == null)
                {
                    return;
                }
                if (entityDef.HasTag(GAME_TAG.EMOTECHARACTER) && (entityDef?.GetTag(GAME_TAG.EMOTECHARACTER) > 0))
                {
                    GameState gameState = GameState.Get();
                    if (gameState == null)
                    {
                        return;
                    }
                    int entityId = gameState.GetPlayerBySide(Player.Side.FRIENDLY).GetEntityId();
                    int tag = gameState.GetEntity(entityId).GetTag(GAME_TAG.HERO_ENTITY);
                    gameState.GetEntity(tag)?.SetTag(GAME_TAG.EMOTECHARACTER, entityDef.GetTag(GAME_TAG.EMOTECHARACTER));
                }
                if (entityDef.HasTag(GAME_TAG.CORNER_REPLACEMENT_TYPE) && (entityDef?.GetTag(GAME_TAG.CORNER_REPLACEMENT_TYPE) > 0))
                {
                    GameState gameState2 = GameState.Get();
                    if (gameState2 == null)
                    {
                        return;
                    }
                    int entityId2 = gameState2.GetPlayerBySide(Player.Side.FRIENDLY).GetEntityId();
                    gameState2.GetEntity(entityId2)?.SetTag(GAME_TAG.CORNER_REPLACEMENT_TYPE, entityDef.GetTag(GAME_TAG.CORNER_REPLACEMENT_TYPE));
                    new CornerSpellReplacementManager(false)?.UpdateCornerReplacements(CornerReplacementSpellType.NONE, CornerReplacementSpellType.NONE);
                }
            }
            catch (Exception ex)
            {
                Utils.MyLogger(BepInEx.Logging.LogLevel.Error, ex);
            }
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
                if (DefLoader.Get()?.GetEntityDef(DbID)?.GetCardType() == TAG_CARDTYPE.HERO)
                {
                    heroType = Assets.CardHero.HeroType.UNKNOWN;
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
    }

}
