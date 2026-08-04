using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlossomTales2
{
    internal class patch_CS_Fisherman : CS_Fisherman
    {
        public extern void orig_Init();
        public extern void orig_callForHelp();
        public extern void orig_getFishingRod();

        public override void Init()
        {
            if (Mod_ShouldDisplayFisherman())
            {
                putFishermanOnBridge();
                SpawnDialogRect spawnDialogRect = new SpawnDialogRect(new Vector3(484f, 0f, 1016f));
                spawnDialogRect.Size = new Vector3(44f, 0f, 24f);
                Game1.CurrentLevel.LevelObjects.Add(spawnDialogRect);
                return;
            }

            foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
            {
                if (levelObject is SpawnDialogRect)
                {
                    levelObject.Alive = false;
                }
            }
        }

        public void callForHelp()
        {
            if (!ModGlobals.SkipCutscenes)
                orig_callForHelp();
        }

        public void getFishingRod()
        {
            Mod_GetFishermanItem();
            tweener.Timer(3f).OnComplete(calmDown2);
        }

        private bool Mod_ShouldDisplayFisherman()
        {
            return !Game1Extensions.HasLevelPermaObject("fisherman");
        }

        private void Mod_GetFishermanItem()
        {
            GameLogger.LogInfo(Game1.CurrentLevel.Name + " " + fisherman.name + " " + fisherman.getPosition());
            //Fisherman can move, so don't register its position.
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(fisherman.name, Vector3.Zero);
            Game1.player.GiveItemReflection(item);
            Game1Extensions.AddLevelPermaObject(fisherman.name, Vector3.Zero);
        }
    }
}
