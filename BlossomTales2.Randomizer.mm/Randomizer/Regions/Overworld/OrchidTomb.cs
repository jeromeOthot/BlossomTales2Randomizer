using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm
{
    public class OrchidTomb : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => inventory.HasTorch;

        public OrchidTomb(World world) : base("King Orchid's Tomb", world)
        {
            Locations = new List<Location>()
            {
                new Location(new LocationId("orchid-tomb-3.tmx", "Chest_Small", new Vector3(416f, 0f, 320f)),
                    "Rat Chest",
                    inventory => inventory.CanSwitchLevers && inventory.CanDoDamage,
                    ItemType.Gold_Key),
                new Location(new LocationId("orchid-tomb-4.tmx", "orchid_heart", new Vector3(604f, 0f, 304f)),
                    "King Orchid Reward",
                    inventory => inventory.HasKeys && inventory.HasTorch && inventory.CanDoDamage,
                    ItemType.HeartQ_4),
                new Location(new LocationId("orchid-tomb-4.tmx", "orchid_sword", new Vector3(604f, 0f, 304f)),
                    "King Orchid Sword",
                    inventory => inventory.HasKeys && inventory.HasTorch && inventory.CanDoDamage,
                    ItemType.Sword),
                new Location(new LocationId("orchid-tomb-4.tmx", "orchid_shield", new Vector3(604f, 0f, 304f)),
                    "King Orchid Shield",
                    inventory => inventory.HasKeys && inventory.HasTorch && inventory.CanDoDamage,
                    ItemType.Shield)
            };
        }
    }
}
