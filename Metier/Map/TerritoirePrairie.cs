using System.Windows.Media.Imaging;

namespace Metier.Map
{
    /// <summary>
    /// Classe représentant les prairies du plateau
    /// </summary>
    public class TerritoirePrairie : TerritoireBase
    {
        public TerritoirePrairie(BitmapImage s, double x, double y) : base(s, x, y)
        {
        }
    }
}
