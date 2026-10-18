// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.ComponentModel;
using System.Reflection;

namespace BlossomTales2.Randomizer.mm
{
    public enum ItemType
    {
        None = -1,
        [Description("Progressive Sword")]
        Sword,
        [Description("Progressive Shield")]
        Shield,
        [Description("Progressive Bow")]
        Bow,
        Bombs,
        Boomerang,
        Tomahawk,
        Torch,
        [Description("Fishing Rod")]
        FishingRod,
        Accordian,
        Guitar,
        [Description("Teleporter")]
        RexTeleporter,
        [Description("Bottle (Empty)")]
        Jar_Empty,
        [Description("Bottle (Health Potion)")]
        Jar_Health,
        [Description("Bottle (Mushroom Soup)")]
        Jar_ReduceCost,
        [Description("Bottle (Double Damage Potion)")]
        Jar_DoubleDamage,
        [Description("Bottle (Slow Time Potion)")]
        Jar_SlowTime,
        [Description("Bottle (Bubble Shield Potion)")]
        Jar_BubbleShield,
        [Description("Bottle (Orb Shield Potion)")]
        Jar_ArmorOrbs,
        [Description("Bottle (Rejuvenation Potion)")]
        Jar_Resurrection,
        [Description("Bottle (Ghoul-ixir Potion)")]
        Jar_Ghost,
        [Description("Bottle (Dragonheart Potion)")]
        Jar_Fire,
        [Description("Bottle (Speedster Potion)")]
        Jar_Speedster,
        Jar_Sludge,
        [Description("Yo-Yo")]
        GrappleHook,
        Empty,
        [Description("Gold Key")]
        Gold_Key,
        [Description("Quarter Heart")]
        HeartQ_1,
        [Description("Full Heart")]
        HeartQ_4,
        [Description("Energy Crystal")]
        Crystal,
        Ingred_Gem,
        Letter,
        [Description("Bee Medallion")]
        BeeMedallion,
        Honeycomb,
        [Description("Old Bone")]
        CanyonBone,
        ResurrectionFlower,
        Crystal_4,
        TriBow,
        [Description("Five Gems")]
        Five_Gems,
        Package,
        Shovel,
        [Description("Key Piece 1")]
        KeyPiece1,
        [Description("Key Piece 2")]
        KeyPiece2,
        [Description("Key Piece 3")]
        KeyPiece3,
        [Description("Necklace")]
        HeartNecklace,
        [Description("Tree Seed")]
        TreeSeed,
        [Description("Coins")]
        GoldCoin,
        WoodSword,
        WoodShield,
        [Description("Green Gem")]
        GreenGem,
        [Description("Blue Gem")]
        BlueGem,
        [Description("Floaties")]
        Flippers,
        MirrorShield,
        KingSword,
        [Description("Combat Scroll")]
        CombatScroll,
        MinotaurCoin,
        Falcon,
        [Description("Minuet of the Desert")]
        OpenSesame,
        [Description("Aria to the Morning")]
        WakeUp,
        [Description("Rising Wind Sonata")]
        SummonBalloon,
        [Description("Concerto of the Hoof")]
        CallHorse,
        [Description("Overture of the Wise")]
        GrandpaHint,
        //Logic flags
        MorklaBoss,
    }

    public static class ItemTypeExtensions
    {
        public static string GetPrettyName(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }

                return name;
            }
            return string.Empty;
        }
    }
}
