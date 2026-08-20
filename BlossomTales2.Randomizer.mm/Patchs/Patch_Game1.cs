using BlossomTales2.Randomizer.mm;
using MonoMod;

namespace BlossomTales2
{
    class patch_Game1 : Game1
    {
        public extern void orig_Initialize();
        public static extern void orig_LoadStuff();

        protected override void Initialize()
        {
            orig_Initialize();
        }

        public static void LoadStuff()
        {
            orig_LoadStuff();
            GameLogger.LogInfo("Is save successful " + !DidntCompleteLoad);
            if (!DidntCompleteLoad)
            {
                //TODO: Loader la liste de locations depuis la save
                RandomizerSingleton.Initialize();
            }
        }

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
          //Game1.Camera.OverridePosition(new Vector2((float) (int) Game1.player.Position.X, (float) (int) Game1.player.Position.Z), 1f);
          //this.currentState = (GameState) GameState_Playing.GetInstance(this);
          Game1.player.Gold = 9999;
          Game1.player.Energy = 90f;
          Game1.player.MaxEnergy = 90f;
          Game1.player.ShieldLevel = 2;
          Game1.player.Health = 16 /*0x10*/;
          Game1.player.MaxHealth = 16 /*0x10*/;
          Game1.Globals.First_FullHeart = false;
          Game1.player.SwordLevel = 4;
          Game1.player.HasChargeSword = true;
          Game1.player.QuarterHearts = 1;
          Game1.player.QuarterCrystals = 3;
          Game1.player.ChoseGuitar = true;
          Game1.player.HasFlippers = true;
          Game1.player.Ability = new EquipableItem[2]
          {
            (EquipableItem) new E_GrappleHook(),
            (EquipableItem) new E_Boomerang()
          };
          Game1.player.AbilityIndex = new int[2]{ 8, 1 };
          Game1.Globals.Learned_Songs.Add(Globaler.Songs.OpenSesame);
          Game1.Globals.Learned_Songs.Add(Globaler.Songs.WakeUp);
          Game1.Globals.Learned_Songs.Add(Globaler.Songs.CallHorse);
          Game1.Globals.Learned_Songs.Add(Globaler.Songs.SummonBalloon);
          Game1.Globals.Learned_Songs.Add(Globaler.Songs.GrandpaHint);
          Game1.Globals.Teleporters_Found.Add("overworld-21x17.tmx");
          Game1.Globals.Teleporters_Found.Add("overworld-20x19.tmx");
          Game1.Globals.Teleporters_Found.Add("overworld-23x18.tmx");
          Game1.Globals.Teleporters_Found.Add("jungles-24x21.tmx");
          Game1.Globals.Teleporters_Found.Add("overworld-16x20.tmx");
          Game1.Globals.Teleporters_Found.Add("overworld-17x22.tmx");
          Game1.Globals.Teleporters_Found.Add("overworld-18x18.tmx");
          Game1.Globals.Teleporters_Found.Add("overworld-16x17.tmx");
          Game1.Globals.Teleporters_Found.Add("overworld-25x16.tmx");
          Game1.Globals.Teleporters_Found.Add("overworld-18x21.tmx");
          Game1.Globals.Teleporters_Found.Add("overworld-21x21.tmx");
          Game1.Globals.Teleporters_Found.Add("jungles-25x19.tmx");
          Game1.player.Inventory.Add(EquipableItem.ItemList.Torch);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Bow);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Bombs);
          Game1.player.Inventory.Add(EquipableItem.ItemList.GrappleHook);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Guitar);
          Game1.player.Inventory.Add(EquipableItem.ItemList.MirrorShield);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Boomerang);
          Game1.player.Inventory.Add(EquipableItem.ItemList.RexTeleporter);
          Game1.player.Inventory.Add(EquipableItem.ItemList.FishingRod);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Shovel);
          Game1.player.Inventory.Add(EquipableItem.ItemList.BeeMedallion);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Jar_Fire);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Jar_Health);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Jar_Ghost);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Jar_BubbleShield);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Jar_ArmorOrbs);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Jar_DoubleDamage);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Jar_SlowTime);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Jar_Resurrection);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Jar_ReduceCost);
          Game1.player.Inventory.Add(EquipableItem.ItemList.Jar_Speedster);
          Game1.player.Inventory_NE.Add(EquipableItem.ItemList.Flippers);
          Game1.player.Inventory_NE.Add(EquipableItem.ItemList.Honeycomb);
          Game1.player.Inventory_NE.Add(EquipableItem.ItemList.Ingred_Gem);
          Game1.player.Inventory_NE.Add(EquipableItem.ItemList.CombatScroll);
          Game1.player.Inventory_NE.Add(EquipableItem.ItemList.Letter);
          Game1.player.Inventory_NE.Add(EquipableItem.ItemList.TreeSeed);
          Game1.player.Count_Honeycombs = 10;
          Game1.player.Count_Gems = 20;
          Game1.player.Count_CombatScrolls = 4;
          Game1.player.Count_TreeSeeds = 4;
          Game1.Globals.FoundLevels.Add("morkla-1.tmx");
          Game1.Globals.FoundLevels.Add("morkla-2.tmx");
          Game1.Globals.FoundLevels.Add("morkla-3.tmx");
          Game1.Globals.FoundLevels.Add("morkla-4.tmx");
          Game1.Globals.FoundLevels.Add("morkla-5.tmx");
          Game1.Globals.FoundLevels.Add("morkla-6.tmx");
          Game1.Globals.FoundLevels.Add("morkla-7.tmx");
          Game1.Globals.FoundLevels.Add("morkla-8.tmx");
          Game1.Globals.FoundLevels.Add("morkla-9.tmx");
          Game1.Globals.FoundLevels.Add("morkla-10.tmx");
          Game1.Globals.FoundLevels.Add("morkla-11.tmx");
          Game1.Globals.FoundLevels.Add("morkla-12.tmx");
          Game1.Globals.FoundLevels.Add("morkla-13.tmx");
          Game1.Globals.FoundLevels.Add("morkla-14.tmx");
          Game1.Globals.FoundLevels.Add("morkla-15.tmx");
          Game1.Globals.FoundLevels.Add("morkla-16.tmx");
          Game1.Globals.FoundLevels.Add("morkla-17.tmx");
          Game1.Globals.FoundLevels.Add("morkla-18.tmx");
          Game1.Globals.FoundLevels.Add("morkla-19.tmx");
          Game1.Globals.FoundLevels.Add("morkla-pirateBoss.tmx");
          Game1.Globals.FoundLevels.Add("morkla-octopus.tmx");
          Game1.Globals.FoundLevels.Add("temple-1.tmx");
          Game1.Globals.FoundLevels.Add("temple-2.tmx");
          Game1.Globals.FoundLevels.Add("temple-3.tmx");
          Game1.Globals.FoundLevels.Add("temple-4.tmx");
          Game1.Globals.FoundLevels.Add("temple-5.tmx");
          Game1.Globals.FoundLevels.Add("temple-6.tmx");
          Game1.Globals.FoundLevels.Add("temple-7.tmx");
          Game1.Globals.FoundLevels.Add("temple-8.tmx");
          Game1.Globals.FoundLevels.Add("temple-9.tmx");
          Game1.Globals.FoundLevels.Add("temple-10.tmx");
          Game1.Globals.FoundLevels.Add("temple-11.tmx");
          Game1.Globals.FoundLevels.Add("temple-12.tmx");
          Game1.Globals.FoundLevels.Add("temple-13.tmx");
          Game1.Globals.FoundLevels.Add("temple-14.tmx");
          Game1.Globals.FoundLevels.Add("temple-15.tmx");
          Game1.Globals.FoundLevels.Add("temple-16.tmx");
          Game1.Globals.FoundLevels.Add("temple-17.tmx");
          Game1.Globals.FoundLevels.Add("temple-18.tmx");
          Game1.Globals.FoundLevels.Add("temple-19.tmx");
          Game1.Globals.FoundLevels.Add("temple-20.tmx");
          Game1.Globals.FoundLevels.Add("temple-21.tmx");
          Game1.Globals.FoundLevels.Add("temple-22.tmx");
          Game1.Globals.FoundLevels.Add("temple-23.tmx");
          Game1.Globals.FoundLevels.Add("temple-24.tmx");
          Game1.Globals.FoundLevels.Add("temple-genieBoss.tmx");
          Game1.Globals.FoundLevels.Add("mansion-1.tmx");
          Game1.Globals.FoundLevels.Add("mansion-2.tmx");
          Game1.Globals.FoundLevels.Add("mansion-3.tmx");
          Game1.Globals.FoundLevels.Add("mansion-4.tmx");
          Game1.Globals.FoundLevels.Add("mansion-5.tmx");
          Game1.Globals.FoundLevels.Add("mansion-6.tmx");
          Game1.Globals.FoundLevels.Add("mansion-7.tmx");
          Game1.Globals.FoundLevels.Add("mansion-8.tmx");
          Game1.Globals.FoundLevels.Add("mansion-9.tmx");
          Game1.Globals.FoundLevels.Add("mansion-10.tmx");
          Game1.Globals.FoundLevels.Add("mansion-11.tmx");
          Game1.Globals.FoundLevels.Add("mansion-12.tmx");
          Game1.Globals.FoundLevels.Add("mansion-13.tmx");
          Game1.Globals.FoundLevels.Add("mansion-14.tmx");
          Game1.Globals.FoundLevels.Add("mansion-15.tmx");
          Game1.Globals.FoundLevels.Add("mansion-16.tmx");
          Game1.Globals.FoundLevels.Add("mansion-17.tmx");
          Game1.Globals.FoundLevels.Add("mansion-18.tmx");
          Game1.Globals.FoundLevels.Add("mansion-19.tmx");
          Game1.Globals.FoundLevels.Add("mansion-20.tmx");
          Game1.Globals.FoundLevels.Add("mansion-21.tmx");
          Game1.Globals.FoundLevels.Add("mansion-22.tmx");
          Game1.Globals.FoundLevels.Add("mansion-23.tmx");
          Game1.Globals.FoundLevels.Add("mansion-24.tmx");
          Game1.Globals.FoundLevels.Add("mansion-bossScientist.tmx");
          Game1.Globals.FoundLevels.Add("mansion-bossVampire.tmx");
          Game1.Globals.FoundLevels.Add("castle-1.tmx");
          Game1.Globals.FoundLevels.Add("castle-2.tmx");
          Game1.Globals.FoundLevels.Add("castle-3.tmx");
          Game1.Globals.FoundLevels.Add("castle-4.tmx");
          Game1.Globals.FoundLevels.Add("castle-5.tmx");
          Game1.Globals.FoundLevels.Add("castle-6.tmx");
          Game1.Globals.FoundLevels.Add("castle-7.tmx");
          Game1.Globals.FoundLevels.Add("castle-8.tmx");
          Game1.Globals.FoundLevels.Add("castle-9.tmx");
          Game1.Globals.FoundLevels.Add("castle-10.tmx");
          Game1.Globals.FoundLevels.Add("castle-11.tmx");
          Game1.Globals.FoundLevels.Add("castle-12.tmx");
          Game1.Globals.FoundLevels.Add("castle-13.tmx");
          Game1.Globals.FoundLevels.Add("castle-14.tmx");
          Game1.Globals.FoundLevels.Add("castle-15.tmx");
          Game1.Globals.FoundLevels.Add("castle-16.tmx");
          Game1.Globals.FoundLevels.Add("castle-minotaurThrone.tmx");
          //this.LoadLastLevelName();
          //this.ChangeLevel(0, Game1.FadeNewLevelName);
        }
    }
}
