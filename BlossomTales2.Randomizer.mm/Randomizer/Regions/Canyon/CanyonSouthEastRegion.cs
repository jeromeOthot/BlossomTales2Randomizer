using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlossomTales2.Randomizer.mm.Canyon
{
    public class CanyonSouthEastRegion : Region
    {
        public override Predicate<Inventory> CanAccess => inventory => inventory.CanAccesCanyon;

        public CanyonSouthEastRegion() : base("Canyon South East Region")
        {
            Locations = new List<Location>
            {
                //0S
                { new Location(new LocationId("overworld-19x21.tmx", "PickUpItem", new Vector3(536f, 0f, 2072f)), "Canyon Island Bone North East",_ => true, ItemType.CanyonBone) }, //accès canyon

                { new Location(new LocationId("overworld-17x21.tmx", "PickUpItem", new Vector3(412f, 0f, 2164f)),"Canyon Cemetery Bone North",_ => true, ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-17x22.tmx", "PickUpItem", new Vector3(1952f, 0f, 864f)), "Canyon Cemetery Bone",_ => true, ItemType.CanyonBone) }, //accès canyon
                { new Location(new LocationId("overworld-18x22.tmx", "PickUpItem", new Vector3(1864f, 0f, 1940f)), "Canyon Cemetery Bone East",_ => true, ItemType.CanyonBone) }, //accès canyon


                //Chest
                { new Location(new LocationId("overworld-17x22.tmx", "ghostCanyon", new Vector3(0f, 0f, 0f)), "Ghost Canyon",(inventory) => inventory.CanAccesCanyon && inventory.HasGhostPotion && inventory.CanAccesDarkForest && inventory.NbBlueGem > 10, ItemType.Crystal) }, //accès canyon && bouteille && accès dark && (blue gem > 10 || pelle)

            };

        }
    }
}
