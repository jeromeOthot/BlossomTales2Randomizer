using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;

namespace BlossomTales2
{
    internal class patch_BossPirateCaptain : BossPirateCaptain
    {
        private int ExplodingTimer;
        public extern void orig_Update(GameTime gameTime);
        public extern void orig_Die();

        public patch_BossPirateCaptain(Vector3 position) : base(position)
        {            
        }

        public override void Update(GameTime gameTime)
        {
            Globaler.MainGameObjective currentObjective = Game1.Globals.MainQuestObjective;
            orig_Update(gameTime);
            if(ExplodingTimer > 100)
            {
                if(ModGlobals.OpenWorldState)
                {
                    Game1.Globals.MainQuestObjective = currentObjective;
                    Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_morklaEnter);
                }
            }
        }

        public override void Die()
        {
            Globaler.MainGameObjective currentObjective = Game1.Globals.MainQuestObjective;
            orig_Die();

            if (ModGlobals.OpenWorldState)
            {
                Game1.Globals.MainQuestObjective = currentObjective;
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.jungles_morklaEnter);
            }
        }
    }
}
