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
            RandomizerSingleton.Instance.GiveItemAtLocation(bossOctopus.Name, Vector3.Zero);
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
            TypeDefinition modBossOctopusType = MonoModRule.Modder.FindType("BlossomTales2.ModBossOctopus").Resolve();
            MethodDefinition mod_GiveHeartMethod = modBossOctopusType.FindMethod("Mod_GiveHeart");

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
            //ModBossOctopus.Mod_GiveHeart(this)
            cursor.RemoveRange(4);
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Call, mod_GiveHeartMethod);
        }
    }
}
