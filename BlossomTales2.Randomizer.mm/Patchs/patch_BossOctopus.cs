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
    public class patch_BossOctopus : BossOctopus
    {
        public patch_BossOctopus(Vector3 position) : base(position)
        {
        }

        [MonoModIgnore]
        [PatchBossOctopusUpdate]
        public extern void Update(GameTime gameTime);
    }

    public class ModBossOctopus
    {
        public static void Mod_GiveHeart(BossOctopus bossOctopus)
        {
            EquipableItem.ItemList item = RandomizerSingleton.Instance.GetItemAtLocation(new LocationId(Game1.CurrentLevel.Name, bossOctopus.Name, Vector3.Zero));
            Game1.player.GiveItemReflection(item);
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchBossOctopusUpdate))]
    class PatchBossOctopusUpdateAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchBossOctopusUpdate(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition patchType = MonoModRule.Modder.FindType("BlossomTales2.ModBossOctopus").Resolve();
            MethodDefinition mod_GiveHeartMethod = patchType.FindMethod("Mod_GiveHeart");

            ILCursor cursor = new ILCursor(context);

            //IL_01e0: ldsfld       class BlossomTales2.Player BlossomTales2.Game1::player
            //IL_01e5: ldc.i4.s     27 // 0x1b
            //IL_01e7: ldc.i4.1
            //IL_01e8: callvirt instance void BlossomTales2.Player::GiveItem(valuetype BlossomTales2.EquipableItem / ItemList, bool)
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "player"),
                instr => instr.MatchLdcI4(27),
                instr => instr.MatchLdcI4(1),
                instr => instr.MatchCallvirt("BlossomTales2.Player", "GiveItem")
            );

            //en
            //IL_001c: ldarg.0      // this
            //IL_001d: call instance void BlossomTales2.patch_BossOctopus::Mod_GiveHeart()
            //IL_0022: nop
            cursor.RemoveRange(4);
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Call, mod_GiveHeartMethod);
            cursor.Emit(OpCodes.Nop);
        }
    }
}
