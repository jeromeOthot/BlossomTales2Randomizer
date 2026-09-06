using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace BlossomTales2.Randomizer.mm
{
    [TypeConverter(typeof(LocationIdConverter))]
    [Serializable]
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
            return JsonConvert.SerializeObject(this);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LocationId other))
                return false;

            return Equals(other);
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

    public class LocationIdConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string str))
                return base.ConvertFrom(context, culture, value);

            Dictionary<string, string> json =  JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
            Vector3 pos = (Vector3)TypeDescriptor.GetConverter(typeof(Vector3)).ConvertFrom(context, culture, json["Position"]);
            return new LocationId(json["MapName"], json["Name"], pos);
        }
    }
}
