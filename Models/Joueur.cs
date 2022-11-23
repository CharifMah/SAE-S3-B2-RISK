using Models.Exceptions;
using Models.Map;
using Models.Units;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Joueur : IGestionTroupe
    {
        private Teams equipe;
        private List<Unite> troupe;

        public Teams Equipe
        {
            get { return equipe; }
            set { equipe = value; }
        }

        public List<Unite> Troupe
        {
            get { return troupe; }
            set { troupe = value; }
        }

        public Joueur()
        {
            troupe = new List<Unite>();
        }

        public void AddUnit(List<Unite> unites, TerritoireBase territoire)
        {
            if(equipe.ToString() == territoire.Team.ToString())
            {
                foreach(var unit in unites)
                {
                    if(troupe.Contains(unit))
                    {
                        troupe.Remove(unit);
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

        public void AddUnit(Unite unit)
        {
            throw new NotImplementedException();
        }

        public void RemoveUnit(Unite unit)
        {
            throw new NotImplementedException();
        }

        public void RemoveUnit(List<Unite> unites, TerritoireBase territoire)
        {
            foreach (var unit in unites)
            {
                if (troupe.Contains(unit))
                {
                    territoire.RemoveUnit(unit);
                }
            }
        }
    }
}
