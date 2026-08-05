using System;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
                  //TODO: Code qui crash au niveau du tween a corrigé
                  /*
                  Game1.player.RemovePlayerControls = true;
                  Game1.player.CanResurrectState = 1;
                  int num1 = (int) ((double) Game1.player.Position.X - (double) Game1.Camera.Center.X);
                  int num2 = (int) ((double) Game1.player.Position.Z - (double) Game1.Camera.Center.Y);
                  this.resFlowerX = (float) (Game1.ScreenWidth / 2 + num1);
                  this.resFlowerY = (float) (Game1.ScreenHeight / 2 + num2 - 30);
                  this.resFlowerAlpha = 0.0f;
                  this.resFlowerScale = 0.0f;
                  this.resFlowerRotation = 0.0f;
                  this.tweener.Tween((object) this, (object) new
                  {
                    resFlowerAlpha = 1
                  }, 0.5f).Ease(new Func<float, float>(Ease.SineInOut));
                  this.tweener.Tween((object) this, (object) new
                  {
                    resFlowerScale = 4
                  }, 0.5f).Ease(new Func<float, float>(Ease.SineInOut));
                  this.tweener.Tween((object) this, (object) new
                  {
                    resFlowerRotation = 6.2831855f
                  }, 0.5f).Ease(new Func<float, float>(Ease.SineInOut));
                  this.tweener.Tween((object) this, (object) new
                  {
                    resFlowerY = ((int) this.resFlowerY - 50)
                  }, 0.5f).Ease(new Func<float, float>(Ease.SineInOut)).OnComplete((Action) (() =>
                  {
                    Game1.player.RemovePlayerControls = false;
                    int num3 = 164;
                    if (Game1.player.MaxHealth > 20)
                      num3 += 44;
                    this.tweener.Tween((object) this, (object) new
                    {
                      resFlowerX = 80 /*0x50*,
                      resFlowerY = num3
                    }, 2f, 0.5f).Ease(new Func<float, float>(Ease.SineInOut)).OnComplete((Action) (() => Game1.player.CanResurrectState = 2));
                  }));
                  */
                  break;
                case EquipableItem.ItemList.Shovel:
                    Game1.player.Inventory.Add(EquipableItem.ItemList.Shovel);
                    break;
                case EquipableItem.ItemList.Ingred_Gem:
                  Count_Gems++;
                  Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.Gem, Game1.player.Count_Gems);
                  this.GiveIngredient(EquipableItem.IngredientList.Gem);
                  break;
                case EquipableItem.ItemList.Five_Gems:
                  Count_Gems +=5;
                  Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.Gem, 5);
                  this.GiveIngredient(EquipableItem.IngredientList.Gem, 5);
                  break;
                case EquipableItem.ItemList.Letter:
                case EquipableItem.ItemList.Honeycomb:
                  Count_Honeycombs++;
                  Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.HoneycombOLD, Game1.player.Count_Honeycombs);
                  this.GiveIngredient(EquipableItem.IngredientList.HoneycombOLD);
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
                /*
                case EquipableItem.ItemList.Five_Gems:
                  if (!this.Inventory_NE.Contains(EquipableItem.ItemList.Ingred_Gem))
                    this.Inventory_NE.Add(EquipableItem.ItemList.Ingred_Gem);
                  this.Count_Gems += 5;
                  break; */
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
    }
}
