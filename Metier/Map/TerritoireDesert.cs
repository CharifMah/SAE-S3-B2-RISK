using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Metier.Map
{
    /// <summary>
    /// Classe représentant les déserts du plateau
    /// </summary>
    public class TerritoireDesert : TerritoireBase
    {
        public TerritoireDesert(BitmapImage s) : base(s)
        {
        }
    }
}
