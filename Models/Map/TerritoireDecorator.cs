using Models.Units;
using ModelsAPI.Converters;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Models.Map
{

    [DataContract]
    [KnownType(typeof(TerritoireDecorator))]
    public class TerritoireDecorator : ITerritoireBase
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

        public int ID { get => _territoire.ID; set => _territoire.ID = value; }
        public Teams Team { get => _territoire.Team; set => _territoire.Team = value; }
        [JsonConverter(typeof(ConcreteCollectionTypeConverter<List<IUnit>, UniteBase, IUnit>))]
        public List<IUnit> Units { get => _territoire.Units; set => _territoire.Units = value; }

        #endregion

        public TerritoireDecorator(TerritoireBase TerritoireBase, int X, int Y, int Width, int Height, string UriSource)
        {
            this._territoire = TerritoireBase;
            this._x = X;
            this._y = Y;
            this.uriSource = UriSource;
            this.Width = Width;
            this.Height = Height;
            if (TerritoireBase.Team != this.Team)
            {
                throw new Exception();
            }

        }

        public TerritoireDecorator()
        {

        }

        public override string? ToString()
        {
            return $"{_x},{_y}";
        }

        public void AddUnit(IUnit UniteBase)
        {
            _territoire.AddUnit(UniteBase);
        }

        public void RemoveUnit(IUnit UniteBase)
        {
            _territoire.RemoveUnit(UniteBase);
        }
    }
}
