using System.Windows.Media.Imaging;


namespace Metier.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    public class TerritoireBase
    {
        protected BitmapImage sprite;
        protected double x;
        protected double y;

        public TerritoireBase(BitmapImage s)
        {
            sprite = s;
            x = 0;
            y = 0;
        }

        public BitmapImage Sprite
        {
            get { return sprite; }
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }
    }
}