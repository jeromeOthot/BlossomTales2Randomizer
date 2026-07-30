using BlossomTales2.Extensions;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_Chest : Chest
    {
        private bool underwater;
        private float shadowAlpha = 0.0f;
        private float shadowScale = 0.0f;
        private bool hasHit = false;

        public extern void orig_Update();

        public patch_Chest(Vector3 position) : base(position)
        {
        }

        public override void Update(GameTime gameTime)
        {
            underwater = false;
            int num = (int)Position.X / 64;
            int num2 = (int)Position.Z / 64;
            if (num > -1 && num2 > -1 && num < Game1.CurrentLevel.Width && num2 < Game1.CurrentLevel.Height && Game1.CurrentLevel.Grid_Collision[num2, num] == 21)
            {
                underwater = true;
            }

            if (Game1.player.Direction == 1 && Frame == 0 && Position.Y < 10f)
            {
                Rectangle rectangle = new Rectangle((int)Game1.player.Position.X - 4, (int)Game1.player.Position.Z - (int)Game1.player.Size.Z * 2 - 4, 8, 6);
                Rectangle rectangle2 = GetRectangle();
                if (rectangle.Intersects(rectangle2))
                {
                    bool flag = false;
                    for (int i = 0; i < Game1.CurrentLevel.LevelObjects.Count; i++)
                    {
                        if (Game1.CurrentLevel.LevelObjects[i] is DragableGeneric)
                        {
                            rectangle2 = new Rectangle((int)Position.X - (int)Size.X * 2 + 8, (int)Position.Z - (int)Size.Z * 2 + 8, (int)Size.X * 4 - 16, (int)Size.Z * 4 - 16);
                            if (Game1.CurrentLevel.LevelObjects[i].GetRectangle().Intersects(rectangle2))
                            {
                                flag = true;
                                break;
                            }
                        }
                    }

                    if (!flag)
                    {
                        Game1.player.ShowOpenButton = true;
                        if (Input.A_Button_Pressed())
                        {
                            Game1.player.ClearPlayer();
                            Frame = 1;
                            Game1.player.LockPosition = true;
                            Game1.player.LockDirection = true;
                            Game1.player.RemovePlayerControls = true;
                            Game1.player.CurrentAnimation = Player.Animations.UseItem;
                            Game1.player.Frame = 0;
                            Game1.player.Timer = 0;
                        }
                    }
                }
            }

            if (Frame > 0 && Frame < 7)
            {
                Timer += (int)((float)gameTime.ElapsedGameTime.Milliseconds * Game1.TimeDelta);
                if (Timer > 80)
                {
                    Timer = 0;
                    Frame++;
                    if (Frame == 2)
                    {
                        Game1.playSoundCue("unlock_4");
                    }

                    if (Frame == 6)
                    {
                        if (IDNumber == 101)
                        {
                            Game1.player.LockPosition = false;
                            Game1.player.LockDirection = false;
                            Game1.player.RemovePlayerControls = false;
                            Alive = false;
                            Game1.Particles.Add(new SmokePuff(Position + new Vector3(0f, 0f, -20f), 5f, 6f, playsfx: true));
                            if (Game1.CutSceneController != null && Game1.CutSceneController is CS_GenieBoss)
                            {
                                ((CS_GenieBoss)Game1.CutSceneController).initScene();
                            }
                        }
                        else
                        {
                            for (int j = 0; j < Game1.CurrentLevel.LevelObjects.Count; j++)
                            {
                                if (Game1.CurrentLevel.LevelObjects[j] is SpawnRectangle && ((SpawnRectangle)Game1.CurrentLevel.LevelObjects[j]).WaitToDie > 0)
                                {
                                    ((SpawnRectangle)Game1.CurrentLevel.LevelObjects[j]).WaitToDie = 2;
                                }
                            }

                            Game1.playSoundCue("chestOpen");
                            Game1.Perma_Objects.Add(new PermaListItem(Game1.CurrentLevel.Name, Name, Position));
                            Game1.player.LockPosition = false;
                            Game1.player.LockDirection = false;
                            Game1.player.RemovePlayerControls = false;
                            Mod_GiveItem();
                        }
                    }
                }
            }

            if (Frame > 6 && opendoors)
            {
                Timer += (int)((float)gameTime.ElapsedGameTime.Milliseconds * Game1.TimeDelta);
                if (Timer > 1000)
                {
                    Timer = 0;
                    opendoors = false;
                    OpenDoorGates();
                }
            }

            if (Position.Y > 0f)
            {
                if (Velocity.Y > -18f)
                {
                    Velocity.Y -= 0.4f * Game1.TimeDelta;
                }

                Position.Y += Velocity.Y * Game1.TimeDelta;
                if (Position.Y < 1f)
                {
                    Position.Y = 0f;
                    if (!hasHit)
                    {
                        Game1.Particles.Add(new Shockwave(Position));
                        Game1.Camera.Shake(8f, 0.96f);
                        hasHit = true;
                    }

                    if (Velocity.Y < -5f)
                    {
                        Velocity.Y *= -0.3f;
                        Position.Y = 1f;
                    }
                    else
                    {
                        Position.Y = 0f;
                        Velocity.Y = 0f;
                    }
                }
            }

            shadowAlpha = 0.3f - Position.Y / 500f;
            shadowScale = 4f - Position.Y / 90f;
        }

        //TODO: Appeler la fonction parent, sans reflection.
        private void OpenDoorGates()
        {
            bool flag = false;
            if (Game1.LevelName == "overworld-15x18.tmx")
            {
                for (int i = 0; i < Game1.CurrentLevel.LevelObjects.Count; i++)
                {
                    if (Game1.CurrentLevel.LevelObjects[i] is RaisingWall && Game1.CurrentLevel.LevelObjects[i].IDNumber == 9)
                    {
                        Game1.CurrentLevel.LevelObjects[i].Size.Y = 0f;
                        flag = true;
                    }

                    if (Game1.CurrentLevel.LevelObjects[i] is CameraOverrider)
                    {
                        Game1.CurrentLevel.LevelObjects[i].Alive = false;
                    }
                }
            }
            else
            {
                for (int j = 0; j < Game1.CurrentLevel.LevelObjects.Count; j++)
                {
                    if (Game1.CurrentLevel.LevelObjects[j] is DoorGate)
                    {
                        Game1.CurrentLevel.LevelObjects[j].Velocity.Y = 0f;
                        flag = true;
                    }

                    if (Game1.CurrentLevel.LevelObjects[j] is RaisingWall)
                    {
                        Game1.CurrentLevel.LevelObjects[j].Size.Y = 0f;
                        flag = true;
                    }
                }
            }

            if (flag)
            {
                Game1.Camera.Shake(8f, 0.96f);
            }
        }

        private void Mod_GiveItem()
        {
            GameLogger.LogInfo(new LocationId(Game1.CurrentLevel.Name, Name, Position).ToString());
            if (RandomizerSingleton.Instance.TryGetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, Name, Position), out EquipableItem.ItemList item))
            {
                Game1.player.GiveItemReflection(item);
                HandleSpecialChests();
            }
            else
            {
                if (IDNumber == 0)
                {
                    int num3 = Game1.RandomNumber.Next(20, 30);
                    for (int k = 0; k < num3; k++)
                    {
                        Vector3 velocity = new Vector3(patch_Game1.RandomFloat(-60, 60, 10f), patch_Game1.RandomFloat(40, 70, 10f), patch_Game1.RandomFloat(30, 60, 10f));
                        Game1.CurrentLevel.LevelObjects.Add(new Coin_PU(Position, velocity));
                    }
                }
                else if (IDNumber == 1)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Gold_Key);
                }
                else if (IDNumber == 2)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Gold_Key);
                    opendoors = true;
                }
                else if (IDNumber == 5)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.HeartQ_1);
                }
                else if (IDNumber == 12)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.GrappleHook);
                }
                else if (IDNumber == 13)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.RexTeleporter);
                }
                else if (IDNumber == 14)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.MirrorShield);
                    if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.lab_findCastleEntrance)
                    {
                        Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.lab_findCastleEntrance;
                    }
                }
                else if (IDNumber == 21)
                {
                    Game1.player.RemovePlayerControls = true;
                }
                else if (IDNumber == 22)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.KeyPiece2);
                    Game1.player.RemovePlayerControls = true;
                    Game1.Achievementer.CheckAchievment(3);
                }
                else if (IDNumber == 23)
                {
                    Game1.player.GiveItemReflection(EquipableItem.ItemList.KeyPiece3);
                    Game1.player.RemovePlayerControls = true;
                    Game1.Achievementer.CheckAchievment(4);
                }
                else if (IDNumber == 27)
                {
                    int num4 = Game1.RandomNumber.Next(30, 50);
                    for (int l = 0; l < num4; l++)
                    {
                        Vector3 velocity2 = new Vector3(patch_Game1.RandomFloat(-60, 60, 10f), patch_Game1.RandomFloat(40, 70, 10f), patch_Game1.RandomFloat(30, 60, 10f));
                        Game1.CurrentLevel.LevelObjects.Add(new Coin_PU(Position, velocity2));
                    }
                }
            }
        }

        private void HandleSpecialChests()
        {
            if (IDNumber == 2)
            {
                opendoors = true;
            }
            else if (IDNumber == 10)
            {
                if (ModGlobals.OpenWorldState)
                    Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_getBombs);
                else
                    Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.jungles_talkToMorkla;
            }
            else if (IDNumber == 11)
            {
                Game1Extensions.AddLevelPermaObject(Name, Position);
                if (!Game1.WaterLevelUp)
                {
                    Game1.playSoundCue("blank154");
                    Game1.WaterLevelUp = true;
                }
            }
            else if (IDNumber == 14)
            {
                if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.lab_findCastleEntrance)
                {
                    Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.lab_findCastleEntrance;
                }
            }
            else if (IDNumber == 21)
            {
                Game1.player.RemovePlayerControls = true;
            }
            else if (IDNumber == 22)
            {
                Game1.player.RemovePlayerControls = true;
            }
            else if (IDNumber == 23)
            {
                Game1.player.RemovePlayerControls = true;
            }
        }
    }
}
