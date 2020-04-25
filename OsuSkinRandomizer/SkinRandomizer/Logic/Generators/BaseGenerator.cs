using SkinRandomizer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SkinRandomizer.Logic.Generators
{
    public class BaseGenerator : IGenerator
    {
        internal Random rnd = new Random();
        internal string pathToOsuSkinFolder = "";
        internal string skinResultName = "";
        List<string> skinnables = new List<string>();

        public virtual void Generate()
        {
            throw new NotImplementedException();
        }

        public virtual void Init(string path, string skinname)
        {
            pathToOsuSkinFolder = path;
            skinResultName = skinname;

            System.IO.Directory.CreateDirectory(path + @"\" + skinname);
        }
    }
}
