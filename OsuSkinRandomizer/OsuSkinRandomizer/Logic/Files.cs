using OsuSkinRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuSkinRandomizer.Logic
{
    /// <summary>
    /// Takes care of everything file related, gathering, saving and so on...
    /// </summary>
    public class Files
    {
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

        public List<InstalledSkin> GetInstalledSkins()
        {
            List<InstalledSkin> installedSkins = new List<InstalledSkin>();

            var rawOsuSkinFolders = System.IO.Directory.GetDirectories(GetOsuDirectory());
            foreach (string skinPath in rawOsuSkinFolders)
            {
                InstalledSkin addSkin = new InstalledSkin();

                if (System.IO.File.Exists(skinPath + "\\skin.ini"))
                {
                    // check for author
                    var skinINIContent = System.IO.File.ReadAllLines(skinPath + "\\skin.ini");
                    if(skinINIContent.Where(x=>x.Contains("Author")).FirstOrDefault() != null)
                    {
                        addSkin.Author = skinINIContent.Where(x => x.Contains("Author")).FirstOrDefault().Replace("Author:", string.Empty);
                    }
                    else
                    {
                        addSkin.Author = "not defined";
                    }
                }
                else
                {
                    addSkin.Author = "not found";
                }

                addSkin.Path = skinPath;
                addSkin.SkinName = skinPath.Split('\\').Last();
                installedSkins.Add(addSkin);
            }
            return installedSkins;
        }



    }
}
