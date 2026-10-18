using System;
using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod;
using MonoMod.Cil;
using MonoMod.InlineRT;
using MonoMod.Utils;

namespace BlossomTales2
{
    public class patch_Player : Player
    {
        private int idleCount;
        private int idleFrame;
        private int idleRow;
        private int idleWait;

        private float resFlowerX = 0.0f;
        private float resFlowerY = 0.0f;
        private float resFlowerAlpha = 0.0f;
        private float resFlowerScale = 0.0f;
        private float resFlowerRotation = 0.0f;

        public extern void orig_GiveItem(EquipableItem.ItemList item, bool playAnimation = true);

        public extern void orig_GiveIngredient(EquipableItem.IngredientList ingred, int amount = 1, bool playAnimation = false);

        public extern void orig_RemoveItem_NE(EquipableItem.ItemList item, bool playAnimation = false, int amount = 1);
        public extern void orig_ChangeGoldAmount(int p);

        [MonoModIgnore]
        [PatchPlayerUpdate]
        public extern override void Update(GameTime gameTime);

        public void ChangeGoldAmount(int p)
        {
            Gold += p;
            if (Gold < 0)
                Gold = 0;
            if (p < 0)
            {
                Game1.Globals.achieve_totalGoldSpent += p * -1;
                if (Game1.Globals.achieve_totalGoldSpent > 3000)
                    Game1.Globals.achieve_totalGoldSpent = 3000;
                Game1.Achievementer.AchieveStat("STAT_GOLD_SPENT", Game1.Globals.achieve_totalGoldSpent);
            }

            if (p > 2 || p < -2)
            {
                Game1.playSoundCue("coin");
                Game1.Gui.SubtractGold = p;
                Game1.Gui.SubtractGoldAmount = 1;
                if (Game1.Gui.SubtractGold > 100 || Game1.Gui.SubtractGold < -100)
                    Game1.Gui.SubtractGoldAmount = 5;
            }

            Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.GoldCoin, Gold);
        }

        //TODO: Extract method
        internal void GiveItem(EquipableItem.ItemList item, bool playAnimation = true)
        {
            idleCount = 0;
            idleFrame = 0;
            idleTimer = 0;
            idleCount = 0;
            idleRow = 0;
            idleWait = 0;
            switch (item)
            {
                case EquipableItem.ItemList.Sword:
                    item = Mod_HandleProgressiveSword();
                    break;
                case EquipableItem.ItemList.Shield:
                    item = Mod_HandleProgressiveShield();
                    break;
                case EquipableItem.ItemList.Bow:
                    item = Mod_HandleProgressiveBow();
                    break;
                case EquipableItem.ItemList.Bombs:
                    Game1.player.Inventory.Add(EquipableItem.ItemList.Bombs);
                    break;
                case EquipableItem.ItemList.Boomerang:
                    Game1.player.Inventory.Add(EquipableItem.ItemList.Boomerang);
                    break;
                case EquipableItem.ItemList.FishingRod:
                    Game1.player.Inventory.Add(EquipableItem.ItemList.FishingRod);
                    break;
                case EquipableItem.ItemList.Accordian:
                    ChoseGuitar = false;
                    Game1.player.Inventory.Add(EquipableItem.ItemList.Accordian);
                    break;
                case EquipableItem.ItemList.Guitar:
                    ChoseGuitar = true;
                    Game1.player.Inventory.Add(EquipableItem.ItemList.Guitar);
                    break;
                case EquipableItem.ItemList.RexTeleporter:
                    Game1.player.Inventory.Add(EquipableItem.ItemList.RexTeleporter);
                    break;
                case EquipableItem.ItemList.Torch:
                    Game1.player.Inventory.Add(EquipableItem.ItemList.Torch);
                    break;
                case EquipableItem.ItemList.GrappleHook:
                    Game1.player.Inventory.Add(EquipableItem.ItemList.GrappleHook);
                    break;
                case EquipableItem.ItemList.Gold_Key:
                    ++Keys_Gold;
                    break;
                case EquipableItem.ItemList.HeartQ_1:
                    Game1.playSoundCue("blank098");
                    ++QuarterHearts;
                    if (QuarterHearts > 3)
                    {
                        QuarterHearts -= 4;
                        Game1.Gui.NewHeart();
                    }

                    break;
                case EquipableItem.ItemList.HeartQ_4:
                    Game1.playSoundCue("blank098");
                    QuarterHearts += 4;
                    if (QuarterHearts > 3)
                    {
                        QuarterHearts -= 4;
                        Game1.Gui.NewHeart();
                    }

                    break;
                case EquipableItem.ItemList.Crystal:
                    Game1.playSoundCue("blank098");
                    ++QuarterCrystals;
                    if (QuarterCrystals > 3)
                    {
                        QuarterCrystals -= 4;
                        Game1.Gui.NewEnergy();
                    }

                    break;
                case EquipableItem.ItemList.Shovel:
                    Game1.player.Inventory.Add(EquipableItem.ItemList.Shovel);
                    break;
                case EquipableItem.ItemList.Ingred_Gem:
                    if (!Inventory_NE.Contains(EquipableItem.ItemList.Ingred_Gem))
                        Inventory_NE.Add(EquipableItem.ItemList.Ingred_Gem);
                    Count_Gems++;
                    playAnimation = false;
                    break;
                case EquipableItem.ItemList.Five_Gems:
                    if (!Inventory_NE.Contains(EquipableItem.ItemList.Ingred_Gem))
                        Inventory_NE.Add(EquipableItem.ItemList.Ingred_Gem);
                    Count_Gems += 5;
                    break;
                case EquipableItem.ItemList.Letter:
                    if (!Inventory_NE.Contains(item))
                        Inventory_NE.Add(item);
                    break;
                case EquipableItem.ItemList.Honeycomb:
                    if (!Inventory_NE.Contains(item))
                        Inventory_NE.Add(item);
                    Count_Honeycombs++;
                    break;
                case EquipableItem.ItemList.CanyonBone:
                    Count_CanyonBones++;
                    if (!Inventory_NE.Contains(item))
                        Inventory_NE.Add(item);
                    break;
                case EquipableItem.ItemList.Package:
                    if (!Inventory_NE.Contains(item))
                        Inventory_NE.Add(item);
                    break;
                case EquipableItem.ItemList.TreeSeed:
                    if (!Inventory_NE.Contains(item))
                        Inventory_NE.Add(item);
                    Count_TreeSeeds = 5;
                    break;
                case EquipableItem.ItemList.GreenGem:
                    Game1.player.Inventory_NE.Add(EquipableItem.ItemList.GreenGem);
                    Game1.Globals.foundBlueGem = true;
                    break;
                case EquipableItem.ItemList.BlueGem:
                    Game1.player.Inventory_NE.Add(EquipableItem.ItemList.BlueGem);
                    Game1.Globals.foundGreenGem = true;
                    break;
                case EquipableItem.ItemList.Flippers:
                    Game1.player.Inventory_NE.Add(EquipableItem.ItemList.Flippers);
                    HasFlippers = true;
                    break;
                case EquipableItem.ItemList.CombatScroll:
                    ++Count_CombatScrolls;
                    break;
                case EquipableItem.ItemList.MinotaurCoin:
                    if (!Inventory_NE.Contains(item))
                        Inventory_NE.Add(item);
                    ++Count_MinotaurCoins;
                    break;
                case EquipableItem.ItemList.KeyPiece1:
                    Game1.player.KeyPiece1 = true;
                    break;
                case EquipableItem.ItemList.KeyPiece2:
                    Game1.player.KeyPiece2 = true;
                    break;
                case EquipableItem.ItemList.KeyPiece3:
                    Game1.player.KeyPiece3 = true;
                    break;
                case EquipableItem.ItemList.HeartNecklace:
                    if (!Inventory_NE.Contains(EquipableItem.ItemList.HeartNecklace))
                        Inventory_NE.Add(EquipableItem.ItemList.HeartNecklace);
                    break;
                case EquipableItem.ItemList.GoldCoin:
                    int num4 = Game1.RandomNumber.Next(20, 26);
                    for (int l = 0; l < num4; l++)
                    {
                        Vector3 velocity = new Vector3(patch_Game1.RandomFloat(-60, 60, 10f),
                            patch_Game1.RandomFloat(40, 70, 10f), patch_Game1.RandomFloat(30, 60, 10f));
                        Game1.CurrentLevel.LevelObjects.Add(new Coin_PU(Position, velocity));
                    }

                    playAnimation = false;
                    break;
                default:
                    if (item.ToString().Contains("Jar_"))
                    {
                        Inventory.Add(item);
                        break;
                    }

                    if (!Inventory.Contains(item))
                        Inventory.Add(item);

                    break;
            }

            if (!playAnimation)
                return;
            if (ghostTimer < 1)
            {
                ClearPlayer();
                CurrentAnimation = Animations.GetItem;
            }
            else
                Game1.playSoundCue("newWeapon");

            int itemIndex = (int)item;
            Game1.playSoundCue("blank098");
            GameLogger.LogInfo("Play animation item: " + itemIndex);
            Game1.Particles.Add(new P_GetItem(Position + new Vector3(0.0f, 100f, 0.0f), itemIndex));
            Game1.Particles.Add(new GetItemLight(Position));

            if (itemIndex == 26 || itemIndex == 40 || itemIndex == 41 || itemIndex == 42 || itemIndex == 100)
                giveNewItemDescription = itemIndex;
        }

        public void GiveIngredient(EquipableItem.IngredientList ingred, int amount = 1, bool playAnimation = false)
        {
            idleCount = 0;
            idleFrame = 0;
            idleTimer = 0;
            idleCount = 0;
            idleRow = 0;
            idleWait = 0;
            switch (ingred)
            {
                case EquipableItem.IngredientList.MinotaurCoin:
                    ++Count_MinotaurCoins;
                    if (!Inventory_NE.Contains(EquipableItem.ItemList.MinotaurCoin))
                        Inventory_NE.Add(EquipableItem.ItemList.MinotaurCoin);
                    Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.MinotaurCoin, Count_MinotaurCoins);
                    break;
                case EquipableItem.IngredientList.Necklace:
                    GiveItem(EquipableItem.ItemList.HeartNecklace);
                    break;
                default:
                    if (!Ingredients.Contains(ingred))
                        Ingredients.Add(ingred);
                    Items_Count[(int)ingred] += amount;
                    Game1.Gui.AddGuiTicker(ingred, Items_Count[(int)ingred]);
                    break;
            }

            switch (ingred)
            {
                case EquipableItem.IngredientList.Fish1:
                    if (Game1.Globals.Fish1_State == 0)
                    {
                        Game1.Globals.Fish1_State = 1;
                        Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                    }

                    break;
                case EquipableItem.IngredientList.Fish2:
                    if (Game1.Globals.Fish2_State == 0)
                    {
                        Game1.Globals.Fish2_State = 1;
                        Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                    }

                    break;
                case EquipableItem.IngredientList.Fish3:
                    if (Game1.Globals.Fish3_State == 0)
                    {
                        Game1.Globals.Fish3_State = 1;
                        Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                    }

                    break;
                case EquipableItem.IngredientList.Fish4:
                    if (Game1.Globals.Fish4_State == 0)
                    {
                        Game1.Globals.Fish4_State = 1;
                        Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                    }

                    break;
                case EquipableItem.IngredientList.Fish5:
                    if (Game1.Globals.Fish5_State == 0)
                    {
                        Game1.Globals.Fish5_State = 1;
                        Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                    }

                    break;
                case EquipableItem.IngredientList.Fish6:
                    if (Game1.Globals.Fish6_State == 0)
                    {
                        Game1.Globals.Fish6_State = 1;
                        Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                    }

                    break;
                case EquipableItem.IngredientList.Fish7:
                    if (Game1.Globals.Fish7_State == 0)
                    {
                        Game1.Globals.Fish7_State = 1;
                        Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                    }

                    break;
                case EquipableItem.IngredientList.Fish8:
                    if (Game1.Globals.Fish8_State == 0)
                    {
                        Game1.Globals.Fish8_State = 1;
                        Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                    }

                    break;
                case EquipableItem.IngredientList.Fish9:
                    if (Game1.Globals.Fish9_State == 0)
                    {
                        Game1.Globals.Fish9_State = 1;
                        Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                    }

                    break;
                case EquipableItem.IngredientList.Fish10:
                    if (Game1.Globals.Fish10_State == 0)
                    {
                        Game1.Globals.Fish10_State = 1;
                        Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                    }

                    break;
            }

            if (!playAnimation)
                return;
            giveNewItemDescription = -1;
            Game1.playSoundCue("blank098");
            ClearPlayer();
            Se = SpriteEffects.None;
            CurrentAnimation = Animations.GetItem;
            Game1.Particles.Add(new P_GetItem(Position + new Vector3(0.0f, 100f, 0.0f), (int)ingred, 1));
            Game1.Particles.Add(new GetItemLight(Position));
        }

        private EquipableItem.ItemList Mod_HandleProgressiveSword()
        {
            EquipableItem.ItemList item = EquipableItem.ItemList.Sword;
            switch (SwordLevel)
            {
                case 0:
                    item = EquipableItem.ItemList.WoodSword;
                    break;
                case 1:
                    item = EquipableItem.ItemList.Sword;
                    break;
                case 2:
                    item = EquipableItem.ItemList.KingSword;
                    HasChargeSword = true;
                    break;
                case 3:
                    item = EquipableItem.ItemList.KingSword;
                    HasSwordBeams = true;
                    break;
            }

            if (SwordLevel < 3)
                SwordLevel++;

            return item;
        }

        private EquipableItem.ItemList Mod_HandleProgressiveShield()
        {
            if (ShieldLevel == 3)
                return EquipableItem.ItemList.MirrorShield;

            ShieldLevel++;
            for (int n = 0; n < 2; n++)
            {
                if (Ability[n] is E_Shield)
                    Ability[n] = new E_Shield();
            }

            //Upgrade inventory shield
            for (int index = 0; index < Inventory.Count; ++index)
            {
                //woodShield --> Shield
                if (Inventory[index] == EquipableItem.ItemList.WoodShield)
                {
                    Inventory[index] = EquipableItem.ItemList.Shield;
                    return EquipableItem.ItemList.Shield;
                }

                //shield -> mirror shield
                if (Inventory[index] == EquipableItem.ItemList.Shield)
                {
                    Inventory[index] = EquipableItem.ItemList.MirrorShield;
                    return EquipableItem.ItemList.MirrorShield;
                }
            }

            //Add wood shield
            Inventory.Add(EquipableItem.ItemList.WoodShield);
            return EquipableItem.ItemList.WoodShield;
        }

        private EquipableItem.ItemList Mod_HandleProgressiveBow()
        {
            if (BowUpgrade)
                return EquipableItem.ItemList.TriBow;

            for (int index = 0; index < Inventory.Count; ++index)
            {
                //bow --> tribow
                if (Inventory[index] == EquipableItem.ItemList.Bow)
                {
                    BowUpgrade = true;
                    Inventory[index] = EquipableItem.ItemList.TriBow;

                    for (int n = 0; n < 2; n++)
                    {
                        if (Ability[n] is E_Bow)
                            Ability[n].ListType = EquipableItem.ItemList.TriBow;
                    }
                    return EquipableItem.ItemList.TriBow;
                }
            }

            Game1.player.Inventory.Add(EquipableItem.ItemList.Bow);
            return EquipableItem.ItemList.Bow;
        }
    }

    public class ModPlayer
    {
        public static void Mod_GiveKingSword()
        {
            RandomizerSingleton.Instance.GiveItemAtLocation("SwordInStone", Vector3.Zero);
            Game1Extensions.AddLevelPermaObject("SwordInStone", Vector3.Zero);
        }

        public static bool Mod_CanFishNecklace()
        {
            bool vanillaCondition = Game1.LevelName == "jungles-22x22.tmx" && Game1.Globals.Rose_State == 1 &&
                                    Game1.player.Position.Z > 1472.0 && Game1.player.Position.X > 1600.0;
            bool modCondition = !Game1Extensions.HasLevelPermaObject("necklaceFish");
            return vanillaCondition && modCondition;
        }

        public static void Mod_GiveFishingItem(Player player)
        {
            if (player.fishingIngredient == EquipableItem.IngredientList.Necklace)
            {
                RandomizerSingleton.Instance.GiveItemAtLocation("necklaceFish", Vector3.Zero);
                Game1Extensions.AddLevelPermaObject("necklaceFish", Vector3.Zero);
            }
            else
            {
                player.GiveIngredientReflection(player.fishingIngredient, playAnimation: true);
            }
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchPlayerUpdate))]
    class PatchPlayerUpdateAttribute : Attribute
    {
    }

    static partial class MonoModRules
    {
        public static void PatchPlayerUpdate(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modPatchPlayerType = MonoModRule.Modder.FindType("BlossomTales2.ModPlayer").Resolve();
            ILCursor cursor = new ILCursor(context);

            PatchGiveFishingItem(cursor, modPatchPlayerType);
            PatchFishNecklace(cursor, modPatchPlayerType);
            PatchGiveKingSword(cursor, modPatchPlayerType);
            PatchHeartPieceGrandpaDialog(cursor);
        }

        private static void PatchGiveFishingItem(ILCursor cursor, TypeDefinition modPatchPlayerType)
        {
            //Find L.1607
            //this.GiveIngredient(this.fishingIngredient, playAnimation: true);
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdarg(0),
                instr => instr.MatchLdarg(0),
                instr => instr.MatchLdfld("BlossomTales2.Player", "fishingIngredient"),
                instr => instr.MatchLdcI4(1),
                instr => instr.MatchLdcI4(1),
                instr => instr.MatchCallvirt("BlossomTales2.Player", "GiveIngredient")
            );

            //Replace with
            //ModPlayer.Mod_GiveFishingItem()
            MethodDefinition mod_GiveFishingItem = modPatchPlayerType.FindMethod("Mod_GiveFishingItem");
            cursor.RemoveRange(6);
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Call, mod_GiveFishingItem);
        }


        private static void PatchFishNecklace(ILCursor cursor, TypeDefinition modPatchPlayerType)
        {
            //Find L.1787 to retrieve the branch label.
            // if (Game1.LevelName == "jungles-24x20.tmx")
            ILLabel branch24x20 = null; //This label branch to the next if

            cursor.GotoNext(MoveType.After,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "LevelName"),
                instr => instr.MatchLdstr("jungles-24x20.tmx"),
                instr => instr.MatchCall("System.String", "op_Equality"),
                instr => instr.MatchBrfalse(out branch24x20)
            );

            cursor.Index += 3;
            cursor.RemoveRange(23);
            cursor.MarkLabel(branch24x20);

            ILLabel branch22x22 = cursor.DefineLabel();

            //Replace by
            //ModPlayer.Mod_CanFishNecklace()
            MethodDefinition mod_CanFishNecklace = modPatchPlayerType.FindMethod("Mod_CanFishNecklace");
            cursor.Emit(OpCodes.Call, mod_CanFishNecklace);
            cursor.Emit(OpCodes.Brfalse, branch22x22);

            cursor.Index += 3;
            cursor.MarkLabel(branch22x22);
        }


        private static void PatchGiveKingSword(ILCursor cursor, TypeDefinition modPatchPlayerType)
        {
            //Find L.1985
            //Game1.player.GiveItem(EquipableItem.ItemList.KingSword);
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdarg(0),
                instr => instr.MatchLdcI4(52),
                instr => instr.MatchLdcI4(1),
                instr => instr.MatchCallvirt("BlossomTales2.Player", "GiveItem")
            );

            //Replace with
            //ModPlayer.Mod_GiveKingSword()
            MethodDefinition mod_GiveKingSwordMethod = modPatchPlayerType.FindMethod("Mod_GiveKingSword");
            cursor.RemoveRange(4);
            cursor.Emit(OpCodes.Call, mod_GiveKingSwordMethod);
        }

        private static void PatchHeartPieceGrandpaDialog(ILCursor cursor)
        {
            //Find L.3393
            // if (this.giveNewItemDescription == 26)
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdarg(0),
                instr => instr.MatchLdfld("BlossomTales2.Player", "giveNewItemDescription"),
                instr => instr.MatchLdcI4(26),
                instr => instr.MatchBneUn(out ILLabel _),
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals")
            );
            //Remove all except this
            // Game1.Globals.First_Heart = false;
            cursor.Index += 4;
            cursor.RemoveRange(3);
            cursor.Index += 3;
            cursor.RemoveRange(25);
        }
    }
}
