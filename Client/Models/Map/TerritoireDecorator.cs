using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Shapes;

namespace Models.Map
{

    [DataContract]
    [KnownType(typeof(TerritoireDecorator))]
    public class TerritoireDecorator : TerritoireBase
    {
        #region Attributes
        private TerritoireBase _territoire;
        protected int _x;
        protected int _y;
        private int _width;
        private int _height;
        protected string _uriSource;
        private List<Line> _lines;
        private List<Point> _points;
        #endregion

        #region Property
        [DataMember]
        public TerritoireBase TerritoireBase { get { return _territoire; } set => _territoire = value; }
        [DataMember]
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        [DataMember]
        public int Y
        {
            get { return this._y; }
            set => this._y = value;
        }

        [DataMember]
        public string UriSource { get => this._uriSource; set => this._uriSource = value; }
        [DataMember]
        public int Width { get => _width; set => _width = value; }
        [DataMember]
        public int Height { get => _height; set => _height = value; }
        [DataMember]
        public List<Line> Lines { get => _lines; set => _lines = value; }
        [DataMember]
        public List<Point> Points { get => _points; set => _points = value; }


        #endregion

        public TerritoireDecorator(TerritoireBase TerritoireBase, int X, int Y, int Width, int Height, string UriSource)
        {
            this._territoire = TerritoireBase;
            this._x = X;
            this._y = Y;
            this._id = TerritoireBase.ID;
            this._uriSource = UriSource;
            this._width = Width;
            this._height = Height;
            _lines = new List<Line>();
            _points = new List<Point>();
            if (TerritoireBase.Team != this.Team)
            {
                TerritoireBase.Team = this.Team;
            }

        }

        public TerritoireDecorator(TerritoireBase TerritoireBase, string UriSource)
        {
            this._territoire = TerritoireBase;         
            this._id = TerritoireBase.ID;
            this._uriSource = UriSource;
            _lines = new List<Line>();
            _points = new List<Point>();
            if (TerritoireBase.Team != this.Team)
            {
                TerritoireBase.Team = this.Team;
            }
        }

        public TerritoireDecorator()
        {
            _lines = new List<Line>();
            _points = new List<Point>();
        }
    }
}
