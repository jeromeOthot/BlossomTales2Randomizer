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
    public class patch_GUI : GUI
    {
        [MonoModIgnore]
        [PatchGuiDraw]
        public extern void Draw(SpriteBatch spriteBatch);
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchGuiDraw))]
    class PatchGuiDrawAttribute : Attribute
    {
    }

    static partial class MonoModRules
    {
        public static void PatchGuiDraw(ILContext context, CustomAttribute attrib)
        {
            ILCursor cursor = new ILCursor(context);
            //Find L.521
            // else
            // {
            //    int num19 = 46;
            //    if (Game1.player.SwordLevel == 2)

            ILLabel elseBranchLabel = null;
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchBr(out elseBranchLabel),
                instr => instr.MatchLdcI4(46),
                instr => instr.MatchStloc(36),
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "player"),
                instr => instr.MatchLdfld("BlossomTales2.Player", "SwordLevel"),
                instr => instr.MatchLdcI4(2),
                instr => instr.MatchBneUn(out _)
            );

            //Skip the br instruction
            cursor.Index++;

            // Add
            // else if(Game1.player.SwordLevel > 0)
            // instead of
            // else
            ILLabel elseIfLabel = cursor.MarkLabel();
            TypeDefinition game1Type = MonoModRule.Modder.FindType("BlossomTales2.Game1").Resolve();
            FieldDefinition playerField = game1Type.FindField("player");
            cursor.Emit(OpCodes.Ldsfld, playerField);
            TypeDefinition playerType = MonoModRule.Modder.FindType("BlossomTales2.Player").Resolve();
            FieldDefinition swordLevelField = playerType.FindField("SwordLevel");
            cursor.Emit(OpCodes.Ldfld, swordLevelField);
            cursor.Emit(OpCodes.Ldc_I4, 0);
            cursor.Emit(OpCodes.Ble, elseBranchLabel);

            //Find the previous branch
            // if (num24 != -1)
            ILLabel ifBranchLabel = null;
            cursor.GotoPrev(MoveType.Before,
                instr => instr.MatchLdloc(15),
                instr => instr.MatchLdcI4(-1),
                instr => instr.MatchBeq(out ifBranchLabel)
            );

            //Replace branch target.
            ifBranchLabel.Target = elseIfLabel.Target;
        }
    }
}
