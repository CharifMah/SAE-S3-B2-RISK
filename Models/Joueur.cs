﻿using Models.Map;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Joueur : IGestionTroupe
    {
        private Teams equipe;
        private List<IMakeUnit> troupe;

        public Teams Equipe
        {
            get { return equipe; }
            set { equipe = value; }
        }

        public List<IMakeUnit> Troupe
        {
            get { return troupe; }
            set { troupe = value; }
        }

        public Joueur()
        {
            troupe = new List<IMakeUnit>();
        }

        public void PositionnerTroupe(List<IMakeUnit> unites, TerritoireBase territoire)
        {
            if(equipe.ToString() == territoire.Team.ToString())
            {
                foreach(var unit in unites)
                {
                    if (troupe.Contains(unit))
                    {
                        troupe.Remove(unit);
                    }
                    else
                    {
                        throw new Exception("Not enough unit !");
                    }
                }
                territoire.MaJTroupe(unites);
            }
            else
            {
                throw new Exception("Not your territory !")
            }
        }
    }
}
