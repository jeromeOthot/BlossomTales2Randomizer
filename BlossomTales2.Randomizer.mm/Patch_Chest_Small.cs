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
          patch_Player patchPlayer = new patch_Player();
          this.tweener.Update((float) gameTime.ElapsedGameTime.TotalSeconds * Game1.TimeDelta);
          this.underwater = false;
          int index1 = (int) this.Position.X / 64 /*0x40*/;
          int index2 = (int) this.Position.Z / 64 /*0x40*/;
          if (index1 > -1 && index2 > -1 && index1 < Game1.CurrentLevel.Width && index2 < Game1.CurrentLevel.Height && Game1.CurrentLevel.Grid_Collision[index2, index1] == 21)
            this.underwater = true;
          if (!this.underwater)
          {
            if (Game1.player.Direction == 1 && this.Frame == 0 && (double) this.Position.Y < 10.0)
            {
              Rectangle rectangle1 = new Rectangle((int) Game1.player.Position.X - 4, (int) Game1.player.Position.Z - (int) Game1.player.Size.Z * 2 - 4, 8, 6);
              Rectangle rectangle2 = this.GetRectangle();
              if (rectangle1.Intersects(rectangle2))
              {
                bool flag = false;
                for (int index3 = 0; index3 < Game1.CurrentLevel.LevelObjects.Count; ++index3)
                {
                  if (Game1.CurrentLevel.LevelObjects[index3] is DragableGeneric)
                  {
                    rectangle2 = new Rectangle((int) this.Position.X - (int) this.Size.X * 2 + 8, (int) this.Position.Z - (int) this.Size.Z * 2 + 8, (int) this.Size.X * 4 - 16 /*0x10*/, (int) this.Size.Z * 4 - 16 /*0x10*/);
                    if (Game1.CurrentLevel.LevelObjects[index3].GetRectangle().Intersects(rectangle2))
                    {
                      flag = true;
                      break;
                    }
                  }
                }
                if (!flag)
                {
                  Game1.player.ShowOpenButton = true;
                  if (BlossomTales2.Input.A_Button_Pressed())
                  {
                    Game1.player.ClearPlayer();
                    this.Frame = 1;
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
            if (this.Frame > 0 && this.Frame < 3)
            {
              this.Timer += (int) ((double) gameTime.ElapsedGameTime.Milliseconds * (double) Game1.TimeDelta);
              if (this.Timer > 80 /*0x50*/)
              {
                this.Timer = 0;
                ++this.Frame;
                if (this.Frame == 1)
                  Game1.playSoundCue("unlock_4");
                if (this.Frame == 3)
                {
                  for (int index4 = 0; index4 < Game1.CurrentLevel.LevelObjects.Count; ++index4)
                  {
                    if (Game1.CurrentLevel.LevelObjects[index4] is SpawnRectangle && ((SpawnRectangle) Game1.CurrentLevel.LevelObjects[index4]).WaitToDie > 0)
                      ((SpawnRectangle) Game1.CurrentLevel.LevelObjects[index4]).WaitToDie = 2;
                  }
                  Game1.playSoundCue("chestOpen");
                  Game1.Perma_Objects.Add(new PermaListItem(Game1.CurrentLevel.Name, this.Name, this.Position));
                  Game1.player.LockPosition = false;
                  Game1.player.LockDirection = false;
                  Game1.player.RemovePlayerControls = false;
                  if (this.IDNumber == 0)
                  {
                    patchPlayer.GiveItem(EquipableItem.ItemList.GoldCoin);
                    //TODO: A arranger a mettre dans methode GiveItem
                    /*
                    int num = Game1.RandomNumber.Next(20, 26);
                    for (int index5 = 0; index5 < num; ++index5)
                    {
                      Vector3 velocity = new Vector3(Game1.RandomFloat(-60, 60, 10f), Game1.RandomFloat(40, 70, 10f), Game1.RandomFloat(30, 60, 10f));
                      Game1.CurrentLevel.LevelObjects.Add((LevelObject) new Coin_PU(this.Position, velocity));
                    }
                    if (Game1.LevelName == "blossom-house4.tmx" && this.Position == new Vector3(408f, 0.0f, 340f))
                      Game1.Globals.BlossomHouse4Chest = 2; */
                  }
                  else if (this.IDNumber == 1)
                  {
                    patchPlayer.GiveItem(EquipableItem.ItemList.Gold_Key);
                    switch (Game1.LevelName)
                    {
                      case "mansion-4.tmx":
                        Game1.Globals.Mansion4Chest = 2;
                        break;
                      case "mansion-16.tmx":
                        Game1.Globals.Mansion16Chest = 2;
                        break;
                      case "mansion-20.tmx":
                        Game1.Globals.Mansion20Chest = 2;
                        break;
                      case "castle-7.tmx":
                        Game1.Globals.Castle7Chest = 2;
                        this.opendoors = true;
                        this.IDNumber = 4;
                        break;
                      case "morkla-8.tmx":
                        Game1.Globals.Morkla8Chest = 2;
                        break;
                    }
                  }
                  else if (this.IDNumber == 2)
                  {
                    patchPlayer.GiveItem(EquipableItem.ItemList.Gold_Key);
                    this.opendoors = true;
                  }
                  else if (this.IDNumber == 3)
                  {
                    patchPlayer.GiveItem(EquipableItem.ItemList.HeartQ_1);
                    if (Game1.LevelName == "overworld-23x18.tmx")
                      Game1.Globals.Ow23x18Chest = 2;
                  }
                  else if (this.IDNumber == 4)
                    patchPlayer.GiveItem(EquipableItem.ItemList.Crystal);
                  else if (this.IDNumber == 5)
                    patchPlayer.GiveItem(EquipableItem.ItemList.Five_Gems);
                  else if (this.IDNumber == 10)
                  {
                    patchPlayer.GiveItem(EquipableItem.ItemList.BlueGem);
                    Game1.Globals.foundBlueGem = true;
                  }
                  else if (this.IDNumber == 11)
                  {
                    patchPlayer.GiveItem(EquipableItem.ItemList.GreenGem);
                    Game1.Globals.foundGreenGem = true;
                  }
                  else if (this.IDNumber == 13)
                    patchPlayer.GiveItem(EquipableItem.ItemList.CombatScroll);
                  else if (this.IDNumber == 14)
                  {
                    patchPlayer.GiveItem(EquipableItem.ItemList.Honeycomb);
                    Game1.Gui.AddGuiTicker(EquipableItem.IngredientList.HoneycombOLD, Game1.player.Count_Honeycombs);
                  }
                  else if (this.IDNumber == 20)
                  {
                    patchPlayer.GiveItem(EquipableItem.ItemList.HeartQ_1);
                    Game1.Globals.sleepyManState = 3;
                  }
                  else if (this.IDNumber == 21)
                  {
                    patchPlayer.GiveItem(EquipableItem.ItemList.HeartQ_1);
                    Game1.Globals.Lighthouse_State = 5;
                  }
                  else if (this.IDNumber == 22)
                  {
                    patchPlayer.GiveItem(EquipableItem.ItemList.Crystal);
                    Game1.Globals.darklandsGhostTreasureState = 3;
                  }
                  else if (this.IDNumber == 30)
                  {
                    patchPlayer.GiveItem(EquipableItem.ItemList.GoldCoin);
                    //TODO a mettre dans dans la methode GiveItem
                    /*
                    int num = Game1.RandomNumber.Next(30, 40);
                    for (int index6 = 0; index6 < num; ++index6)
                      Game1.CurrentLevel.LevelObjects.Add((LevelObject) new Coin_PU(this.Position, new Vector3(Game1.RandomFloat(-1000, 1000, 100f), Game1.RandomFloat(500, 900, 100f), Game1.RandomFloat(200, 800, 100f))));
                    */
                  }
                }
              }
            }
          }
          if (this.Frame > 2 && this.opendoors)
          {
            this.Timer += (int) ((double) gameTime.ElapsedGameTime.Milliseconds * (double) Game1.TimeDelta);
            if (this.Timer > 1000)
            {
              this.Timer = 0;
              this.opendoors = false;
              this.OpenDoorGates();
            }
          }
          if ((double) this.Position.Y > 0.0)
          {
            if ((double) this.Velocity.Y > -18.0)
              this.Velocity.Y -= 0.4f * Game1.TimeDelta;
            this.Position.Y += this.Velocity.Y * Game1.TimeDelta;
            if ((double) this.Position.Y < 1.0)
            {
              this.Position.Y = 0.0f;
              if (!this.hasHit)
              {
                Game1.Particles.Add((Particle) new Shockwave(this.Position));
                Game1.Camera.Shake(8f, 0.96f);
                this.hasHit = true;
              }
              if ((double) this.Velocity.Y < -5.0)
              {
                this.Velocity.Y *= -0.3f;
                this.Position.Y = 1f;
              }
              else
              {
                this.Position.Y = 0.0f;
                this.Velocity.Y = 0.0f;
              }
            }
          }
          this.shadowAlpha = (float) (0.30000001192092896 - (double) this.Position.Y / 500.0);
          this.shadowScale = (float) (4.0 - (double) this.Position.Y / 90.0);
        }
        
        private void OpenDoorGates()
        {
          bool flag1 = false;
          for (int index = 0; index < Game1.CurrentLevel.LevelObjects.Count; ++index)
          {
            if (Game1.CurrentLevel.LevelObjects[index] is CameraOverrideObject && Game1.CurrentLevel.LevelObjects[index].IDNumber == this.IDNumber)
            {
              Game1.CamController.focusCameraOnTarget(new Vector2(Game1.CurrentLevel.LevelObjects[index].Position.X, Game1.CurrentLevel.LevelObjects[index].Position.Z), Game1.CurrentLevel.LevelObjects[index].Velocity.X, Game1.CurrentLevel.LevelObjects[index].Velocity.Y);
              Game1.CamController.IDNumber = this.IDNumber;
              Game1.CamController.OpenBoth = true;
              flag1 = true;
              break;
            }
          }
          if (flag1)
            return;
          bool flag2 = false;
          for (int index = 0; index < Game1.CurrentLevel.LevelObjects.Count; ++index)
          {
            if (Game1.CurrentLevel.LevelObjects[index] is DoorGate)
            {
              Game1.CurrentLevel.LevelObjects[index].Velocity.Y = 0.0f;
              flag2 = true;
            }
          }
          if (!flag2)
            return;
          Game1.Camera.Shake(8f, 0.96f);
        }
    }
}
    