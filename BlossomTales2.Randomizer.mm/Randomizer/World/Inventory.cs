using System.Collections.Generic;

namespace BlossomTales2.Randomizer.mm
{
    public class Inventory
    {
        public Dictionary<ItemType, int> Items { get; set; } =  new Dictionary<ItemType, int>();
        private World _world;

        public Inventory(World world)
        {
            _world = world;
        }

        //TODO: Real endgame flag.
        public bool HasBeatenMinotaurKing => HasBombs && HasSword && HasFlipper && HasTorch && HasGrappleHook &&
                                             HasBoomerang && HasBow && CanWakeUpPeople;

        public bool HasSword => Items.ContainsKey(ItemType.Sword);
        public bool HasSwordBeams => Items.TryGetValue(ItemType.Sword, out int swordLevel) && swordLevel >= 4;
        public bool HasMirrorShield => Items.TryGetValue(ItemType.Shield, out int shieldLevel) && shieldLevel >= 3;
        public bool HasTorch => Items.ContainsKey(ItemType.Torch);
        public bool HasBombs => Items.ContainsKey(ItemType.Bombs);
        public bool HasFlipper => Items.ContainsKey(ItemType.Flippers);
        public bool HasInstrument => Items.ContainsKey(ItemType.Accordian) || Items.ContainsKey(ItemType.Guitar);
        public bool HasBow => Items.ContainsKey(ItemType.Bow);
        public bool HasTriBow => Items.TryGetValue(ItemType.Bow, out int bowLevel)  && bowLevel >= 2;
        public bool HasGrappleHook  => Items.ContainsKey(ItemType.GrappleHook);
        public bool HasBoomerang => Items.ContainsKey(ItemType.Boomerang);
        public bool HasShovel => Items.ContainsKey(ItemType.Shovel);
        public bool HasRexTeleporter => Items.ContainsKey(ItemType.RexTeleporter);
        public bool HasFishingRod => Items.ContainsKey(ItemType.FishingRod);
        public bool HasJar => Items.ContainsKey(ItemType.Jar_Empty) || Items.ContainsKey(ItemType.Jar_Health) || Items.ContainsKey(ItemType.Jar_ReduceCost)
                              || Items.ContainsKey(ItemType.Jar_DoubleDamage) || Items.ContainsKey(ItemType.Jar_SlowTime) || Items.ContainsKey(ItemType.Jar_BubbleShield)
                              || Items.ContainsKey(ItemType.Jar_ArmorOrbs) || Items.ContainsKey(ItemType.Jar_Resurrection) || Items.ContainsKey(ItemType.Jar_Ghost)
                              || Items.ContainsKey(ItemType.Jar_Fire) || Items.ContainsKey(ItemType.Jar_Speedster);
        public bool HasBeatenMorklaBoss => Items.ContainsKey(ItemType.MorklaBoss);
        public bool HasKeys => Items.ContainsKey(ItemType.Gold_Key);
        public bool HasTreeSeeds => Items.ContainsKey(ItemType.TreeSeed);
        public bool HasHeartNecklace => Items.ContainsKey(ItemType.HeartNecklace);
        public bool HasBlueGem =>  Items.ContainsKey(ItemType.BlueGem);
        public bool HasGreenGem => Items.ContainsKey(ItemType.GreenGem);
        public bool Has3DongeonKeys => Items.ContainsKey(ItemType.KeyPiece1) && Items.ContainsKey(ItemType.KeyPiece2) && Items.ContainsKey(ItemType.KeyPiece3);

        public bool CanOpenNoteDoor => HasInstrument && Items.ContainsKey(ItemType.OpenSesame);
        public bool CanWakeUpPeople => HasInstrument && Items.ContainsKey(ItemType.WakeUp);
        public bool CanActivateBlueSwitch => true;
        public bool  HasBottle => Items.ContainsKey(ItemType.Jar_Empty);
        public bool  HasGhostPotion => Items.ContainsKey(ItemType.Jar_Ghost);

        public bool HasCollectedAllHoneycombs => Items.TryGetValue(ItemType.Honeycomb, out int count) && count >= 10;
        public bool CanLiftMasterSword => Items.TryGetValue(ItemType.HeartQ_4, out int fullHeartCount) && Items.TryGetValue(ItemType.HeartQ_1, out int quarterHeartCount) && fullHeartCount + quarterHeartCount/4 >= 7;
        //todo
        public int  NbBlueGem => 0;

        //TODO
        public bool CanAccessCanyon => true;
        //TODO
        public bool CanAccessCanyonSteppe => true;

        //TODO
        public bool CanAccessDarkForest => true;

        //TODO
        public bool CanDoDamage => HasSword || HasBombs || HasBow;

        public bool CanCutPegs => Items.TryGetValue(ItemType.Sword, out int swordLevel) && swordLevel >= 2;
        public bool CanCutBushes => HasSword || HasBoomerang || HasGrappleHook || HasBombs || HasTorch || HasShovel;
        public bool CanSwitchLevers => HasSword || HasGrappleHook || HasBoomerang || HasBow;
        public bool CanHitWaterLevers => HasSword;

        public bool CanCollectAllFishes => true;

        public bool CanCollectIngredient(EquipableItem.IngredientList ingredient)
        {
            switch (ingredient)
            {
                case EquipableItem.IngredientList.Mushroom: return _world.CanAccessAnyOverworld(this);
                case EquipableItem.IngredientList.Clover: return _world.CanAccessAnyOverworld(this);
                case EquipableItem.IngredientList.Lily: return _world.CanAccessAnyDarkForest(this);
                case EquipableItem.IngredientList.RootWeed: return _world.CanAccessAnyDarkForest(this);
                case EquipableItem.IngredientList.MoonFlower: return _world.CanAccessAnyDarkForest(this);
                case EquipableItem.IngredientList.Apple: return _world.CanAccessAnyOverworld(this);
                case EquipableItem.IngredientList.Tulip: return _world.CanAccessAnyOverworld(this);
                case EquipableItem.IngredientList.Toadstool: return _world.CanAccessAnyJungle(this);
                case EquipableItem.IngredientList.Willow: return _world.CanAccessAnyJungle(this);
                case EquipableItem.IngredientList.Skyblossom: return _world.CanAccessAnyJungle(this);
                case EquipableItem.IngredientList.CanyonWisp: return _world.CanAccessAnyJungle(this);
                case EquipableItem.IngredientList.Orange: return _world.CanAccessAnyJungle(this);
                case EquipableItem.IngredientList.Spikeshell: return _world.CanAccesJungleBeach(this);
                case EquipableItem.IngredientList.Clam: return _world.CanAccesJungleBeach(this);
                case EquipableItem.IngredientList.Snailshell: return _world.CanAccesJungleBeach(this);
                case EquipableItem.IngredientList.Seaweed: return _world.CanAccesJungleBeach(this) || HasFishingRod;
                case EquipableItem.IngredientList.Starfish: return _world.CanAccesJungleBeach(this);
                case EquipableItem.IngredientList.Melon: return _world.CanAccessAnyDarkForest(this);
                case EquipableItem.IngredientList.Chrysanthemum: return _world.CanAccessAnyOverworld(this);
                case EquipableItem.IngredientList.Aster: return _world.CanAccessAnyDarkForest(this) && CanCutBushes;
                case EquipableItem.IngredientList.Sunkiss: return _world.CanAccessAnyCanyon(this);
                case EquipableItem.IngredientList.Jojoba: return _world.CanAccessAnyCanyon(this);
                case EquipableItem.IngredientList.DesertPuff: return _world.CanAccessAnyCanyon(this);
                case EquipableItem.IngredientList.WaterDrop: return _world.CanAccessAnyCanyon(this);
                case EquipableItem.IngredientList.FlameTongue: return _world.CanAccessAnyCanyon(this);
                case EquipableItem.IngredientList.CactusRose: return _world.CanAccessAnyCanyon(this);
                case EquipableItem.IngredientList.PurpleMushroom: return _world.CanAccessAnyDarkForest(this);
                case EquipableItem.IngredientList.RedMushroom: return _world.CanAccessAnyDarkForest(this);
                case EquipableItem.IngredientList.GreenMushroom: return _world.CanAccessAnyDarkForest(this);
                case EquipableItem.IngredientList.Guts: return CanDoDamage;
                case EquipableItem.IngredientList.Poinsettia: return _world.CanAccessAnyLabyrinth(this);
                case EquipableItem.IngredientList.Bellflower: return _world.CanAccessAnyLabyrinth(this);
                case EquipableItem.IngredientList.Daisy: return _world.CanAccessAnyLabyrinth(this);
                case EquipableItem.IngredientList.Carambola: return _world.CanAccessAnyLabyrinth(this);
                case EquipableItem.IngredientList.Crab: return HasFishingRod && (_world.CanAccessAnyCanyon(this) || _world.MorklaDungeon.CanAccess(this));
                case EquipableItem.IngredientList.Fishbones: return HasFishingRod;
                case EquipableItem.IngredientList.Fish1: return HasFishingRod && (_world.OverworldNorth.CanAccess(this) || _world.CanAccessAnyOverworld(this) || _world.CanAccessAnyLabyrinth(this));
                case EquipableItem.IngredientList.Fish2: return HasFishingRod && (_world.OverworldNorth.CanAccess(this) || _world.CanAccessAnyOverworld(this));
                case EquipableItem.IngredientList.Fish3: return HasFishingRod && (_world.OverworldNorth.CanAccess(this) || _world.CanAccessAnyOverworld(this) || _world.CanAccessAnyJungle(this) );
                case EquipableItem.IngredientList.Fish4: return HasFishingRod && (_world.OverworldNorth.CanAccess(this) || _world.CanAccessAnyCanyon(this) || _world.CanAccessAnyDarkForest(this));
                case EquipableItem.IngredientList.Fish5: return HasFishingRod && (_world.OverworldNorth.CanAccess(this) || _world.CanAccessAnyDarkForest(this));
                case EquipableItem.IngredientList.Fish6: return HasFishingRod && (_world.OverworldNorth.CanAccess(this) || _world.CanAccessAnyDarkForest(this) || _world.CanAccessAnyLabyrinth(this));
                case EquipableItem.IngredientList.Fish7: return HasFishingRod && (_world.OverworldNorth.CanAccess(this) || _world.CanAccessAnyJungle(this));
                case EquipableItem.IngredientList.Fish8: return HasFishingRod && (_world.OverworldNorth.CanAccess(this) || _world.CanAccessAnyJungle(this) || _world.CanAccessAnyCanyon(this));
                case EquipableItem.IngredientList.Fish9: return HasFishingRod && (_world.OverworldNorth.CanAccess(this) || _world.CanAccessAnyCanyon(this));
                case EquipableItem.IngredientList.Fish10: return HasFishingRod && (_world.OverworldNorth.CanAccess(this) || _world.CanAccessAnyLabyrinth(this));
                default: return true;
            }
        }

        public bool CanCraftPotion(ItemType potionType)
        {
            if (!HasJar)
                return false;

            switch(potionType)
            {
                case ItemType.Jar_Health: return true;
                case ItemType.Jar_ReduceCost: return true;
                case ItemType.Jar_DoubleDamage: return true;
                case ItemType.Jar_SlowTime: return true;
                case ItemType.Jar_BubbleShield: return true;
                case ItemType.Jar_ArmorOrbs: return true;
                case ItemType.Jar_Resurrection: return true;
                case ItemType.Jar_Ghost: return true;
                case ItemType.Jar_Fire: return true;
                case ItemType.Jar_Speedster: return true;
                default: return false;
            }
        }

        public void AddItem(ItemType itemType, int amount)
        {
            if(Items.ContainsKey(itemType))
                Items[itemType] += amount;
            else
                Items.Add(itemType, amount);
        }

        public void RemoveItem(ItemType itemType, int amount)
        {
            if (Items.ContainsKey(itemType))
                Items[itemType] -= amount;
        }
    }
}
