using Microsoft.VisualBasic;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Models.Map
{
    [KnownType(typeof(TerritoireDecorator))]
    [DataContract]
    public class TerritoireDecorator : ITerritoireBase
    {
        [DataMember]
        private TerritoireBase _territoire;
        [DataMember]
        protected int _x;
        [DataMember]
        protected int _y;
        [DataMember]
        private int width;
        [DataMember]
        private int height;
        [DataMember]
        protected string _UriSource;

        public TerritoireDecorator(TerritoireBase territoire, int x, int y, int width, int height, string UriSource)
        {
            this._territoire = territoire;
            _x = x;
            _y = y;
            _UriSource = UriSource;
            Width = width;
            Height = height;
            if (territoire.Team != this.Team)
            {
                throw new Exception();
            }

        }

        public TerritoireBase TerritoireBase { get { return _territoire; } }
        public int x => this._x;
        public int y => this._y;
        public string UriSource { get => this._UriSource; set => this._UriSource = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int ID { get => _territoire.ID; set => _territoire.ID = value; }
        public Teams Team { get => _territoire.Team; set => _territoire.Team = value; }
        public List<UniteBase> Units { get => _territoire.Units; set => _territoire.Units = value; }

        public override string? ToString()
        {
            return $"{x},{y}";
        }

        public void AddUnit(UniteBase UniteBase)
        {
            _territoire.AddUnit(UniteBase);
        }
    }
}
