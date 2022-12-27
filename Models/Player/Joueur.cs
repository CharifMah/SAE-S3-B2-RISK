﻿using Models.Fabriques.FabriqueUnite;
using Models.Map;
using Models.Units;
using Newtonsoft.Json;
using Stockage.Converters;

namespace Models.Player
{
    public class Joueur : IGestionTroupe
    {
        #region Attribute

        private Teams _team;
        private List<IUnit> _units;
        private Profil _profil;

        #endregion

        #region Property

        public Teams Team
        {
            get { return _team; }
            set { _team = value; }
        }
        [JsonConverter(typeof(ConcreteCollectionTypeConverter<List<IUnit>, UniteBase, IUnit>))]
        public List<IUnit> Units
        {
            get { return _units; }
            set { _units = value; }
        }

        public Profil Profil
        {
            get { return _profil; }
            set { _profil = value; }
        }

        #endregion

        #region Constructor

        public Joueur(Profil profil, Teams team)
        {
            FabriqueUniteBase f = new FabriqueUniteBase();
            _units = new List<IUnit>();
            Random random = new Random();
            for (int i = 0; i < 40; i++)
            {
                switch (random.Next(4))
                {
                    case 0:
                        _units.Add(new UniteDecorator(f.Create("Rex")));
                        break;
                    case 1:
                        _units.Add(new UniteDecorator(f.Create("Brachiosaurus")));
                        break;
                    case 2:
                        _units.Add(new UniteDecorator(f.Create("Baryonyx")));
                        break;
                    case 3:
                        _units.Add(new UniteDecorator(f.Create("Pterosaure")));
                        break;
                }
            }

            _profil = profil;
            _team = team;
        }

        #endregion
        public void AddUnits(List<IUnit> unites, ITerritoireBase territoire)
        {
            if (unites.Count > 0 && (this._team == territoire.Team || territoire.Team == Models.Teams.NEUTRE))
            {
                foreach (var unit in unites)
                {
                    if (_units.Contains(unit))
                    {
                        _units.Remove(unit);

                        territoire.AddUnit(unit);
                        territoire.Team = this._team;
                    }
                }
            }
        }

        public void AddUnit(IUnit unit)
        {
            this._units.Add(unit);
        }

        public void RemoveUnit(IUnit unit)
        {
            this._units.Remove(unit);
        }

        public void RemoveUnit(List<IUnit> unites, ITerritoireBase territoire)
        {
            foreach (var unit in unites)
            {
                if (_units.Contains(unit))
                {
                    territoire.RemoveUnit(unit);
                }
            }
        }
    }
}
