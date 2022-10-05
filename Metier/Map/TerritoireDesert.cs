using System.Windows.Media.Imaging;

namespace Metier.Map
{
    /// <summary>
    /// Classe représentant les déserts du plateau
    /// </summary>
    public class TerritoireDesert : TerritoireBase
    {
        public TerritoireDesert(BitmapImage s, double x, double y) : base(s, x, y)
        {
        }
    }
}
