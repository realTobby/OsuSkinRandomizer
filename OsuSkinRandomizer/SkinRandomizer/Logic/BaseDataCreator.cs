using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Logic
{
    public class BaseDataCreator
    {
        List<string> filenamesWithExtensions = new List<string>();

        public void StartReading()
        {
            string path = @"D:\Spiele\osu!\Skins\Template";



            filenamesWithExtensions = System.IO.Directory.GetFiles(path).ToList();




        }


        public void CreateBaseDataFile()
        {
            string filename = "baseDataSkinnables.base";
            string fileContent = "";

            foreach (string file in filenamesWithExtensions)
            {
                string onlyFilename = System.IO.Path.GetFileName(file);


                fileContent = fileContent + onlyFilename + System.Environment.NewLine;
            }

            System.IO.File.WriteAllText(filename, fileContent);

        }
    }
}
