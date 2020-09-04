using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuSkinRandomizer.Models
{
    public class SkinInfo
    {
        public string Path { get; set; }
        public string SkinName { get; set; }
        public string Author { get; set; }

        public SkinInfo()
        {
            Path = "";
            SkinName = "EpicRandomSkin";
            Author = "OsuSkinRandomizer";
        }

        public List<string> AvailableSkinElements = new List<string>();

    }
}
