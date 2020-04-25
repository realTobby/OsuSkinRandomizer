using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Interfaces
{
    public interface IGenerator
    {
        void Init(string path, string skinname);
        void Generate();
        
    }
}
