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
        private ObservableCollection<SkinInfo> ocInstalledSkins = new ObservableCollection<SkinInfo>();
        private SkinInfo userGeneratedSkin = new SkinInfo();
        private string osuSkinFolder = "";
        private RandomizerOptions randomizerOptions = new RandomizerOptions();
        private bool randomizeInterface = false;
        private bool randomizeStandard = false;
        #endregion

        #region Publics
        public bool RandomizeInterface
        {
            get
            {
                return randomizerOptions.RandomizeInterface;
            }
            set
            {
                randomizerOptions.RandomizeInterface = value;
                OnPropertyChanged(nameof(RandomizeInterface));
            }
        }

        public bool RandomizeStandard
        {
            get
            {
                return randomizerOptions.RandomizeStandard;
            }
            set
            {
                randomizerOptions.RandomizeStandard = value;
                OnPropertyChanged(nameof(RandomizeStandard));
            }
        }

        public RandomizerOptions RandomizerOptions
        {
            get
            {
                return randomizerOptions;
            }
            set
            {
                randomizerOptions = value;
                OnPropertyChanged(nameof(RandomizerOptions));
            }
        }

        public string OsuSkinFolder
        {
            get
            {
                return osuSkinFolder;
            }
            set
            {
                osuSkinFolder = value;
                OnPropertyChanged(nameof(OsuSkinFolder));
            }
        }

        public SkinInfo UserGeneratedSkin
        {
            get
            {
                return userGeneratedSkin;
            }
            set
            {
                userGeneratedSkin = value;
                OnPropertyChanged(nameof(UserGeneratedSkin));
            }
        }
        public ObservableCollection<SkinInfo> InstalledSkins
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
