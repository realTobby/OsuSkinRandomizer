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
        private Logger myLogger = new Logger();

        private List<string> credit = new List<string>();

        public TotalRandomGenerator()
        {
            myLogger.AddLoggerLine("totalrandomGenerator successfully initialized", Severity.Information);
        }

        public List<string> GetCredits()
        {
            return credit;
        }

        /// <summary>
        /// TotalRandomGenerator creates a playable skin, which sticks to a list of skinnable elements and searches for these, so they skin elements align
        /// </summary>
        public override void Generate()
        {
            myLogger.AddLoggerLine("generating skin...", Severity.Information);
            string resultSkinDirectory = base.pathToOsuSkinFolder + @"\" + base.skinResultName + @"\";
            myLogger.AddLoggerLine("path the app wants to read: " + resultSkinDirectory, Severity.Information);

            if(base.relevantFiles.Count <= 0 || base.relevantFiles == null)
            {
                myLogger.AddLoggerLine("base.relevantFiles does not contain items " + resultSkinDirectory, Severity.Error);
            }


            foreach (SkinnableFile file in base.relevantFiles) // go over the result skin list and copy everys file over
            {
                myLogger.AddLoggerLine("Skin File: " + file.skinnableName + " / " + file.overrideSkinnable + " - " + file.extension + " (copy over)", Severity.Information);
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
