using SkinRandomizer.Logic;
using SkinRandomizer.ViewModels;
using System.Windows;
using SkinRandomizer.Logic.Generators;
using SkinRandomizer.Interfaces;
using System.Windows.Forms;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace SkinRandomizer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private ViewModel myViewModel;
        private SaveHandler SAVE_HANDLER = new SaveHandler();
        
        public MainWindow()
        {
            InitializeComponent();

            myViewModel = new ViewModel();
            this.DataContext = myViewModel;
            this.Show();

            SAVE_HANDLER.Load();
            
            if(SAVE_HANDLER.IsDirectoryAvailable == false) // check if the config has the directory in it
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
                    SAVE_HANDLER.Save(myViewModel.OsuFolder);
                }
            }
        }

        private async void btn_generate_Click(object sender, RoutedEventArgs e)
        {
            ProgressDialogController controllerWait = await this.ShowProgressAsync("Please wait...", "Generating OSU skin with the selected options...", false, null);
            controllerWait.SetIndeterminate();

            if (System.IO.Directory.Exists(myViewModel.OsuFolder + @"\" + myViewModel.CreationName)) // check if the skin folder with the creation name exists
            {
                System.IO.Directory.Delete(myViewModel.OsuFolder + @"\" + myViewModel.CreationName, true); // delete everything in it and the folder 
            }
            System.IO.Directory.CreateDirectory(myViewModel.OsuFolder + @"\" + myViewModel.CreationName); // create it

            IGenerator sg = new BaseGenerator(); // create a generic base generator

            if (myViewModel.IsCorruptionMode == true)
            {
                sg = new CorruptionGenerator(); // specify it
            }
            if (myViewModel.IsNormalRandomMode == true)
            {
                sg = new TotalRandomGenerator(); // specify it
            }

            sg.Init(myViewModel.OsuFolder, myViewModel.CreationName); // init generstor with the directory info

            await Task.Run(() =>
            {
                
                sg.Generate(); // generate the skin

                
            });
            PreviewGenerate pg = new PreviewGenerate();
            myViewModel.ImagePreview = pg.GenerateBitmap(myViewModel.OsuFolder + @"\" + myViewModel.CreationName); // create the preview of the generate skin
            await controllerWait.CloseAsync();
            var controllerMessage = await this.ShowMessageAsync("Done.", "The Random Skin was successfully created!", MessageDialogStyle.Affirmative);
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
