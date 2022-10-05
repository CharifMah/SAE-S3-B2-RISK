using System.Windows.Media.Imaging;


namespace Metier.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    public class TerritoireBase
    {
        protected BitmapImage sprite;

        public TerritoireBase(BitmapImage s)
        {
            sprite = s;
        }
    }
}