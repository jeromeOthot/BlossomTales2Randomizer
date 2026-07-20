using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
#nullable disable
namespace BlossomTales2
{
    internal class patch_CS_Cemetery : CS_Cemetery
    {
        private Puppet lanternGuy = new("Stub Lantern", new Vector3(0f, 0f, 0f));

        public extern void orig_wakeUpLanternGuy();
        public extern void orig_giveLantern();

        public void wakeUpLanternGuy()
        {
            giveLantern();
        }

        public void giveLantern()
        {
            lanternGuy.play("stand");
            GameLogger.LogInfo(Game1.CurrentLevel.Name + " " + lanternGuy.name + " " + lanternGuy.getPosition());
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, lanternGuy.name, lanternGuy.getPosition()));
            Game1.player.GiveItemReflection(item);
            for (int i = 0; i < Game1.CurrentLevel.Lights.Count; i++)
            {
                if (Game1.CurrentLevel.Lights[i].position.X == 740f)
                {
                    Game1.CurrentLevel.Lights.RemoveAt(i);
                }
            }

            EraseNPCStuff();
            tweener.Timer(2f).OnComplete(runOff);
        }

        //Stub method
        private void EraseNPCStuff() { }
    }
}
