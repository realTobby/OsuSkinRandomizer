using SkinRandomizer.Interfaces;
using SkinRandomizer.Models;
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
        internal Logger myLogger = new Logger();
        internal Random rnd = new Random();
        internal string pathToOsuSkinFolder = "";
        internal string skinResultName = "";
        List<string> skinnables = new List<string>();
        internal List<SkinnableFile> relevantFiles = new List<SkinnableFile>();

        /// <summary>
        /// will be used to generate the skin
        /// </summary>
        public virtual void Generate()
        {
            myLogger.AddLoggerLine("no Generator set...", Severity.Error);
            throw new NotImplementedException();
        }

        /// <summary>
        /// is used to give the generator a general idea of the directory
        /// </summary>
        /// <param name="path"></param>
        /// <param name="skinname"></param>
        public virtual void Init(string path, string skinname)
        {
            myLogger.AddLoggerLine("init generator", Severity.Information);
            pathToOsuSkinFolder = path;
            skinResultName = skinname;
            System.IO.Directory.CreateDirectory(path + @"\" + skinname);
        }

        public void GatherFiles()
        {
            myLogger.AddLoggerLine("reading files...", Severity.Information);
            List<string> installedSkins = System.IO.Directory.GetDirectories(pathToOsuSkinFolder).ToList(); // skin paths => all installed skin folders
            // get skinnables file
            string skinnableBase = @"Assets\SkinnableData\baseDataSkinnables.base";
            // read it
            List<string> EverySkinnable = System.IO.File.ReadAllLines(skinnableBase).ToList();
            // go over list
            foreach (string skinnableBaseName in EverySkinnable)
            {
                myLogger.AddLoggerLine("File: " + skinnableBaseName + " gathered (from installed)", Severity.Information);
                List<string> matchingFiles = new List<string>();
                foreach (string installedSkin in installedSkins) // go over installed skins
                {
                    string foundMatch = System.IO.Directory.GetFiles(installedSkin, skinnableBaseName, System.IO.SearchOption.TopDirectoryOnly).FirstOrDefault(); // check for file
                    if (foundMatch != null)
                        matchingFiles.Add(foundMatch);
                }
                if (matchingFiles.Count() > 0)
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
        }
    }
}
