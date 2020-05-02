using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Logic
{
    public class CreditGiver
    {
        private string applicationVersion = "";
        private string ResultSkinPath = "";
        public List<string> creditAuthors = new List<string>();

        public CreditGiver(List<string> authors, string version, string resultSkinFolder)
        {
            applicationVersion = version;
            creditAuthors = authors;
            ResultSkinPath = resultSkinFolder;
        }

        public void AddMoreauthors(List<string> authors)
        {
            creditAuthors.AddRange(authors);
        }

        public void CreateCreditsFile()
        {
            // give proper credit to the actual guys who made the original skins :)

            string creditContent = "THIS SKIN WAS MADE WITH THE OSUSKINRANDOMIZER V" + applicationVersion + System.Environment.NewLine +
                "THE RANDOMIZING PROCESS CHOSE THE FOLLOWING SKINS:" + System.Environment.NewLine;
            foreach (var credit in creditAuthors)
            {
                creditContent = creditContent + credit + System.Environment.NewLine;
            }

            creditContent = creditContent + System.Environment.NewLine + "PLEASE PROVIDE THIS FILE WHEN YOU ARE SHARING THE SKIN! KEEP IND MIND THAT MIXED SKINS ARE NOT ALLOWED TO BE POSTED IN THE OSU SKINNING FORUM! HAPPY RANDOMIZING!";

            System.IO.File.WriteAllText(ResultSkinPath + "\\MIXED-SKINS-CREDITS.txt", creditContent);
        }

    }
}
