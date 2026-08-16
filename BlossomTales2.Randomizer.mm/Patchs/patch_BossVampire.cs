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
    public class patch_BossVampire : BossVampire
    {
        public patch_BossVampire(Vector3 position) : base(position)
        {
        }

        [MonoModIgnore]
        [PatchBossVampireUpdate]
        public extern override void Update(GameTime gameTime);
    }


    public class ModBossVampire
    {
        public static void Mod_CompleteVampireObjective()
        {
            if (ModGlobals.OpenWorldState)
            {
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.dark_headToVlad);
            }
            else
            {
                if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.dark_openBossDoor)
                    Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.dark_openBossDoor;
            }
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchBossVampireUpdate))]
    class PatchBossVampireUpdateAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchBossVampireUpdate(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modBossVampireType = MonoModRule.Modder.FindType("BlossomTales2.ModBossVampire").Resolve();
            MethodDefinition mod_CompleteVampireObjectiveMethod = modBossVampireType.FindMethod("Mod_CompleteVampireObjective");

            ILCursor cursor = new ILCursor(context);
            //Find
            //if (Game1.Globals.MainQuestObjective < Globaler.MainGameObjective.dark_openBossDoor)
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdfld("BlossomTales2.Globaler", "MainQuestObjective"),
                instr => instr.MatchLdcI4(42),
                instr => instr.MatchBge(out ILLabel label)
            );
            //Replace with
            //ModBossVampire.Mod_CompleteVampireObjective()
            cursor.RemoveRange(7);
            cursor.Emit(OpCodes.Call, mod_CompleteVampireObjectiveMethod);
        }
    }
}
