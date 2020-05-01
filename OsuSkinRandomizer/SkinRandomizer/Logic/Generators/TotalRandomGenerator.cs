using SkinRandomizer.Interfaces;
using SkinRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SkinRandomizer.Logic.Generators
{
    public class TotalRandomGenerator : BaseGenerator
    {
        /// <summary>
        /// TotalRandomGenerator creates a playable skin, which sticks to a list of skinnable elements and searches for these, so they skin elements align
        /// </summary>
        public override void Generate()
        {
            
            List<string> installedSkins = System.IO.Directory.GetDirectories(base.pathToOsuSkinFolder).ToList(); // skin paths => all installed skin folders
            List<SkinnableFile> relevantFiles = new List<SkinnableFile>(); // ==> wasteful resources => better idea with the skinnables.base file i just created

            // get skinnables file
            string skinnableBase = @"Assets\SkinnableData\baseDataSkinnables.base";
            // read it
            List<string> EverySkinnable = System.IO.File.ReadAllLines(skinnableBase).ToList();
            // go over list
            foreach(string skinnableBaseName in EverySkinnable)
            {
                List<string> matchingFiles = new List<string>();
                foreach(string installedSkin in installedSkins) // go over installed skins
                {
                    string foundMatch = System.IO.Directory.GetFiles(installedSkin, skinnableBaseName, System.IO.SearchOption.TopDirectoryOnly).FirstOrDefault(); // check for file
                    if(foundMatch != null)
                        matchingFiles.Add(foundMatch);
                }
                if(matchingFiles.Count() > 0)
                {
                    string filePath = matchingFiles[rnd.Next(0, matchingFiles.Count())];
                    string[] skinFolderSegments = filePath.Split('\\');
                    if (skinFolderSegments[skinFolderSegments.Count() - 3] == "Skins") // the file is actually in the skin folder and not in a folder below it
                    {
                        string skinName = skinFolderSegments[skinFolderSegments.Count() - 2];
                        SkinnableFile newFile = new SkinnableFile();
                        newFile.extension = System.IO.Path.GetExtension(filePath);
                        newFile.skinnableName = System.IO.Path.GetFileNameWithoutExtension(filePath);
                        newFile.skinName = skinName;
                        relevantFiles.Add(newFile); // add the file to the installed skinnable file list
                    }
                }
            }

            string resultSkinDirectory = base.pathToOsuSkinFolder + @"\" + base.skinResultName + @"\";
            foreach (SkinnableFile file in relevantFiles) // go over the result skin list and copy everys file over
            {
                string sourcePath = base.pathToOsuSkinFolder + @"\" + file.skinName + @"\" + file.skinnableName + file.extension;
                string targetPath = resultSkinDirectory + file.skinnableName + file.extension;
                if (!System.IO.File.Exists(targetPath))
                {
                    System.IO.File.Copy(sourcePath, targetPath);
                }
            }
        }
    }
}
