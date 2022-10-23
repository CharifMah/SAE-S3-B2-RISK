using System.Windows.Media.Imaging;


namespace Models.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    public class TerritoireBase
    {
        protected BitmapImage sprite;
        protected double x;
        protected double y;

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

        /// <summary>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public TerritoireBase(BitmapImage s, double x, double y)
        {
            sprite = s;
            this.x = x;
            this.y = y;
        }
    }
}