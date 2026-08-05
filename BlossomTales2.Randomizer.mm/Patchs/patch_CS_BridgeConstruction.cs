using System.Collections.Generic;
using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using MonoMod;

namespace BlossomTales2
{
    public class patch_CS_BridgeConstruction : CS_BridgeConstruction
    {
        private Rectangle killRect;

        public extern void orig_ctor();
        public extern void orig_youDidIt();

        [MonoModConstructor]
        public void ctor()
        {
            //Base
            CutSceneName = "";
            tweener = new Tweener();
            Running = true;
            puppets = new List<Puppet>();
            CameraPosition = Vector2.Zero;
            mapHeight = Game1.CurrentLevel.Height * 64;
            mapCenter = new Vector2(Game1.CurrentLevel.Width * 64 / 2, Game1.CurrentLevel.Height * 64 / 2);
            puppetList = new List<Puppet>();
            killRect = new Rectangle(964, 1140, 760, 356);

            if (Mod_IsObjectiveNotHeadToConstruction())
		    {
			    removeBridgeAnimations();
			    foreach (LevelObject levelObject in Game1.CurrentLevel.LevelObjects)
			    {
				    if (killRect.Contains((int)levelObject.Position.X, (int)levelObject.Position.Z) && !(levelObject is CuttableGeneric) && !(levelObject is Sign))
				    {
					    levelObject.Alive = false;
				    }
			    }
		    }
		    else if (Mod_IsSaveBettyNotCompleted())
		    {
			    removeBridgeAnimations();
			    foreach (LevelObject levelObject2 in Game1.CurrentLevel.LevelObjects)
			    {
				    if (killRect.Contains((int)levelObject2.Position.X, (int)levelObject2.Position.Z) && levelObject2.IDNumber != 69)
				    {
					    levelObject2.Alive = false;
				    }
			    }
		    }
		    else if (Mod_IsSaveBettyCompleted())
		    {
			    foreach (LevelObject levelObject3 in Game1.CurrentLevel.LevelObjects)
			    {
				    if (killRect.Contains((int)levelObject3.Position.X, (int)levelObject3.Position.Z) && levelObject3.IDNumber != 69 && levelObject3.IDNumber != 70 && !(levelObject3 is FortyFiveDegree) && !(levelObject3 is CollisionRect))
				    {
					    levelObject3.Alive = false;
				    }
			    }
			    if (Mod_IsObjectiveCrossBridge())
			    {
				    SpawnDialogRect item = new SpawnDialogRect(new Vector3(960f, 0f, 1212f))
				    {
					    Size = new Vector3(212f, 0f, 132f)
				    };
				    Game1.CurrentLevel.LevelObjects.Add(item);
			    }
			    foreach (AnimTile animation in Game1.CurrentLevel.Animations)
			    {
				    if (killRect.Contains((int)animation.position.X, (int)animation.position.Y) && animation.name != "Overworld/waterGrass-9x11x1" && animation.name != "Overworld/waterWave-5x3x13" && animation.name != "Overworld/bridge-110x38x1" && animation.name != "Overworld/pillarsWithRope-119x29x1" && animation.name != "Overworld/shadowPostOffice-54x22x1")
				    {
					    animation.Alive = false;
				    }
			    }
			    for (int i = 0; i < Game1.CurrentLevel.Coll_Anims.Count; i++)
			    {
				    if (killRect.Contains(Game1.CurrentLevel.Coll_Anims[i].X, Game1.CurrentLevel.Coll_Anims[i].Y))
				    {
					    Game1.CurrentLevel.Coll_Anims.RemoveAt(i);
					    i--;
				    }
			    }
		    }
		    if (Mod_IsObjectiveBeforeCrossBridge())
		    {
			    return;
		    }
		    foreach (LevelObject levelObject4 in Game1.CurrentLevel.LevelObjects)
		    {
			    if (killRect.Contains((int)levelObject4.Position.X, (int)levelObject4.Position.Z) && !(levelObject4 is FortyFiveDegree) && !(levelObject4 is CollisionRect) && ((levelObject4.IDNumber != 69 && levelObject4.IDNumber != 70) || Mod_IsCrossBridgeCompleted()))
			    {
				    levelObject4.Alive = false;
			    }
		    }
		    if (Mod_IsObjectiveCrossBridge())
		    {
			    SpawnDialogRect item2 = new SpawnDialogRect(new Vector3(960f, 0f, 1212f))
			    {
				    Size = new Vector3(212f, 0f, 132f)
			    };
			    Game1.CurrentLevel.LevelObjects.Add(item2);
		    }
		    foreach (AnimTile animation2 in Game1.CurrentLevel.Animations)
		    {
			    if (killRect.Contains((int)animation2.position.X, (int)animation2.position.Y) && animation2.name != "Overworld/waterGrass-9x11x1" && animation2.name != "Overworld/waterWave-5x3x13" && animation2.name != "Overworld/bridge-110x38x1" && animation2.name != "Overworld/pillarsWithRope-119x29x1" && animation2.name != "Overworld/shadowPostOffice-54x22x1")
			    {
				    animation2.Alive = false;
			    }
		    }
		    for (int j = 0; j < Game1.CurrentLevel.Coll_Anims.Count; j++)
		    {
			    if (killRect.Contains(Game1.CurrentLevel.Coll_Anims[j].X, Game1.CurrentLevel.Coll_Anims[j].Y))
			    {
				    Game1.CurrentLevel.Coll_Anims.RemoveAt(j);
				    j--;
			    }
		    }
        }

        public void youDidIt()
        {
            Globaler.MainGameObjective mainGameObjective = Game1.Globals.MainQuestObjective;
            orig_youDidIt();

            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.dark_crossBridge);
                Game1.Globals.MainQuestObjective = mainGameObjective;
            }
        }

        private bool Mod_IsObjectiveNotHeadToConstruction()
        {
            // if (ModGlobals.OpenWorldState)
            //     return false;
            // else
                return Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.dark_headToConstruction;
        }

        private bool Mod_IsSaveBettyNotCompleted()
        {
            // if (ModGlobals.OpenWorldState)
            //     return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_saveBetty);
            // else
                return Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.dark_saveBetty;
        }

        private bool Mod_IsSaveBettyCompleted()
        {
            // if (ModGlobals.OpenWorldState)
            //     return Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_saveBetty);
            // else
                return Game1.Globals.MainQuestObjective >= Globaler.MainGameObjective.dark_crossBridge;
        }

        private bool Mod_IsObjectiveCrossBridge()
        {
            // if (ModGlobals.OpenWorldState)
            //     return Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_saveBetty) && !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_crossBridge);
            // else
                return Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.dark_crossBridge;
        }

        private bool Mod_IsObjectiveBeforeCrossBridge()
        {
            // if (ModGlobals.OpenWorldState)
            //     return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_saveBetty);
            return Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.dark_crossBridge;
        }

        private bool Mod_IsCrossBridgeCompleted()
        {
            // if (ModGlobals.OpenWorldState)
            //     return Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_crossBridge);
            // else
                return Game1.Globals.MainQuestObjective > Globaler.MainGameObjective.dark_crossBridge;
        }
    }
}

