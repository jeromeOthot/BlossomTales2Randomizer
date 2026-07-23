using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_Player : Player
    {
        private int idleCount = 0;
        private int idleFrame = 0;
        private int idleRow = 0;
        private int idleWait = 0;

        public extern void orig_GiveItem(EquipableItem.ItemList item, bool playAnimation = true);

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
                //TODO: Je n'ai pas arrivé encore a faire que Lily n'est aucun épée
                case EquipableItem.ItemList.Sword:
                  SwordLevel++;
                    if (SwordLevel >= 3)
                        HasChargeSword = true;
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
                  Game1.player.Inventory.Add(EquipableItem.ItemList.Accordian);
                  break;
                case EquipableItem.ItemList.Guitar:
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
                case EquipableItem.ItemList.Ingred_Gem:
                case EquipableItem.ItemList.Letter:
                case EquipableItem.ItemList.Honeycomb:
                case EquipableItem.ItemList.CanyonBone:
                case EquipableItem.ItemList.Package:
                case EquipableItem.ItemList.Shovel:
                  Game1.player.Inventory.Add(EquipableItem.ItemList.Shovel);
                  break;
                case EquipableItem.ItemList.TreeSeed:
                case EquipableItem.ItemList.GreenGem:
                case EquipableItem.ItemList.BlueGem:
                case EquipableItem.ItemList.Flippers:
                case EquipableItem.ItemList.CombatScroll:
                case EquipableItem.ItemList.MinotaurCoin:
                  if (!this.Inventory_NE.Contains(item))
                    this.Inventory_NE.Add(item);
                  if (item == EquipableItem.ItemList.CanyonBone)
                    ++this.Count_CanyonBones;
                  if (item == EquipableItem.ItemList.Honeycomb)
                    ++this.Count_Honeycombs;
                  if (item == EquipableItem.ItemList.CombatScroll)
                    ++this.Count_CombatScrolls;
                  if (item == EquipableItem.ItemList.MinotaurCoin)
                    ++this.Count_MinotaurCoins;
                  if (item == EquipableItem.ItemList.TreeSeed)
                    ++this.Count_TreeSeeds;
                  if (item == EquipableItem.ItemList.Ingred_Gem)
                  {
                    ++this.Count_Gems;
                    playAnimation = false;
                    break;
                  }
                  break;
                case EquipableItem.ItemList.Five_Gems:
                  if (!this.Inventory_NE.Contains(EquipableItem.ItemList.Ingred_Gem))
                    this.Inventory_NE.Add(EquipableItem.ItemList.Ingred_Gem);
                  this.Count_Gems += 5;
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
            Game1.playSoundCue("blank098");
            GameLogger.LogInfo("Play animation item: " + (int)item );
            Game1.Particles.Add((Particle)new P_GetItem(this.Position + new Vector3(0.0f, 100f, 0.0f), (int)item));
            Game1.Particles.Add((Particle)new GetItemLight(this.Position));
            this.giveNewItemDescription = (int)item;
        }
    }
}