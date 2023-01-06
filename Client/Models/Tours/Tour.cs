using Models;
using Models.Exceptions;
using Models.Fabriques.FabriqueUnite;
using Models.Fight;
using Models.Player;
using Models.Units;
using System;

namespace Models.Tours
{
    public class Tour
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

        /* Plus possible de récupérer la carte
        public void Strengthen(int nbRenfort)
        {
            FabriqueUniteBase f = new FabriqueUniteBase();
            Random random = new Random();
            for(int i = 0; i < nbRenfort; i++)
            {
                switch (random.Next(4))
                {
                    case 0:
                        _joueur.Units.Add(new UniteDecorator(f.Create("Rex")));
                        break;
                    case 1:
                        _joueur.Units.Add(new UniteDecorator(f.Create("Brachiosaurus")));
                        break;
                    case 2:
                        _joueur.Units.Add(new UniteDecorator(f.Create("Baryonyx")));
                        break;
                    case 3:
                        _joueur.Units.Add(new UniteDecorator(f.Create("Pterosaure")));
                        break;
                }
            } // Attribution des renforts

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
        }
        */

        public void Attack()
        {

        }

        public void Move()
        {

        }


        public void TerminerTour()
        {
            _tourEnd = true;
        }
    }
}
