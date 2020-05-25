using SkinRandomizer.Logic;
using SkinRandomizer.ViewModels;
using System.Windows;
using SkinRandomizer.Logic.Generators;
using SkinRandomizer.Interfaces;
using System.Windows.Forms;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System;

namespace SkinRandomizer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Logger myLogger = new Logger();

        private ViewModel myViewModel;
        private SaveHandler SAVE_HANDLER = new SaveHandler();
        
        public MainWindow()
        {
            InitializeComponent();
            myLogger.AddLoggerLine("Startup application...", Severity.Information);
            //#if DEBUG ==> PLACE A TEMPLATE SKIN INSDIE YOUR OSU SKIN FOLDER AND REDIRECT THE DIRECTORY TO GET BASE DATA => ALL SKINNABLE FILES THERE SHOULD BE
            //            BaseDataCreator bdc = new BaseDataCreator();
            //            bdc.StartReading(@"D:\Spiele\osu!\Skins\Template");
            //            bdc.CreateBaseDataFile();
            //            Console.WriteLine("[DEBUG]: Base Data File created!");
            //#endif


            myViewModel = new ViewModel();
            this.DataContext = myViewModel;
            this.Show();

            SAVE_HANDLER.Load();

            CheckOsuFolder();
        }

        private async void  CheckOsuFolder()
        {
            myLogger.AddLoggerLine("checking osu folder...", Severity.Information);
            if (SAVE_HANDLER.IsDirectoryAvailable == false) // check if the config has the directory in it
            {
                StartupCheck sc = new StartupCheck();
                string path = sc.GetDirectoryString();
                if (path == "NOTFOUND")
                {
                    myViewModel.OsuFolder = "Osu Folder not found...please specify manually!";
                    var controllerMessage = await this.ShowMessageAsync("Oops...", "Could not find your osu directory :( Please specify it manually!", MessageDialogStyle.Affirmative);
                }
                else
                {
                    myViewModel.OsuFolder = path;
                    SAVE_HANDLER.Save(myViewModel.OsuFolder);
                }
            }
        }

        private async void btn_generate_Click(object sender, RoutedEventArgs e)
        {
            myLogger.AddLoggerLine("starting generation...", Severity.Information);
            if (!System.IO.File.Exists(@"Assets\SkinnableData\baseDataSkinnables.base"))
            {
                myLogger.AddLoggerLine("couldnt find the baseDataSkinnables.base file", Severity.Error);
                var oopsController = await this.ShowMessageAsync("Oops...", "Cant find the 'baseDataSkinnables.base' file...please redownload the application! Without this file it wont work :( Contact the DEVs on GitHub maybe? :)", MessageDialogStyle.Affirmative);
            }

            if (!System.IO.File.Exists(@"Assets\SkinnableData\baseDataSkinINI.base"))
            {
                myLogger.AddLoggerLine("couldnt find the baseDataSkinINI.base file", Severity.Error);
                var oopsController = await this.ShowMessageAsync("Oops...", "Cant find the 'baseDataSkinINI.base' file...please redownload the application! Without this file it wont work :( Contact the DEVs on GitHub maybe? :)", MessageDialogStyle.Affirmative);
            }

            ProgressDialogController controllerWait = await this.ShowProgressAsync("Please wait...", "Generating OSU skin with the selected options...", false, null);
            controllerWait.SetIndeterminate();

            if (System.IO.Directory.Exists(myViewModel.OsuFolder + @"\" + myViewModel.CreationName)) // check if the skin folder with the creation name exists
            {
                System.IO.Directory.Delete(myViewModel.OsuFolder + @"\" + myViewModel.CreationName, true); // delete everything in it and the folder 
            }
            System.IO.Directory.CreateDirectory(myViewModel.OsuFolder + @"\" + myViewModel.CreationName); // create it

            await Task.Run(() =>
            {
                IGenerator skinelementsstuff = new BaseGenerator(); // create a generic base generator

                MagicalINI inistuff = new MagicalINI(myViewModel.Version);
                inistuff.OsuSkinFolder = myViewModel.OsuFolder;
                inistuff.SkinName = myViewModel.CreationName;
                if (myViewModel.IsCorruptionMode == true)
                {
                    skinelementsstuff = new CorruptionGenerator(); // specify generator
                    inistuff.CreateSkinINI(INIMagic.Corrupted);
                }
                if (myViewModel.IsNormalRandomMode == true)
                {
                    skinelementsstuff = new TotalRandomGenerator(); // specify generator
                    inistuff.CreateSkinINI(INIMagic.Random);
                }

                CreditGiver cg = new CreditGiver(inistuff.GetCredits(), myViewModel.Version, myViewModel.OsuFolder + @"\" + myViewModel.CreationName);
                cg.CreateCreditsFile();

                skinelementsstuff.Init(myViewModel.OsuFolder, myViewModel.CreationName); // init generstor with the directory info
                skinelementsstuff.GatherFiles(); // get files :)
                skinelementsstuff.Generate(); // generate the skin :)
            });
            PreviewGenerate pg = new PreviewGenerate();
            myViewModel.ImagePreview = pg.GenerateBitmap(myViewModel.OsuFolder + @"\" + myViewModel.CreationName); // create the preview of the generate skin
            await controllerWait.CloseAsync();
            var doneController = await this.ShowMessageAsync("Done.", "The Random Skin was successfully created!", MessageDialogStyle.Affirmative);
        }

        private void btn_osu_folder_find_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog objDialog = new FolderBrowserDialog();
            objDialog.Description = "Select OSU Directory";
            objDialog.SelectedPath = @"C:\";
            if (objDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if(objDialog.SelectedPath.Contains("Skins")) // check fi the osu folder with the skin is selected
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
