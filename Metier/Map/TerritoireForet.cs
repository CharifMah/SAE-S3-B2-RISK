using System.Windows.Media.Imaging;

namespace Models.Map
{
    /// <summary>
    /// Classe représentant les forêts du plateau
    /// </summary>
    public class TerritoireForet : TerritoireBase
    {
        public TerritoireForet(BitmapImage s, double x, double y) : base(s, x, y)
        {
        }
    }
}
