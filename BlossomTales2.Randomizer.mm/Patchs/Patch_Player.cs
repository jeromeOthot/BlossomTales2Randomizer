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
        private int idleCount = 0;
        private int idleFrame = 0;
        private int idleRow = 0;
        private int idleWait = 0;

        private float resFlowerX = 0.0f;
        private float resFlowerY = 0.0f;
        private float resFlowerAlpha = 0.0f;
        private float resFlowerScale = 0.0f;
        private float resFlowerRotation = 0.0f;

        public extern void orig_GiveItem(EquipableItem.ItemList item, bool playAnimation = true);
        public extern void orig_GiveIngredient(EquipableItem.IngredientList ingred, int amount = 1, bool playAnimation = false);
        public extern void orig_RemoveItem_NE(EquipableItem.ItemList item, bool playAnimation = false, int amount = 1);
        public extern void orig_ChangeGoldAmount(int p);

        public  void ChangeGoldAmount(int p)
        {
            this.Gold += p;
            if (this.Gold < 0)
                this.Gold = 0;
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
            Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.GoldCoin, this.Gold);
        }

        //TODO: Extract method
        public void GiveItem(EquipableItem.ItemList item, bool playAnimation = true)
        {
            this.idleCount = 0;
            this.idleFrame = 0;
            this.idleTimer = 0;
            this.idleCount = 0;
            this.idleRow = 0;
            this.idleWait = 0;
            switch (item)
            {
                case EquipableItem.ItemList.Sword:
                  if (SwordLevel >= 3)
                    HasSwordBeams = true;
                  else
                  {
                    if (SwordLevel >= 2)
                      HasChargeSword = true;
                    SwordLevel++;
                  }
                  break;
                case EquipableItem.ItemList.Shield:
                    this.ShieldLevel = 1;
                    for (int index = 0; index < this.Inventory.Count; ++index)
                    {
                        //Si on passe de woodShield --> Shield
                        if ((object)this.Inventory[index] is EquipableItem.ItemList.WoodShield)
                        {
                          this.ShieldLevel = 2;
                          this.Inventory[index] = EquipableItem.ItemList.Shield;

                          if (!this.Inventory.Contains(EquipableItem.ItemList.Shield))
                            this.Inventory.Add(EquipableItem.ItemList.Shield);

                          if (this.Ability[0] is E_Shield || this.Ability[0] is E_Empty && !(this.Ability[1] is E_Shield))
                          {
                            this.Ability[0] = (EquipableItem) new E_Shield();
                            break;
                          }
                          if (this.Ability[1] is E_Shield)
                          {
                            this.Ability[1] = (EquipableItem) new E_Shield();
                            break;
                          }
                          break;
                        }

                        //On upgrade au shield -> mirror shield
                        if ((object)this.Inventory[index] is EquipableItem.ItemList.Shield)
                        {
                          this.ShieldLevel = 3;
                          this.Inventory[index] = EquipableItem.ItemList.MirrorShield;
                          if (!this.Inventory.Contains(EquipableItem.ItemList.MirrorShield))
                            this.Inventory.Add(EquipableItem.ItemList.MirrorShield);
                          if (this.Ability[0] is E_Shield)
                            this.Ability[0] = (EquipableItem) new E_Shield();
                          if (this.Ability[1] is E_Shield)
                          {
                            this.Ability[1] = (EquipableItem) new E_Shield();
                          }

                          item = EquipableItem.ItemList.MirrorShield;
                          GameLogger.LogInfo("Get MirrorShield item: " + (int)item );
                          break;
                        }
                    }

                    //Si on passe de aucun shield --> wood shield
                    if (this.ShieldLevel == 1)
                    {
                      Game1.player.Inventory.Add(EquipableItem.ItemList.WoodShield);
                      Game1.player.Ability[0] = (EquipableItem) new E_Shield();
                      item = EquipableItem.ItemList.WoodShield;
                      GameLogger.LogInfo("Get wood Shield item: " + (int)item );
                    }
                    break;
                case EquipableItem.ItemList.Bow:
                  for (int index = 0; index < Inventory.Count; ++index)
                  {
                    //Si on passe de bow --> tribow
                    if (Inventory[index] == EquipableItem.ItemList.Bow)
                    {
                            Game1.player.BowUpgrade = true;
                            for (int n = 0; n < Game1.player.Inventory.Count; n++)
                            {
                                if (Equals(EquipableItem.ItemList.Bow, Game1.player.Inventory[n]))
                                    Game1.player.Inventory[n] = EquipableItem.ItemList.TriBow;
                            }

                            if (Game1.player.Ability[0] is E_Bow)
                                Game1.player.Ability[0].ListType = EquipableItem.ItemList.TriBow;

                            if (Game1.player.Ability[1] is E_Bow)
                                Game1.player.Ability[1].ListType = EquipableItem.ItemList.TriBow;

                            item = EquipableItem.ItemList.TriBow;
                            break;
                    }
                  }
                  if(!Game1.player.BowUpgrade)
                    Game1.player.Inventory.Add(EquipableItem.ItemList.Bow);
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
                  ++this.Keys_Gold;
                  break;
                case EquipableItem.ItemList.HeartQ_1:
                  Game1.playSoundCue("blank098");
                  ++this.QuarterHearts;
                  if (this.QuarterHearts > 3)
                  {
                    this.QuarterHearts -= 4;
                    Game1.Gui.NewHeart();
                    break;
                  }
                  break;
                case EquipableItem.ItemList.HeartQ_4:
                  Game1.playSoundCue("blank098");
                  this.QuarterHearts += 4;
                  if (this.QuarterHearts > 3)
                  {
                    this.QuarterHearts -= 4;
                    Game1.Gui.NewHeart();
                    break;
                  }
                  break;
                case EquipableItem.ItemList.Crystal:
                  Game1.playSoundCue("blank098");
                  ++this.QuarterCrystals;
                  if (this.QuarterCrystals > 3)
                  {
                    this.QuarterCrystals -= 4;
                    Game1.Gui.NewEnergy();
                    break;
                  }
                  break;
                case  EquipableItem.ItemList.ResurrectionFlower:
                  //Unused
                  break;
                case EquipableItem.ItemList.Shovel:
                    Game1.player.Inventory.Add(EquipableItem.ItemList.Shovel);
                    break;
                case EquipableItem.ItemList.Ingred_Gem:
                    if (!this.Inventory_NE.Contains(EquipableItem.ItemList.Ingred_Gem))
                        this.Inventory_NE.Add(EquipableItem.ItemList.Ingred_Gem);
                    this.Count_Gems++;
                  break;
                case EquipableItem.ItemList.Five_Gems:
                  if (!this.Inventory_NE.Contains(EquipableItem.ItemList.Ingred_Gem))
                    this.Inventory_NE.Add(EquipableItem.ItemList.Ingred_Gem);
                  this.Count_Gems += 5;
                  break;
                case EquipableItem.ItemList.Letter:
                case EquipableItem.ItemList.Honeycomb:
                    if (!Inventory_NE.Contains(item))
                    {
                        Inventory_NE.Add(item);
                    }
                    Count_Honeycombs++;
                  break;
                case EquipableItem.ItemList.CanyonBone:
                  Count_CanyonBones++;
                  Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.Bones, Game1.player.Count_CanyonBones);
                  this.GiveIngredient(EquipableItem.IngredientList.Bones);
                  break;
                case EquipableItem.ItemList.Package:
                  Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewSong, Game1.player.Count_CanyonBones);
                  this.GiveIngredient(EquipableItem.IngredientList.Bones);
                  break;
                case EquipableItem.ItemList.TreeSeed:
                  if (!this.Inventory_NE.Contains(item))
                    this.Inventory_NE.Add(item);
                  if (item == EquipableItem.ItemList.TreeSeed)
                    ++this.Count_TreeSeeds;
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
                  ++this.Count_CombatScrolls;
                  break;
                case EquipableItem.ItemList.MinotaurCoin:
                  if (!this.Inventory_NE.Contains(item))
                    this.Inventory_NE.Add(item);
                  if (item == EquipableItem.ItemList.MinotaurCoin)
                    ++this.Count_MinotaurCoins;
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
                  if (!this.Inventory_NE.Contains(EquipableItem.ItemList.HeartNecklace))
                  {
                    this.Inventory_NE.Add(EquipableItem.ItemList.HeartNecklace);
                    break;
                  }
                  break;
                case EquipableItem.ItemList.GoldCoin:
                  int num4 = Game1.RandomNumber.Next(20, 40);
                  for (int l = 0; l < num4; l++)
                  {
                    Game1.CurrentLevel.LevelObjects.Add(new Coin_PU(Position, new Vector3(patch_Game1.RandomFloat(-1000, 1000, 100f), patch_Game1.RandomFloat(500, 900, 100f), patch_Game1.RandomFloat(200, 800, 100f))));
                  }

                  playAnimation = false;
                  break;
                default:
                    if (item.ToString().Contains("Jar_"))
                    {
                        this.Inventory.Add(item);
                        break;
                    }
                    if (!this.Inventory.Contains(item))
                    {
                        this.Inventory.Add(item);
                        break;
                    }
                    break;
            }
            if (!playAnimation)
                return;
            if (this.ghostTimer < 1)
            {
                this.ClearPlayer();
                this.CurrentAnimation = Player.Animations.GetItem;
            }
            else
                Game1.playSoundCue("newWeapon");

            int itemIndex = (int)item;
            Game1.playSoundCue("blank098");
            GameLogger.LogInfo("Play animation item: " + itemIndex);
            Game1.Particles.Add((Particle)new P_GetItem(this.Position + new Vector3(0.0f, 100f, 0.0f), itemIndex));
            Game1.Particles.Add((Particle)new GetItemLight(this.Position));

            if(itemIndex == 26 || itemIndex == 27 || itemIndex == 40 || itemIndex == 41 || itemIndex == 42 || itemIndex == 100)
                giveNewItemDescription = itemIndex;
        }

        public void GiveIngredient(EquipableItem.IngredientList ingred, int amount = 1, bool playAnimation = false)
        {
          this.idleCount = 0;
          this.idleFrame = 0;
          this.idleTimer = 0;
          this.idleCount = 0;
          this.idleRow = 0;
          this.idleWait = 0;
          switch (ingred)
          {
            case EquipableItem.IngredientList.MinotaurCoin:
              ++this.Count_MinotaurCoins;
              if (!this.Inventory_NE.Contains(EquipableItem.ItemList.MinotaurCoin))
                this.Inventory_NE.Add(EquipableItem.ItemList.MinotaurCoin);
              Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.MinotaurCoin, this.Count_MinotaurCoins);
              break;
            /* not used
            case EquipableItem.IngredientList.GoldCoin:
              this.ChangeGoldAmount(1);
              playAnimation = false;
              break; */
            case EquipableItem.IngredientList.Necklace:
              this.GiveItem(EquipableItem.ItemList.HeartNecklace);
              break;
            default:
              if (!this.Ingredients.Contains(ingred))
                this.Ingredients.Add(ingred);
              this.Items_Count[(int) ingred] += amount;
              Game1.Gui.AddGuiTicker(ingred, this.Items_Count[(int) ingred]);
              break;
          }
          switch (ingred)
          {
            case EquipableItem.IngredientList.Fish1:
              if (Game1.Globals.Fish1_State == 0)
              {
                Game1.Globals.Fish1_State = 1;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                break;
              }
              break;
            case EquipableItem.IngredientList.Fish2:
              if (Game1.Globals.Fish2_State == 0)
              {
                Game1.Globals.Fish2_State = 1;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                break;
              }
              break;
            case EquipableItem.IngredientList.Fish3:
              if (Game1.Globals.Fish3_State == 0)
              {
                Game1.Globals.Fish3_State = 1;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                break;
              }
              break;
            case EquipableItem.IngredientList.Fish4:
              if (Game1.Globals.Fish4_State == 0)
              {
                Game1.Globals.Fish4_State = 1;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                break;
              }
              break;
            case EquipableItem.IngredientList.Fish5:
              if (Game1.Globals.Fish5_State == 0)
              {
                Game1.Globals.Fish5_State = 1;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                break;
              }
              break;
            case EquipableItem.IngredientList.Fish6:
              if (Game1.Globals.Fish6_State == 0)
              {
                Game1.Globals.Fish6_State = 1;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                break;
              }
              break;
            case EquipableItem.IngredientList.Fish7:
              if (Game1.Globals.Fish7_State == 0)
              {
                Game1.Globals.Fish7_State = 1;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                break;
              }
              break;
            case EquipableItem.IngredientList.Fish8:
              if (Game1.Globals.Fish8_State == 0)
              {
                Game1.Globals.Fish8_State = 1;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                break;
              }
              break;
            case EquipableItem.IngredientList.Fish9:
              if (Game1.Globals.Fish9_State == 0)
              {
                Game1.Globals.Fish9_State = 1;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                break;
              }
              break;
            case EquipableItem.IngredientList.Fish10:
              if (Game1.Globals.Fish10_State == 0)
              {
                Game1.Globals.Fish10_State = 1;
                Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.NewFish, 1);
                break;
              }
              break;
          }
          if (!playAnimation)
            return;
          this.giveNewItemDescription = -1;
          Game1.playSoundCue("blank098");
          this.ClearPlayer();
          this.Se = SpriteEffects.None;
          this.CurrentAnimation = Player.Animations.GetItem;
          Game1.Particles.Add((Particle) new P_GetItem(this.Position + new Vector3(0.0f, 100f, 0.0f), (int) ingred, 1));
          Game1.Particles.Add((Particle) new GetItemLight(this.Position));
        }

        public void RemoveItem_NE(EquipableItem.ItemList item, bool playAnimation = false, int amount = 1)
        {
            switch (item)
            {
                case EquipableItem.ItemList.Honeycomb:
                    Count_Honeycombs -= amount;
                    if (Count_Honeycombs < 1)
                    {
                        Count_Honeycombs = 0;
                        Inventory_NE.Remove(item);
                    }
                    break;
                case EquipableItem.ItemList.CanyonBone:
                    Count_CanyonBones -= amount;
                    if (Count_CanyonBones < 1)
                    {
                        Count_CanyonBones = 0;
                        Ingredients.Remove(EquipableItem.IngredientList.Bones);
                    }
                    break;
                case EquipableItem.ItemList.CombatScroll:
                    Count_CombatScrolls -= amount;
                    if (Count_CombatScrolls < 1)
                    {
                        Count_CombatScrolls = 0;
                        Inventory_NE.Remove(item);
                    }
                    break;
                case EquipableItem.ItemList.MinotaurCoin:
                    Count_MinotaurCoins -= amount;
                    if (Count_MinotaurCoins < 1)
                    {
                        Count_MinotaurCoins = 0;
                        Inventory_NE.Remove(item);
                    }
                    break;
                case EquipableItem.ItemList.TreeSeed:
                    Count_TreeSeeds -= amount;
                    if (Count_TreeSeeds < 1)
                    {
                        Count_TreeSeeds = 0;
                        Inventory_NE.Remove(item);
                    }
                    break;
                case EquipableItem.ItemList.Ingred_Gem:
                    Count_Gems -= amount;
                    if (Count_Gems < 1)
                    {
                        Count_Gems = 0;
                        Inventory_NE.Remove(item);
                    }
                    break;
                default:
                    Inventory_NE.Remove(item);
                    break;
            }
            if (playAnimation)
            {
                Game1.Particles.Add(new P_RemoveItem(Position + new Vector3(0f, 100f, 0f), (int)item));
            }
        }

        [MonoModIgnore]
        [PatchPlayerUpdate]
        public extern override void Update(GameTime gameTime);
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
            bool vanillaCondition = Game1.LevelName == "jungles-22x22.tmx" && Game1.Globals.Rose_State == 1 && Game1.player.Position.Z > 1472.0 && Game1.player.Position.X > 1600.0;
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
    class PatchPlayerUpdateAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchPlayerUpdate(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modPatchPlayerType = MonoModRule.Modder.FindType("BlossomTales2.ModPlayer").Resolve();
            ILCursor cursor = new ILCursor(context);

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
    }
}
