using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_CS_Blacksmith : CS_Blacksmith
    {
        private Puppet blacksmith = new Puppet("fake blacksmith", Vector3.Zero);
        private Puppet bowBuild = new Puppet("fake bow", Vector3.Zero);

        private extern void orig_giveBow();

        public void giveBow()
        {
            Game1.Globals.Blacksmith_State = 4;
            bowBuild.setPosition(new Vector3(-100f, 0f, -100f));
            Mod_GiveItem();
            Game1.player.Direction = 3;
            blacksmith.Alive = false;
            NPC_2 nPC_ = new NPC_2(blacksmith.getPosition() + new Vector3(0f, 0f, -4f));
            nPC_.IDNumber = 20;
            nPC_.linePointer = 102;
            Game1.CurrentLevel.LevelObjects.Add(nPC_);
        }

        private void Mod_GiveItem()
        {
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(blacksmith.Name, blacksmith.getPosition());
            Game1.player.GiveItemReflection(item);
            Game1Extensions.AddLevelPermaObject(blacksmith.Name, blacksmith.getPosition());
        }
    }
}
