using JurassicRisk.observable;
using JurassicRisk.Ressources;
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
using System.Windows.Media.Imaging;

namespace JurassicRisk.ViewsModels
{
    public class JoueurViewModel : observable.Observable
    {
        #region Attributes
        private Joueur _joueur;
        private ObservableCollection<IUnit> _units;
        private IUnit _selectedUnit;
        #endregion

        #region Property

        public Joueur Joueur
        {
            get { return _joueur; }
        }

        public int NombreTrp
        {
            get { return _joueur.Troupe.Count(); }
        }

        public ObservableCollection<IUnit> Units
        {
            get
            {
                return _units;
            }
        }

        public IUnit SelectedUnit
        {
            get { return _selectedUnit; }
            set { _selectedUnit = value; }
        }

        #endregion


        public JoueurViewModel()
        {
            FabriqueUniteBase f = new FabriqueUniteBase();
            _units = new ObservableCollection<IUnit>();
          
            #region CreatePlayer

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
            _joueur = new Joueur(ProfilViewModel.Instance.SelectedProfil, _units.ToList(), Models.Teams.VERT);

            #endregion

            NotifyPropertyChanged("Units");

        }

        /// <summary>
        /// Ajoute des Unites a un territoire
        /// </summary>
        /// <param name="UniteBases">Les unite a ajouter</param>
        /// <param name="territoire">le territoire</param>
        public bool AddUnits(List<IUnit> UniteBases, ITerritoireBase territoire)
        {
            bool res = false;
            if (UniteBases.Count > 0 && (_joueur.Equipe == territoire.Team || territoire.Team == Models.Teams.NEUTRE))
            {
                _joueur.AddUnits(UniteBases, territoire);
                res = true;
            }
            else
            {
                MessageBox.Show(new NotYourTerritoryException("Not your territory !").Message);
            }
            NotifyPropertyChanged("NombreTrp");
            NotifyPropertyChanged("Units");

            return res;

        }
    }
}
