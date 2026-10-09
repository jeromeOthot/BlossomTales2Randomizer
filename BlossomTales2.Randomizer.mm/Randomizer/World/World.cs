using System.Collections.Generic;
using System.Text;
using BlossomTales2.Randomizer.mm.Canyon;

namespace BlossomTales2.Randomizer.mm
{
    public class World
    {
        public List<Region> Regions { get; }

        public Blossomdale Blossomdale { get; }
        public BlossomCemetary BlossomdaleCemetary { get; }
        public OrchidTomb OrchidTomb { get; }
        public OverworldEast OverworldEast { get; }
        public OverworldWest OverworldWest { get; }
        public OverworldNorth OverworldNorth { get; }
        public JungleFront JungleFront { get; }
        public Anchortown Anchortown { get; }
        public JungleBack JungleBack { get; }
        public JungleNorthEast JungleNorthEast { get; }
        public JungleIsland JungleIsland { get; }
        public MorklaDungeon MorklaDungeon { get; }
        public CanyonNorthRegion CanyonNorth { get; }
        public CanyonSouthWestRegion CanyonSouthWest { get; }
        public CanyonSouthEastRegion CanyonSouthEast { get; }
        public CanyonVillageRegion CanyonVillage { get; }
        public CanyonIslandRegion CanyonIsland { get; }
        public CanyonSteppesNorthRegion CanyonSteppesNorth { get; }
        public CanyonSteppesSouthRegion CanyonSteppesSouth { get; }
        public CanyonTempleRegion  CanyonTemple { get; }
        public DarkWoodsFront DarkWoodsFront { get; }
        public Monsterton Monsterton { get; }
        public DarkWoodsBack DarkWoodsBack { get; }
        public MansionDungeon MansionDungeon { get; }
        public LabyrinthFront LabyrinthFront { get; }
        public Blockburg Blockburg { get; }
        public LabyrinthBack LabyrinthBack { get; }
        public MinotaurCastle MinotaurCastle { get; }
        public GlobalRegion Global { get; }

        public World()
        {
            Blossomdale = new Blossomdale(this);
            BlossomdaleCemetary = new BlossomCemetary(this);
            OrchidTomb = new OrchidTomb(this);
            OverworldEast = new OverworldEast(this);
            OverworldWest = new OverworldWest(this);
            OverworldNorth = new OverworldNorth(this);
            JungleFront = new JungleFront(this);
            Anchortown = new Anchortown(this);
            JungleBack = new JungleBack(this);
            JungleNorthEast = new JungleNorthEast(this);
            JungleIsland = new JungleIsland(this);
            MorklaDungeon = new MorklaDungeon(this);
            CanyonNorth = new CanyonNorthRegion(this);
            CanyonSouthWest = new CanyonSouthWestRegion(this);
            CanyonSouthEast = new CanyonSouthEastRegion(this);
            CanyonIsland = new CanyonIslandRegion(this);
            CanyonVillage = new CanyonVillageRegion(this);
            CanyonSteppesNorth = new CanyonSteppesNorthRegion(this);
            CanyonSteppesSouth = new CanyonSteppesSouthRegion(this);
            CanyonTemple = new CanyonTempleRegion(this);
            DarkWoodsFront = new DarkWoodsFront(this);
            Monsterton = new Monsterton(this);
            DarkWoodsBack = new DarkWoodsBack(this);
            MansionDungeon = new MansionDungeon(this);
            LabyrinthFront = new LabyrinthFront(this);
            Blockburg = new Blockburg(this);
            LabyrinthBack = new LabyrinthBack(this);
            MinotaurCastle = new MinotaurCastle(this);
            Global = new GlobalRegion(this);

            Regions = new List<Region>
            {
                Blossomdale,
                BlossomdaleCemetary,
                OrchidTomb,
                OverworldEast,
                OverworldWest,
                OverworldNorth,
                JungleFront,
                Anchortown,
                JungleBack,
                JungleNorthEast,
                JungleIsland,
                MorklaDungeon,
                CanyonNorth,
                CanyonSouthWest,
                CanyonSouthEast,
                CanyonIsland,
                CanyonVillage,
                CanyonSteppesNorth,
                CanyonSteppesSouth,
                CanyonTemple,
                DarkWoodsFront,
                Monsterton,
                DarkWoodsBack,
                MansionDungeon,
                LabyrinthFront,
                Blockburg,
                LabyrinthBack,
                MinotaurCastle,
                Global
            };
        }

        public bool CanAccessAnyOverworld(Inventory inventory) => true;
        public bool CanAccessAnyJungle(Inventory inventory) => JungleFront.CanAccess(inventory) || Anchortown.CanAccess(inventory) || JungleBack.CanAccess(inventory)
                                                               ||  JungleIsland.CanAccess(inventory) || JungleNorthEast.CanAccess(inventory);
        public bool CanAccesJungleBeach(Inventory inventory) => JungleBack.CanAccess(inventory) || JungleIsland.CanAccess(inventory);
        public bool CanAccessAnyCanyon(Inventory inventory) => CanyonNorth.CanAccess(inventory) || CanyonSouthWest.CanAccess(inventory) ||  CanyonSouthEast.CanAccess(inventory)
                                                            ||  CanyonIsland.CanAccess(inventory) || CanyonVillage.CanAccess(inventory)
                                                            ||  CanyonSteppesNorth.CanAccess(inventory) ||  CanyonSteppesSouth.CanAccess(inventory);
        public bool CanAccessAnyDarkForest(Inventory inventory) => DarkWoodsFront.CanAccess(inventory) ||  DarkWoodsBack.CanAccess(inventory) || Monsterton.CanAccess(inventory);
        public bool CanAccessAnyLabyrinth(Inventory inventory) => LabyrinthFront.CanAccess(inventory) ||  LabyrinthBack.CanAccess(inventory) || Blockburg.CanAccess(inventory);

        public List<ItemType> CollectItems(Inventory inventory, ref StringBuilder spheres)
        {
            List<ItemType> newItems = new List<ItemType>();

            foreach (Region region in Regions)
            {
                List<ItemType> items = region.CollectItems(inventory, ref spheres);
                newItems.AddRange(items);
            }
            return newItems;
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
