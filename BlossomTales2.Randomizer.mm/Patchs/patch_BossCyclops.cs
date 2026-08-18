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
    public class patch_BossCyclops : BossCyclops
    {
        public patch_BossCyclops(Vector3 position) : base(position)
        {
        }
        [MonoModIgnore]
        [PatchBossCyclopsUpdate]
        public extern void Update(GameTime gameTime);
    }

    public class ModBossCyclops
    {
        public static void Mod_CompleteCyclopsObjective()
        {
            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.lab_enterLabyrinth);
            }
            else
            {
                if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.lab_getMirrorShield)
                {
                    Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.lab_getMirrorShield;
                }
            }
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchBossCyclopsUpdate))]
    class PatchBossCyclopsUpdateAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchBossCyclopsUpdate(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modBossCyclopsType = MonoModRule.Modder.FindType("BlossomTales2.ModBossCyclops").Resolve();
            MethodDefinition mod_CompleteCyclopsObjectiveMethod = modBossCyclopsType.FindMethod("Mod_CompleteCyclopsObjective");

            ILCursor cursor = new ILCursor(context);
            //Find
            //if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.lab_getMirrorShield)
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdfld("BlossomTales2.Globaler", "MainQuestObjective"),
                instr => instr.MatchLdcI4(49),
                instr => instr.MatchBge(out ILLabel label)
            );
            //Replace with
            //ModBossCyclops.Mod_CompleteCyclopsObjective()
            cursor.RemoveRange(7);
            cursor.Emit(OpCodes.Call, mod_CompleteCyclopsObjectiveMethod);
        }
    }
}
