using SkinRandomizer.Forms;
using SkinRandomizer.Logic;
using SkinRandomizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                StartupWindow sw = new StartupWindow();
                try
                {
                    var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
                    Task.Run(() =>
                    {
                        myViewModel.OsuFolder = sw.GetOsuFolder();
                    }).ContinueWith(task =>
                    {
                        sw.Close();
                    }, uiScheduler);
                }
                catch (System.InvalidOperationException sio)
                {
                    sw.Close();
                }
            }
            sh.Save(myViewModel.OsuFolder);
        }
    }
}
