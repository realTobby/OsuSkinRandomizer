using SkinRandomizer.Logic;
using SkinRandomizer.ViewModels;
using System.Windows;

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
    }
}
