using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Logic
{ 
    /// <summary>
    /// check for the osu directory in the registry, if not found the user can manually find/select the osu folder in the ui
    /// </summary>
    public class StartupCheck
    {
        private string osuFolder = "NOTFOUND";
        public string GetDirectoryString()
        {
            return GetOsuDirectory();
        }

        /// Code Credits: https://osu.ppy.sh/users/1404615 THANKS! Found your snippet in the Forum and changed it a bit :)
        public string GetOsuDirectory()
        {
            string keyName1 = @"HKEY_CLASSES_ROOT\osu\shell\open\command";
            string keyName2 = @"HKEY_CLASSES_ROOT\osu!\shell\open\command";
            string path = string.Empty;
            try
            {
                path = Microsoft.Win32.Registry.GetValue(keyName1, string.Empty, string.Empty).ToString();
                if (path == string.Empty)
                    path = Microsoft.Win32.Registry.GetValue(keyName2, string.Empty, string.Empty).ToString();
                if (path != string.Empty)
                {
                    path = path.Remove(0, 1);
                    path = path.Split('\"')[0];
                    path = System.IO.Path.GetDirectoryName(path);
                    return path + @"\Skins";
                }
            }
            catch
            {
                return "NOTFOUND";
            }
            return "NOTFOUND";
        }
    }
}
