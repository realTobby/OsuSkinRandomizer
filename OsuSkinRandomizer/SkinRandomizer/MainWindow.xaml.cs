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
            if (System.IO.File.Exists("log.txt"))
                System.IO.File.Delete("log.txt");



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
            myLogger.AddLoggerLine("starting generation", Severity.Information);
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
                myLogger.AddLoggerLine("deleting existing creation folder", Severity.Information);
                System.IO.Directory.Delete(myViewModel.OsuFolder + @"\" + myViewModel.CreationName, true); // delete everything in it and the folder 
            }
            myLogger.AddLoggerLine("creating new empty creation folder", Severity.Information);
            System.IO.Directory.CreateDirectory(myViewModel.OsuFolder + @"\" + myViewModel.CreationName); // create it

            await Task.Run(() =>
            {
                try
                {
                    myLogger.AddLoggerLine("starting to generate skin...", Severity.Information);

                    IGenerator skinelementsstuff = new BaseGenerator(); // create a generic base generator

                    MagicalINI inistuff = new MagicalINI(myViewModel.Version);
                    myLogger.AddLoggerLine("setting ini file...", Severity.Information);
                    inistuff.OsuSkinFolder = myViewModel.OsuFolder;
                    inistuff.SkinName = myViewModel.CreationName;
                    if (myViewModel.IsCorruptionMode == true)
                    {
                        myLogger.AddLoggerLine("corruption mode generator", Severity.Information);
                        skinelementsstuff = new CorruptionGenerator(); // specify generator
                        inistuff.CreateSkinINI(INIMagic.Corrupted);
                    }
                    if (myViewModel.IsNormalRandomMode == true)
                    {
                        myLogger.AddLoggerLine("normal random generator", Severity.Information);
                        skinelementsstuff = new TotalRandomGenerator(); // specify generator
                        inistuff.CreateSkinINI(INIMagic.Random);
                    }

                    myLogger.AddLoggerLine("creating credits file", Severity.Information);
                    CreditGiver cg = new CreditGiver(inistuff.GetCredits(), myViewModel.Version, myViewModel.OsuFolder + @"\" + myViewModel.CreationName);
                    cg.CreateCreditsFile();

                    skinelementsstuff.Init(myViewModel.OsuFolder, myViewModel.CreationName); // init generstor with the directory info
                    myLogger.AddLoggerLine("skin generator init", Severity.Information);
                    skinelementsstuff.GatherFiles(); // get files :)
                    myLogger.AddLoggerLine("skin generator GatherFiles", Severity.Information);
                    skinelementsstuff.Generate(); // generate the skin :)
                    myLogger.AddLoggerLine("skin generator generate", Severity.Information);
                }catch(Exception ex)
                {
                    myLogger.AddLoggerLine("ran into exception: " + ex.Message, Severity.Error);
                }
                
            });
            //PreviewGenerate pg = new PreviewGenerate();
            //myViewModel.ImagePreview = pg.GenerateBitmap(myViewModel.OsuFolder + @"\" + myViewModel.CreationName); // create the preview of the generate skin
            await controllerWait.CloseAsync();
            var doneController = await this.ShowMessageAsync("Done.", "The Random Skin was successfully created!", MessageDialogStyle.Affirmative);
            myLogger.AddLoggerLine("skin should be created...", Severity.Information);
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
