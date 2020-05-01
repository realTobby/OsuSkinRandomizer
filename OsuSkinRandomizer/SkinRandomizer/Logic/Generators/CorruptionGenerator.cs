using SkinRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Logic.Generators
{
    public class CorruptionGenerator : BaseGenerator
    {
        /// <summary>
        /// the corruption generator copies files without checking the names, so only the extensions are the same, the outcome is truly random so a cursor might look like a combo number or whatever
        /// </summary>
        public override void Generate()
        {
            List<SkinnableFile> newSkin = new List<SkinnableFile>();
            foreach (SkinnableFile installedFile in base.relevantFiles) // go over every installed file
            {
                if (!newSkin.Exists(x => x.skinnableName == installedFile.skinnableName)) // check if the skinnable element is already there
                {
                    // if not found
                    List<SkinnableFile> skinnableGroup = base.relevantFiles.Where(x => x.extension == installedFile.extension).ToList(); // look for matches in extension ==> dont care for anything else
                    SkinnableFile corruptedFile = new SkinnableFile();
                    corruptedFile = skinnableGroup[rnd.Next(0, skinnableGroup.Count)];
                    corruptedFile.overrideSkinnable = installedFile.skinnableName;
                    newSkin.Add(corruptedFile); // add file to result skin
                }
            }

            string resultSkinDirectory = base.pathToOsuSkinFolder + @"\" + base.skinResultName + @"\";

            foreach (SkinnableFile file in newSkin) // go over the result skin and copy over the files
            {
                string sourcePath = base.pathToOsuSkinFolder + @"\" + file.skinName + @"\" + file.skinnableName + file.extension;
                string targetPath = resultSkinDirectory + file.overrideSkinnable + file.extension;
                if (!System.IO.File.Exists(targetPath))
                {
                    System.IO.File.Copy(sourcePath, targetPath);
                }
            }
        }
    }
}
