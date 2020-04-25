using SkinRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Logic.Generators
{
    class PlayableGenerator : BaseGenerator
    {
        public override void Generate()
        {
            Random rnd = new Random();
            List<string> installedSkins = System.IO.Directory.GetDirectories(base.pathToOsuSkinFolder).ToList();
            List<SkinnableFile> allFoundFiles = new List<SkinnableFile>();

            foreach (string skinFolder in installedSkins)
            {
                var fileInSkinFolder = System.IO.Directory.GetFiles(skinFolder, "*.*").ToList().Where(x=>x.EndsWith(".png") || x.EndsWith(".mp3") || x.EndsWith(".wav") || x.EndsWith(".jpg") || x.EndsWith(".jpeg"));
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
                    string fileName = installedFile.skinnableName.Split("@2x".ToArray()).First();
                    if (installedFile.skinnableName.ToLower().Contains("@2x"))
                    {
                        // higher resolution
                        List<SkinnableFile> animation = allFoundFiles.Where(x => x.skinnableName.Contains(fileName) && x.skinName == installedFile.skinName && x.skinnableName.ToLower().Contains("@2x")).ToList();
                        if (animation.Count() != 0)
                        {
                            newSkin.AddRange(animation);
                        }
                    }
                    else
                    {
                        List<SkinnableFile> skinnableGroup = allFoundFiles.Where(x => x.skinnableName.Contains(fileName) && x.skinName == installedFile.skinName && !x.skinnableName.ToLower().Contains("@2x")).ToList();
                        newSkin.Add(skinnableGroup[rnd.Next(0, skinnableGroup.Count)]);
                    }
                }
            }

            string resultSkinDirectory = base.pathToOsuSkinFolder + @"\" + base.skinResultName + @"\";

            foreach (SkinnableFile file in newSkin)
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
