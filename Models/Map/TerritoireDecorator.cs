using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Models.Map
{
    public class TerritoireDecorator : TerritoireBase
    {
        protected TerritoireBase _territoire;
        protected int _x;
        protected int _y;
        private int width;
        private int height;
        protected BitmapImage _sprite;

        public TerritoireDecorator(TerritoireBase territoire,int x, int y, int width, int height, BitmapImage sprite)
        {
            this._territoire = territoire;
            _x = x;
            _y = y;
            _sprite = sprite;
            Width = width;
            Height = height;
        }

        public int x => this._x;

        public int y => this._y;

        public BitmapImage Sprite { get => this._sprite; set => this._sprite = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public override string? ToString()
        {
            return $"{x},{y}";
        }
    }
}
