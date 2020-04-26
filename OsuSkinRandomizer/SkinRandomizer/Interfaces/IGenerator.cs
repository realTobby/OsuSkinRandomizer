using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Interfaces
{
    /// <summary>
    /// Interface to make the Generators Generic, so a new Generator can be created quite easily
    /// </summary>
    public interface IGenerator
    {
        void Init(string path, string skinname);
        void Generate();
        
    }
}
