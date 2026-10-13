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
            { ItemType.FishingRod, 1},
            { ItemType.Jar_Empty, 1},
            { ItemType.Flippers, 1},
            { ItemType.BlueGem, 1},
            { ItemType.GreenGem, 1},
            { ItemType.Gold_Key, 11},
            { ItemType.OpenSesame, 1},
            { ItemType.WakeUp, 1},
        };

        private static readonly IReadOnlyDictionary<ItemType, int> s_usefulItems = new Dictionary<ItemType, int>()
        {
            { ItemType.Sword, 2},
            { ItemType.Shovel, 1},
            { ItemType.Bow, 1},
            { ItemType.BeeMedallion, 1},
            { ItemType.Falcon, 1},
            { ItemType.Jar_Empty, 7},
            { ItemType.Honeycomb, 10},
            { ItemType.Five_Gems, 8},
            { ItemType.TreeSeed, 1},
            { ItemType.CanyonBone, 20},
            { ItemType.CombatScroll, 4},
            { ItemType.HeartNecklace, 1},
            { ItemType.CallHorse, 1},
            { ItemType.SummonBalloon, 1},
            { ItemType.GrandpaHint, 1},
            { ItemType.HeartQ_4, 4},
            { ItemType.HeartQ_1, 52},
            { ItemType.Crystal, 20},
        };

        private static readonly IReadOnlyDictionary<ItemType, int> s_keyRewards = new Dictionary<ItemType, int>()
        {
            { ItemType.KeyPiece1, 1},
            { ItemType.KeyPiece2, 1},
            { ItemType.KeyPiece3, 1},
        };

        public static IReadOnlyCollection<ItemType> CreateProgressionItemsPool()
        {
            return CreatePool(s_progressionItems);
        }

        public static IReadOnlyCollection<ItemType> CreateUsefulItemsPool()
        {
            return CreatePool(s_usefulItems);
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
