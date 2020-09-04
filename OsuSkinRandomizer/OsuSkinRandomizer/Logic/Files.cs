using OsuSkinRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace OsuSkinRandomizer.Logic
{
    /// <summary>
    /// Takes care of everything file related, gathering, saving and so on...
    /// </summary>
    public class Files
    {
        public List<string> EVERYSKINNABLE = new List<string>();

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

        public List<SkinInfo> GetInstalledSkins(string osuSkinFolder)
        {
            List<SkinInfo> installedSkins = new List<SkinInfo>();

            string[] rawSkinDirectories = System.IO.Directory.GetDirectories(osuSkinFolder);

            foreach (string skinPath in rawSkinDirectories)
            {
                SkinInfo nextFoundSkin = new SkinInfo();

                if (System.IO.File.Exists(skinPath + "\\skin.ini"))
                {
                    // check for author
                    var skinINIContent = System.IO.File.ReadAllLines(skinPath + "\\skin.ini");
                    if(skinINIContent.Where(x=>x.Contains("Author")).FirstOrDefault() != null)
                    {
                        nextFoundSkin.Author = skinINIContent.Where(x => x.Contains("Author")).FirstOrDefault().Replace("Author:", string.Empty);
                    }
                    else
                    {
                        nextFoundSkin.Author = " NOT DEFINED";
                    }
                }
                else
                {
                    nextFoundSkin.Author = " NO SKIN INI";
                }

                nextFoundSkin.Path = skinPath;
                nextFoundSkin.SkinName = skinPath.Split('\\').Last();
                installedSkins.Add(nextFoundSkin);
            }
            return installedSkins;
        }


        public void DetermineWhatSkinnablesToUse(RandomizerOptions options)
        {
            if(options.RandomizeInterface == true)
                EVERYSKINNABLE.AddRange(System.IO.File.ReadAllLines("Skinnables\\interface.txt").ToList());
            if(options.RandomizeStandard == true)
                EVERYSKINNABLE.AddRange(System.IO.File.ReadAllLines("Skinnables\\standard.txt").ToList());
        }


        public List<SkinInfo> GatherSkinnableElements(List<SkinInfo> skins)
        {
            List<SkinInfo> resultSkins = new List<SkinInfo>();

            foreach(SkinInfo skin in skins)
            {
                // go over the baseData => /Skinnables/interface
                // /Skinnables/Standard
                // and so on => check what files the installed skin have
                foreach (string skinnableName in EVERYSKINNABLE)
                {
                    if(System.IO.File.Exists(skin.Path + @"\" + skinnableName))
                    {
                        skin.AvailableSkinElements.Add(skinnableName);
                    }
                }
                resultSkins.Add(skin);
            }
            return resultSkins;
        }

        public void SaveCreatedSkin(SkinInfo userSkin)
        {
            foreach(string originSkinnable in userSkin.AvailableSkinElements)
            {
                // split origin so we get the skinnableName
                string rawSkinnableName = originSkinnable.Split('\\').Last();
                if(!System.IO.File.Exists(userSkin.Path + "\\" + rawSkinnableName))
                    System.IO.File.Copy(originSkinnable, userSkin.Path + "\\" + rawSkinnableName);
            }
        }

    }
}
