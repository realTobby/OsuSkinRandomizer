using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.ViewModels
{
    public class ViewModel : BaseViewModel
    {
        private string _windowTitle = "OsuSkinRandomizer v2.0";
        public string WindowTitle
        {
            get 
            { 
                return _windowTitle;
            }
            set 
            {
                _windowTitle = value;
                base.OnPropertyChanged(nameof(WindowTitle));
            }
        }

        private bool _isSkinFolderFound = false;
        public bool IsSkinFolderFound
        {
            get
            {
                return _isSkinFolderFound;
            }
            set
            {
                _isSkinFolderFound = value;
                base.OnPropertyChanged(nameof(IsSkinFolderFound));
            }
        }

        private string _osuFolder = "";
        public string OsuFolder
        {
            get
            {
                return _osuFolder;
            }
            set
            {
                _osuFolder = value;
                base.OnPropertyChanged(nameof(OsuFolder));
            }
        }
    }
}
