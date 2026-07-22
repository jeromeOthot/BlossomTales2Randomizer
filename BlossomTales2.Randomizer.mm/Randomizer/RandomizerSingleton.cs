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

        //Use PermaObjects to abstract the game objectives and make them non-linear.
        public static void MarkObjectiveComplete(Globaler.MainGameObjective objective)
        {
            if (IsObjectiveCompleted(objective))
                return;

            Game1.Perma_Objects.Add(new PermaListItem(string.Empty, objective.ToString(), Vector3.Zero));
        }

        public static bool IsObjectiveCompleted(Globaler.MainGameObjective objective)
        {
            string objectiveName = objective.ToString();
            return Game1.Perma_Objects.FirstOrDefault(obj => obj.Name == objectiveName) != null;
        }

        private void InitializeLocations()
        {
            _locations = new Dictionary<LocationId, EquipableItem.ItemList>
            {
                //{ new LocationId("anchor-house4.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                { new LocationId("blossom-house1.tmx", "Chest_Small", new Vector3(672f, 0f, 308f)), EquipableItem.ItemList.GoldCoin }, //Random small
                { new LocationId("blossom-house2.tmx", "Chest_Small", new Vector3(708f, 0f, 356f)), EquipableItem.ItemList.GoldCoin }, //Random small
                { new LocationId("blossom-house3.tmx", "Chest_Small", new Vector3(368f, 0f, 148f)), EquipableItem.ItemList.GoldCoin }, //Random small
                { new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(696f, 0f, 416f)), EquipableItem.ItemList.GoldCoin }, //Random small
                { new LocationId("blossom-house4.tmx", "Chest_Small", new Vector3(780f, 0f, 156f)), EquipableItem.ItemList.GoldCoin }, //Random small
                { new LocationId("blossom-house5.tmx", "Chest_Small", new Vector3(480f, 0f, 172f)), EquipableItem.ItemList.GoldCoin }, //Random small, besoin épée 2 && lampe
                { new LocationId("blossom-lilyHouse.tmx", "npc7_1", new Vector3(480f, 0f, 328f)), EquipableItem.ItemList.WoodShield }, // WoodShield
                { new LocationId("blossom-lilyHouse.tmx", "npc7_2", new Vector3(480f, 0f, 328f)), EquipableItem.ItemList.WoodSword }, // WoodSword
                //Blossom tavern  Chest
                { new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 272f)), EquipableItem.ItemList.GoldCoin }, //Random small
                { new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(256f, 0f, 464f)), EquipableItem.ItemList.GoldCoin },  //Random small
                { new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 272f)), EquipableItem.ItemList.GoldCoin },  //Random small
                { new LocationId("blossom-tavern-basement.tmx", "Chest_Small", new Vector3(448f, 0f, 464f)), EquipableItem.ItemList.HeartQ_1 },  //Heart piece
                //...
                //{ new LocationId("canyon-house1.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("canyon-house3.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("canyon-house3.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("castle-4.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Gold_Key }, //unknown
                //{ new LocationId("castle-6.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("castle-9.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("castle-12.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("darklands-house2-floor2.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("darklands-house6.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("darklands-house7.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("forestMaze-end.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown (big coins ID30)
                //{ new LocationId("forestMaze-end.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("jungles-21x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-21x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-21x22-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-22x21.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-22x21-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-22x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-22x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-22x22-lighthouse.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-23x19.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Crystal }, //unknown
                //{ new LocationId("jungles-23x20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-23x20-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-23x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-24x19.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-24x19.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-24x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-24x20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-24x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-25x19.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-25x19-combat.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.CombatScroll }, //unknown
                //{ new LocationId("jungles-25x20.-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("jungles-25x21.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-25x21.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-25x21.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-25x21.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-25x21.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-25x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-25x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-25x22-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("jungles-25x22-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("lighthouse.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("mansion-12.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Gold_Key }, //unknown
                //{ new LocationId("mansion-12-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("mansion-15-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("mansion-20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("morkla-3.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("morkla-4.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("morkla-4.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("morkla-17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.BlueGem }, //unknown
                //{ new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("morkla-18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("morkla-19.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("morkla-20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("morkla-21.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GreenGem }, //unknown
                //{ new LocationId("objectPalette.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //pas sur si vraie location, semble etre tool debug
                { new LocationId("orchid-tomb-3.tmx", "Chest_Small", new Vector3(416f, 0f, 320f)), EquipableItem.ItemList.Gold_Key }, // Besoin lampe && moyen de hit switch && dmg
                { new LocationId("orchid-tomb-4.tmx", "orchid_heart", new Vector3(604f, 0f, 304f)), EquipableItem.ItemList.HeartQ_4 }, //lampe && dmg
                { new LocationId("orchid-tomb-4.tmx", "orchid_sword", new Vector3(604f, 0f, 304f)), EquipableItem.ItemList.Sword }, //lampe && dmg
                { new LocationId("orchid-tomb-4.tmx", "orchid_shield", new Vector3(604f, 0f, 304f)), EquipableItem.ItemList.Shield }, //lampe && dmg
                //{ new LocationId("overworld-15x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x21-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-15x22-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-16x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-16x18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-16x21-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-17x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-17x20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Honeycomb }, //unknown
                //{ new LocationId("overworld-17x20-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-17x21-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x19.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Honeycomb }, //unknown
                //{ new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x21.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x21.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-18x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x16-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Honeycomb }, //unknown
                //{ new LocationId("overworld-19x19.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x20-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x21.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-19x22-bardCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-20x16-combat.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.CombatScroll }, //unknown
                //{ new LocationId("overworld-20x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-20x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Honeycomb }, //unknown
                //{ new LocationId("overworld-20x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                { new LocationId("overworld-20x18.tmx", "lanternGuy", new Vector3(1036f, 0f, 660f)), EquipableItem.ItemList.Torch },
                //{ new LocationId("overworld-20x18-cave1.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-20x20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //Random small, besoin gant force
                //{ new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-20x21-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-20x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-20x22.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.CombatScroll }, //unknown
                //{ new LocationId("overworld-21x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x17-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x17-wizHouse.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Honeycomb }, //unknown
                //{ new LocationId("overworld-21x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-21x18-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                { new LocationId("overworld-21x20-cave.tmx", "Chest_Small", new Vector3(1192f, 0f, 612f)), EquipableItem.ItemList.GoldCoin }, //Random small
                { new LocationId("overworld-21x20-cave.tmx", "Chest_Small", new Vector3(1268f, 0f, 612f)), EquipableItem.ItemList.GoldCoin }, //Random small
                //{ new LocationId("overworld-22x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-22x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-22x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-22x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-22x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-22x19.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-22x19-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-22x20.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Honeycomb }, //unknown
                { new LocationId("overworld-22x20.tmx", "Chest_Small", new Vector3(264f, 0f, 472f)), EquipableItem.ItemList.GoldCoin }, //besoin épée 2
                //{ new LocationId("overworld-23x16.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Honeycomb }, //unknown
                //{ new LocationId("overworld-23x16-cave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("overworld-23x17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-23x17-farm.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-23x17-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("overworld-23x17-noteCave.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Five_Gems }, //unknown
                //{ new LocationId("overworld-24x16-mausoleum.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
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
                //{ new LocationId("temple-1.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Gold_Key }, //unknown
                //{ new LocationId("temple-4.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("temple-5.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Gold_Key }, //unknown
                //{ new LocationId("temple-5-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Crystal }, //unknown
                //{ new LocationId("temple-6.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("temple-8.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("temple-8.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("temple-11.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("temple-11.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.Gold_Key }, //unknown
                //{ new LocationId("temple-15-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.HeartQ_1 }, //unknown
                //{ new LocationId("temple-17.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("temple-18.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
                //{ new LocationId("temple-18-secret.tmx", "Chest_Small", new Vector3(0f, 0f, 0f)), EquipableItem.ItemList.GoldCoin }, //unknown
            };
        }
    }
}
