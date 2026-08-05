using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod;
using MonoMod.Cil;
using MonoMod.InlineRT;
using MonoMod.Utils;
using System;

namespace BlossomTales2
{
    public class patch_BossGenie : BossGenie
    {
        public patch_BossGenie(Vector3 position) : base(position)
        {
        }

        [MonoModIgnore]
        [PatchBossGenieUpdate]
        public extern void Update(GameTime gameTime);
    }

    public class ModBossGenie
    {
        public static void Mod_GiveHeart(BossGenie bossGenie)
        {
            RandomizerSingleton.Instance.GiveItemAtLocation(bossGenie.Name, Vector3.Zero);
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchBossGenieUpdate))]
    class PatchBossGenieUpdateAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchBossGenieUpdate(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modBossGenieType = MonoModRule.Modder.FindType("BlossomTales2.ModBossGenie").Resolve();
            MethodDefinition mod_GiveHeartMethod = modBossGenieType.FindMethod("Mod_GiveHeart");

            ILCursor cursor = new ILCursor(context);
            //Find
            //Game1.player.GiveItem(EquipableItem.ItemList.HeartQ_4);
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "player"),
                instr => instr.MatchLdcI4(27),
                instr => instr.MatchLdcI4(1),
                instr => instr.MatchCallvirt("BlossomTales2.Player", "GiveItem")
            );
            //Replace with
            //ModBossGenie.Mod_GiveHeart(this)
            cursor.RemoveRange(4);
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Call, mod_GiveHeartMethod);
        }
    }
}
