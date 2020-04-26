using SkinRandomizer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


 
namespace SkinRandomizer.Logic.Generators
{
    /// <summary>
    /// The BaseGenerator is the base class for all generators, it implements the Interface so every generator knows the same methods
    /// </summary>
    public class BaseGenerator : IGenerator
    {
        internal Random rnd = new Random();
        internal string pathToOsuSkinFolder = "";
        internal string skinResultName = "";
        List<string> skinnables = new List<string>();

        /// <summary>
        /// will be used to generate the skin
        /// </summary>
        public virtual void Generate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// is used to give the generator a general idea of the directory
        /// </summary>
        /// <param name="path"></param>
        /// <param name="skinname"></param>
        public virtual void Init(string path, string skinname)
        {
            pathToOsuSkinFolder = path;
            skinResultName = skinname;

            System.IO.Directory.CreateDirectory(path + @"\" + skinname);
        }
    }
}
