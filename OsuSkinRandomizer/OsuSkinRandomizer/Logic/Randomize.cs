using OsuSkinRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuSkinRandomizer.Logic
{
    /// <summary>
    /// Takes care of everything related to randomizing
    /// </summary>
    public class Randomize
    {
        public ViewModel UILayer; // used for binding with the WPF UI Layer
        public Files fileLogic;


        #region Redirectives
        private string OSUFOLDER => UILayer.OsuSkinFolder;
        private string USERSKINNAME => UILayer.UserGeneratedSkin.SkinName;
        private string NEWOSUSKINPATH => OSUFOLDER + @"\" +  USERSKINNAME;
        #endregion

        public Randomize()
        {
            UILayer = new ViewModel();
            fileLogic = new Files();
        }




        public SkinInfo CreateSkin()
        {
            UILayer.UserGeneratedSkin.Path = NEWOSUSKINPATH;
            UILayer.UserGeneratedSkin.Author = "OsuSkinRandomizer";
            Random rnd = new Random(); // generate "random" numbers so the skin will be randomized

            // create folder
            if (!System.IO.Directory.Exists(NEWOSUSKINPATH))
                System.IO.Directory.CreateDirectory(NEWOSUSKINPATH);

            foreach(string skinnableName in fileLogic.EVERYSKINNABLE)
            {
                // only match with the skins that have this skinnable => dont run into the "lol the skin doesnt have this file, but i tried to copy it anyway XD"
                List<SkinInfo> matches = UILayer.InstalledSkins.Where(x => x.AvailableSkinElements.Exists(y => y == skinnableName)).ToList();
                if(matches != null && matches.Count() > 1)
                {
                    SkinInfo chosenOne = matches[rnd.Next(0, matches.Count())];
                    UILayer.UserGeneratedSkin.AvailableSkinElements.Add(chosenOne.Path + "\\" + skinnableName);
                }

            }



            return UILayer.UserGeneratedSkin;
        }

    }
}
