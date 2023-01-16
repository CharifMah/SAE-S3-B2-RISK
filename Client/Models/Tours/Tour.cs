using Models;
using Models.Fight;
using Models.Exceptions;
using Models.Fabriques.FabriqueUnite;
using Models.Map;
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
        private Carte carte;
        private ITerritoireBase? selectedTerritory;

        public bool TourEnd => false;

        public bool PhaseEnd => false;

        public Tour(Joueur joueur)
        {
            _joueur = joueur;
            _phaseEnd = false;
            _tourEnd = false;
            carte = JurasicRiskGameClient.Get.Lobby.Partie.Carte;
        }

        /// <summary>
        /// Gestion du renforcement d'un territoire (à répéter jusqu'à ce que tout les renforts soient posés)
        /// </summary>
        /// <param name="nbRenforts">Nombre de renforts a placé sur le territoire choisi</param>
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


            selectedTerritory = carte.SelectedTerritoire;
            if(selectedTerritory != null)
            {
                for(int i=0; i < nbRenforts; i++)
                {
                    selectedTerritory.AddUnit(_joueur.Units[0]);
                    _joueur.RemoveUnit(_joueur.Units[0]);
                }
            }
        }

        public void Attack()
        {
            // Récupérer Joueur attaquant (attribut _joueur)
            // Récupérer infos adversaires (joueur cible)
            // Récupérer le territoire attaquant (selected territory)
            // Récupérer le territoire attaqué (2nd selected territory)
            // Récupérer le nombre et les troupes envoyés à l'attaque
            // Récupérer le nombre et les troupes envoyés en défense

            /*
            Combat c = new Combat();
            */
        }
            

        public void Move()
        {
            // Récupérer le territoire 
            // Récupérer le territoire où envoyer les troupes
            // Ajouter les troupes au territoire cible
            // Retirer les troupes au 1er territoire 
        }

        public void TerminerTour()
        {
            _tourEnd = true;
        }
    }
}
