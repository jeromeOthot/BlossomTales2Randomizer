using System.Collections.Generic;
using BlossomTales2.Randomizer.mm.Canyon;

namespace BlossomTales2.Randomizer.mm
{
    public class World
    {
        public List<Region> Regions { get; }
        public Inventory Inventory { get; set; }

        public Blossomdale Blossomdale { get; }
        public BlossomCemetary BlossomdaleCemetary { get; }
        public OrchidTomb OrchidTomb { get; }
        public OverworldEast OverworldEast { get; }
        public OverworldWest OverworldWest { get; }
        public OverworldNorth OverworldNorth { get; }
        public CanyonNorthWestRegion CanyonNorthWest { get; }
        public CanyonSouthWestRegion CanyonSouthWest { get; }
        public CanyonSouthEastRegion CanyonSouthEast { get; }
        public CanyonVillageRegion CanyonVillage { get; }
        public CanyonIslandRegion CanyonIsland { get; }
        public CanyonSteppesNorthRegion CanyonSteppesNorth { get; }

        public World()
        {
            Blossomdale = new Blossomdale(this);
            BlossomdaleCemetary = new BlossomCemetary(this);
            OrchidTomb = new OrchidTomb(this);
            OverworldEast = new OverworldEast(this);
            OverworldWest = new OverworldWest(this);
            OverworldNorth = new OverworldNorth(this);
            CanyonNorthWest = new CanyonNorthWestRegion(this);
            CanyonSouthWest = new CanyonSouthWestRegion(this);
            CanyonSouthEast = new CanyonSouthEastRegion(this);
            CanyonIsland = new CanyonIslandRegion(this);
            CanyonVillage = new CanyonVillageRegion(this);
            CanyonSteppesNorth = new CanyonSteppesNorthRegion(this);

            Regions = new List<Region>
            {
                Blossomdale,
                BlossomdaleCemetary,
                OrchidTomb,
                OverworldEast,
                OverworldWest,
                OverworldNorth,
                CanyonNorthWest,
                CanyonSouthWest,
                CanyonSouthEast,
                CanyonIsland,
                CanyonVillage,
                CanyonSteppesNorth,
            };
        }

        public bool TryCollectItems(Inventory inventory)
        {
            bool hasCollectedItem = false;

            foreach (Region region in Regions)
            {
                bool hasItem = region.TryCollectItems(inventory);
                if(!hasCollectedItem)
                    hasCollectedItem = hasItem;
            }
            return hasCollectedItem;
        }

        public void AddRegion(Region region)
        {
            Regions.Add(region);
        }

        public List<Location> CollectLocations()
        {
            List<Location> locations = new List<Location>();
            foreach (Region region in Regions)
                region.CollectLocations(locations);

            return locations;
        }
    }
}
