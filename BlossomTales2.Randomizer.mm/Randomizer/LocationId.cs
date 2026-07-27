using Microsoft.Xna.Framework;
using System;

namespace BlossomTales2.Randomizer.mm
{
    public struct LocationId : IEquatable<LocationId>
    {
        public string MapName { get; private set; }
        public string Name { get; private set; }
        public Vector3 Position { get; private set; }

        public LocationId(string mapName, string name, Vector3 position)
        {
            MapName = mapName;
            Name = name;
            Position = position;
        }

        public override string ToString()
        {
            return "Map: [" + MapName + "] Object: [" + Name + "] Position: " + Position;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + MapName.GetHashCode();
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Position.GetHashCode();
                return hash;
            }
        }

        public bool Equals(LocationId other)
        {
            return other.MapName == MapName && other.Name == Name && other.Position == Position;
        }
    }
}
