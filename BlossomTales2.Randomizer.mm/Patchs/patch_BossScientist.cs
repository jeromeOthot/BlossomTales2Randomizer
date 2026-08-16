using System;
using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod;
using MonoMod.Cil;
using MonoMod.InlineRT;
using MonoMod.Utils;

namespace BlossomTales2
{
    public class patch_BossScientist : BossScientist
    {
        public patch_BossScientist(Vector3 position) : base(position)
        {
        }

        [MonoModIgnore]
        [PatchBossScientistUpdate]
        public extern void Update(GameTime gameTime);
    }

    public class ModBossScientist
    {
        public static void Mod_GiveHeart(BossScientist bossScientist)
        {
            RandomizerSingleton.Instance.GiveItemAtLocation(bossScientist.Name, Vector3.Zero);
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchBossScientistUpdate))]
    class PatchBossScientistUpdateAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchBossScientistUpdate(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modBossScientistType = MonoModRule.Modder.FindType("BlossomTales2.ModBossScientist").Resolve();
            MethodDefinition mod_GiveHeartMethod = modBossScientistType.FindMethod("Mod_GiveHeart");

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
            //ModBossScientist.Mod_GiveHeart(this)
            cursor.RemoveRange(4);
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Call, mod_GiveHeartMethod);
        }
    }
}
