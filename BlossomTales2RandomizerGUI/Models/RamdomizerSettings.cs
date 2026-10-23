using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomTales2Randomizer.Models
{
    public static class RamdomizerSettings
    {
        public static ItemLocalisationSetting ItemLocalisation;

        public class ItemLocalisationSetting
        {
            public static bool SkipCutscenes = true;
            public static bool OpenWorldState = true;
            public static LocationPools LocationPools = LocationPools.AllExceptKeyPieces;
        }
    }
}
