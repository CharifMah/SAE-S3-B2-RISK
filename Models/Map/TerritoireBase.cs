﻿using Models.Units;
using System.Runtime.Serialization;

namespace Models.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    [KnownType(typeof(TerritoireBase))]
    [DataContract]
    public class TerritoireBase : ITerritoireBase
    {
        [DataMember]
        protected Teams _teams;
        [DataMember]
        protected List<IUnit> _troupe;
        [DataMember]
        protected int _id;

        public Teams Team { get => this._teams; set => this._teams = value; }
        public int ID { get => this._id; set => this._id = value; }

        public List<IUnit> Units
        {
            get => this._troupe;
            set => this._troupe = value;
        }

        public TerritoireBase(int id)
        {
            this._id = id;
            this._teams = Teams.NEUTRE;
            this._troupe = new List<IUnit>();
        }

        public TerritoireBase(List<IUnit> troupe, int id)
        {
            this._id = id;
            this._teams = Teams.NEUTRE;
            this._troupe = troupe;
        }

        public void AddUnit(IUnit UniteBase)
        {
            this._troupe.Add(UniteBase);
        }

        public void RemoveUnit(IUnit UniteBase)
        {
            this._troupe.Remove(UniteBase);
        }
    }
}