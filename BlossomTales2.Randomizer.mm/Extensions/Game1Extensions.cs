using BlossomTales2.Randomizer.mm;
using Microsoft.Xna.Framework;
using System.Linq;

namespace BlossomTales2.Extensions
{
    internal class Game1Extensions
    {
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

        public static bool HasLevelPermaObject(string name, bool ignoreLevel = false)
        {
            return Game1.Perma_Objects.FirstOrDefault(predicate) != null;

            bool predicate(PermaListItem item)
            {
                return (ignoreLevel || item.LevelName == Game1.CurrentLevel.Name) && item.Name == name;
            }
        }

        public static void AddLevelPermaObject(string name, Vector3 position)
        {
            Game1.Perma_Objects.Add(new PermaListItem(Game1.CurrentLevel.Name, name, position));
        }
    }
}
