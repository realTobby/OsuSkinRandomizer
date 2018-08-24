using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsuSkinRandomizer
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(textBox1.Text))
            {
                System.IO.Directory.CreateDirectory(textBox1.Text + "/" + textBox2.Text);

                List<string> filesToCopy = new List<string>();
                List<string> allSkinnable = new List<string>();


                if(chk_Catch.Checked == true)
                {
                    var catchSkinnable = System.IO.File.ReadAllLines("OsuSkinnables/skinnableCatch.txt");
                    allSkinnable.AddRange(catchSkinnable.ToList());
                }

                if(chk_interface.Checked == true)
                {
                    var interfaceSkinnable = System.IO.File.ReadAllLines("OsuSkinnables/skinnableInterface.txt");
                    allSkinnable.AddRange(interfaceSkinnable.ToList());
                }

                if(chk_Mania.Checked == true)
                {
                    var maniaSkinnable = System.IO.File.ReadAllLines("OsuSkinnables/skinnableMania.txt");
                    allSkinnable.AddRange(maniaSkinnable.ToList());
                }

                if(chk_Sounds.Checked == true)
                {
                    var soundSkinnable = System.IO.File.ReadAllLines("OsuSkinnables/skinnableSounds.txt");
                    allSkinnable.AddRange(soundSkinnable.ToList());
                }

                if(chk_standard.Checked == true)
                {
                    var StandardSkinnable = System.IO.File.ReadAllLines("OsuSkinnables/skinnableStandard.txt");
                    allSkinnable.AddRange(StandardSkinnable.ToList());
                    
                }

                if(chk_taiko.Checked == true)
                {
                    var taikoSkinnable = System.IO.File.ReadAllLines("OsuSkinnables/skinnableTaiko.txt");
                    allSkinnable.AddRange(taikoSkinnable.ToList());
                }


                if(checkBox1.Checked == true)
                {
                    var interfaceSkinnable = System.IO.File.ReadAllLines("OsuSkinnables/skinnableInterface.txt");
                    allSkinnable.AddRange(interfaceSkinnable.ToList());
                    var soundSkinnable = System.IO.File.ReadAllLines("OsuSkinnables/skinnableSounds.txt");
                    allSkinnable.AddRange(soundSkinnable.ToList());
                    var StandardSkinnable = System.IO.File.ReadAllLines("OsuSkinnables/skinnableStandard.txt");
                    allSkinnable.AddRange(StandardSkinnable.ToList());
                    var NumberSkinnables = System.IO.File.ReadAllLines("OsuSkinnables/skinnableNumbers.txt");
                    allSkinnable.AddRange(NumberSkinnables.ToList());

                    var allImageFiles = System.IO.Directory.GetFiles(textBox1.Text, "*.png", System.IO.SearchOption.AllDirectories);
                    var allSoundFiles = System.IO.Directory.GetFiles(textBox1.Text, "*.wav", System.IO.SearchOption.AllDirectories);


                    List<string> allFuckingSkinFiles = new List<string>();

                    allFuckingSkinFiles.AddRange(allImageFiles);
                    allFuckingSkinFiles.AddRange(allSoundFiles);

                    List<string> standardStuff = System.IO.File.ReadAllLines("standardFileSizes.txt").ToList();

                    List<string> pathList = new List<string>();
                    foreach(var skinnable in allSkinnable)
                    {

                        string skinFile = allFuckingSkinFiles[rnd.Next(0, allFuckingSkinFiles.Count)];
                        // todo: resize random image to the original resolurtion
                        if(skinFile.Contains(".png") && !System.IO.Path.GetFileName(skinFile).StartsWith("._") && !skinnable.StartsWith("default-") && !skinnable.StartsWith("score-"))
                        {
                            if (standardStuff.Exists(x => x.Contains(skinnable)))
                            {
                                string complete = "";
                                complete = standardStuff.Find(x => x.Contains(skinnable));
                                string[] paras = complete.Split('#');

                                Bitmap orig = new Bitmap(skinFile);
                                Bitmap file = new Bitmap(orig, Convert.ToInt32(paras[1]), Convert.ToInt32(paras[2]));
                                file.Save(textBox1.Text + "/" + textBox2.Text + "/" + skinnable);
                            }
                        }
                        else if(skinFile.Contains(".wav"))
                        {
                            if(!System.IO.File.Exists(textBox1.Text + "/" + textBox2.Text + "/" + skinnable))
                                System.IO.File.Copy(skinFile, textBox1.Text + "/" + textBox2.Text + "/" + skinnable);
                        }
                        else if (skinnable.StartsWith("default-"))
                        {
                            List<string> allFoundNumbers = allFuckingSkinFiles.Where(x => x.Contains("default-")).ToList();
                            string[] paras = skinnable.Split('-');
                            string actualNumber = paras[1].Split('.')[0];

                            List<string> allRelevantNumbers = allFoundNumbers.Where(x => x.Contains("default-" + actualNumber)).ToList();

                            string toCopyNumber = allRelevantNumbers[rnd.Next(0, allRelevantNumbers.Count)];

                            string complete = "";
                            complete = standardStuff.Find(x => x.Contains(skinnable));
                            string[] pars1 = complete.Split('#');

                            Bitmap orig = new Bitmap(skinFile);
                            Bitmap file = new Bitmap(orig, Convert.ToInt32(pars1[1]), Convert.ToInt32(pars1[2]));
                            file.Save(textBox1.Text + "/" + textBox2.Text + "/" + skinnable);

                            //if (!System.IO.File.Exists(textBox1.Text + "/" + textBox2.Text + "/" + skinnable))
                            //    System.IO.File.Copy(toCopyNumber, textBox1.Text + "/" + textBox2.Text + "/" + skinnable);

                        }
                        // HOLY SHIT THIS IS A FUCKING MESS
                        else if (skinnable.StartsWith("score-"))
                        {
                            List<string> allFoundNumbers = allFuckingSkinFiles.Where(x => x.Contains("score-")).ToList();
                            string[] paras = skinnable.Split('-');
                            string actualNumber = paras[1].Split('.')[0];

                            List<string> allRelevantNumbers = allFoundNumbers.Where(x => x.Contains("score-" + actualNumber)).ToList();

                            string toCopyNumber = allRelevantNumbers[rnd.Next(0, allRelevantNumbers.Count)];

                            //if (!System.IO.File.Exists(textBox1.Text + "/" + textBox2.Text + "/" + skinnable))
                            //    System.IO.File.Copy(toCopyNumber, textBox1.Text + "/" + textBox2.Text + "/" + skinnable);

                            string complete = "";
                            complete = standardStuff.Find(x => x.Contains(skinnable));
                            string[] pars1 = complete.Split('#');

                            Bitmap orig = new Bitmap(skinFile);
                            Bitmap file = new Bitmap(orig, Convert.ToInt32(pars1[1]), Convert.ToInt32(pars1[2]));
                            file.Save(textBox1.Text + "/" + textBox2.Text + "/" + skinnable);

                        }

                    }
                }else if(chk_corruption.Checked == true)
                {
                    var interfaceSkinnable = System.IO.File.ReadAllLines("OsuSkinnables/skinnableInterface.txt");
                    allSkinnable.AddRange(interfaceSkinnable.ToList());
                    var soundSkinnable = System.IO.File.ReadAllLines("OsuSkinnables/skinnableSounds.txt");
                    allSkinnable.AddRange(soundSkinnable.ToList());
                    var StandardSkinnable = System.IO.File.ReadAllLines("OsuSkinnables/skinnableStandard.txt");
                    allSkinnable.AddRange(StandardSkinnable.ToList());

                    var allImageFiles = System.IO.Directory.GetFiles(textBox1.Text, "*.png", System.IO.SearchOption.AllDirectories);
                    var allSoundFiles = System.IO.Directory.GetFiles(textBox1.Text, "*.wav", System.IO.SearchOption.AllDirectories);

                    List<string> allFuckingSkinFiles = new List<string>();
                    allFuckingSkinFiles.AddRange(allImageFiles);
                    allFuckingSkinFiles.AddRange(allSoundFiles);

                    foreach (var skinnable in allSkinnable)
                    {
                        string skinFile = allFuckingSkinFiles[rnd.Next(0, allFuckingSkinFiles.Count)];
                        if (!System.IO.File.Exists(textBox1.Text + "/" + textBox2.Text + "/" + skinnable))
                            System.IO.File.Copy(skinFile, textBox1.Text + "/" + textBox2.Text + "/" + skinnable);
                    }
                }
                else
                {
                    foreach (var item in allSkinnable)
                    {
                        var result = System.IO.Directory.GetFiles(textBox1.Text, item, System.IO.SearchOption.AllDirectories);
                        if (result.Count() > 0)
                        {
                            filesToCopy.Add(result[rnd.Next(0, result.Count())]);
                        }
                    }

                    foreach (var item in filesToCopy)
                    {
                        string target = System.IO.Path.GetFileName(item);
                        try
                        {
                            System.IO.File.Copy(item, textBox1.Text + "/" + textBox2.Text + "/" + target, true);
                        }
                        catch (Exception ex)
                        {
                            // dont care enough to care
                        }

                    }
                }


                
            }
            else
            {
                MessageBox.Show("Seems like the OSU-Path you selected, doesnt exist? Please try again!");
                return;
            }
            MessageBox.Show("Osu Skin Randomization: Done");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = folderDialog.SelectedPath;
                    // folderDialog.SelectedPath -- your result
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            chk_Catch.Checked = false;
            chk_interface.Checked = false;
            chk_Mania.Checked = false;
            chk_Sounds.Checked = false;
            chk_standard.Checked = false;
            chk_taiko.Checked = false;






        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> fileRes = new List<string>();
            var files = System.IO.Directory.GetFiles(textBox1.Text,"*.png");
            foreach(var f in files)
            {
                Bitmap tmpLOLHeavyLoad = new Bitmap(f);
                fileRes.Add(System.IO.Path.GetFileName(f) + "#" + tmpLOLHeavyLoad.Width + "#" + tmpLOLHeavyLoad.Height);
            }

            System.IO.File.WriteAllLines("standardFileSizes.txt", fileRes.ToArray());




        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            chk_Catch.Checked = false;
            chk_interface.Checked = false;
            chk_Mania.Checked = false;
            chk_Sounds.Checked = false;
            chk_standard.Checked = false;
            chk_taiko.Checked = false;
            chk_corruption.Checked = false;
        }
    }
}
