using System;
using System.Linq;
using BlossomTales2;
using BlossomTales2.Randomizer.mm;
using Mono.Cecil;
using MonoMod;
using MonoMod.Cil;

namespace BlossomTales2
{
    class patch_Game1 : Game1
    {
        public extern void orig_Initialize();
        public static extern void orig_LoadStuff();

        protected override void Initialize()
        {
            orig_Initialize();
            RandomizerSingleton.Initialize();
        }

        public static void LoadStuff()
        {
            orig_LoadStuff();
            GameLogger.LogInfo("Is save successful " + !DidntCompleteLoad);
            if (!DidntCompleteLoad)
            {
                //TODO: Loader la liste de locations depuis la save<
                PermaListItem permaItem = Game1.Perma_Objects.FirstOrDefault(o => o.Name == "RandomizedLocations");

                RandomizerSingleton.Instance.LoadRandomizedLocations(permaItem?.LevelName);
            }
        }

        [MonoModIgnore]
        [PatchGame1LoadThreadStuff]
        public extern void LoadThreadStuff();

        //TODO: Trouver une façon de caller la vraie fonction. Game1.RandomFloat
        public static float RandomFloat(int a, int b, float divisor)
        {
            if (a == 0 && b == 0)
            {
                return 0f;
            }

            return (float)RandomNumber.Next(a, b) / divisor;
        }

        public static void UnlockAllForDebug()
        {
          player.Gold = 9999;
          player.Energy = 90f;
          player.MaxEnergy = 90f;
          player.ShieldLevel = 2;
          player.Health = 40;
          player.MaxHealth = 40;
          Globals.First_FullHeart = false;
          player.SwordLevel = 3;
          player.HasChargeSword = true;
          player.HasSwordBeams = true;
          player.QuarterHearts = 0;
          player.QuarterCrystals = 0;
          player.ChoseGuitar = true;
          player.HasFlippers = true;
          player.Ability = new EquipableItem[]
          {
            new E_GrappleHook(),
            new E_Boomerang()
          };
          player.AbilityIndex = new int[]{ 8, 1 };
          Globals.Learned_Songs.Add(Globaler.Songs.OpenSesame);
          Globals.Learned_Songs.Add(Globaler.Songs.WakeUp);
          Globals.Learned_Songs.Add(Globaler.Songs.CallHorse);
          Globals.Learned_Songs.Add(Globaler.Songs.SummonBalloon);
          Globals.Learned_Songs.Add(Globaler.Songs.GrandpaHint);
          Globals.Teleporters_Found.Add("overworld-21x17.tmx");
          Globals.Teleporters_Found.Add("overworld-20x19.tmx");
          Globals.Teleporters_Found.Add("overworld-23x18.tmx");
          Globals.Teleporters_Found.Add("jungles-24x21.tmx");
          Globals.Teleporters_Found.Add("overworld-16x20.tmx");
          Globals.Teleporters_Found.Add("overworld-17x22.tmx");
          Globals.Teleporters_Found.Add("overworld-18x18.tmx");
          Globals.Teleporters_Found.Add("overworld-16x17.tmx");
          Globals.Teleporters_Found.Add("overworld-25x16.tmx");
          Globals.Teleporters_Found.Add("overworld-18x21.tmx");
          Globals.Teleporters_Found.Add("overworld-21x21.tmx");
          Globals.Teleporters_Found.Add("jungles-25x19.tmx");
          player.Inventory.Add(EquipableItem.ItemList.Torch);
          player.Inventory.Add(EquipableItem.ItemList.Bow);
          player.Inventory.Add(EquipableItem.ItemList.Bombs);
          player.Inventory.Add(EquipableItem.ItemList.GrappleHook);
          player.Inventory.Add(EquipableItem.ItemList.Guitar);
          player.Inventory.Add(EquipableItem.ItemList.MirrorShield);
          player.Inventory.Add(EquipableItem.ItemList.Boomerang);
          player.Inventory.Add(EquipableItem.ItemList.RexTeleporter);
          player.Inventory.Add(EquipableItem.ItemList.FishingRod);
          player.Inventory.Add(EquipableItem.ItemList.Shovel);
          player.Inventory.Add(EquipableItem.ItemList.BeeMedallion);
          player.Inventory.Add(EquipableItem.ItemList.Falcon);
          player.Inventory.Add(EquipableItem.ItemList.Jar_Fire);
          player.Inventory.Add(EquipableItem.ItemList.Jar_Health);
          player.Inventory.Add(EquipableItem.ItemList.Jar_Ghost);
          player.Inventory.Add(EquipableItem.ItemList.Jar_BubbleShield);
          player.Inventory.Add(EquipableItem.ItemList.Jar_ArmorOrbs);
          player.Inventory.Add(EquipableItem.ItemList.Jar_DoubleDamage);
          player.Inventory.Add(EquipableItem.ItemList.Jar_SlowTime);
          player.Inventory.Add(EquipableItem.ItemList.Jar_Resurrection);
          player.Inventory.Add(EquipableItem.ItemList.Jar_ReduceCost);
          player.Inventory.Add(EquipableItem.ItemList.Jar_Speedster);
          player.Inventory_NE.Add(EquipableItem.ItemList.Flippers);
          player.Inventory_NE.Add(EquipableItem.ItemList.Honeycomb);
          player.Inventory_NE.Add(EquipableItem.ItemList.Ingred_Gem);
          player.Inventory_NE.Add(EquipableItem.ItemList.CombatScroll);
          player.Inventory_NE.Add(EquipableItem.ItemList.TreeSeed);
          player.Inventory_NE.Add(EquipableItem.ItemList.BlueGem);
          player.Inventory_NE.Add(EquipableItem.ItemList.GreenGem);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Mushroom, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Clover, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Lily, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.RootWeed, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.MoonFlower, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Apple, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Tulip, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Toadstool, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Willow, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Skyblossom, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.CanyonWisp, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Orange, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Spikeshell, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Clam, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Snailshell, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Seaweed, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Starfish, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Melon, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Chrysanthemum, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Aster, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Sunkiss, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Jojoba, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.DesertPuff, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.WaterDrop, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.FlameTongue, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.CactusRose, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.PurpleMushroom, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.RedMushroom, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.GreenMushroom, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Guts, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Poinsettia, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Bellflower, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Daisy, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Carambola, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Fishbones, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Fish1, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Fish2, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Fish3, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Fish4, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Fish5, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Fish6, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Fish7, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Fish8, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Fish9, 99);
          player.GiveIngredientReflection(EquipableItem.IngredientList.Fish10, 99);
          player.Count_Honeycombs = 10;
          player.Count_Gems = 60;
          player.Count_CombatScrolls = 4;
          player.Count_TreeSeeds = 5;
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchGame1LoadThreadStuff))]
    class PatchGame1LoadThreadStuffAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchGame1LoadThreadStuff(ILContext context, CustomAttribute attrib)
        {
            ILCursor cursor = new ILCursor(context);
            PatchCanyonBardCutscene(cursor);
            PatchFalconCutscene(cursor);
        }

        private static void PatchCanyonBardCutscene(ILCursor cursor)
        {
            //Find L.2094
            //CutSceneController = new CS_CanyonBard();
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchNewobj<CS_CanyonBard>()
            );

            //Remove Globals.MainQuestObjective <= Globaler.MainGameObjective.canyons_headToBard
            cursor.Index -= 4;
            cursor.RemoveRange(4);
        }

        private static void PatchFalconCutscene(ILCursor cursor)
        {
            //Find L.2189
            //CutSceneController = new CS_Falcon();
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchNewobj<CS_Falcon>()
            );

            //Remove !Game1.player.Inventory.Contains(EquipableItem.ItemList.Falcon)
            cursor.Index -= 5;
            cursor.RemoveRange(5);
        }
    }
}
