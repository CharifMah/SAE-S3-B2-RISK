using System.Runtime.Serialization;

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
        private int width;
        private int height;
        protected string uriSource;
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
        public string UriSource { get => this.uriSource; set => this.uriSource = value; }
        [DataMember]
        public int Width { get => width; set => width = value; }
        [DataMember]
        public int Height { get => height; set => height = value; }

        #endregion

        public TerritoireDecorator(TerritoireBase TerritoireBase, int X, int Y, int Width, int Height, string UriSource)
        {
            this._territoire = TerritoireBase;
            this._x = X;
            this._y = Y;
            this._id = TerritoireBase.ID;
            this.uriSource = UriSource;
            this.Width = Width;
            this.Height = Height;
            if (TerritoireBase.Team != this.Team)
            {
                TerritoireBase.Team = this.Team;
            }

        }

        public TerritoireDecorator(TerritoireBase TerritoireBase, string UriSource)
        {
            this._territoire = TerritoireBase;         
            this._id = TerritoireBase.ID;
            this.uriSource = UriSource;
            if (TerritoireBase.Team != this.Team)
            {
                TerritoireBase.Team = this.Team;
            }
        }

        public TerritoireDecorator()
        {

        }
    }
}
