// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

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
    public class patch_CS_MansionMiniDoor : CS_MansionMiniDoor
    {
        private Puppet door;
        private int doorState;
        private bool checkRect = true;

        public extern void orig_Init();

        public override void Init()
        {
            door = new Puppet("mansionMiniDoor", new Vector3(1056f, 0f, 284f));
            door.play("closed");
            if (Mod_IsMansionMiniBossDoorOpened())
            {
                door.play("open");
                door.collide = false;
                doorState = -1;
                checkRect = false;
            }
            puppets.Add(door);
        }

        [MonoModIgnore]
        [PatchCSMansionMiniDoorUpdate]
        public extern override void Update(GameTime gameTime);

        private bool Mod_IsMansionMiniBossDoorOpened()
        {
            if (ModGlobals.OpenWorldState)
                return Game1Extensions.IsObjectiveCompleted(Globaler.MainGameObjective.dark_openMiniBossDoor);
            else
                return Game1.Globals.MainQuestObjective > Globaler.MainGameObjective.dark_openMiniBossDoor;
        }
    }

    public class ModCSMansionMiniDoor
    {
        public static void Mod_CompleteOpenMiniDoor()
        {
            if(ModGlobals.OpenWorldState)
                Game1Extensions.MarkObjectiveComplete(Globaler.MainGameObjective.dark_openMiniBossDoor);
            else
                Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.dark_headToVlad;
        }
    }
}

namespace MonoMod
{
    [MonoModCustomMethodAttribute(nameof(MonoModRules.PatchCSMansionMiniDoorUpdate))]
    class PatchCSMansionMiniDoorUpdateAttribute : Attribute { }

    static partial class MonoModRules
    {
        public static void PatchCSMansionMiniDoorUpdate(ILContext context, CustomAttribute attrib)
        {
            TypeDefinition modCSMansionMiniDoorType = MonoModRule.Modder.FindType("BlossomTales2.ModCSMansionMiniDoor").Resolve();
            MethodDefinition mod_CompleteOpenMiniDoorMethod = modCSMansionMiniDoorType.FindMethod("Mod_CompleteOpenMiniDoor");

            ILCursor cursor = new ILCursor(context);
            //Find
            //Game1.Globals.MainQuestObjective = Globaler.MainGameObjective.dark_headToVlad;
            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchLdsfld("BlossomTales2.Game1", "Globals"),
                instr => instr.MatchLdcI4(41),
                instr => instr.MatchStfld("BlossomTales2.Globaler", "MainQuestObjective")
            );
            //Replace with
            //ModCSMansionMiniDoor.Mod_CompleteOpenMiniDoor()
            cursor.RemoveRange(3);
            cursor.Emit(OpCodes.Call, mod_CompleteOpenMiniDoorMethod);
        }
    }
}
