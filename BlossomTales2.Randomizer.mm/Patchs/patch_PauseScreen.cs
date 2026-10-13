using System;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod;
using MonoMod.Cil;
using MonoMod.InlineRT;
using MonoMod.Utils;

namespace BlossomTales2
{
    public class patch_PauseScreen : PauseScreen
    {
        [MonoModIgnore]
        [PatchPauseScreenDraw]
        public extern void Draw(SpriteBatch spriteBatch);
    }

    public class ModPauseMenu
    {
        public static bool Mod_HasAllKeyPieces()
        {
            return Game1.player.KeyPiece1 &&  Game1.player.KeyPiece2 && Game1.player.KeyPiece3;
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchPauseScreenDraw))]
    class PatchPauseScreenDrawAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchPauseScreenDraw(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modPauseMenu = MonoModRule.Modder.FindType("BlossomTales2.ModPauseMenu").Resolve();
            MethodDefinition mod_HasAllKeyPieces = modPauseMenu.FindMethod("Mod_HasAllKeyPieces");

            ILCursor cursor = new ILCursor(context);
            //Find
            // if (Game1.Globals.MainQuestObjective >= Globaler.MainGameObjective.lab_openDoor)
            ILLabel returnLabel = null;
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdfld("BlossomTales2.Globaler", "MainQuestObjective"),
                instr => instr.MatchLdcI4(47),
                instr => instr.MatchBlt(out returnLabel)
            );
            //Replace with
            //if (Mod_HasAllKeyPieces())
            cursor.RemoveRange(4);
            cursor.Emit(OpCodes.Call, mod_HasAllKeyPieces);
            cursor.Emit(OpCodes.Brfalse, returnLabel);
        }
    }
}
