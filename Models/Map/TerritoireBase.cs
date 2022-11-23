﻿using Models.Units;
using System.Runtime.Serialization;

namespace Models.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    [DataContract]
    public class TerritoireBase : ITerritoireBase, IGestionTroupe
    {
        [DataMember]
        protected Teams _teams;
        [DataMember]
        protected List<Unite> _troupe;
        [DataMember]
        protected int _id;

        public Teams Team { get => this._teams; set => this._teams = value; }
        public int ID { get => this._id; set => this._id = value; }

        public List<Unite> Units
        {
            get => this._troupe;
            set => this._troupe = value;
        }

        public TerritoireBase(int id)
        {
            this._id = id;
            this._teams = Teams.NEUTRE;
            this._troupe = new List<Unite>();
        }


        public void AddUnit(Unite unit)
        {
            _troupe.Add(unit);
        }


        public void RemoveUnit(Unite unit)
        {
            _troupe.Remove(unit);
        }


        public void AddUnit(List<Unite> unites, TerritoireBase territoire)
        {
            throw new NotImplementedException();
        }

        public void RemoveUnit(List<Unite> unites, TerritoireBase territoire)
        {
            throw new NotImplementedException();
        }
    }
}