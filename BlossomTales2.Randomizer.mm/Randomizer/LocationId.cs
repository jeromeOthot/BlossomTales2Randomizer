using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

#nullable disable
namespace BlossomTales2.Randomizer.mm
{
    public struct LocationId
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

        public override readonly bool Equals(object obj)
        {
            if (obj is not LocationId other)
                return false;

            return other.MapName == MapName && other.Name == Name && other.Position == Position;
        }

        public override readonly int GetHashCode()
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
    }
}
