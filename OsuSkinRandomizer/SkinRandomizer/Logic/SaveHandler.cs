using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Logic
{
    public class SaveHandler
    {
        private string OsuDirectory = "";
        public bool IsDirectoryAvailable = false;

        public void Save(string dir)
        {
            string txt = "OsuDir=" + OsuDirectory;
            System.IO.File.WriteAllText("config.cfg", txt);
        }

        public void Load()
        {
            if(System.IO.File.Exists("config.cfg"))
            {
                string[] configLines = System.IO.File.ReadAllLines("config.cfg");
                OsuDirectory = configLines[0].Replace("OsuDir=", string.Empty);
                if (OsuDirectory == string.Empty)
                {
                    IsDirectoryAvailable = false;
                }
                else
                {
                    IsDirectoryAvailable = true;
                }
            }
        }

    }
}
