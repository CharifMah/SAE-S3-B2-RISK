using Models.Exceptions;
using Models.Map;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Joueur
{
    public class Joueur : IGestionTroupe
    {
        #region Attribute

        private Teams _equipe;
        private List<Unite> _troupe;
        private Profil _profil;

        #endregion

        #region Property

        public Teams Equipe
        {
            get { return _equipe; }
            set { _equipe = value; }
        }

        public List<Unite> Troupe
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

        public Joueur(Profil profil, List<Unite> troupe, Teams equipe)
        {
            _troupe = troupe;
            _profil = profil;
            _equipe = equipe;
        }

        #endregion

        public void PositionnerTroupe(List<Unite> unites, ITerritoireBase territoire)
        {
            if (_equipe == territoire.Team)
            {
                foreach (var unit in unites)
                {
                    if (_troupe.Contains(unit))
                    {
                        _troupe.Remove(unit);

                        territoire.AddUnit(unit);
                    }
                    else
                    {
                        throw new NotEnoughUnitException("Not enough unit !");
                    }
                }
            }
            else
            {
                throw new NotYourTerritoryException("Not your territory !");
            }
        }
    }
}
