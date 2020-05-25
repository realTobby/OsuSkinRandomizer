using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Logic
{
    /// <summary>
    /// used to handle config stuff, dont know yet if i want to keep it
    /// </summary>
    public class SaveHandler
    {
        private Logger myLogger = new Logger();

        private string OsuDirectory = "";
        public bool IsDirectoryAvailable = false;

        public void Save(string dir)
        {
            myLogger.AddLoggerLine("saving config", Severity.Information);
            string txt = "OsuDir=" + OsuDirectory;
            System.IO.File.WriteAllText("config.cfg", txt);
        }

        public void Load()
        {
            myLogger.AddLoggerLine("loading config", Severity.Information);
            if (System.IO.File.Exists("config.cfg"))
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
