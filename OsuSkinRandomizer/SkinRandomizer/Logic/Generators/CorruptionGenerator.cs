﻿using SkinRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Logic.Generators
{
    public class CorruptionGenerator : BaseGenerator
    {
        public override void Generate()
        {
            Random rnd = new Random();
            List<string> installedSkins = System.IO.Directory.GetDirectories(base.pathToOsuSkinFolder).ToList();
            List<SkinnableFile> allFoundFiles = new List<SkinnableFile>();

            foreach (string skinFolder in installedSkins)
            {
                var fileInSkinFolder = System.IO.Directory.GetFiles(skinFolder);
                foreach (string file in fileInSkinFolder)
                {
                    SkinnableFile newFile = new SkinnableFile();
                    newFile.extension = System.IO.Path.GetExtension(file);
                    newFile.skinnableName = System.IO.Path.GetFileNameWithoutExtension(file);
                    newFile.skinName = System.IO.Path.GetFullPath(skinFolder).Split('\\').Last();
                    allFoundFiles.Add(newFile);
                }
            }

            List<SkinnableFile> newSkin = new List<SkinnableFile>();
            foreach (SkinnableFile installedFile in allFoundFiles)
            {
                if (!newSkin.Exists(x => x.skinnableName == installedFile.skinnableName))
                {
                    // if not found
                    List<SkinnableFile> skinnableGroup = allFoundFiles.Where(x => x.extension == installedFile.extension).ToList();
                    SkinnableFile corruptedFile = new SkinnableFile();
                    corruptedFile = skinnableGroup[rnd.Next(0, skinnableGroup.Count)];
                    corruptedFile.overrideSkinnable = installedFile.skinnableName;
                    newSkin.Add(corruptedFile);

                }
            }

            string resultSkinDirectory = base.pathToOsuSkinFolder + @"\" + base.skinResultName + @"\";

            foreach (SkinnableFile file in newSkin)
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