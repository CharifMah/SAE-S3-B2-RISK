﻿using Models.Exceptions;
using Models.GameStatus;
using Models.Map;
using Models.Player;
using Models.Units;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using IUnit = Models.Units.IUnit;

namespace JurassicRisk.ViewsModels
{
    public class JoueurViewModel : observable.Observable
    {
        #region Attributes
        private string _isReady;
        private Joueur _joueur;
        private ObservableCollection<IUnit> _units;
        private IUnit? _selectedUnit;
        #endregion

        #region Property

        public Joueur Joueur
        {
            get { return _joueur; }
            set
            {
                _joueur = value;

                NotifyPropertyChanged("Joueur");
            }
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
            set
            { _units = value; NotifyPropertyChanged(); }
        }

        public IUnit SelectedUnit
        {
            get { return _selectedUnit; }
            set
            {
                _selectedUnit = value;
                NotifyPropertyChanged("SelectedUnit");
            }
        }

        public string IsReady
        {
            get
            {
                if (_joueur.IsReady)
                    _isReady = "✅";
                else
                    _isReady = "❌";
                return _isReady;
            }

            set
            {
                _isReady = value;

                if (_isReady == "✅")
                    _joueur.IsReady = true;
                else
                    _joueur.IsReady = false;

                JurassicRiskViewModel.Get.LobbyVm.IsReady().Wait();
                NotifyPropertyChanged("IsReady");
            }
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
        public void PlaceUnits(List<int> UniteBases, ITerritoireBase territoire)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if ((_joueur.Team == territoire.Team || territoire.Team == Teams.NEUTRE) && _selectedUnit != null)
                {
                    if (_units.Count > 0)
                        _selectedUnit = _units[0];

                    _joueur.PlaceUnits(UniteBases, territoire);
                }
                NotifyPropertyChanged("NombreTrp");
                NotifyPropertyChanged("Units");
            });
        }

        /// <summary>
        /// Ajoute des Unites a un territoire
        /// </summary>
        /// <param name="UniteBases">Les unite a ajouter</param>
        /// <param name="territoire">le territoire</param>
        public void PlaceUnit(int indexUnit, ITerritoireBase territoire)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_units.Count > 0 && (_joueur.Team == territoire.Team || territoire.Team == Teams.NEUTRE && _selectedUnit != null))
                {
                    Partie p = JurassicRiskViewModel.Get.PartieVm.Partie;
                    p.Joueurs[p.PlayerIndex].RemoveUnit(indexUnit);
                    territoire.AddUnit(_units[indexUnit]);
                    territoire.Team = p.Joueurs[p.PlayerIndex].Team;
                    _units.RemoveAt(indexUnit);

                }
                NotifyPropertyChanged("NombreTrp");
                NotifyPropertyChanged("Units");
            });

        }
    }
}
