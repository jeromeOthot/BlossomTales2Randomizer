using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomTales2Randomizer.Models
{
    public static class RamdomizerSettings
    {
#        public static ItemLocalisationSetting ItemLocalisation;

        public class ItemLocalisationSetting
        {
            public static bool SkipCutscenes = true;
            public static bool OpenWorldState = true;
            public static LocationPools LocationPools = LocationPools.AllExceptKeyPieces;

            [NullableBoolChoices("Random", "horse", "pig")]
            public static bool? Mount = null;

            [NullableBoolChoices("Random", "guitar", "accordion")]
            public static bool? Instrument = null;
        }
    }
}
