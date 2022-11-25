using JurassicRisk.observable;
using Models.Exceptions;
using Models.Fabriques.FabriqueUnite;
using Models.Map;
using Models.Player;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public ObservableCollection<UniteBase> Units
        {
            get
            {
                return new ObservableCollection<UniteBase>(_joueur.Troupe);
            }
        }

        public JoueurViewModel()
        {
            FabriqueUniteBase f = new FabriqueUniteBase();
            List<UniteBase> units = new List<UniteBase>();
            Random random= new Random();
            for (int i = 0; i < 40; i++)
            {
                switch (random.Next(4))
                {
                    case 0:
                        units.Add(f.Create("rex"));
                        break;
                    case 1:
                        units.Add(f.Create("brachiosaure"));
                        break;
                    case 2:
                        units.Add(f.Create("baryonix"));
                        break;
                    case 3:
                        units.Add(f.Create("pterosaure"));
                        break;
                }
            }
            _joueur = new Joueur(ProfilViewModel.Instance.SelectedProfil, units, Models.Teams.VERT);
            NotifyPropertyChanged("Units");

        }

        public void PositionnerTroupe(List<UniteBase> UniteBases, ITerritoireBase territoire)
        {
            if (UniteBases.Count > 0 && (_joueur.Equipe == territoire.Team || territoire.Team == Models.Teams.NEUTRE))
            {
                _joueur.AddUnit(UniteBases, territoire);
            }
            else
            {
                MessageBox.Show(new NotYourTerritoryException("Not your territory !").Message);
            }
            NotifyPropertyChanged("NombreTrp");
            NotifyPropertyChanged("Units");

        }
    }
}
