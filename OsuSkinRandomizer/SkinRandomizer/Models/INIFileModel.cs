using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Models
{
    public class INIFileModel
    {
        public string originalPath { get; set; }
        public List<SkINIOptionModel> iniContent { get; set; } = new List<SkINIOptionModel>();


        public bool HasOptinKey(SkINIOptionModel item)
        {
            var tmp = iniContent.Where(x => x.OptionKey == item.OptionKey).FirstOrDefault();
            if (tmp == null)
                return false;
            return true;
        }

    }
}
