using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_CS_CanyonTown : CS_CanyonTown
    {
        private Puppet beggar;

        public extern void orig_Init();

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
    }
}
