using JurassicRisk.observable;
using Models.Exceptions;
using Models.Map;
using Models.Player;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JurassicRisk.ViewsModels
{
    public class JoueurViewModel : observable.Observable
    {
        private Joueur _joueur;

        public Joueur Joueur
        {
            get { return _joueur; }
        }

        public int NombreTrp
        {
            get { return _joueur.Troupe.Count(); }
        }

        public JoueurViewModel()
        {
            _joueur = new Joueur(ProfilViewModel.Instance.SelectedProfil, new List<UniteBase>() { new UniteBase(), new UniteBase() }, Models.Teams.VERT);
        }

        public void PositionnerTroupe(List<UniteBase> UniteBases, ITerritoireBase territoire)
        {
            if (_joueur.Equipe == territoire.Team || territoire.Team == Models.Teams.NEUTRE)
            {
                _joueur.PositionnerTroupe(UniteBases,territoire);                    
            }
            else
            {
                MessageBox.Show(new NotYourTerritoryException("Not your territory !").Message);
            }
            NotifyPropertyChanged("NombreTrp");
         
        }
    }
}
