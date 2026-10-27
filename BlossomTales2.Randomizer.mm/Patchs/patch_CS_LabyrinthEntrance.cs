using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_LabyrinthEntrance : CS_LabyrinthEntrance
    {
        public void startScene()
        {
            if (Game1.Globals.FirstLabyrinthVisit && (!Mod_HasAllKeyPieces() || Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.lab_openDoor))
            {
                Game1.Globals.FirstLabyrinthVisit = false;
                focusCam = true;
                Game1.Gui.HideHud = true;
                Game1.player.RemovePlayerControls = true;
                Game1.player.MoveToPosition(new Vector3(1440f, 0f, 648 + offset), 1);
                focusCameraOnTarget(new Vector2(1440f, 482f), 2f);
                tweener.Timer(1f).OnComplete(delegate
                {
                    Game1.SwitchBGMusic("BT2_OW_Overworld", 0.01f, 0.75f);
                    Game1.Narrator.AddLine("Grandpa: <B>Lily stood in awe as she beheld the great Labyrinth Gates for the first time.", 12);
                    Game1.Narrator.AddLine("Grandpa: She knew there was no entry without the powerful key made just for this lock alone.", 12);
                    Game1.Narrator.AddLine("Grandpa: Her quest continued!", 12, preGoPlayer);
                });
            }
            else
            {
                if (!Mod_HasAllKeyPieces() || Game1.Globals.MainQuestObjective != Globaler.MainGameObjective.lab_openDoor)
                {
                    return;
                }
                focusCam = true;
                Game1.Gui.HideHud = true;
                Game1.player.RemovePlayerControls = true;
                Game1.player.MoveToPosition(new Vector3(1440f, 0f, 648 + offset), 1);
                focusCameraOnTarget(new Vector2(1440f, 482f), 2f);
                tweener.Timer(1f).OnComplete(delegate
                {
                    if (Game1.Globals.FirstLabyrinthVisit)
                    {
                        Game1.Narrator.AddLine("Grandpa: <B>Lily stood in awe as she beheld the great Labyrinth Gates for the first time.", 12);
                    }
                    Game1.Narrator.AddLine("Grandpa: She felt excitement but also doubt. What if the key didn't work?", 12);
                    Game1.Narrator.AddLine("Chrys: WHAT?! Don't even mess like that, Grandpa!", 1);
                    Game1.Narrator.AddLine("Lily: Seriously! All that for nothing? I can't even!", 11);
                    Game1.Narrator.AddLine("Grandpa: Oh ho ho! Remember, kids, nothing is certain except death and back pain.", 12);
                    Game1.Narrator.AddLine("Grandpa: Just then, <B>Lily felt the key come alive as if longing to perform its purpose!", 12, preRemoveKey);
                });
            }
        }

        public void goPlayer()
        {
            Game1.player.Direction = 3;
            Game1.Gui.HideHud = false;
            focusCam = false;
            Game1.player.HasMoved = false;
            Game1.player.CamOffset = Vector2.Zero;
            Game1.player.RemovePlayerControls = false;
            if (Mod_HasAllKeyPieces() && Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.lab_openDoor)
            {
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.lab_enterLabyrinth;
            }
        }


        public bool Mod_HasAllKeyPieces()
        {
            return Game1.player.KeyPiece1 &&  Game1.player.KeyPiece2 && Game1.player.KeyPiece3;
        }
    }
}
