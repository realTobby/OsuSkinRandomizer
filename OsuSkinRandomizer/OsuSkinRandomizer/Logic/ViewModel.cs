using OsuSkinRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuSkinRandomizer.Logic
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Privates
        private string windowTitle = "OsuSkinRandomizer";
        private string version = "3.0";
        private ObservableCollection<InstalledSkin> ocInstalledSkins;
        #endregion

        #region Publics
        public ObservableCollection<InstalledSkin> InstalledSkins
        {
            get
            {
                return ocInstalledSkins;
            }
            set
            {
                ocInstalledSkins = value;
                OnPropertyChanged(nameof(InstalledSkins));
            }
        }

        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
                OnPropertyChanged(nameof(Version));
            }
        }

        public string WindowTitle
        {
            get
            {
                return windowTitle + " v" + Version + " - by github.com/realTobby";
            }
        }

        #endregion

    }
}
