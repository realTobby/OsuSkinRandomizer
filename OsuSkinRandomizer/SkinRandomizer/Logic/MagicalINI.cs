using SkinRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Logic
{
    public enum INIMagic
    {
        Corrupted,
        Random

    }



    public class MagicalINI
    {
        private string applicationVersion = "";
        private Random rnd = new Random();
        public string OsuSkinFolder = "";
        public string SkinName = "";
        public string AuthorName = "OsuSkinRandomizer: https://osu.ppy.sh/community/forums/topics/777098 MADE WITH VERSION: ";
        private List<SkINIOptionModel> baseDataIni = new List<SkINIOptionModel>();
        private List<SkINIOptionModel> resultINI = new List<SkINIOptionModel>();
        private List<INIFileModel> installedINIs = new List<INIFileModel>();
        private List<string> credits = new List<string>();

        public MagicalINI(string v)
        {
            applicationVersion = v;
        }

        public List<string> GetCredits()
        {
            return credits;
        }

        private void ReadBaseData()
        {
            string[] baseData = System.IO.File.ReadAllLines(@"Assets\SkinnableData\baseDataSkinINI.base");
            OptionGroup currentGroup = OptionGroup.General;
            for (int i = 0; i < baseData.Count(); i++)
            {
                string line = baseData[i];

                switch(line) // get group
                {
                    case "[General]":
                        currentGroup = OptionGroup.General;
                        continue;
                    case "[Colours]":
                        currentGroup = OptionGroup.Colours;
                        continue;
                    case "[Fonts]":
                        currentGroup = OptionGroup.Fonts;
                        continue;
                    case "[CatchTheBeat]":
                        currentGroup = OptionGroup.CatchTheBeat;
                        continue;
                    case "[Mania]":
                        currentGroup = OptionGroup.Mania;
                        continue;
                }

                SkINIOptionModel newOptionLine = new SkINIOptionModel();
                if (currentGroup == OptionGroup.Mania)
                    newOptionLine.isMania = true;
                newOptionLine.isMania = false;
                newOptionLine.OptionGroup = currentGroup;
                string[] optionLine = line.Split(':');
                newOptionLine.OptionKey = optionLine[0];
                newOptionLine.OptionValue = optionLine[1];
                baseDataIni.Add(newOptionLine);
            }
        }

        private void GatherInstalledINIs()
        {
            // get skins
            string[] installedSkins = System.IO.Directory.GetDirectories(OsuSkinFolder);
            // look for ini in only skin folder

            List<string> foundINIS = new List<string>();

            foreach (string installedSkin in installedSkins)
            {
                if (System.IO.File.Exists(installedSkin + @"\skin.ini"))
                {
                    foundINIS.Add(installedSkin + @"\skin.ini");
                }
            }

            foreach (string installedIniFile in foundINIS)
            {
                List<SkINIOptionModel> iniFile = ReadRealSkinINI(installedIniFile);
                INIFileModel nextINIFile = new INIFileModel();
                nextINIFile.iniContent = iniFile;
                nextINIFile.originalPath = installedIniFile;
                installedINIs.Add(nextINIFile);
            }

        }

        

        public void CreateSkinINI(INIMagic option)
        {
            // collect the installed ini files
            GatherInstalledINIs();

            // read base data file
            ReadBaseData();

            SkINIOptionModel authorLine = new SkINIOptionModel("Author", AuthorName + applicationVersion, OptionGroup.General);
            SkINIOptionModel skinNameLine = new SkINIOptionModel("Name", SkinName, OptionGroup.General);
            SkINIOptionModel versionLine = new SkINIOptionModel("Version", "latest", OptionGroup.General);

            resultINI.Add(authorLine);
            resultINI.Add(skinNameLine);
            resultINI.Add(versionLine);


            switch (option)
            {
                case INIMagic.Random:

                    for(int i = 0; i < baseDataIni.Count; i++) // OPTION LINE IN RESULT INI!
                    {
                        SkINIOptionModel indexOption = baseDataIni[i];
                        if (indexOption.OptionKey == "Author" || indexOption.OptionKey == "Name" || indexOption.OptionKey == "Version" || indexOption.OptionGroup == OptionGroup.CatchTheBeat || indexOption.OptionGroup == OptionGroup.Mania)
                            continue;

                        // get ini files that have the optionKey in use
                        List<INIFileModel> matchingFiles = new List<INIFileModel>();

                        foreach(INIFileModel installedINI in installedINIs)
                        {
                            if (installedINI.HasOptinKey(indexOption))
                                matchingFiles.Add(installedINI);
                        }

                        if(matchingFiles.Count() > 0)
                        {
                            // select random ini
                            INIFileModel luckyWinner = matchingFiles[rnd.Next(0, matchingFiles.Count)];
                            // get value
                            indexOption.OptionValue = luckyWinner.iniContent.Where(x => x.OptionKey == indexOption.OptionKey).First().OptionValue;// + " (DEBUG: " + luckyWinner.originalPath + ")";
                            resultINI.Add(indexOption);

                            // save author if not already using
                            // example: skin name by author
                            // get used skin
                            // get author
                            string[] skinFolderSegments = luckyWinner.originalPath.Split('\\');
                            string skinName = skinFolderSegments[skinFolderSegments.Count() - 2];
                            string author = luckyWinner.iniContent.Where(x => x.OptionKey == "Author").First().OptionValue;
                            string creditString = "Skin: " + skinName + " by " + author;
                            if(!credits.Contains(creditString))
                            {
                                credits.Add(creditString);
                            }
                        }
                    }
                    break;
                case INIMagic.Corrupted:
                    break;
            }

            SaveNewINI();
        }

        private void SaveNewINI()
        {

            // go trough resultINI
            // group objects
            // create header for groups

            string iniContentString = "";

            // general
            iniContentString = ManipulateResultINIString(iniContentString, resultINI.Where(x => x.OptionGroup == OptionGroup.General).ToList());

            // colours
            iniContentString = ManipulateResultINIString(iniContentString, resultINI.Where(x => x.OptionGroup == OptionGroup.Colours).ToList());
            // fonts
            iniContentString = ManipulateResultINIString(iniContentString, resultINI.Where(x => x.OptionGroup == OptionGroup.Fonts).ToList());
            //// CatchTheBeat
            //iniContentString = ManipulateResultINIString(iniContentString, resultINI.Where(x => x.OptionGroup == OptionGroup.CatchTheBeat).ToList());
            //// mania
            //iniContentString = ManipulateResultINIString(iniContentString, resultINI.Where(x => x.OptionGroup == OptionGroup.Mania).ToList());

            System.IO.File.WriteAllText(OsuSkinFolder + "\\" + SkinName + "\\skin.ini", iniContentString);
        }

        private string ManipulateResultINIString(string input, List<SkINIOptionModel> groupedObjects)
        {
            // get group
            string groupID = "[" + groupedObjects.First().OptionGroup.ToString().ToUpper() + "]";
            input = input + groupID + System.Environment.NewLine;
            foreach (var line in groupedObjects)
            {
                string key = line.OptionKey.TrimEnd(' ');
                string value = line.OptionValue.TrimStart(' ');
                string correctLine = key + ": " + value;
                input = input + correctLine + System.Environment.NewLine;
            }
            return input;
        }

        private List<SkINIOptionModel> ReadRealSkinINI(string path)
        {
            List<SkINIOptionModel> iniFile = new List<SkINIOptionModel>();
            List<string> skininicontent = System.IO.File.ReadAllLines(path).ToList();

            for(int i = 0; i < skininicontent.Count; i++) // clean the line of tabs, eww
            {
                skininicontent[i] = skininicontent[i].Replace("\t", string.Empty);
            }

            skininicontent.RemoveAll(x=>x.StartsWith("//")); // remove unnecessary lines
            skininicontent.RemoveAll(x => x == "" || x == string.Empty); // remove unnecessary lines

            OptionGroup currentGroup = OptionGroup.General;
            for (int i = 0; i < skininicontent.Count; i++)
            {
                string line = skininicontent[i];

                switch (line) // get group
                {
                    case "[General]":
                        currentGroup = OptionGroup.General;
                        continue;
                    case "[Colours]":
                        currentGroup = OptionGroup.Colours;
                        continue;
                    case "[Fonts]":
                        currentGroup = OptionGroup.Fonts;
                        continue;
                    case "[CatchTheBeat]":
                        currentGroup = OptionGroup.CatchTheBeat;
                        continue;
                    case "[Mania]":
                        currentGroup = OptionGroup.Mania;
                        continue;

                }

                if (!line.Contains(":"))
                    continue;


                SkINIOptionModel newOptionLine = new SkINIOptionModel();
                if (currentGroup == OptionGroup.Mania)
                    newOptionLine.isMania = true;
                newOptionLine.isMania = false;
                newOptionLine.OptionGroup = currentGroup;
                string[] optionLine = line.Split(':');
                newOptionLine.OptionKey = optionLine[0];
                newOptionLine.OptionValue = optionLine[1];
                iniFile.Add(newOptionLine);
            }
            return iniFile;
        }

    }
}
