using System.Numerics;

namespace BlossomTales2
{
    class Patch_CS_LilysHouse : CS_LilysHouse
    {
        public extern void orig_equipShield();
        
        public void equipShield()
        {
            //this.lily.play("getItem");
            Game1.playSoundCue("newWeapon");
            //Game1.player.Position = this.lily.getPosition();
            //Game1.Particles.Add((Particle) new P_GetItem(this.lily.getPosition() + new Vector3(0.0f, 100f, 0.0f), 47));
            //Game1.Particles.Add((Particle) new GetItemLight(this.lily.getPosition()));
            Game1.player.Inventory.Add(EquipableItem.ItemList.Guitar);
            //Game1.player.Ability[0] = (EquipableItem) new E_Shield();
            this.tweener.Timer(2.3f).OnComplete((Action) (() =>
            {
                //this.lily.play("idleDown");
                this.tweener.Timer(0.2f).OnComplete(new Action(this.equipSword));
            }));
        }
    }
}

