﻿using ModelsAPI.ClassMetier.Player;
using ModelsAPI.ClassMetier.Units;
using Newtonsoft.Json;
using Stockage.Converters;

namespace ModelsAPI.ClassMetier.Map
{
    /// <summary>
    /// Classe générique des territoires
    /// </summary>
    public class TerritoireBase : ITerritoireBase
    {
        #region Attributes
        protected Teams _teams = Teams.NEUTRE;
        protected List<IUnit> _units;
        protected int _id;
        #endregion

        #region Property
        public Teams Team { get => _teams; set => _teams = value; }
        public int ID { get => _id; set => _id = value; }

        [JsonConverter(typeof(ConcreteCollectionTypeConverter<List<IUnit>,UniteBase, IUnit>))]
        public List<IUnit> Units
        {
            get => _units;
            set => _units = value;
        }

        #endregion

        #region Constructor
        public TerritoireBase(int id)
        {
            _id = id;
            _teams = Teams.NEUTRE;
            _units = new List<IUnit>();
        }

        [JsonConstructor]
        public TerritoireBase(int ID, List<IUnit> Units, Teams Team = Teams.NEUTRE)
        {
            this._id = ID;
            this._teams = Team;
            if (Units == null)
                this._units = new List<IUnit>();
            else
                this._units = Units;
        }

        public TerritoireBase()
        {
            this._units = new List<IUnit>();
        }
        #endregion


        public void AddUnit(IUnit UniteBase)
        {
            _units.Add(UniteBase);
        }

        public void RemoveUnit(IUnit UniteBase)
        {
            _units.Remove(UniteBase);
        }
    }
}