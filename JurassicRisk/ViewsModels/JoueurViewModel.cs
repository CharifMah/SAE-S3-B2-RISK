using Models;
using Models.Exceptions;
using Models.Fabriques.FabriqueUnite;
using Models.Map;
using Models.Player;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IUnit = Models.Units.IUnit;

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
            get { return _joueur.Units.Count(); }
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

        public JoueurViewModel(Teams teams = Teams.NEUTRE)
        {
            _joueur = new Joueur(ProfilViewModel.Get.SelectedProfil, teams);
            _units = new ObservableCollection<IUnit>(_joueur.Units);
            _selectedUnit = _units[0];

            NotifyPropertyChanged("Units");

        }

        /// <summary>
        /// Ajoute des Unites a un territoire
        /// </summary>
        /// <param name="UniteBases">Les unite a ajouter</param>
        /// <param name="territoire">le territoire</param>
        public void AddUnits(List<IUnit> UniteBases, ITerritoireBase territoire)
        {
            if ((_joueur.Team == territoire.Team || territoire.Team == Teams.NEUTRE) && _selectedUnit != null)
            {
                _joueur.AddUnits(UniteBases, territoire);
                this._units.Remove(_selectedUnit);
                if (_units.Count > 0)
                    _selectedUnit = _units[0];
            }
            else
            {
                MessageBox.Show(new NotYourTerritoryException("Not your territory !").Message);
            }
            NotifyPropertyChanged("NombreTrp");
            NotifyPropertyChanged("Units");
        }

        public async Task SetTeam(Teams team)
        {
            _joueur.Team = team;
        }
    }
}
