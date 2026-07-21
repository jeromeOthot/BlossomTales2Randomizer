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
                case EquipableItem.ItemList.Sword:
                    this.SwordLevel = 2;
                    break;
                case EquipableItem.ItemList.Shield:
                    this.ShieldLevel = 2;
                    for (int index = 0; index < this.Inventory.Count; ++index)
                    {
                        if ((object)this.Inventory[index] is EquipableItem.ItemList.WoodShield)
                            this.Inventory[index] = EquipableItem.ItemList.Shield;
                    }
                    if (!this.Inventory.Contains(EquipableItem.ItemList.Shield))
                        this.Inventory.Add(EquipableItem.ItemList.Shield);
                    if (this.Ability[0] is E_Shield || this.Ability[0] is E_Empty && !(this.Ability[1] is E_Shield))
                    {
                        this.Ability[0] = (EquipableItem)new E_Shield();
                        break;
                    }
                    if (this.Ability[1] is E_Shield)
                    {
                        this.Ability[1] = (EquipableItem)new E_Shield();
                        break;
                    }
                    break;
                case EquipableItem.ItemList.Torch:
                    if (!this.Inventory.Contains(EquipableItem.ItemList.Torch))
                        this.Inventory.Add(EquipableItem.ItemList.Torch);
                    if (this.Ability[0] is E_Empty)
                    {
                        this.Ability[0] = (EquipableItem)new E_Torch();
                        break;
                    }
                    if (this.Ability[1] is E_Empty)
                    {
                        this.Ability[1] = (EquipableItem)new E_Torch();
                        break;
                    }
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
                case EquipableItem.ItemList.WoodSword:
                    this.SwordLevel = 1;
                    break;
                case EquipableItem.ItemList.MirrorShield:
                    this.ShieldLevel = 3;
                    for (int index = 0; index < this.Inventory.Count; ++index)
                    {
                        if ((object)this.Inventory[index] is EquipableItem.ItemList.Shield)
                            this.Inventory[index] = EquipableItem.ItemList.MirrorShield;
                    }
                    if (!this.Inventory.Contains(EquipableItem.ItemList.MirrorShield))
                        this.Inventory.Add(EquipableItem.ItemList.MirrorShield);
                    if (this.Ability[0] is E_Shield)
                        this.Ability[0] = (EquipableItem)new E_Shield();
                    if (this.Ability[1] is E_Shield)
                    {
                        this.Ability[1] = (EquipableItem)new E_Shield();
                        break;
                    }
                    break;
                case EquipableItem.ItemList.KingSword:
                    this.SwordLevel = 3;
                    this.HasChargeSword = true;
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
            Game1.Particles.Add((Particle)new P_GetItem(this.Position + new Vector3(0.0f, 100f, 0.0f), (int)item));
            Game1.Particles.Add((Particle)new GetItemLight(this.Position));
            this.giveNewItemDescription = (int)item;
        }
    }
}