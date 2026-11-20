using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomTales2Randomizer.Models
{
    public static class RamdomizerSettings
    {
        public static Gameplay GameplaySetting;

        public class Gameplay
        {
            public static bool SkipCutscenes = true;
            public static bool OpenWorldState = true;
            public static LocationPools LocationPools = LocationPools.AllExceptKeyPieces;
        }
    }
}
