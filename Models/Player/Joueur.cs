﻿using Models.Exceptions;
using Models.Map;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Models.Player
{
    public class Joueur : IGestionTroupe
    {
        #region Attribute

        private Teams _equipe;
        private List<IUnit> _troupe;
        private Profil _profil;

        #endregion

        #region Property

        public Teams Equipe
        {
            get { return _equipe; }
            set { _equipe = value; }
        }

        public List<IUnit> Troupe
        {
            get { return _troupe; }
            set { _troupe = value; }
        }

        public Profil Profil
        {
            get { return _profil; }
            set { _profil = value; }
        }

        #endregion

        #region Constructor

        public Joueur(Profil profil, List<IUnit> troupe, Teams equipe)
        {
            _troupe = troupe;
            _profil = profil;
            _equipe = equipe;
        }

        #endregion
        public void AddUnits(List<IUnit> unites, ITerritoireBase territoire)
        {
            if (unites.Count > 0 && (this._equipe == territoire.Team || territoire.Team == Models.Teams.NEUTRE))
            {
                foreach (var unit in unites)
                {
                    if (_troupe.Contains(unit))
                    {
                        _troupe.Remove(unit);

                        territoire.AddUnit(unit);
                        territoire.Team = this._equipe;
                    }
                }
            }
        }

        public void AddUnit(IUnit unit)
        {
            this._troupe.Add(unit);
        }

        public void RemoveUnit(IUnit unit)
        {
            this._troupe.Remove(unit);
        }

        public void RemoveUnit(List<IUnit> unites, ITerritoireBase territoire)
        {
            foreach (var unit in unites)
            {
                if (_troupe.Contains(unit))
                {
                    territoire.RemoveUnit(unit);
                }
            }
        }      
    }
}