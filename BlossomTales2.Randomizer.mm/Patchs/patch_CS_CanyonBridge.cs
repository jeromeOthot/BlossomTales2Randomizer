
using System.Collections.Generic;
using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlossomTales2
{
    internal class patch_CS_CanyonBridge : CS_CanyonBridge
    {
        private bool pretalk;
        private Puppet post1;
        private Puppet post2;
        private Puppet bridgeBottom;

        public extern void orig_firstTalk();

        public override void Init()
        {
            post1 = new Puppet("bridgePost", new Vector3(1140f, 0f, 970f));
            post2 = new Puppet("bridgePost", new Vector3(1140f, 0f, 1058f));
            bridgeBottom = new Puppet("bridgeBottom", new Vector3(1148f, 0f, 1106f));
            bridgeBottom.Zdepth -= 200f;
            puppets.AddRange(new List<Puppet> { post1, post2, bridgeBottom });
            puppetList.AddRange(new List<Puppet> { post1, post2, bridgeBottom });
            if (Mod_HasNotRaisedBridge())
            {
                post1.play("up");
                post2.play("up");
                bridgeBottom.play("up");
                Game1.CurrentLevel.LevelObjects.Add(new BridgeWheel(new Vector3(884f, 0f, 1092f)));
                NPC_2 nPC_ = new NPC_2(new Vector3(1464f, 0f, 1040f));
                nPC_.IDNumber = 46;
                nPC_.linePointer = 101;
                nPC_.Se = SpriteEffects.FlipHorizontally;
                Game1.CurrentLevel.LevelObjects.Add(nPC_);
            }
            else
            {
                post1.play("downIdle");
                post2.play("downIdle");
                bridgeBottom.play("downIdle");
                Game1.CurrentLevel.LevelObjects.Add(new BridgeWheel(new Vector3(880f, 0f, 1092f), 1));
                Game1.CurrentLevel.Grid_Collision[15, 21] = 0;
                Game1.CurrentLevel.Grid_Collision[16, 21] = 0;
                Game1.CurrentLevel.Grid_Collision[15, 14] = 0;
                Game1.CurrentLevel.Grid_Collision[16, 14] = 0;
            }
        }

        public void firstTalk()
        {
            if (pretalk)
            {
                pretalk = false;
                Game1.Dialoger.AddLine("Raider: No, no, NO!!! They raised the bridge to the <N>Sunkiss <N>Canyons while I was at the festival!!");
                Game1.Dialoger.AddLine("Raider: They must of heard about the return of the <A>Minotaur <A>King!");
                Game1.Dialoger.AddLine("Raider: Got something that can hit that wheel on the other side to drop the bridge?");
                if (Game1.player.Inventory.Contains(EquipableItem.ItemList.Bow))
                {
                    Game1.Dialoger.AddLine("Raider: Is that a <R>Bow? Huzzah!");
                    Game1.Dialoger.AddLine("Raider: Hit the wheel on the other side of the bridge with an arrow.");
                }
                else if (Mod_HasBowObjective())
                {
                    Mod_SetBowObjective();
                    Game1.Dialoger.AddLine("Raider: Doesn't look like it. Hmm...");
                    Game1.Dialoger.AddLine("Raider: I bet the blacksmith in <M>Blossomdale can make you something.");
                    Game1.Dialoger.AddLine("Raider: I'd lend ya some coins, but I spent most of them at the festival.");
                    Game1.Dialoger.AddLine("Raider: I lost the rest trying to hit the wheel.", "updateMap");
                }
            }
            else if (Game1.player.Inventory.Contains(EquipableItem.ItemList.Bow))
            {
                Game1.Dialoger.AddLine("Raider: Is that a <R>Bow? Huzzah!");
                Game1.Dialoger.AddLine("Raider: Hit the wheel on the other side of the bridge with an arrow.");
            }
            else
            {
                Game1.Dialoger.AddLine("Raider: You don't have anything to drop the bridge?");
                if (Mod_HasBowObjective())
                {
                    Game1.Dialoger.AddLine("Raider: Hmmm, I bet the blacksmith in <M>Blossomdale can make you something.");
                }
            }
        }

        public void lowerBridge()
        {
            Mod_CompleteBridgeObjective();
            post1.play("down");
            post2.play("down");
            bridgeBottom.play("down");
            post1.Timer = 0;
            post2.Timer = 0;
            bridgeBottom.Timer = 0;
            tweener.Timer(0.3f).OnComplete(delegate
            {
                Game1.CurrentLevel.Grid_Collision[15, 21] = 0;
                Game1.CurrentLevel.Grid_Collision[16, 21] = 0;
                Game1.CurrentLevel.Grid_Collision[15, 14] = 0;
                Game1.CurrentLevel.Grid_Collision[16, 14] = 0;
                Game1.playSoundCue("blank057");
                Game1.Camera.Shake(12f, 0.95f);
                for (int i = 0; i < 50; i++)
                {
                    int num = Game1.RandomNumber.Next(1, 3);
                    Game1.Particles.Add(new P_WaterSquare(new Vector3(Game1.RandomNumber.Next(940, 1360), 0f, Game1.RandomNumber.Next(1065, 1096)), Game1.GetRandomVelocity(5f, 10f, 5f, 10f), new Vector3(num, 0f, num), Color.LightBlue));
                }

                pretalk = false;
                Game1.Dialoger.AddLine("Raider: You saved me! Thank you so much! Maybe I'll see you in town.");
                Game1.Dialoger.AddLine("Raider: Oh, and be careful; the <N>Sunkiss <N>Canyons have always been a treacherous place.", moveNPC);
            });
        }

        private bool Mod_HasNotRaisedBridge()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.canyons_getBow);
            else
                return Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.canyons_crossBridge;
        }

        private bool Mod_HasBowObjective()
        {
            if (ModGlobals.OpenWorldState)
                return Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.jungles_morklaComplete) && !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.canyons_getBow);
            else
                return Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.canyons_talkToBowGuy || Game1.Globals.MainQuestObjective == Globaler.MainGameObjective.canyons_getBow;
        }

        private void Mod_SetBowObjective()
        {
            if(!ModGlobals.OpenWorldState)
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.canyons_getBow;
        }

        private void Mod_CompleteBridgeObjective()
        {
            if(ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.canyons_getBow);
            }
            else
            {
                if (Game1.Globals.MainQuestObjective <= Globaler.MainGameObjective.canyons_crossBridge)
                    Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.canyons_talkWithOwl;
            }
        }
    }
}
