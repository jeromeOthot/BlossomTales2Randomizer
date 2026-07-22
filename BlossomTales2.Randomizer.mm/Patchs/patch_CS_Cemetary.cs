using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using System.Linq;

namespace BlossomTales2
{
    internal class patch_CS_Cemetery : CS_Cemetery
    {
        private Puppet lanternGuy = new Puppet ("Stub Lantern", new Vector3(0f, 0f, 0f));
        private Puppet doorPuppet = new Puppet ("Stub tombDoor", new Vector3(0f, 0f, 0f));

        public extern void orig_Init();
        public extern void orig_wakeUpLanternGuy();
        public extern void orig_giveLantern();
        public extern void orig_openDoor();

        public override void Init()
        {
            Game1.fadeController.flashScreen(Color.Black, null, 0f, 0f, 0f, reverse: false, 0);
            if (Game1.Globals.ghostRejuvPotionState == 2)
            {
                foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
                {
                    if (levelObject is NPC_2)
                    {
                        levelObject.IDNumber = 4;
                        (levelObject as NPC_2).isGhost = 0;
                    }
                }
            }

            if (Game1.Perma_Objects.FirstOrDefault((obj) => obj.LevelName == Game1.CurrentLevel.Name && obj.Name == "lanternGuy") == null)
            {
                lanternGuy = new Puppet("lanternGuy", new Vector3(1036f, 0f, 660f));
                lanternGuy.isNPC = true;
                lanternGuy.DialogNum = 70;
                lanternGuy.play("sitEyesClosed");
                puppets.Add(lanternGuy);
            }
            else
            {
                EraseNPCStuff();
            }

            if (Game1.Perma_Objects.FirstOrDefault((obj) => obj.LevelName == Game1.CurrentLevel.Name && obj.Name == "tombDoor") == null)
            {
                doorPuppet = new Puppet("tombDoor", new Vector3(1280f, 0f, 472f));
                doorPuppet.Zdepth = 36.4f;
                puppets.Add(doorPuppet);
                Game1.CurrentLevel.LevelObjects.Add(new CollisionRect(new Vector3(1248f, 0f, 448f)));
                foreach (Light light in Game1.CurrentLevel.Lights)
                {
                    if (light.position.X == 1164f)
                    {
                        light.position.X -= 5000f;
                        light.minOpacity = 0;
                        light.maxOpacity = 0;
                        light.opacity = 0f;
                    }
                }
            }
            else
            {
                foreach (LevelObject levelObject2 in Game1.CurrentLevel.LevelObjects)
                {
                    if (levelObject2 is Brazier)
                    {
                        levelObject2.Frame = 1;
                        ((Brazier)levelObject2).flameScale = 4f;
                    }
                }
            }
        }

        public void wakeUpLanternGuy()
        {
            if (ModGlobals.SkipCutscenes)
                giveLantern();
            else
                orig_wakeUpLanternGuy();
        }

        public void giveLantern()
        {
            lanternGuy.play("stand");
            GameLogger.LogInfo(Game1.CurrentLevel.Name + " " + lanternGuy.name + " " + lanternGuy.getPosition());
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, lanternGuy.name, lanternGuy.getPosition()));
            Game1.player.GiveItemReflection(item);
            Game1.Perma_Objects.Add(new PermaListItem(Game1.CurrentLevel.Name, lanternGuy.name, lanternGuy.getPosition()));
            for (int i = 0; i < Game1.CurrentLevel.Lights.Count; i++)
            {
                if (Game1.CurrentLevel.Lights[i].position.X == 740f)
                {
                    Game1.CurrentLevel.Lights.RemoveAt(i);
                }
            }

            EraseNPCStuff();
            if(ModGlobals.SkipCutscenes)
            {
                tweener.Timer(2f).OnComplete(runOff);
            }
            else
            {
                tweener.Timer(2f).OnComplete(delegate
                {
                    Game1.Dialoger.AddLine("Traveler: I'm getting out of here before you wake up something truly terrifying!", runOff);
                });
            }
        }

        private void openDoor()
        {
            orig_openDoor();
            Game1.Perma_Objects.Add(new PermaListItem(Game1.CurrentLevel.Name, doorPuppet.name, doorPuppet.getPosition()));
        }

        //TODO: Find a way to call base method, without reflection if possible.
        private void EraseNPCStuff()
        {
            for (int i = 0; i < Game1.CurrentLevel.LevelObjects.Count; i++)
            {
                if (Game1.CurrentLevel.LevelObjects[i] is CollisionRect && Game1.CurrentLevel.LevelObjects[i].Position.X == 1004f && Game1.CurrentLevel.LevelObjects[i].Position.Z == 672f)
                {
                    Game1.CurrentLevel.LevelObjects.RemoveAt(i);
                    i--;
                }
            }

            for (int j = 0; j < Game1.CurrentLevel.Lights.Count; j++)
            {
                if (Game1.CurrentLevel.Lights[j].position.X == 740f)
                {
                    Game1.CurrentLevel.Lights.RemoveAt(j);
                }
            }
        }
    }
}
