using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    public class patch_CS_DarkTown : CS_DarkTown
    {
        public extern void orig_Init();
        public extern void orig_MoveGuardGhost();

        public override void Init()
        {
            if (Mod_HasNeverEnteredTown())
            {
                return;
            }
            for (int i = 0; i < Game1.CurrentLevel.LevelObjects.Count; i++)
            {
                if (Game1.CurrentLevel.LevelObjects[i] is NPC_2 && Game1.CurrentLevel.LevelObjects[i].Position == new Vector3(1344f, 0f, 2264f))
                {
                    ((NPC_2)Game1.CurrentLevel.LevelObjects[i]).Alive = false;
                }
            }
        }

        public void MoveGuardGhost()
        {
            if (Mod_HasNeverEnteredTown())
            {
                Mod_CompleteEnterTownObjective();
            }
            for (int i = 0; i < Game1.CurrentLevel.LevelObjects.Count; i++)
            {
                if (Game1.CurrentLevel.LevelObjects[i] is NPC_2 && Game1.CurrentLevel.LevelObjects[i].Position == new Vector3(1344f, 0f, 2264f))
                {
                    Game1.Particles.Add(new SmokePuff(((NPC_2)Game1.CurrentLevel.LevelObjects[i]).Position, 3f, 4f, playsfx: true));
                    Game1.makeParticleExplosion(((NPC_2)Game1.CurrentLevel.LevelObjects[i]).Position, Color.White, 10, 5, 0.25f);
                    Game1.Camera.Shake(12f, 0.98f);
                    ((NPC_2)Game1.CurrentLevel.LevelObjects[i]).Alive = false;
                }
            }
        }

        private bool Mod_HasNeverEnteredTown()
        {
            if (ModGlobals.OpenWorldState)
                return !Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_enterTown);
            else
                return Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.dark_learnSong;
        }

        private void Mod_CompleteEnterTownObjective()
        {
            if (ModGlobals.OpenWorldState)
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.dark_enterTown);
            else
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.dark_learnSong;
        }
    }
}
