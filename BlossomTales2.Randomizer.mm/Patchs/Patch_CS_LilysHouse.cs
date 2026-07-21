using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using System;

namespace BlossomTales2
{
    class patch_CS_LilysHouse : CS_LilysHouse
    {
        private Puppet lily = new Puppet("lily", new Vector3(0f, 0.0f, 0f));
        private Puppet grandma = new Puppet("Fake grandma", new Vector3(0f, 0f, 0f));

        public extern void orig_equipShield();

        public void equipShield()
        {
            lily.play("getItem");
            Game1.playSoundCue("newWeapon");
            Game1.player.Position = lily.getPosition();
            GameLogger.LogInfo(Game1.CurrentLevel.Name + " " + grandma.name + " " + grandma.getPosition());
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, grandma.name + "_1", grandma.getPosition()));
            Game1.Particles.Add(new P_GetItem(lily.getPosition() + new Vector3(0.0f, 100f, 0.0f), (int)item));
            Game1.Particles.Add(new GetItemLight(lily.getPosition()));
            Game1.player.GiveItemReflection(item, false);
            tweener.Timer(2.3f).OnComplete((() =>
            {
                lily.play("idleDown");
                tweener.Timer(0.2f).OnComplete(new Action(equipSword));
            }));
        }
    }
}

