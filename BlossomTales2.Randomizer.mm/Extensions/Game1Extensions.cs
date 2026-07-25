using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlossomTales2.Extensions
{
    internal class Game1Extensions
    {
        public static bool HasLevelPermaObject(string name)
        {
            return Game1.Perma_Objects.FirstOrDefault(predicate) != null;

            bool predicate(PermaListItem item)
            {
                return item.LevelName == Game1.CurrentLevel.Name && item.Name == name;
            }
        }

        public static void AddLevelPermaObject(string name, Vector3 position)
        {
            Game1.Perma_Objects.Add(new PermaListItem(Game1.CurrentLevel.Name, name, position));
        }
    }
}
