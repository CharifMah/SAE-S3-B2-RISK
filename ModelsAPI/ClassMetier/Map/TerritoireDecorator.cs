using ModelsAPI.ClassMetier.Units;
using ModelsAPI.Converters;
using Newtonsoft.Json;

namespace ModelsAPI.ClassMetier.Map
{
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
        public int ID { get => _territoire.ID; set => _territoire.ID = value; }
        public Teams Team { get => _territoire.Team; set => _territoire.Team = value; }
        [JsonConverter(typeof(ConcreteCollectionTypeConverter<List<IUnit>, UniteBase, IUnit>))]
        public List<IUnit> Units { get => _territoire.Units; set => _territoire.Units = value; }
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
            if (TerritoireBase.Team != Team)
            {
                throw new Exception();
            }

        }

        public TerritoireDecorator()
        {

        }

        #endregion

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
