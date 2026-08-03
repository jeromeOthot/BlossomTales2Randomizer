using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlossomTales2.Randomizer.mm
{
    public class RandomizerSingleton
    {
        public static RandomizerSingleton Instance { get; private set; }
        private Dictionary<LocationId, EquipableItem.ItemList> _locations;

        public static void Initialize()
        {
            Instance = new RandomizerSingleton();
            Instance.InitializeLocations();
        }

        public EquipableItem.ItemList GetItemAtLocation(LocationId location)
        {
            return _locations[location];
        }

        public bool TryGetItemAtLocation(LocationId location, out EquipableItem.ItemList item)
        {
            return _locations.TryGetValue(location, out item);
        }

        private void InitializeLocations()
        {
            //damage: épée || bombes || arc
            //levier: épée || grappin || boomerang || arc
            //water switch: épée
            //cart switch: épée || grappin || boomerang || arc
            //accès est: épée 2 || flippers
            //accès ouest: bombes || flippers
            //accès nord: accès ouest && grappin
            //accès jungle: accès est && bouteille || accès jungle ile
            //accès jungle ile: accès est && flippers || accès canyon && grappin
            //accès jungle NE: accès jungle && flippers
            //accès morkla: accès jungle && canne pêche && (bombes || flippers)
            //accès canyon: accès ouest && arc
            //accès canyon plateau: (accès canyon || accès ouest) && grappin
            //accès canyon steppe: accès canyon && grappin
            //ouvrir portes note: instrument && chanson sesame
            //accès temple: accès canyon && ouvrir portes note
            //accès temple 2: accès temple && clé
            //accès temple 3: accès temple 2 && leviers && clé && damage
            //accès temple 4: accès temple 3 && grappin

            _locations = new Dictionary<LocationId, EquipableItem.ItemList>
            {
                { new LocationId("anchor-house4.tmx", "Chest_Small", new Vector3(348f, 0f, 436f)), EquipableItem.ItemList.GoldCoin }, //accès jungle
                { new LocationId("blossom-blacksmith.tmx", "npc21", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Bow }, //Flag: MorklaComplete
                { new LocationId("blossom-house1.tmx", "Chest_Small", new Vector3(672f, 0f, 308f)), EquipableItem.ItemList.GoldCoin },
                { new LocationId("blossom-house2.tmx", "Chest_Small", new Vector3(708f, 0f, 356f)), EquipableItem.ItemList.GoldCoin },
                { new LocationId("blossom-house3.tmx", "Chest_Small", new Vector3(368f, 0f, 148f)), EquipableItem.ItemList.GoldCoin },
                { new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(696f, 0f, 416f)), EquipableItem.ItemList.GoldCoin },
                { new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(780f, 0f, 156f)), EquipableItem.ItemList.GoldCoin },
                { new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(408f, 0f, 340f)), EquipableItem.ItemList.GoldCoin }, //lampe
                { new LocationId("blossom-house5.tmx", "Chest_Small", new Vector3(480f, 0f, 172f)), EquipableItem.ItemList.GoldCoin }, //accès est && lampe
                { new LocationId("blossom-house5.tmx", "Chest_Small", new Vector3(660f, 0f, 348f)), EquipableItem.ItemList.GoldCoin }, //accès est && lampe
                { new LocationId("blossom-lilyHouse.tmx", "npc7_1", new Vector3(480f, 0f, 328f)), EquipableItem.ItemList.Shield },
                { new LocationId("blossom-lilyHouse.tmx", "npc7_2", new Vector3(480f, 0f, 328f)), EquipableItem.ItemList.Sword },
                //Blossom tavern  Chest
                { new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 272f)), EquipableItem.ItemList.GoldCoin }, //bombes
                { new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 464f)), EquipableItem.ItemList.GoldCoin },  //bombes
                { new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 272f)), EquipableItem.ItemList.GoldCoin },  //bombes
                { new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 464f)), EquipableItem.ItemList.HeartQ_1 },  //bombes
                //...
                { new LocationId("canyon-house1.tmx", "Chest_Small", new Vector3(576f, 0f, 208f)), EquipableItem.ItemList.GoldCoin }, //accès canyon
                { new LocationId("canyon-house3.tmx", "Chest_Small", new Vector3(772f, 0f, 580f)), EquipableItem.ItemList.GoldCoin }, //accès canyon
                { new LocationId("canyon-house3.tmx", "Chest_Small", new Vector3(344f, 0f, 496f)), EquipableItem.ItemList.GoldCoin }, //accès canyon
                //{ new LocationId("castle-4.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Gold_Key }, //unknown
                //{ new LocationId("castle-6.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("castle-9.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("castle-12.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("darklands-house2-floor2.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("darklands-house6.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("darklands-house7.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("forestMaze-end.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown (big coins ID30)
                //{ new LocationId("forestMaze-end.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                { new LocationId("jungles-21x22.tmx", "Chest_Small", new Vector3(852f, 0f, 684f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && grappin
                { new LocationId("jungles-21x22.tmx", "hunter", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Bow }, //accès jungle ile && grappin && arc
                { new LocationId("jungles-21x22.tmx", "Chest_Small", new Vector3(1336f, 0f, 896f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 496f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(332f, 0f, 400f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 304f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(672f, 0f, 304f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(764f, 0f, 404f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(672f, 0f, 496f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(552f, 0f, 688f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && flippers && pelle
                { new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(524f, 0f, 808f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && flippers && pelle
                { new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(600f, 0f, 888f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && flippers && pelle
                { new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(804f, 0f, 892f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && flippers && pelle
                { new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(924f, 0f, 788f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && flippers && pelle
                { new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(864f, 0f, 684f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && flippers && pelle
                { new LocationId("jungles-22x20.tmx", "Chest_Small", new Vector3(1764f, 0f, 1896f)), EquipableItem.ItemList.Honeycomb }, //accès jungle ile && flippers || accès jungle && bombes
                { new LocationId("jungles-22x21-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 360f)), EquipableItem.ItemList.HeartQ_1 }, //accès jungle ile && flippers
                { new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(480f, 0f, 224f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && ouvrir portes notes
                { new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(608f, 0f, 224f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && ouvrir portes notes
                { new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(480f, 0f, 352f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && ouvrir portes notes
                { new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(608f, 0f, 352f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile && ouvrir portes notes
                { new LocationId("jungles-22x22.tmx", "Chest_Small", new Vector3(1440f, 0f, 2016f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile
                { new LocationId("jungles-22x22.tmx", "Chest_Small", new Vector3(1668f, 0f, 512f)), EquipableItem.ItemList.GoldCoin }, //accès jungle
                { new LocationId("jungles-22x22-lighthouse.tmx", "Chest_Small", new Vector3(556f, 0f, 144f)), EquipableItem.ItemList.GoldCoin }, //accès jungle ile
                //{ new LocationId("jungles-23x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Crystal }, //unknown
                { new LocationId("jungles-23x19.tmx", "archJungle", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Shovel }, //accès jungle
                { new LocationId("jungles-23x20.tmx", "Chest_Small", new Vector3(2264f, 0f, 1928f)), EquipableItem.ItemList.GoldCoin }, //accès jungle
                { new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(448f, 0f, 492f)), EquipableItem.ItemList.GoldCoin }, //accès jungle && bombes
                { new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 368f)), EquipableItem.ItemList.GoldCoin }, //accès jungle && bombes
                { new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 288f)), EquipableItem.ItemList.GoldCoin }, //accès jungle && bombes
                { new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(736f, 0f, 368f)), EquipableItem.ItemList.GoldCoin }, //accès jungle && bombes
                { new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(640f, 0f, 492f)), EquipableItem.ItemList.GoldCoin }, //accès jungle && bombes
                { new LocationId("jungles-23x21.tmx", "fisherman", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.FishingRod }, //accès jungle
                { new LocationId("jungles-23x22.tmx", "Chest_Small", new Vector3(740f, 0f, 1252f)), EquipableItem.ItemList.GoldCoin }, //accès jungle
                { new LocationId("jungles-24x19.tmx", "Chest_Small", new Vector3(1548f, 0f, 1272f)), EquipableItem.ItemList.Honeycomb }, //accès jungle && (flippers || bombes)
                { new LocationId("jungles-24x19.tmx", "Chest_Small", new Vector3(2468f, 0f, 2308f)), EquipableItem.ItemList.GoldCoin }, //accès jungle NE
                //{ new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                { new LocationId("jungles-24x20.tmx", "Chest_Small", new Vector3(2088f, 0f, 2200f)), EquipableItem.ItemList.GoldCoin }, //accès jungle && bombes
                { new LocationId("jungles-24x22.tmx", "Chest_Small", new Vector3(2336f, 0f, 0496f)), EquipableItem.ItemList.GoldCoin }, //accès jungle
                { new LocationId("jungles-25x19.tmx", "Chest_Small", new Vector3(276f, 0f, 1876f)), EquipableItem.ItemList.GoldCoin }, //accès jungle NE && bombes
                { new LocationId("jungles-25x19-combat.tmx", "Chest_Small", new Vector3(2176f, 0f, 1792f)), EquipableItem.ItemList.CombatScroll }, //accès jungle NE && lanterne && arc
                { new LocationId("jungles-25x20-cave.tmx", "Chest_Small", new Vector3(448f, 0f, 200f)), EquipableItem.ItemList.HeartQ_1 }, //accès jungle NE && lanterne
                { new LocationId("jungles-25x21.tmx", "Chest_Small", new Vector3(232f, 0f, 272f)), EquipableItem.ItemList.GoldCoin }, //accès jungle && bombes
                //{ new LocationId("jungles-25x21-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-25x21-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-25x21-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-25x21-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                { new LocationId("jungles-25x22.tmx", "Chest_Small", new Vector3(160f, 0f, 836f)), EquipableItem.ItemList.GoldCoin }, //accès jungle
                { new LocationId("jungles-25x22.tmx", "Chest_Small", new Vector3(1760f, 0f, 728f)), EquipableItem.ItemList.GoldCoin }, //accès jungle && bombes
                { new LocationId("jungles-25x22-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 488f)), EquipableItem.ItemList.GoldCoin }, //accès jungle && bombes
                { new LocationId("jungles-25x22-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 488f)), EquipableItem.ItemList.GoldCoin }, //accès jungle && bombes
                { new LocationId("jungles-firstPrimate.tmx", "Chest", new Vector3(480f, 0f, 256f)), EquipableItem.ItemList.Bombs }, //accès jungle
                //{ new LocationId("lighthouse.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown (probablement renommée en jungles-22x22)
                //{ new LocationId("mansion-12.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Gold_Key }, //unknown
                //{ new LocationId("mansion-12-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("mansion-20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                { new LocationId("morkla-3.tmx", "Chest_Small", new Vector3(704f, 0f, 1732f)), EquipableItem.ItemList.GoldCoin }, //accès Morkla && water switch && (lanterne && damage || flippers)
                { new LocationId("morkla-4.tmx", "Chest_Small", new Vector3(1216f, 0f, 384f)), EquipableItem.ItemList.GoldCoin }, //accès Morkla && water switch && flippers && leviers
                { new LocationId("morkla-4.tmx", "Chest_Small", new Vector3(448f, 0f, 768f)), EquipableItem.ItemList.GoldCoin }, //accès Morkla && water switch && flippers && leviers
                { new LocationId("morkla-8.tmx", "Chest_Small", new Vector3(736f, 0f, 556f)), EquipableItem.ItemList.Gold_Key }, //accès Morkla && lanterne && water switch && (damage || flippers)
                { new LocationId("morkla-17.tmx", "Chest_Small", new Vector3(1732f, 0f, 384f)), EquipableItem.ItemList.BlueGem }, //accès Morkla && water switch && flippers && leviers
                { new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(736f, 0f, 288f)), EquipableItem.ItemList.GoldCoin }, //accès Morkla && flippers && (water switch && leviers || lanterne)
                { new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(928f,0f, 288f)), EquipableItem.ItemList.GoldCoin }, //accès Morkla && flippers && (water switch && leviers || lanterne)
                { new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(928f, 0f, 608f)), EquipableItem.ItemList.GoldCoin }, //accès Morkla && flippers && (water switch && leviers || lanterne)
                { new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(736f, 0f, 608f)), EquipableItem.ItemList.GoldCoin }, //accès Morkla && flippers && (water switch && leviers || lanterne)
                { new LocationId("morkla-19.tmx", "Chest_Small", new Vector3(640f, 0f, 312f)), EquipableItem.ItemList.HeartQ_1 }, //accès Morkla && water switch && flippers && bombes
                { new LocationId("morkla-20.tmx", "Chest_Small", new Vector3(640f, 0f, 384f)), EquipableItem.ItemList.HeartQ_1 }, //accès Morkla && bombes && (lanterne || flippers)
                { new LocationId("morkla-21.tmx", "Chest_Small", new Vector3(896f, 0f, 776f)), EquipableItem.ItemList.GreenGem }, //accès Morkla && water switch && flippers && leviers
                { new LocationId("morkla-octopus.tmx", "BossOctopus", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_4 }, //accès Morkla && damage && flippers && water switch && blue gem && green gem
                { new LocationId("morkla-octopus.tmx", "Chest", new Vector3(896f, 0f, 624f)), EquipableItem.ItemList.KeyPiece1 }, //accès Morkla && damage && flippers && water switch && blue gem && green gem
                { new LocationId("morkla-pirateBoss.tmx", "Chest", new Vector3(768f, 0f, 640f)), EquipableItem.ItemList.Flippers }, //accès Morkla && damage && (lanterne && water switch && clé || flippers)
                //{ new LocationId("objectPalette.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //pas sur si vraie location, semble etre tool debug
                { new LocationId("orchid-tomb-3.tmx", "Chest_Small", new Vector3(416f, 0f, 320f)), EquipableItem.ItemList.Gold_Key }, //lampe && levier && damage
                { new LocationId("orchid-tomb-4.tmx", "orchid_heart", new Vector3(604f, 0f, 304f)), EquipableItem.ItemList.HeartQ_4 }, //lampe && damage && clé
                { new LocationId("orchid-tomb-4.tmx", "orchid_sword", new Vector3(604f, 0f, 304f)), EquipableItem.ItemList.Sword }, //lampe && damage && clé
                { new LocationId("orchid-tomb-4.tmx", "orchid_shield", new Vector3(604f, 0f, 304f)), EquipableItem.ItemList.Shield }, //lampe && damage && clé
                //{ new LocationId("overworld-15x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                { new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 368f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && bombes
                { new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(480f, 0f, 492f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && bombes
                { new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(576f, 0f, 616f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && bombes
                { new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(768f, 0f, 616f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && bombes
                { new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(864f, 0f, 488f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && bombes
                { new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(800f, 0f, 364f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && bombes
                { new LocationId("overworld-15x20.tmx", "archCanyon", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Crystal }, //accès canyon && CanyonBone == 20
                { new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 412f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && bombes
                { new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 408f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && bombes
                { new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 604f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && bombes
                { new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 604f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && bombes
                { new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1216f, 0f, 448f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && ouvrir portes note && levier
                { new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1408f, 0f, 448f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && ouvrir portes note && levier
                { new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1312f, 0f, 312f)), EquipableItem.ItemList.HeartQ_1 }, //accès canyon && ouvrir portes note && levier
                //{ new LocationId("overworld-16x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-16x18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                { new LocationId("overworld-16x21-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 228f)), EquipableItem.ItemList.HeartQ_1 }, //accès canyon && levier
                { new LocationId("overworld-16x22.tmx", "beggar", new Vector3(980f, 0f, 1616f)), EquipableItem.ItemList.HeartQ_1 }, //accès canyon
                //{ new LocationId("overworld-17x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                { new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(1056f, 0f, 2008f)), EquipableItem.ItemList.GoldCoin }, //accès canyon steppe
                { new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(1184f, 0f, 2008f)), EquipableItem.ItemList.GoldCoin }, //accès canyon steppe
                { new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(476f, 0f, 208f)), EquipableItem.ItemList.Honeycomb }, //accès canyon plateau
                { new LocationId("overworld-17x20-cave.tmx", "Chest_Small", new Vector3(736f, 0f, 224f)), EquipableItem.ItemList.HeartQ_1 }, //accès canyon plateau && grappin
                { new LocationId("overworld-17x21-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 480f)), EquipableItem.ItemList.HeartQ_1 }, //accès canyon steppe && leviers
                //{ new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                { new LocationId("overworld-18x18.tmx", "Chest_Small", new Vector3(596f, 0f, 2064f)), EquipableItem.ItemList.GoldCoin }, //accès ouest && bombes
                { new LocationId("overworld-18x19.tmx", "Chest_Small", new Vector3(792f, 0f, 1348f)), EquipableItem.ItemList.Honeycomb }, //accès canyon plateau && grappin
                { new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(512f, 0f, 308f)), EquipableItem.ItemList.GoldCoin }, //accès ouest && bombes
                { new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(704f, 0f, 308f)), EquipableItem.ItemList.GoldCoin }, //accès ouest && bombes
                { new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(512f, 0f, 544f)), EquipableItem.ItemList.GoldCoin }, //accès ouest && bombes
                { new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(704f, 0f, 544f)), EquipableItem.ItemList.GoldCoin }, //accès ouest && bombes
                { new LocationId("overworld-18x21.tmx", "Chest_Small", new Vector3(224f, 0f, 592f)), EquipableItem.ItemList.GoldCoin }, //accès canyon steppe
                { new LocationId("overworld-18x21.tmx", "Chest_Small", new Vector3(784f, 0f, 156f)), EquipableItem.ItemList.GoldCoin }, //accès canyon steppe
                { new LocationId("overworld-18x22.tmx", "Chest_Small", new Vector3(448f, 0f, 448f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && flippers && bombes
                { new LocationId("overworld-18x22.tmx", "Chest_Small", new Vector3(640f, 0f, 448f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && flippers && bombes
                { new LocationId("overworld-18x22.tmx", "Chest_Small", new Vector3(544f, 0f, 344f)), EquipableItem.ItemList.GoldCoin }, //accès canyon && flippers && bombes
                //{ new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                { new LocationId("overworld-19x17.tmx", "Chest_Small", new Vector3(2168f, 0f, 1600f)), EquipableItem.ItemList.GoldCoin }, //accès nord && bombes
                { new LocationId("overworld-19x18.tmx", "Chest_Small", new Vector3(184f, 0f, 296f)), EquipableItem.ItemList.Honeycomb }, //accès ouest
                //{ new LocationId("overworld-19x19.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //pelle id=2
                //{ new LocationId("overworld-19x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                { new LocationId("overworld-19x21.tmx", "Chest_Small", new Vector3(2348f, 0f, 1832f)), EquipableItem.ItemList.GoldCoin }, //accès canyon
                { new LocationId("overworld-19x22.tmx", "bard", new Vector3(852f, 0f, 1348f)), EquipableItem.ItemList.Guitar }, //accès canyon && damage
                { new LocationId("overworld-19x22-bardCave.tmx", "Chest_Small", new Vector3(416f, 0f, 240f)), EquipableItem.ItemList.HeartQ_1 }, //accès canyon && damage (&& ouvrir portes note)
                //{ new LocationId("overworld-20x16-combat.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.CombatScroll }, //unknown
                { new LocationId("overworld-20x17.tmx", "Chest_Small", new Vector3(376f, 0f, 1480f)), EquipableItem.ItemList.GoldCoin }, //accès nord && bombes
                //{ new LocationId("overworld-20x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Honeycomb }, //unknown
                { new LocationId("overworld-20x17-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 232f)), EquipableItem.ItemList.HeartQ_1 }, //accès nord
                { new LocationId("overworld-20x18.tmx", "lanternGuy", new Vector3(1036f, 0f, 660f)), EquipableItem.ItemList.Torch },
                { new LocationId("overworld-20x18-cave1.tmx", "Chest_Small", new Vector3(1184f, 0f, 2208f)), EquipableItem.ItemList.HeartQ_1 }, //épée 2 && lanterne
                { new LocationId("overworld-20x20.tmx", "Chest_Small", new Vector3(2352f, 0f, 2212f)), EquipableItem.ItemList.Guitar }, //bombes
                { new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(296f, 0f, 224f)), EquipableItem.ItemList.GoldCoin }, //accès est && bombes
                { new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(424f, 0f, 224f)), EquipableItem.ItemList.GoldCoin }, //accès est && bombes
                { new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(424f, 0f, 320f)), EquipableItem.ItemList.GoldCoin }, //accès est && bombes
                { new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(296f, 0f, 320f)), EquipableItem.ItemList.GoldCoin }, //accès est && bombes
                //{ new LocationId("overworld-20x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                { new LocationId("overworld-20x22-combat.tmx", "Chest_Small", new Vector3(2176f, 0f, 1828f)), EquipableItem.ItemList.CombatScroll }, //accès canyon && leviers (&& damage)
                //{ new LocationId("overworld-21x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                { new LocationId("overworld-21x17-wizHouse.tmx", "Chest_Small", new Vector3(956f, 0f, 128f)), EquipableItem.ItemList.GoldCoin }, //accès nord
                //{ new LocationId("overworld-21x18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Honeycomb }, //unknown
                //{ new LocationId("overworld-21x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x19.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //pelle id=2
                { new LocationId("overworld-21x20-cave.tmx", "Chest_Small", new Vector3(1192f, 0f, 612f)), EquipableItem.ItemList.GoldCoin },
                { new LocationId("overworld-21x20-cave.tmx", "Chest_Small", new Vector3(1268f, 0f, 612f)), EquipableItem.ItemList.GoldCoin },
                //{ new LocationId("overworld-21x20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //pelle id=6
                //{ new LocationId("overworld-22x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-22x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-22x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-22x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-22x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //pelle
                //{ new LocationId("overworld-22x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                { new LocationId("overworld-22x19.tmx", "Chest_Small", new Vector3(2240f, 0f, 1752f)), EquipableItem.ItemList.GoldCoin }, //accès jungle
                //{ new LocationId("overworld-22x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-22x20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Honeycomb }, //unknown
                { new LocationId("overworld-22x20.tmx", "Chest_Small", new Vector3(264f, 0f, 472f)), EquipableItem.ItemList.GoldCoin }, //accès est
                //{ new LocationId("overworld-23x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Honeycomb }, //unknown
                //{ new LocationId("overworld-23x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-23x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-23x17-farm.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-23x17-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-23x17-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Five_Gems }, //unknown
                //{ new LocationId("overworld-24x16-mausoleum.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-24x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Crystal }, //pelle id=3
                //{ new LocationId("overworld-24x18-blueTent.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-24x18-greenTent.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-24x18-greenTent.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-25x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-25x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-25x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-25x17-combat.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.CombatScroll }, //unknown
                //{ new LocationId("overworld-25x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-25x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-25x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Honeycomb }, //unknown
                //{ new LocationId("sandCastle.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Five_Gems }, //unknown
                { new LocationId("temple-1.tmx", "Chest_Small", new Vector3(1376f, 0f, 1184f)), EquipableItem.ItemList.Gold_Key }, //accès temple && (arc && leviers || grappin)
                { new LocationId("temple-4.tmx", "Chest_Small", new Vector3(3232f, 0f, 1392f)), EquipableItem.ItemList.GoldCoin }, //accès temple 2 && leviers
                { new LocationId("temple-5.tmx", "Chest_Small", new Vector3(624f, 0f, 1728f)), EquipableItem.ItemList.Gold_Key }, //accès temple 2 && (leviers || grappin)
                { new LocationId("temple-5-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 292f)), EquipableItem.ItemList.Crystal }, //accès temple 2 && leviers && bombes
                { new LocationId("temple-6.tmx", "Chest_Small", new Vector3(388f, 0f, 756f)), EquipableItem.ItemList.GoldCoin }, //accès temple 2 && leviers && bombes
                { new LocationId("temple-8.tmx", "Chest_Small", new Vector3(896f, 0f, 1792f)), EquipableItem.ItemList.GoldCoin }, //accès temple 3 && grappin
                { new LocationId("temple-8.tmx", "Chest_Small", new Vector3(1728f, 0f, 1792f)), EquipableItem.ItemList.GoldCoin }, //accès temple 3 && grappin
                { new LocationId("temple-11.tmx", "Chest_Small", new Vector3(440f, 0f, 452f)), EquipableItem.ItemList.GoldCoin }, //accès temple 3 && leviers
                { new LocationId("temple-11.tmx", "Chest_Small", new Vector3(448f, 0f, 1920f)), EquipableItem.ItemList.Gold_Key }, //accès temple 3 && leviers && bombes
                { new LocationId("temple-15-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //accès temple 4 && bombes
                { new LocationId("temple-17.tmx", "Chest_Small", new Vector3(276f, 0f, 220f)), EquipableItem.ItemList.GoldCoin }, //accès temple 4
                { new LocationId("temple-18.tmx", "Chest_Small", new Vector3(1152f, 0f, 1724f)), EquipableItem.ItemList.GoldCoin }, //accès temple 4
                { new LocationId("temple-18-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 284f)), EquipableItem.ItemList.HeartQ_1 }, //accès temple 4 && bombes
                { new LocationId("temple-genieBoss.tmx", "BossGenie", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_4 }, //accès temple 4 && lanterne
                { new LocationId("temple-genieBoss.tmx", "Chest", new Vector3(832f, 0f, 640f)), EquipableItem.ItemList.KeyPiece2 }, //accès temple 4 && lanterne
                { new LocationId("temple-vultureBoss.tmx", "Chest", new Vector3(768f, 0f, 640f)), EquipableItem.ItemList.GrappleHook }, //accès temple 3 && (clé || grappin) && damage
            };
        }
    }
}
