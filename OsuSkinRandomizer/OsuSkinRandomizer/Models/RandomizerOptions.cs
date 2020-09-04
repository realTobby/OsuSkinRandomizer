using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuSkinRandomizer.Models
{
    public class RandomizerOptions
    {

        public bool MirrorRandomize { get; set; } = true;
        public bool CorruptionRandomize { get; set; } = false;

        public bool RandomizeInterface { get; set; } = true;
        public bool RandomizeStandard { get; set; } = true;

    }
}
