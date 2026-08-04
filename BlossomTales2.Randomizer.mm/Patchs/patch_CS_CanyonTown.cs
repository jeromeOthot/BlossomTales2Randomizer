using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_CS_CanyonTown : CS_CanyonTown
    {
        private Puppet beggar;

        public extern void orig_Init();
        public extern void orig_giveHeart();

        public override void Init()
        {
            if (ModGlobals.OpenWorldState)
            {
                if (Game1.Globals.beggarState < 5)
                {
                    beggar = new Puppet("beggar", new Vector3(980f, 0f, 1616f));
                    beggar.play("sit");
                    beggar.collide = true;
                    beggar.isNPC = true;
                    beggar.DialogNum = 163;
                    puppets.Add(beggar);
                    puppetList.Add(beggar);
                }
                return;
            }
            else
            {
                orig_Init();
            }
        }

        public void giveHeart()
        {
            Mod_GiveItem();
            tweener.Timer(3f).OnBegin(delegate
            {
                Game1.Dialoger.AddLine("Beggar Fairy: Farewell.", flyAway);
            });
        }

        private void Mod_GiveItem()
        {
            GameLogger.LogInfo(Game1.CurrentLevel.Name + " " + beggar.name + " " + beggar.getPosition());
            Vector3 positionOffset = beggar.getPosition();
            positionOffset.Y = 0f;
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(beggar.name, positionOffset);
            Game1.player.GiveItemReflection(item);
        }
    }
}
