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
        public Files fileLogic; // used for file processes

        public MainWindow()
        {
            InitializeComponent();
            fileLogic = new Files();
            skinRandomizer = new Randomize();
            this.DataContext = skinRandomizer.UILayer;

            OnUILoad();
        }

        public void OnUILoad()
        {
            // load cached file => check for changes in the osu-skin-folder
            // if changes visible => refresh installed folder
            // show the installed skins in a list

            // poc => should be move somewhere else:

            // get list of installed skins

            skinRandomizer.UILayer.InstalledSkins = new System.Collections.ObjectModel.ObservableCollection<InstalledSkin>(fileLogic.GetInstalledSkins());

        }
    }
}
