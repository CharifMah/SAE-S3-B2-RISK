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
    [DataContract]
    public class TerritoireDecorator : TerritoireBase, ITerritoireBase
    {
        [DataMember]
        protected TerritoireBase _territoire;
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
        }

        public TerritoireBase TerritoireBase { get { return _territoire; } }

        public int x => this._x;

        public int y => this._y;

        public string UriSource { get => this._UriSource; set => this._UriSource = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public override string? ToString()
        {
            return $"{x},{y}";
        }
    }
}
