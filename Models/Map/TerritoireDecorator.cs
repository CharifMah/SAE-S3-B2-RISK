﻿using Models.Units;
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
        protected string _UriSource;
        #endregion

        #region Property
        [DataMember]
        public TerritoireBase TerritoireBase { get { return _territoire; } set => _territoire = value; }
        [DataMember]
        public int x
        {
            get { return _x; }
            set { _x = value; }
        }
        [DataMember]
        public int y
        {
            get { return this._y; }
            set => this._y = value;
        }

        [DataMember]
        public string UriSource { get => this._UriSource; set => this._UriSource = value; }
        [DataMember]
        public int Width { get => width; set => width = value; }
        [DataMember]
        public int Height { get => height; set => height = value; }

        public int ID { get => _territoire.ID; set => _territoire.ID = value; }
        public Teams Team { get => _territoire.Team; set => _territoire.Team = value; }
        public List<IUnit> Units { get => _territoire.Units; set => _territoire.Units = value; }

        #endregion

        public TerritoireDecorator(TerritoireBase TerritoireBase, int x, int y, int Width, int Height, string UriSource)
        {
            this._territoire = TerritoireBase;
            _x = x;
            _y = y;
            _UriSource = UriSource;
            this.Width = Width;
            this.Height = Height;
            if (TerritoireBase.Team != this.Team)
            {
                throw new Exception();
            }

        }

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
