using Microsoft.Xna.Framework;
using MonoMod;
#nullable disable
namespace BlossomTales2
{
    class patch_CS_LilysHouse : CS_LilysHouse
    {
        private Puppet lily = new Puppet("lily", new Vector3(0f, 0.0f, 0f));
        
        public extern void orig_equipShield();
        
        public void equipShield()
        {
            lily.play("getItem");
            Game1.playSoundCue("newWeapon");
            Game1.player.Position = lily.getPosition();
            Game1.Particles.Add((Particle) new P_GetItem(lily.getPosition() + new Vector3(0.0f, 100f, 0.0f), 48));
            Game1.Particles.Add((Particle) new GetItemLight(lily.getPosition()));
            Game1.player.Inventory.Add(EquipableItem.ItemList.Guitar);
            //Game1.player.Ability[0] = (EquipableItem) new E_Shield();
            this.tweener.Timer(2.3f).OnComplete((Action) (() =>
            {
                lily.play("idleDown");
                this.tweener.Timer(0.2f).OnComplete(new Action(this.equipSword));
            }));
        }
    }
}

