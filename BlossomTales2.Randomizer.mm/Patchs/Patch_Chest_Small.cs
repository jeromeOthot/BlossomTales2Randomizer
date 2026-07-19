using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using MonoMod;
#nullable disable
namespace BlossomTales2
{
    class patch_Chest_Small : Chest_Small
    {
        private bool underwater = false;
        private float shadowAlpha = 0.0f;
        private float shadowScale = 0.0f;
        private bool hasHit = false;
        
        public patch_Chest_Small(Vector3 position) : base(position)
        {
        }
        
        public extern void orig_Update(GameTime gameTime);

        public override void Update(GameTime gameTime)
        {
            tweener.Update((float)gameTime.ElapsedGameTime.TotalSeconds * Game1.TimeDelta);
            underwater = false;
            int num = (int)Position.X / 64;
            int num2 = (int)Position.Z / 64;
            if (num > -1 && num2 > -1 && num < Game1.CurrentLevel.Width && num2 < Game1.CurrentLevel.Height && Game1.CurrentLevel.Grid_Collision[num2, num] == 21)
            {
                underwater = true;
            }

            if (!underwater)
            {
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

                if (Frame > 0 && Frame < 3)
                {
                    Timer += (int)((float)gameTime.ElapsedGameTime.Milliseconds * Game1.TimeDelta);
                    if (Timer > 80)
                    {
                        Timer = 0;
                        Frame++;
                        if (Frame == 1)
                        {
                            Game1.playSoundCue("unlock_4");
                        }

                        if (Frame == 3)
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

                            //Juste pour un test d'un coffre est bien mapper au dictionnaire
                            GameLogger.LogInfo(new LocationId(Game1.CurrentLevel.Name, Name, Position).ToString());
                            if (RandomizerSingleton.Instance.TryGetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, Name, Position), out EquipableItem.ItemList item))
                            {
                                Game1.player.GiveItemReflection(item);
                            }
                            else //Conserver le comportement de base si le chest n'est pas dans la liste.
                            {
                                if (IDNumber == 0)
                                {
                                    int num3 = Game1.RandomNumber.Next(20, 26);
                                    for (int k = 0; k < num3; k++)
                                    {
                                        Vector3 velocity = new Vector3(patch_Game1.RandomFloat(-60, 60, 10f), patch_Game1.RandomFloat(40, 70, 10f), patch_Game1.RandomFloat(30, 60, 10f));
                                        Game1.CurrentLevel.LevelObjects.Add(new Coin_PU(Position, velocity));
                                    }

                                    if (Game1.LevelName == "blossom-house4.tmx" && Position == new Vector3(408f, 0f, 340f))
                                    {
                                        Game1.Globals.BlossomHouse4Chest = 2;
                                    }
                                }
                                else if (IDNumber == 1)
                                {
                                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Gold_Key);
                                    if (Game1.LevelName == "mansion-4.tmx")
                                    {
                                        Game1.Globals.Mansion4Chest = 2;
                                    }
                                    else if (Game1.LevelName == "mansion-16.tmx")
                                    {
                                        Game1.Globals.Mansion16Chest = 2;
                                    }
                                    else if (Game1.LevelName == "mansion-20.tmx")
                                    {
                                        Game1.Globals.Mansion20Chest = 2;
                                    }
                                    else if (Game1.LevelName == "castle-7.tmx")
                                    {
                                        Game1.Globals.Castle7Chest = 2;
                                        opendoors = true;
                                        IDNumber = 4;
                                    }
                                    else if (Game1.LevelName == "morkla-8.tmx")
                                    {
                                        Game1.Globals.Morkla8Chest = 2;
                                    }
                                }
                                else if (IDNumber == 2)
                                {
                                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Gold_Key);
                                    opendoors = true;
                                }
                                else if (IDNumber == 3)
                                {
                                    Game1.player.GiveItemReflection(EquipableItem.ItemList.HeartQ_1);
                                    if (Game1.LevelName == "overworld-23x18.tmx")
                                    {
                                        Game1.Globals.Ow23x18Chest = 2;
                                    }
                                }
                                else if (IDNumber == 4)
                                {
                                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Crystal);
                                }
                                else if (IDNumber == 5)
                                {
                                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Five_Gems);
                                }
                                else if (IDNumber == 10)
                                {
                                    Game1.player.GiveItemReflection(EquipableItem.ItemList.BlueGem);
                                    Game1.Globals.foundBlueGem = true;
                                }
                                else if (IDNumber == 11)
                                {
                                    Game1.player.GiveItemReflection(EquipableItem.ItemList.GreenGem);
                                    Game1.Globals.foundGreenGem = true;
                                }
                                else if (IDNumber == 13)
                                {
                                    Game1.player.GiveItemReflection(EquipableItem.ItemList.CombatScroll);
                                }
                                else if (IDNumber == 14)
                                {
                                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Honeycomb);
                                    Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.HoneycombOLD, Game1.player.Count_Honeycombs);
                                }
                                else if (IDNumber == 20)
                                {
                                    Game1.player.GiveItemReflection(EquipableItem.ItemList.HeartQ_1);
                                    Game1.Globals.sleepyManState = 3;
                                }
                                else if (IDNumber == 21)
                                {
                                    Game1.player.GiveItemReflection(EquipableItem.ItemList.HeartQ_1);
                                    Game1.Globals.Lighthouse_State = 5;
                                }
                                else if (IDNumber == 22)
                                {
                                    Game1.player.GiveItemReflection(EquipableItem.ItemList.Crystal);
                                    Game1.Globals.darklandsGhostTreasureState = 3;
                                }
                                else if (IDNumber == 30)
                                {
                                    int num4 = Game1.RandomNumber.Next(30, 40);
                                    for (int l = 0; l < num4; l++)
                                    {
                                        Game1.CurrentLevel.LevelObjects.Add(new Coin_PU(Position, new Vector3(patch_Game1.RandomFloat(-1000, 1000, 100f), patch_Game1.RandomFloat(500, 900, 100f), patch_Game1.RandomFloat(200, 800, 100f))));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (Frame > 2 && opendoors)
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

        //Stub for compiler
        private void OpenDoorGates() { }
    }
}
    