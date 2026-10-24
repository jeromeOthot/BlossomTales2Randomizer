using System.Collections.Generic;

namespace BlossomTales2.Randomizer.mm
{
    public static class ItemPools
    {
        private static readonly IReadOnlyDictionary<ItemType, int> s_progressionItems = new Dictionary<ItemType, int>()
        {
            { ItemType.Sword, 2},
            { ItemType.Shield, 3},
            { ItemType.Torch, 1},
            { ItemType.Bow, 1},
            { ItemType.Bombs, 1},
            { ItemType.GrappleHook, 1},
            { ItemType.Guitar, 1},
            { ItemType.Boomerang, 1},
            { ItemType.RexTeleporter, 1},
            { ItemType.Shovel, 1},
            { ItemType.FishingRod, 1},
            { ItemType.Jar_Empty, 1},
            { ItemType.Flippers, 1},
            { ItemType.BlueGem, 1},
            { ItemType.GreenGem, 1},
            { ItemType.HeartNecklace, 1},
            { ItemType.Gold_Key, 11},
            { ItemType.OpenSesame, 1},
            { ItemType.WakeUp, 1},
        };

        private static readonly IReadOnlyDictionary<ItemType, int> s_usefulItems = new Dictionary<ItemType, int>()
        {
            { ItemType.Sword, 2},
            { ItemType.Bow, 1},
            { ItemType.BeeMedallion, 1},
            { ItemType.Falcon, 1},
            { ItemType.Jar_Empty, 7},
            { ItemType.Five_Gems, 8},
            { ItemType.CombatScroll, 4},
            { ItemType.CallHorse, 1},
            { ItemType.SummonBalloon, 1},
            { ItemType.GrandpaHint, 1},
            { ItemType.HeartQ_4, 4},
            { ItemType.HeartQ_1, 52},
            { ItemType.Crystal, 20},
        };

        private static readonly IReadOnlyDictionary<ItemType, int> s_fetchQuestItems = new Dictionary<ItemType, int>()
        {
            { ItemType.Honeycomb, 10 },
            { ItemType.TreeSeed, 1 },
            { ItemType.CanyonBone, 20 },
        };

        private static readonly IReadOnlyDictionary<ItemType, int> s_keyRewards = new Dictionary<ItemType, int>()
        {
            { ItemType.KeyPiece1, 1},
            { ItemType.KeyPiece2, 1},
            { ItemType.KeyPiece3, 1},
        };

        public static IReadOnlyCollection<ItemType> CreateProgressionItemsPool()
        {
            List<ItemType> progressionItemsPool = CreatePool(s_progressionItems);
            if(RandomizerSettings.GamePlay.LocationPools.HasFlag(LocationPool.LongFetchQuests))
                progressionItemsPool.AddRange(CreatePool(s_fetchQuestItems));
            return progressionItemsPool;
        }

        public static IReadOnlyCollection<ItemType> CreateUsefulItemsPool()
        {
            List<ItemType> usefulItemsPool = CreatePool(s_usefulItems);
            if(!RandomizerSettings.GamePlay.LocationPools.HasFlag(LocationPool.LongFetchQuests))
                usefulItemsPool.AddRange(CreatePool(s_fetchQuestItems));
            return usefulItemsPool;
        }

        public static IReadOnlyCollection<ItemType> CreateKeyRewardsPool()
        {
            return CreatePool(s_keyRewards);
        }

        private static List<ItemType> CreatePool(IReadOnlyDictionary<ItemType, int> poolDefinition)
        {
            List<ItemType> pool = new List<ItemType>();
            foreach (KeyValuePair<ItemType, int> kvp in poolDefinition)
            {
                for (int i = 0; i < kvp.Value; i++)
                    pool.Add(kvp.Key);
            }
            return pool;
        }
    }
}
