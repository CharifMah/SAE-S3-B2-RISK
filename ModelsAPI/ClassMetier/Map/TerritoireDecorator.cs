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

        protected string _UriSource;
        #endregion

        #region Property
        public TerritoireBase TerritoireBase { get { return _territoire; } set => _territoire = value; }
        public int x => _x;
        public int y => _y;
        public string UriSource { get => _UriSource; set => _UriSource = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int ID { get => _territoire.ID; set => _territoire.ID = value; }
        public Teams Team { get => _territoire.Team; set => _territoire.Team = value; }
        [JsonConverter(typeof(ConcreteCollectionTypeConverter<List<IUnit>, UniteBase, IUnit>))]
        public List<IUnit> Units { get => _territoire.Units; set => _territoire.Units = value; }
        #endregion

        #region Constructor
        public TerritoireDecorator(TerritoireBase TerritoireBase, int x, int y, int Width, int Height, string UriSource)
        {
            _territoire = TerritoireBase;
            _x = x;
            _y = y;
            _UriSource = UriSource;
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
            return $"{x},{y}";
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
