using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Models
{
    public enum OptionGroup
    {
        General,
        Colours,
        Fonts,
        CatchTheBeat,
        Mania
    }

    public class SkINIOptionModel
    {
        public OptionGroup OptionGroup { get; set; }
        public string OptionKey = "";
        public string OptionValue = "";
        public bool isMania { get; set; } = false;
        public int keySet { get; set; }

        public SkINIOptionModel()
        {

        }

        public SkINIOptionModel(string key, string value, OptionGroup group)
        {
            OptionKey = key;
            OptionValue = value;
            OptionGroup = group;
        }
    }
}
