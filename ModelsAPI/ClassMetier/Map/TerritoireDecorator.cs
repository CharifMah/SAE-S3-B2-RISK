using ModelsAPI.ClassMetier.Units;
using ModelsAPI.Converters;
using Newtonsoft.Json;

namespace ModelsAPI.ClassMetier.Map
{
    public class TerritoireDecorator : TerritoireBase
    {

        #region Attributes

        private TerritoireBase _territoire;

        protected int _x;

        protected int _y;

        private int width;

        private int height;

        protected string uriSource;
        #endregion

        #region Property
        public TerritoireBase TerritoireBase { get { return _territoire; } set => _territoire = value; }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return this._y; }
            set => this._y = value;
        }
        public string UriSource { get => uriSource; set => uriSource = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        #endregion

        #region Constructor
        public TerritoireDecorator(TerritoireBase TerritoireBase, int X, int Y, int Width, int Height, string UriSource)
        {
            _territoire = TerritoireBase;
            _x = X;
            _y = Y;
            uriSource = UriSource;
            this.Width = Width;
            this.Height = Height;
            if (TerritoireBase.Team != this.Team)
            {
                TerritoireBase.Team = this.Team;
            }

        }

        public TerritoireDecorator()
        {

        }

        #endregion
    }
}
