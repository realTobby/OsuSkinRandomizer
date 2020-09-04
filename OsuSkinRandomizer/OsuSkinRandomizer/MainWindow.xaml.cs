using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
using OsuSkinRandomizer.Logic;
using OsuSkinRandomizer.Models;

namespace OsuSkinRandomizer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public Randomize skinRandomizer; // used for randomizer logic => UI shouldnt have any logic

        public MainWindow()
        {
            InitializeComponent();
            skinRandomizer = new Randomize();
            this.DataContext = skinRandomizer.UILayer;

            Startup();
        }

        public void Startup()
        {
            FindOsuSkinFolder();
            LookForInstalledSkins();

        }

        public void FindOsuSkinFolder()
        {
            skinRandomizer.UILayer.OsuSkinFolder = skinRandomizer.fileLogic.GetOsuDirectory();
        }

        public async void LookForInstalledSkins()
        {
            ProgressDialogController controller = await this.ShowProgressAsync("Please wait...", "Search for installed skins");
            controller.SetIndeterminate();
            skinRandomizer.UILayer.InstalledSkins.Clear();
            await Task.Delay(2000); // artificial wait => bad for user experience??? But looks nicer when the Dialog is visible, else the user would think there was a problem...idk
            // search the skisn
            skinRandomizer.UILayer.InstalledSkins = new System.Collections.ObjectModel.ObservableCollection<SkinInfo>(skinRandomizer.fileLogic.GetInstalledSkins(skinRandomizer.UILayer.OsuSkinFolder));
            // check files
            skinRandomizer.UILayer.InstalledSkins = new System.Collections.ObjectModel.ObservableCollection<SkinInfo>(skinRandomizer.fileLogic.GatherSkinnableElements(skinRandomizer.UILayer.InstalledSkins.ToList()));
            await controller.CloseAsync();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LookForInstalledSkins();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            skinRandomizer.fileLogic.DetermineWhatSkinnablesToUse(skinRandomizer.UILayer.RandomizerOptions);

            skinRandomizer.UILayer.UserGeneratedSkin = skinRandomizer.CreateSkin();
            skinRandomizer.fileLogic.SaveCreatedSkin(skinRandomizer.UILayer.UserGeneratedSkin);
            LookForInstalledSkins();
        }
    }
}
