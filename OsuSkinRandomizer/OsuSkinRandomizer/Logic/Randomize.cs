using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuSkinRandomizer.Logic
{
    /// <summary>
    /// Takes care of everything related to randomizing
    /// </summary>
    public class Randomize
    {
        public ViewModel UILayer; // used for binding with the WPF UI Layer

        public Randomize()
        {
            UILayer = new ViewModel();
        }



    }
}
