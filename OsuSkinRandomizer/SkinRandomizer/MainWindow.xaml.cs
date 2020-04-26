﻿using SkinRandomizer.Logic;
using SkinRandomizer.ViewModels;
using System.Windows;
using SkinRandomizer.Logic.Generators;
using SkinRandomizer.Interfaces;
using System.Windows.Forms;

namespace SkinRandomizer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel myViewModel;
        private SaveHandler sh = new SaveHandler();
        
        public MainWindow()
        {
            InitializeComponent();

            myViewModel = new ViewModel();
            this.DataContext = myViewModel;
            this.Show();

            sh.Load();
            
            if(sh.IsDirectoryAvailable == false)
            {
                StartupCheck sc = new StartupCheck();
                string path = sc.GetDirectoryString();
                if(path == "NOTFOUND")
                {
                    myViewModel.OsuFolder = "Osu Folder not found...please specify manually!";
                }
                else
                {
                    myViewModel.OsuFolder = path;
                    sh.Save(myViewModel.OsuFolder);
                }
            }
        }

        private void btn_generate_Click(object sender, RoutedEventArgs e)
        {

            if(System.IO.Directory.Exists(myViewModel.OsuFolder + @"\" + myViewModel.CreationName))
            {
                System.IO.Directory.Delete(myViewModel.OsuFolder + @"\" + myViewModel.CreationName, true);
            }
            System.IO.Directory.CreateDirectory(myViewModel.OsuFolder + @"\" + myViewModel.CreationName);

            IGenerator sg = new BaseGenerator();

            if (myViewModel.IsCorruptionMode == true)
            {
                sg = new CorruptionGenerator();
            }
            if (myViewModel.IsNormalRandomMode == true)
            {
                sg = new TotalRandomGenerator();
            }

            sg.Init(myViewModel.OsuFolder, myViewModel.CreationName);
            sg.Generate();

            PreviewGenerate pg = new PreviewGenerate();
            myViewModel.ImagePreview = pg.GenerateBitmap(myViewModel.OsuFolder + @"\" + myViewModel.CreationName);

            System.Windows.MessageBox.Show("Your skin has been successfully created!");
        }

        private void btn_osu_folder_find_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog objDialog = new FolderBrowserDialog();
            objDialog.Description = "Beschreibung";
            objDialog.SelectedPath = @"C:\";       // Vorgabe Pfad (und danach der gewählte Pfad)
            if (objDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if(objDialog.SelectedPath.Contains("Skins"))
                {
                    myViewModel.OsuFolder = objDialog.SelectedPath;
                }
                else
                {
                    myViewModel.OsuFolder = objDialog.SelectedPath + "\\Skins";
                }
            }
        }
    }
}