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
        private Dictionary<LocationId, ItemData> _locations;

        public static void Initialize()
        {
            Instance = new RandomizerSingleton();
            Instance.InitializeLocations();
        }

        public void GiveItemAtLocation(string name, Vector3 position)
        {
            ItemData item = TryGetItemByNameAndLocation(name, position);
            if(item != null)
                GiveItem(item);
        }

        public ItemData TryGetItem(string mapName, string name, Vector3 position)
        {
            LocationId location = new  LocationId(mapName, name, position);
            if (TryGetItemAtLocation(mapName, name, position, out ItemData item))
                return item;
            return null;
        }

        public ItemData TryGetItemByNameAndLocation(string name, Vector3 position)
        {
            if (TryGetItemAtLocation(Game1.CurrentLevel.Name ,name, position, out ItemData item))
                return item;
            return null;
        }

        public bool TryGetItemAtLocation(string mapName, string name, Vector3 position, out ItemData item)
        {
            LocationId location = new  LocationId(mapName, name, position);
            return _locations.TryGetValue(location, out item);
        }

        public ItemData TryGetItemWithMapNameAndName(string mapName, string name)
        {
            var itemByName = _locations.Where(x => x.Key.MapName == mapName).Where(x => x.Key.Name == name).FirstOrDefault().Value;
            if(itemByName  == null)
                Game1.Dialoger.AddLine($"Item not found on {mapName} {name}");
            return itemByName;
        }

        public void GiveItem(ItemData itemData)
        {
            if (itemData.TryConvertToEquipableItem(out EquipableItem.ItemList item))
            {
                Game1.player.GiveItemReflection(item);
                return;
            }

            if (itemData.TryConvertToIngredientItem(out EquipableItem.IngredientList ingredient))
            {
                //Game1.player.GiveIngredientReflection(item);
                return;
            }

            if (itemData.TryConvertToSongItem(out Globaler.Songs song))
            {
                Game1.player.LearnSong(song);
                return;
            }
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
            //accès jungle: accès est && bouteille || accès jungle ile || accès dark
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
            //accès dark: accès nord && damage
            //accès monsterton: accès dark && bouteille && canne pêche
            //accès mansion: accès monsterton && boomerang
            //accès mansion 2: accès mansion && clé && damage

            _locations = new Dictionary<LocationId, ItemData>
            {
                { new LocationId("anchor-house4.tmx", "Chest_Small", new Vector3(348f, 0f, 436f)), new ItemData(ItemType.GoldCoin) }, //accès jungle
                { new LocationId("blossom-blacksmith.tmx", "npc21", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.Bow) }, //Flag: MorklaComplete
                { new LocationId("blossom-house1.tmx", "Chest_Small", new Vector3(672f, 0f, 308f)), new ItemData(ItemType.GoldCoin) },
                { new LocationId("blossom-house2.tmx", "Chest_Small", new Vector3(708f, 0f, 356f)), new ItemData(ItemType.GoldCoin) },
                { new LocationId("blossom-house3.tmx", "Chest_Small", new Vector3(368f, 0f, 148f)), new ItemData(ItemType.GoldCoin) },
                { new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(696f, 0f, 416f)), new ItemData(ItemType.GoldCoin) },
                { new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(780f, 0f, 156f)), new ItemData(ItemType.GoldCoin) },
                { new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(408f, 0f, 340f)), new ItemData(ItemType.GoldCoin) }, //lampe
                { new LocationId("blossom-house5.tmx", "Chest_Small", new Vector3(480f, 0f, 172f)), new ItemData(ItemType.GoldCoin) }, //accès est && lampe
                { new LocationId("blossom-house5.tmx", "Chest_Small", new Vector3(660f, 0f, 348f)), new ItemData(ItemType.GoldCoin) }, //accès est && lampe
                { new LocationId("blossom-lilyHouse.tmx", "npc7_1", new Vector3(480f, 0f, 328f)), new ItemData(ItemType.Shield) },
                { new LocationId("blossom-lilyHouse.tmx", "npc7_2", new Vector3(480f, 0f, 328f)), new ItemData(ItemType.Sword) },
                //Blossom tavern  Chest
                { new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 272f)), new ItemData(ItemType.GoldCoin) }, //bombes
                { new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 464f)), new ItemData(ItemType.GoldCoin) },  //bombes
                { new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 272f)), new ItemData(ItemType.GoldCoin) },  //bombes
                { new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 464f)), new ItemData(ItemType.HeartQ_1) },  //bombes
                //...
                { new LocationId("canyon-house1.tmx", "Chest_Small", new Vector3(576f, 0f, 208f)), new ItemData(ItemType.GoldCoin) }, //accès canyon
                { new LocationId("canyon-house3.tmx", "Chest_Small", new Vector3(772f, 0f, 580f)), new ItemData(ItemType.GoldCoin) }, //accès canyon
                { new LocationId("canyon-house3.tmx", "Chest_Small", new Vector3(344f, 0f, 496f)), new ItemData(ItemType.GoldCoin) }, //accès canyon
                //{ new LocationId("castle-4.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.Gold_Key) }, //unknown
                //{ new LocationId("castle-6.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("castle-9.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("castle-12.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                { new LocationId("darklands-house2-floor2.tmx", "bard_song", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.WakeUp) }, //accès monsterton
                { new LocationId("darklands-house2-floor2.tmx", "Chest_Small", new Vector3(284f, 0f, 496f)), new ItemData(ItemType.GoldCoin) }, //accès monsterton
                { new LocationId("darklands-house6.tmx", "Chest_Small", new Vector3(264f, 0f, 428f)), new ItemData(ItemType.GoldCoin) }, //accès monsterton
                { new LocationId("darklands-house7.tmx", "Chest_Small", new Vector3(320f, 0f, 380f)), new ItemData(ItemType.GoldCoin) }, //accès monsterton
                { new LocationId("forestMaze-end.tmx", "Chest_Small", new Vector3(560f, 0f, 308f)), new ItemData(ItemType.HeartQ_1) }, //accès nord
                { new LocationId("forestMaze-end.tmx", "Chest_Small", new Vector3(724f, 0f, 308f)), new ItemData(ItemType.GoldCoin) }, //accès nord (big coins ID30)
                { new LocationId("jungles-21x22.tmx", "Chest_Small", new Vector3(852f, 0f, 684f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && grappin
                { new LocationId("jungles-21x22.tmx", "hunter", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.Bow) }, //accès jungle ile && grappin && arc
                { new LocationId("jungles-21x22.tmx", "Chest_Small", new Vector3(1336f, 0f, 896f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 496f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(332f, 0f, 400f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 304f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(672f, 0f, 304f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(764f, 0f, 404f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(672f, 0f, 496f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && bombes
                { new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(552f, 0f, 688f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && flippers && pelle
                { new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(524f, 0f, 808f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && flippers && pelle
                { new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(600f, 0f, 888f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && flippers && pelle
                { new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(804f, 0f, 892f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && flippers && pelle
                { new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(924f, 0f, 788f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && flippers && pelle
                { new LocationId("jungles-21x22-island.tmx", "Chest_Small", new Vector3(864f, 0f, 684f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && flippers && pelle
                { new LocationId("jungles-22x20.tmx", "Chest_Small", new Vector3(1764f, 0f, 1896f)), new ItemData(ItemType.Honeycomb) }, //accès jungle ile && flippers || accès jungle && bombes
                { new LocationId("jungles-22x21-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 360f)), new ItemData(ItemType.HeartQ_1) }, //accès jungle ile && flippers
                { new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(480f, 0f, 224f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && ouvrir portes notes
                { new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(608f, 0f, 224f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && ouvrir portes notes
                { new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(480f, 0f, 352f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && ouvrir portes notes
                { new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(608f, 0f, 352f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile && ouvrir portes notes
                { new LocationId("jungles-22x22.tmx", "Chest_Small", new Vector3(1440f, 0f, 2016f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile
                { new LocationId("jungles-22x22.tmx", "Chest_Small", new Vector3(1668f, 0f, 512f)), new ItemData(ItemType.GoldCoin) }, //accès jungle
                { new LocationId("jungles-22x22-lighthouse.tmx", "Chest_Small", new Vector3(556f, 0f, 144f)), new ItemData(ItemType.GoldCoin) }, //accès jungle ile
                { new LocationId("jungles-23x19-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 896f)), new ItemData(ItemType.Crystal) }, //accès dark && ouvrir portes notes && leviers
                { new LocationId("jungles-23x19.tmx", "archJungle", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.Shovel) }, //accès jungle
                { new LocationId("jungles-23x20.tmx", "Chest_Small", new Vector3(2264f, 0f, 1928f)), new ItemData(ItemType.GoldCoin) }, //accès jungle
                { new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(448f, 0f, 492f)), new ItemData(ItemType.GoldCoin) }, //accès jungle && bombes
                { new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 368f)), new ItemData(ItemType.GoldCoin) }, //accès jungle && bombes
                { new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 288f)), new ItemData(ItemType.GoldCoin) }, //accès jungle && bombes
                { new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(736f, 0f, 368f)), new ItemData(ItemType.GoldCoin) }, //accès jungle && bombes
                { new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(640f, 0f, 492f)), new ItemData(ItemType.GoldCoin) }, //accès jungle && bombes
                { new LocationId("jungles-23x21.tmx", "fisherman", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.FishingRod) }, //accès jungle
                { new LocationId("jungles-23x22.tmx", "Chest_Small", new Vector3(740f, 0f, 1252f)), new ItemData(ItemType.GoldCoin) }, //accès jungle
                { new LocationId("jungles-24x19.tmx", "Chest_Small", new Vector3(1548f, 0f, 1272f)), new ItemData(ItemType.Honeycomb) }, //accès jungle && (flippers || bombes)
                { new LocationId("jungles-24x19.tmx", "Chest_Small", new Vector3(2468f, 0f, 2308f)), new ItemData(ItemType.GoldCoin) }, //accès jungle NE
                { new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 272f)), new ItemData(ItemType.GoldCoin) }, //accès dark && bombes
                { new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 272f)), new ItemData(ItemType.GoldCoin) }, //accès dark && bombes
                { new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 416f)), new ItemData(ItemType.GoldCoin) }, //accès dark && bombes
                { new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 416f)), new ItemData(ItemType.GoldCoin) }, //accès dark && bombes
                { new LocationId("jungles-24x20.tmx", "Chest_Small", new Vector3(2088f, 0f, 2200f)), new ItemData(ItemType.GoldCoin) }, //accès jungle && bombes
                { new LocationId("jungles-24x22.tmx", "Chest_Small", new Vector3(2336f, 0f, 0496f)), new ItemData(ItemType.GoldCoin) }, //accès jungle
                { new LocationId("jungles-25x19.tmx", "Chest_Small", new Vector3(276f, 0f, 1876f)), new ItemData(ItemType.GoldCoin) }, //accès jungle NE && bombes
                { new LocationId("jungles-25x19-combat.tmx", "Chest_Small", new Vector3(2176f, 0f, 1792f)), new ItemData(ItemType.CombatScroll) }, //accès jungle NE && lanterne && arc
                { new LocationId("jungles-25x20-cave.tmx", "Chest_Small", new Vector3(448f, 0f, 200f)), new ItemData(ItemType.HeartQ_1) }, //accès jungle NE && lanterne
                { new LocationId("jungles-25x21.tmx", "Chest_Small", new Vector3(232f, 0f, 272f)), new ItemData(ItemType.GoldCoin) }, //accès jungle && bombes
                //{ new LocationId("jungles-25x21-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("jungles-25x21-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("jungles-25x21-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("jungles-25x21-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                { new LocationId("jungles-25x22.tmx", "Chest_Small", new Vector3(160f, 0f, 836f)), new ItemData(ItemType.GoldCoin) }, //accès jungle
                { new LocationId("jungles-25x22.tmx", "Chest_Small", new Vector3(1760f, 0f, 728f)), new ItemData(ItemType.GoldCoin) }, //accès jungle && bombes
                { new LocationId("jungles-25x22-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 488f)), new ItemData(ItemType.GoldCoin) }, //accès jungle && bombes
                { new LocationId("jungles-25x22-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 488f)), new ItemData(ItemType.GoldCoin) }, //accès jungle && bombes
                { new LocationId("jungles-firstPrimate.tmx", "Chest", new Vector3(480f, 0f, 256f)), new ItemData(ItemType.Bombs) }, //accès jungle
                //{ new LocationId("lighthouse.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown (probablement renommée en jungles-22x22)
                { new LocationId("mansion-4.tmx", "Chest_Small", new Vector3(640f, 0f, 1280f)), new ItemData(ItemType.Gold_Key) }, //accès mansion && damage
                { new LocationId("mansion-12.tmx", "Chest_Small", new Vector3(1920f, 0f, 892f)), new ItemData(ItemType.Gold_Key) }, //accès mansion 2 && grappin
                { new LocationId("mansion-12-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 384f)), new ItemData(ItemType.HeartQ_1) }, //accès mansion 2 && grappin && bombes
                //{ new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.HeartQ_1) }, //unknown
                //{ new LocationId("mansion-20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                { new LocationId("mansion-bossVampire.tmx", "Chest", new Vector3(704f, 0f, 448f)), new ItemData(ItemType.RexTeleporter) }, //accès mansion 2 && clé && damage
                { new LocationId("morkla-3.tmx", "Chest_Small", new Vector3(704f, 0f, 1732f)), new ItemData(ItemType.GoldCoin) }, //accès Morkla && water switch && (lanterne && damage || flippers)
                { new LocationId("morkla-4.tmx", "Chest_Small", new Vector3(1216f, 0f, 384f)), new ItemData(ItemType.GoldCoin) }, //accès Morkla && water switch && flippers && leviers
                { new LocationId("morkla-4.tmx", "Chest_Small", new Vector3(448f, 0f, 768f)), new ItemData(ItemType.GoldCoin) }, //accès Morkla && water switch && flippers && leviers
                { new LocationId("morkla-8.tmx", "Chest_Small", new Vector3(736f, 0f, 556f)), new ItemData(ItemType.Gold_Key) }, //accès Morkla && lanterne && water switch && (damage || flippers)
                { new LocationId("morkla-17.tmx", "Chest_Small", new Vector3(1732f, 0f, 384f)), new ItemData(ItemType.BlueGem) }, //accès Morkla && water switch && flippers && leviers
                { new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(736f, 0f, 288f)), new ItemData(ItemType.GoldCoin) }, //accès Morkla && flippers && (water switch && leviers || lanterne)
                { new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(928f,0f, 288f)), new ItemData(ItemType.GoldCoin) }, //accès Morkla && flippers && (water switch && leviers || lanterne)
                { new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(928f, 0f, 608f)), new ItemData(ItemType.GoldCoin) }, //accès Morkla && flippers && (water switch && leviers || lanterne)
                { new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(736f, 0f, 608f)), new ItemData(ItemType.GoldCoin) }, //accès Morkla && flippers && (water switch && leviers || lanterne)
                { new LocationId("morkla-19.tmx", "Chest_Small", new Vector3(640f, 0f, 312f)), new ItemData(ItemType.HeartQ_1) }, //accès Morkla && water switch && flippers && bombes
                { new LocationId("morkla-20.tmx", "Chest_Small", new Vector3(640f, 0f, 384f)), new ItemData(ItemType.HeartQ_1) }, //accès Morkla && bombes && (lanterne || flippers)
                { new LocationId("morkla-21.tmx", "Chest_Small", new Vector3(896f, 0f, 776f)), new ItemData(ItemType.GreenGem) }, //accès Morkla && water switch && flippers && leviers
                { new LocationId("morkla-octopus.tmx", "BossOctopus", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.HeartQ_4) }, //accès Morkla && damage && flippers && water switch && blue gem && green gem
                { new LocationId("morkla-octopus.tmx", "Chest", new Vector3(896f, 0f, 624f)), new ItemData(ItemType.KeyPiece1) }, //accès Morkla && damage && flippers && water switch && blue gem && green gem
                { new LocationId("morkla-pirateBoss.tmx", "Chest", new Vector3(768f, 0f, 640f)), new ItemData(ItemType.Flippers) }, //accès Morkla && damage && (lanterne && water switch && clé || flippers)
                //{ new LocationId("objectPalette.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //pas sur si vraie location, semble etre tool debug
                { new LocationId("orchid-tomb-3.tmx", "Chest_Small", new Vector3(416f, 0f, 320f)), new ItemData(ItemType.Gold_Key) }, //lampe && levier && damage
                { new LocationId("orchid-tomb-4.tmx", "orchid_heart", new Vector3(604f, 0f, 304f)), new ItemData(ItemType.HeartQ_4) }, //lampe && damage && clé
                { new LocationId("orchid-tomb-4.tmx", "orchid_sword", new Vector3(604f, 0f, 304f)), new ItemData(ItemType.Sword) }, //lampe && damage && clé
                { new LocationId("orchid-tomb-4.tmx", "orchid_shield", new Vector3(604f, 0f, 304f)), new ItemData(ItemType.Shield) }, //lampe && damage && clé
                //{ new LocationId("overworld-15x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-15x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-15x18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                { new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 368f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && bombes
                { new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(480f, 0f, 492f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && bombes
                { new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(576f, 0f, 616f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && bombes
                { new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(768f, 0f, 616f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && bombes
                { new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(864f, 0f, 488f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && bombes
                { new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(800f, 0f, 364f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && bombes
                { new LocationId("overworld-15x20.tmx", "archCanyon", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.Crystal) }, //accès canyon && CanyonBone == 20
                { new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 412f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && bombes
                { new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 408f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && bombes
                { new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 604f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && bombes
                { new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 604f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && bombes
                { new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1216f, 0f, 448f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && ouvrir portes note && levier
                { new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1408f, 0f, 448f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && ouvrir portes note && levier
                { new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(1312f, 0f, 312f)), new ItemData(ItemType.HeartQ_1) }, //accès canyon && ouvrir portes note && levier
                //{ new LocationId("overworld-16x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-16x18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                { new LocationId("overworld-16x21-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 228f)), new ItemData(ItemType.HeartQ_1) }, //accès canyon && levier
                { new LocationId("overworld-16x22.tmx", "beggar", new Vector3(980f, 0f, 1616f)), new ItemData(ItemType.HeartQ_1) }, //accès canyon
                //{ new LocationId("overworld-17x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-17x18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                { new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(1056f, 0f, 2008f)), new ItemData(ItemType.GoldCoin) }, //accès canyon steppe
                { new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(1184f, 0f, 2008f)), new ItemData(ItemType.GoldCoin) }, //accès canyon steppe
                { new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(476f, 0f, 208f)), new ItemData(ItemType.Honeycomb) }, //accès canyon plateau
                { new LocationId("overworld-17x20-cave.tmx", "Chest_Small", new Vector3(736f, 0f, 224f)), new ItemData(ItemType.HeartQ_1) }, //accès canyon plateau && grappin
                { new LocationId("overworld-17x21-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 480f)), new ItemData(ItemType.HeartQ_1) }, //accès canyon steppe && leviers
                //{ new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                { new LocationId("overworld-18x18.tmx", "Chest_Small", new Vector3(596f, 0f, 2064f)), new ItemData(ItemType.GoldCoin) }, //accès ouest && bombes
                { new LocationId("overworld-18x19.tmx", "Chest_Small", new Vector3(792f, 0f, 1348f)), new ItemData(ItemType.Honeycomb) }, //accès canyon plateau && grappin
                { new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(512f, 0f, 308f)), new ItemData(ItemType.GoldCoin) }, //accès ouest && bombes
                { new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(704f, 0f, 308f)), new ItemData(ItemType.GoldCoin) }, //accès ouest && bombes
                { new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(512f, 0f, 544f)), new ItemData(ItemType.GoldCoin) }, //accès ouest && bombes
                { new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(704f, 0f, 544f)), new ItemData(ItemType.GoldCoin) }, //accès ouest && bombes
                { new LocationId("overworld-18x21.tmx", "Chest_Small", new Vector3(224f, 0f, 592f)), new ItemData(ItemType.GoldCoin) }, //accès canyon steppe
                { new LocationId("overworld-18x21.tmx", "Chest_Small", new Vector3(784f, 0f, 156f)), new ItemData(ItemType.GoldCoin) }, //accès canyon steppe
                { new LocationId("overworld-18x22.tmx", "Chest_Small", new Vector3(448f, 0f, 448f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && flippers && bombes
                { new LocationId("overworld-18x22.tmx", "Chest_Small", new Vector3(640f, 0f, 448f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && flippers && bombes
                { new LocationId("overworld-18x22.tmx", "Chest_Small", new Vector3(544f, 0f, 344f)), new ItemData(ItemType.GoldCoin) }, //accès canyon && flippers && bombes
                { new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(384f, 0f, 208f)), new ItemData(ItemType.GoldCoin) }, //accès nord && leviers (&& bombes?)
                { new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(512f, 0f, 208f)), new ItemData(ItemType.GoldCoin) }, //accès nord && leviers (&& bombes?)
                { new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(640f, 0f, 208f)), new ItemData(ItemType.GoldCoin) }, //accès nord && leviers (&& bombes?)
                { new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(384f, 0f, 336f)), new ItemData(ItemType.GoldCoin) }, //accès nord && leviers (&& bombes?)
                { new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(512f, 0f, 336f)), new ItemData(ItemType.GoldCoin) }, //accès nord && leviers (&& bombes?)
                { new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(640f, 0f, 336f)), new ItemData(ItemType.GoldCoin) }, //accès nord && leviers (&& bombes?)
                { new LocationId("overworld-19x17.tmx", "Chest_Small", new Vector3(2168f, 0f, 1600f)), new ItemData(ItemType.GoldCoin) }, //accès nord && bombes
                { new LocationId("overworld-19x18.tmx", "Chest_Small", new Vector3(184f, 0f, 296f)), new ItemData(ItemType.Honeycomb) }, //accès ouest
                //{ new LocationId("overworld-19x19.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.HeartQ_1) }, //pelle id=2
                { new LocationId("overworld-19x19-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 224f)), new ItemData(ItemType.HeartQ_1) }, //accès ouest && arc
                //{ new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                { new LocationId("overworld-19x21.tmx", "Chest_Small", new Vector3(2348f, 0f, 1832f)), new ItemData(ItemType.GoldCoin) }, //accès canyon
                { new LocationId("overworld-19x22.tmx", "bard", new Vector3(852f, 0f, 1348f)), new ItemData(ItemType.Guitar) }, //accès canyon && damage
                { new LocationId("overworld-19x22.tmx", "bard_song", new Vector3(852f, 0f, 1348f)), new ItemData(ItemType.OpenSesame) }, //accès canyon && damage
                { new LocationId("overworld-19x22-bardCave.tmx", "Chest_Small", new Vector3(416f, 0f, 240f)), new ItemData(ItemType.HeartQ_1) }, //accès canyon && damage (&& ouvrir portes note)
                { new LocationId("overworld-20x16-combat.tmx", "Chest_Small", new Vector3(416f, 0f, 2084f)), new ItemData(ItemType.CombatScroll) }, //accès nord && leviers
                { new LocationId("overworld-20x17.tmx", "Chest_Small", new Vector3(376f, 0f, 1480f)), new ItemData(ItemType.GoldCoin) }, //accès nord && bombes
                { new LocationId("overworld-20x17.tmx", "Chest_Small", new Vector3(2348f, 0f, 168f)), new ItemData(ItemType.Honeycomb) }, //accès nord
                { new LocationId("overworld-20x17-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 232f)), new ItemData(ItemType.HeartQ_1) }, //accès nord
                { new LocationId("overworld-20x18.tmx", "lanternGuy", new Vector3(1036f, 0f, 660f)), new ItemData(ItemType.Torch) },
                { new LocationId("overworld-20x18-cave1.tmx", "Chest_Small", new Vector3(1184f, 0f, 2208f)), new ItemData(ItemType.HeartQ_1) }, //épée 2 && lanterne
                { new LocationId("overworld-20x20.tmx", "Chest_Small", new Vector3(2352f, 0f, 2212f)), new ItemData(ItemType.GoldCoin) }, //bombes
                { new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(296f, 0f, 224f)), new ItemData(ItemType.GoldCoin) }, //accès est && bombes
                { new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(424f, 0f, 224f)), new ItemData(ItemType.GoldCoin) }, //accès est && bombes
                { new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(424f, 0f, 320f)), new ItemData(ItemType.GoldCoin) }, //accès est && bombes
                { new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(296f, 0f, 320f)), new ItemData(ItemType.GoldCoin) }, //accès est && bombes
                //{ new LocationId("overworld-20x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.HeartQ_1) }, //unknown (sirène)
                { new LocationId("overworld-20x22-combat.tmx", "Chest_Small", new Vector3(2176f, 0f, 1828f)), new ItemData(ItemType.CombatScroll) }, //accès canyon && leviers (&& damage)
                //{ new LocationId("overworld-21x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown (renommé en jungles 22-20)
                { new LocationId("overworld-21x17.tmx", "Chest_Small", new Vector3(1468f, 0f, 972f)), new ItemData(ItemType.GoldCoin) }, //accès nord && bombes
                { new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(384f, 0f, 320f)), new ItemData(ItemType.GoldCoin) }, //accès nord && bombes
                { new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(576f, 0f, 320f)), new ItemData(ItemType.GoldCoin) }, //accès nord && bombes
                { new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(288f, 0f, 512f)), new ItemData(ItemType.GoldCoin) }, //accès nord && bombes
                { new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(480f, 0f, 512f)), new ItemData(ItemType.GoldCoin) }, //accès nord && bombes
                { new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(672f, 0f, 512f)), new ItemData(ItemType.GoldCoin) }, //accès nord && bombes
                { new LocationId("overworld-21x17-wizHouse.tmx", "Chest_Small", new Vector3(956f, 0f, 128f)), new ItemData(ItemType.GoldCoin) }, //accès nord
                { new LocationId("overworld-21x18.tmx", "Chest_Small", new Vector3(1832f, 0f, 1824f)), new ItemData(ItemType.Honeycomb) }, //accès nord
                { new LocationId("overworld-21x18-cave.tmx", "Chest_Small", new Vector3(480f, 0f, 324f)), new ItemData(ItemType.GoldCoin) }, //accès nord && bombes
                { new LocationId("overworld-21x18-cave.tmx", "Chest_Small", new Vector3(384f, 0f, 480f)), new ItemData(ItemType.GoldCoin) }, //accès nord && bombes
                { new LocationId("overworld-21x18-cave.tmx", "Chest_Small", new Vector3(576f, 0f, 480f)), new ItemData(ItemType.GoldCoin) }, //accès nord && bombes
                //{ new LocationId("overworld-21x19.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.HeartQ_1) }, //pelle id=2
                { new LocationId("overworld-21x20-cave.tmx", "Chest_Small", new Vector3(1192f, 0f, 612f)), new ItemData(ItemType.GoldCoin) },
                { new LocationId("overworld-21x20-cave.tmx", "Chest_Small", new Vector3(1268f, 0f, 612f)), new ItemData(ItemType.GoldCoin) },
                //{ new LocationId("overworld-21x20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.HeartQ_1) }, //pelle id=6
                { new LocationId("overworld-22x16.tmx", "Chest_Small", new Vector3(824f, 0f, 2452f)), new ItemData(ItemType.GoldCoin) }, //accès nord && pelle
                { new LocationId("overworld-22x16-cave.tmx", "Chest_Small", new Vector3(1408f, 0f, 1536f)), new ItemData(ItemType.GoldCoin) }, //accès nord && flippers && grappin (&& arc?)
                { new LocationId("overworld-22x16-cave.tmx", "Chest_Small", new Vector3(1600f, 0f, 1536f)), new ItemData(ItemType.GoldCoin) }, //accès nord && flippers && grappin (&& arc?)
                { new LocationId("overworld-22x16-cave.tmx", "Chest_Small", new Vector3(1408f, 0f, 1664f)), new ItemData(ItemType.GoldCoin) }, //accès nord && flippers && grappin (&& arc?)
                { new LocationId("overworld-22x16-cave.tmx", "Chest_Small", new Vector3(1600f, 0f, 1664f)), new ItemData(ItemType.GoldCoin) }, //accès nord && flippers && grappin (&& arc?)
                { new LocationId("overworld-22x17.tmx", "Chest_Small", new Vector3(684f, 0f, 2104f)), new ItemData(ItemType.GoldCoin) }, //accès nord && bombes
                { new LocationId("overworld-22x19.tmx", "Chest_Small", new Vector3(2240f, 0f, 1752f)), new ItemData(ItemType.GoldCoin) }, //accès jungle
                { new LocationId("overworld-22x19-cave.tmx", "Chest_Small", new Vector3(992f, 0f, 1648f)), new ItemData(ItemType.HeartQ_1) }, //accès est && ouvrir portes notes
                //{ new LocationId("overworld-22x20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.Honeycomb) }, //unknown
                { new LocationId("overworld-22x20.tmx", "Chest_Small", new Vector3(264f, 0f, 472f)), new ItemData(ItemType.GoldCoin) }, //accès est
                { new LocationId("overworld-23x16.tmx", "Chest_Small", new Vector3(2272f, 0f, 1364f)), new ItemData(ItemType.Honeycomb) }, //accès dark
                { new LocationId("overworld-23x16-cave.tmx", "Chest_Small", new Vector3(736f, 0f, 224f)), new ItemData(ItemType.HeartQ_1) }, //accès dark && arc
                { new LocationId("overworld-23x17.tmx", "Chest_Small", new Vector3(2176f, 0f, 704f)), new ItemData(ItemType.GoldCoin) }, //accès monsterton
                { new LocationId("overworld-23x17-farm.tmx", "Chest_Small", new Vector3(752f, 0f, 140f)), new ItemData(ItemType.GoldCoin) }, //accès dark
                { new LocationId("overworld-23x17-farm.tmx", "farmer", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.HeartQ_1) }, //accès dark && damage
                { new LocationId("overworld-23x17-noteCave.tmx", "Chest_Small", new Vector3(576f, 0f, 260f)), new ItemData(ItemType.GoldCoin) }, //accès dark && ouvrir portes notes && (bombes && épée)
                { new LocationId("overworld-23x17-noteCave.tmx", "Chest_Small", new Vector3(768f, 0f, 260f)), new ItemData(ItemType.Five_Gems) }, //accès dark && ouvrir portes notes && (bombes && épée)
                { new LocationId("overworld-23x18.tmx", "Chest_Small", new Vector3(932f, 0f, 752f)), new ItemData(ItemType.HeartQ_1) }, //accès dark && leviers
                { new LocationId("overworld-24x16-mausoleum.tmx", "Chest_Small", new Vector3(736f, 0f, 200f)), new ItemData(ItemType.HeartQ_1) }, //accès monsterton
                //{ new LocationId("overworld-24x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.Crystal) }, //pelle id=3
                { new LocationId("overworld-24x18.tmx", "campCups", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.Crystal) }, //accès dark
                { new LocationId("overworld-24x18-blueTent.tmx", "Chest_Small", new Vector3(776f, 0f, 160f)), new ItemData(ItemType.GoldCoin) }, //accès dark
                { new LocationId("overworld-24x18-greenTent.tmx", "Chest_Small", new Vector3(576f, 0f, 148f)), new ItemData(ItemType.GoldCoin) }, //accès dark
                { new LocationId("overworld-24x18-greenTent.tmx", "Chest_Small", new Vector3(680f, 0f, 148f)), new ItemData(ItemType.GoldCoin) }, //accès dark
                { new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(416f, 0f, 304f)), new ItemData(ItemType.GoldCoin) }, //accès mansion && bombes
                { new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 304f)), new ItemData(ItemType.GoldCoin) }, //accès mansion && bombes
                { new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(352f, 0f, 432f)), new ItemData(ItemType.GoldCoin) }, //accès mansion && bombes
                { new LocationId("overworld-25x16-cave.tmx", "Chest_Small", new Vector3(608f, 0f, 432f)), new ItemData(ItemType.GoldCoin) }, //accès mansion && bombes
                { new LocationId("overworld-25x17-cave.tmx", "Chest_Small", new Vector3(320f, 0f, 352f)), new ItemData(ItemType.GoldCoin) }, //accès monsterton
                { new LocationId("overworld-25x17-cave.tmx", "Chest_Small", new Vector3(544f, 0f, 352f)), new ItemData(ItemType.GoldCoin) }, //accès monsterton
                { new LocationId("overworld-25x17-cave.tmx", "Chest_Small", new Vector3(772f, 0f, 352f)), new ItemData(ItemType.GoldCoin) }, //accès monsterton
                { new LocationId("overworld-25x17-combat.tmx", "Chest_Small", new Vector3(1280f, 0f, 516f)), new ItemData(ItemType.CombatScroll) }, //accès monsterton && (bombes || teleporter)
                //{ new LocationId("overworld-25x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-25x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.GoldCoin) }, //unknown
                //{ new LocationId("overworld-25x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.Honeycomb) }, //unknown
                { new LocationId("owlMap.tmx", "owl", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.Boomerang) }, //accès dark && instrument && chanson wakeup
                //{ new LocationId("sandCastle.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.Five_Gems) }, //unknown
                { new LocationId("temple-1.tmx", "Chest_Small", new Vector3(1376f, 0f, 1184f)), new ItemData(ItemType.Gold_Key) }, //accès temple && (arc && leviers || grappin)
                { new LocationId("temple-4.tmx", "Chest_Small", new Vector3(3232f, 0f, 1392f)), new ItemData(ItemType.GoldCoin) }, //accès temple 2 && leviers
                { new LocationId("temple-5.tmx", "Chest_Small", new Vector3(624f, 0f, 1728f)), new ItemData(ItemType.Gold_Key) }, //accès temple 2 && (leviers || grappin)
                { new LocationId("temple-5-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 292f)), new ItemData(ItemType.Crystal) }, //accès temple 2 && leviers && bombes
                { new LocationId("temple-6.tmx", "Chest_Small", new Vector3(388f, 0f, 756f)), new ItemData(ItemType.GoldCoin) }, //accès temple 2 && leviers && bombes
                { new LocationId("temple-8.tmx", "Chest_Small", new Vector3(896f, 0f, 1792f)), new ItemData(ItemType.GoldCoin) }, //accès temple 3 && grappin
                { new LocationId("temple-8.tmx", "Chest_Small", new Vector3(1728f, 0f, 1792f)), new ItemData(ItemType.GoldCoin) }, //accès temple 3 && grappin
                { new LocationId("temple-11.tmx", "Chest_Small", new Vector3(440f, 0f, 452f)), new ItemData(ItemType.GoldCoin) }, //accès temple 3 && leviers
                { new LocationId("temple-11.tmx", "Chest_Small", new Vector3(448f, 0f, 1920f)), new ItemData(ItemType.Gold_Key) }, //accès temple 3 && leviers && bombes
                { new LocationId("temple-15-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.HeartQ_1) }, //accès temple 4 && bombes
                { new LocationId("temple-17.tmx", "Chest_Small", new Vector3(276f, 0f, 220f)), new ItemData(ItemType.GoldCoin) }, //accès temple 4
                { new LocationId("temple-18.tmx", "Chest_Small", new Vector3(1152f, 0f, 1724f)), new ItemData(ItemType.GoldCoin) }, //accès temple 4
                { new LocationId("temple-18-secret.tmx", "Chest_Small", new Vector3(640f, 0f, 284f)), new ItemData(ItemType.HeartQ_1) }, //accès temple 4 && bombes
                { new LocationId("temple-genieBoss.tmx", "BossGenie", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.HeartQ_4) }, //accès temple 4 && lanterne
                { new LocationId("temple-genieBoss.tmx", "Chest", new Vector3(832f, 0f, 640f)), new ItemData(ItemType.KeyPiece2) }, //accès temple 4 && lanterne
                { new LocationId("temple-vultureBoss.tmx", "Chest", new Vector3(768f, 0f, 640f)), new ItemData(ItemType.GrappleHook) }, //accès temple 3 && (clé || grappin) && damage
                { new LocationId("ufo.tmx", "aliens", new Vector3(0f, 0f, 0f)), new ItemData(ItemType.Crystal) }, //accès dark

                //spots a os dans le canyon pour chaque il faut un accès canyon
                { new LocationId("overworld-15x19.tmx", "PickUpItem", new Vector3(1804f, 0f, 2292f)), new ItemData(ItemType.CanyonBone) }, //accès canyon
                { new LocationId("overworld-15x19.tmx", "PickUpItem", new Vector3(820f, 0f, 952f)), new ItemData(ItemType.CanyonBone) }, //accès canyon
                { new LocationId("overworld-15x20.tmx", "PickUpItem", new Vector3(192f, 0f, 416f)), new ItemData(ItemType.CanyonBone) }, //accès canyon
                { new LocationId("overworld-15x21.tmx", "PickUpItem", new Vector3(288f, 0f, 1952f)), new ItemData(ItemType.CanyonBone) }, //accès canyon
                { new LocationId("overworld-15x22.tmx", "PickUpItem", new Vector3(1568f, 0f, 2400f)), new ItemData(ItemType.CanyonBone) }, //accès canyon
                { new LocationId("overworld-16x19.tmx", "PickUpItem", new Vector3(992f, 0f, 1636f)), new ItemData(ItemType.CanyonBone) }, //accès canyon
                { new LocationId("overworld-16x19.tmx", "PickUpItem", new Vector3(1616f, 0f, 988f)), new ItemData(ItemType.CanyonBone) }, //accès canyon plateau
                { new LocationId("overworld-16x21.tmx", "PickUpItem", new Vector3(288f, 0f, 296f)), new ItemData(ItemType.CanyonBone) }, //accès canyon
                { new LocationId("overworld-17x19.tmx", "PickUpItem", new Vector3(796f, 0f, 1076f)), new ItemData(ItemType.CanyonBone) }, //accès canyon plateau
                { new LocationId("overworld-17x19.tmx", "PickUpItem", new Vector3(844f, 0f, 2180f)), new ItemData(ItemType.CanyonBone) }, //accès canyon plateau
                { new LocationId("overworld-17x21.tmx", "PickUpItem", new Vector3(412f, 0f, 2164f)), new ItemData(ItemType.CanyonBone) }, //accès canyon
                { new LocationId("overworld-17x22.tmx", "PickUpItem", new Vector3(1952f, 0f, 864f)), new ItemData(ItemType.CanyonBone) }, //accès canyon
                { new LocationId("overworld-18x19.tmx", "PickUpItem", new Vector3(256f, 0f, 1168f)), new ItemData(ItemType.CanyonBone) }, //accès canyon plateau
                { new LocationId("overworld-18x20.tmx", "PickUpItem", new Vector3(304f, 0f, 520f)), new ItemData(ItemType.CanyonBone) }, //accès canyon plateau
                { new LocationId("overworld-18x21.tmx", "PickUpItem", new Vector3(160f, 0f, 736f)), new ItemData(ItemType.CanyonBone) }, //accès canyon steppe
                { new LocationId("overworld-18x22.tmx", "PickUpItem", new Vector3(1864f, 0f, 1940f)), new ItemData(ItemType.CanyonBone) }, //accès canyon
                { new LocationId("overworld-19x21.tmx", "PickUpItem", new Vector3(536f, 0f, 2072f)), new ItemData(ItemType.CanyonBone) }, //accès canyon
                { new LocationId("overworld-19x22.tmx", "PickUpItem", new Vector3(1568f, 0f, 928f)), new ItemData(ItemType.CanyonBone) }, //accès canyon
                { new LocationId("overworld-20x21.tmx", "PickUpItem", new Vector3(800f, 0f, 2208)), new ItemData(ItemType.CanyonBone) }, //accès canyon
                { new LocationId("overworld-20x22.tmx", "PickUpItem", new Vector3(688f, 0f, 916f)), new ItemData(ItemType.CanyonBone) }, //accès canyon

                //les shops
                { new LocationId("blossom-shop.tmx", "emptyJar", new Vector3(60f, 0f, 284f)), new ItemData(ItemType.Accordian) },
                { new LocationId("blossom-shop.tmx", "heart", new Vector3(52f, 0f, 284f)), new ItemData(ItemType.BeeMedallion) },
                { new LocationId("blossom-shop.tmx", "crystal", new Vector3(44f, 0f, 284f)), new ItemData(ItemType.Boomerang) }
            };
        }
    }
}
