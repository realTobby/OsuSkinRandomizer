using SkinRandomizer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SkinRandomizer.Forms
{
    /// <summary>
    /// Interaktionslogik für StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        private StartupCheck starter = new StartupCheck();
        private bool _isStartupSuccessfull = false;
        public StartupWindow()
        {
            InitializeComponent();
            this.Show();
            
            if (starter.FindOsuFolder() == FolderStatus.FOUND)
            {
                _isStartupSuccessfull = true;
            }
            else
            {
                MessageBox.Show("Folder not found! Please specify it manually!");
                _isStartupSuccessfull = false;
            }
        }

        public string GetOsuFolder()
        {
            Thread.Sleep(3000);

            return starter.GetDirectoryString();
        }


    }
}
