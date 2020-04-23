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
        private SkinGenerator sg;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(System.IO.Directory.Exists(myViewModel.OsuFolder + @"\EpicRandomSkin"))
            {
                MessageBox.Show("currently, you need to delete the old random skin before you create a new one :/");
            }
            else
            {
                sg = new SkinGenerator(myViewModel.OsuFolder, "EpicRandomSkin");
                sg.Generate();
                MessageBox.Show("New skin created! its probably hot garbage :/");
            }
            
        }
    }
}
