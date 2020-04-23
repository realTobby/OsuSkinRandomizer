using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Logic
{
    public class SkinGenerator
    {
        private Random rnd = new Random();
        private string pathToOsuSkinFolder = "";

        private string skinResultName = "";

        List<string> skinnables = new List<string>();

        public SkinGenerator(string path, string newSkinName)
        {
            pathToOsuSkinFolder = path;
            skinResultName = newSkinName;

            System.IO.Directory.CreateDirectory(path + @"\" + newSkinName);
            InitSizeFile();
            skinnables.AddRange(System.IO.File.ReadAllLines("OsuSkinnables/skinnableCatch.txt"));
            skinnables.AddRange(System.IO.File.ReadAllLines("OsuSkinnables/skinnableInterface.txt"));
            skinnables.AddRange(System.IO.File.ReadAllLines("OsuSkinnables/skinnableMania.txt"));
            skinnables.AddRange(System.IO.File.ReadAllLines("OsuSkinnables/skinnableSounds.txt"));
            skinnables.AddRange(System.IO.File.ReadAllLines("OsuSkinnables/skinnableStandard.txt"));
            skinnables.AddRange(System.IO.File.ReadAllLines("OsuSkinnables/skinnableTaiko.txt"));
        }

        private void InitSizeFile()
        {
            List<string> fileRes = new List<string>();
            var files = System.IO.Directory.GetFiles(pathToOsuSkinFolder, "*.png", System.IO.SearchOption.AllDirectories);
            foreach (var f in files)
            {
                Bitmap tmpLOLHeavyLoad = new Bitmap(f);
                fileRes.Add(System.IO.Path.GetFileName(f) + "#" + tmpLOLHeavyLoad.Width + "#" + tmpLOLHeavyLoad.Height);
                tmpLOLHeavyLoad.Dispose();
            }

            System.IO.File.WriteAllLines("standardFileSizes.txt", fileRes.ToArray());
        }

        public void Generate()
        {
            List<string> standardStuff = System.IO.File.ReadAllLines("standardFileSizes.txt").ToList();

            List<string> allSkinFiles = new List<string>();
            var allImageFiles = System.IO.Directory.GetFiles(pathToOsuSkinFolder, "*.png", System.IO.SearchOption.AllDirectories);
            var allSoundFiles = System.IO.Directory.GetFiles(pathToOsuSkinFolder, "*.wav", System.IO.SearchOption.AllDirectories);

            allSkinFiles.AddRange(allImageFiles);
            allSkinFiles.AddRange(allSoundFiles);
            List<string> pathList = new List<string>();

            foreach (var skinnable in skinnables)
            {
                string skinnableInQuestion = skinnable;

                List<string> canidates = allSkinFiles.Where(x => x.Contains(System.IO.Path.GetFileNameWithoutExtension(skinnable))).ToList();

                if(canidates.Count() == 0)
                {
                    continue;
                }
                string skinFile = canidates[rnd.Next(0, canidates.Count)];

                // todo: resize random image to the original resolurtion
                if (skinFile.Contains(".png") && !System.IO.Path.GetFileName(skinFile).StartsWith("._") && !skinnable.StartsWith("default-") && !skinnable.StartsWith("score-"))
                {
                    string complete = "";
                    complete = standardStuff.Find(x => x.Contains(System.IO.Path.GetFileNameWithoutExtension(skinnable)));
                    string[] paras = complete.Split('#');

                    Bitmap orig = new Bitmap(skinFile);
                    Bitmap file = new Bitmap(orig, Convert.ToInt32(paras[1]), Convert.ToInt32(paras[2]));
                    file.Save(pathToOsuSkinFolder + "/" + skinResultName + "/" + skinnable);
                }
                else if (skinFile.Contains(".wav"))
                {
                    if (!System.IO.File.Exists(pathToOsuSkinFolder + "/" + skinResultName + "/" + skinnable))
                        System.IO.File.Copy(skinFile, pathToOsuSkinFolder + "/" + skinResultName + "/" + skinnable);
                }
                else if (skinnable.StartsWith("default-"))
                {
                    List<string> allFoundNumbers = allSkinFiles.Where(x => x.Contains("default-")).ToList();
                    string[] paras = skinnable.Split('-');
                    string actualNumber = paras[1].Split('.')[0];

                    List<string> allRelevantNumbers = allFoundNumbers.Where(x => x.Contains("default-" + actualNumber)).ToList();

                    string toCopyNumber = allRelevantNumbers[rnd.Next(0, allRelevantNumbers.Count)];

                    string complete = "";
                    complete = standardStuff.Find(x => x.Contains(skinnable));
                    string[] paras1 = complete.Split('#');

                    Bitmap orig = new Bitmap(skinFile);
                    Bitmap file = new Bitmap(orig, Convert.ToInt32(paras1[1]), Convert.ToInt32(paras1[2]));
                    file.Save(pathToOsuSkinFolder + "/" + skinResultName + "/" + skinnable);

                    //if (!System.IO.File.Exists(textBox1.Text + "/" + textBox2.Text + "/" + skinnable))
                    //    System.IO.File.Copy(toCopyNumber, textBox1.Text + "/" + textBox2.Text + "/" + skinnable);

                }
                // HOLY SHIT THIS IS A FUCKING MESS
                else if (skinnable.StartsWith("score-"))
                {
                    List<string> allFoundNumbers = allSkinFiles.Where(x => x.Contains("score-")).ToList();
                    string[] paras = skinnable.Split('-');
                    string actualNumber = paras[1].Split('.')[0];

                    List<string> allRelevantNumbers = allFoundNumbers.Where(x => x.Contains("score-" + actualNumber)).ToList();

                    string toCopyNumber = allRelevantNumbers[rnd.Next(0, allRelevantNumbers.Count)];

                    //if (!System.IO.File.Exists(textBox1.Text + "/" + textBox2.Text + "/" + skinnable))
                    //    System.IO.File.Copy(toCopyNumber, textBox1.Text + "/" + textBox2.Text + "/" + skinnable);

                    string complete = "";
                    complete = standardStuff.Find(x => x.Contains(skinnable));
                    string[] paras1 = complete.Split('#');

                    Bitmap orig = new Bitmap(skinFile);
                    Bitmap file = new Bitmap(orig, Convert.ToInt32(paras1[1]), Convert.ToInt32(paras1[2]));
                    file.Save(pathToOsuSkinFolder + "/" + skinResultName + "/" + skinnable);

                }

            }
        }

    }
}
