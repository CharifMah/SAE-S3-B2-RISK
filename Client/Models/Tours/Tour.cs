﻿using Models;
using Models.Combat;
using Models.Exceptions;
using Models.Fabriques.FabriqueUnite;
using Models.Fight;
using Models.Player;
using Models.Units;
using System;

namespace Models.Tours
{
    public class Tour : ITour
    {
        private Joueur _joueur;
        private bool _tourEnd;
        private bool _phaseEnd;

        public bool TourEnd => false;

        public bool PhaseEnd => false;

        public Tour(Joueur joueur)
        {
            _joueur = joueur;
            _phaseEnd = false;
            _tourEnd = false;
        }

        /// <summary>
        /// Gestion de la phase de renforcement
        /// </summary>
        /// <param name="nbRenforts">Renforts calculé par la division par 3 des territoires occupés par le joueur</param>
        public void Strengthen(int nbRenforts)
        {
            // Attribution des renforts
            FabriqueUniteBase f = new FabriqueUniteBase();
            Random random = new Random();
            for (int i = 0; i < nbRenforts; i++)
            {
                switch (random.Next(4))
                {
                    case 0:
                        _joueur.AddUnit(f.Create("Rex"));
                        break;
                    case 1:
                        _joueur.AddUnit(f.Create("Brachiosaurus"));
                        break;
                    case 2:
                        _joueur.AddUnit(f.Create("Baryonyx"));
                        break;
                    case 3:
                        _joueur.AddUnit(f.Create("Pterosaure"));
                        break;
                }
            }

            
        }

            if (_joueur.Units.Count > 0)
            {
                
                if(JurasicRiskGameClient.Get.Carte.SelectedTerritoire.Team == _joueur.Team)
                {
                    IUnit unitToPlace = _joueur.Units[0];
                    _joueur.AddUnits((List<IUnit>)unitToPlace, JurasicRiskGameClient.Get.Carte.SelectedTerritoire);
                }
                else
                {
                    throw new NotYourTerritoryException();
                }
            }
            else
            {
                throw new NotEnoughUniteBasexception();
            }
            /*
            Combat c = new Combat();
            */
        }
        */

        public void Attack()
        {
            Combat c = new Combat();

        }




        public void TerminerTour()
        {
            _tourEnd = true;
        }
    }
}
