using System;
using BlossomTales2.Extensions;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod;
using MonoMod.Cil;
using MonoMod.InlineRT;
using MonoMod.Utils;

namespace BlossomTales2
{
    internal class patch_BossVulture : BossVulture
    {
        public patch_BossVulture(Vector3 position) : base(position)
        {
        }

        [MonoModIgnore]
        [PatchBossVultureUpdate]
        public extern void Update(GameTime gameTime);
    }

    public class ModBossVulture
    {
        public static void Mod_CompleteVultureObjective()
        {
            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.canyons_headToVulture);
            }
            else
            {
                if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.canyons_headToGolem)
                {
                    Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.canyons_headToGolem;
                }
            }
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchBossVultureUpdate))]
    class PatchBossVultureUpdateAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchBossVultureUpdate(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modBossVultureType = MonoModRule.Modder.FindType("BlossomTales2.ModBossVulture").Resolve();
            MethodDefinition mod_CompleteVultureObjectiveMethod = modBossVultureType.FindMethod("Mod_CompleteVultureObjective");

            ILCursor cursor = new ILCursor(context);
            //Find
            //if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.canyons_headToGolem)
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdfld("BlossomTales2.Globaler", "MainQuestObjective"),
                instr => instr.MatchLdcI4(28),
                instr => instr.MatchBge(out ILLabel label)
            );
            //Replace with
            //ModBossVulture.Mod_CompleteVultureObjective()
            cursor.RemoveRange(7);
            cursor.Emit(OpCodes.Call, mod_CompleteVultureObjectiveMethod);
        }
    }
}
