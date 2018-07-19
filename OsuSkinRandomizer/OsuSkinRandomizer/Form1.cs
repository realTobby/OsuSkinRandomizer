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
            else
            {
                MessageBox.Show("Seems like the OSU-Path you selected, doesnt exist? Please try again!");
                return;
            }
            MessageBox.Show("Done");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap("banner.png");
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
    }
}
