using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Models.GameStatus;
using Models.Map;
using Models.Player;
using Models.Services;
using Models.Tours;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JurassicRisk.ViewsModels
{
    public class PartieViewModel : observable.Observable
    {
        #region Attributes
        private SignalRPartieService _partieChatService;
        private Partie? _partie;
        #endregion

        #region Property

        public Partie? Partie
        {
            get => _partie;

            set
            {
                _partie = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Constructor
        public PartieViewModel(Carte carte, List<Joueur> joueurs,string id)
        {
            _partie = new Partie(carte, joueurs, id);
            _partie.Owner = JurassicRiskViewModel.Get.LobbyVm.Lobby.Owner;
            _partieChatService = JurasicRiskGameClient.Get.PartieChatService;
            _partieChatService.YourTurn += _chatService_YourTurn;
            _partieChatService.EndTurn += _chatService_EndTurn;
            _partieChatService.Connected += _partieChatService_Connected; ;
            _partieChatService.Disconnected += _partieChatService_Disconnected;
            _partieChatService.Deploiment += _partieChatService_Deploiment ;

            NotifyPropertyChanged("Partie");
           Joueur j = joueurs.Find(j => j.Profil.Pseudo == JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo);
            if (j != null)
            {
                j = JurassicRiskViewModel.Get.JoueurVm.Joueur;
            }
        }

       
        #endregion

        #region Request
        public async Task SendEndTurn()
        {
         
            await _partieChatService.SendEndTurn(JurassicRiskViewModel.Get.LobbyVm.Lobby.Id, JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo);
        }

        /// <summary>
        /// Exit actual Lobby
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ExitPartie()
        {
            await _partieChatService.ExitPartie(JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo);
            return true;
        }
        #endregion

        #region Event
        private async void _partieChatService_Connected(string connectionId)
        {
            JurassicRiskViewModel vm = JurassicRiskViewModel.Get;
            if (vm.JoueurVm.Joueur != null && connectionId != String.Empty)
            {
                vm.JoueurVm.Joueur.Profil.ConnectionId = connectionId;
                vm.PartieVm.Partie.Joueurs.Find(j => j.Profil.Pseudo == vm.JoueurVm.Joueur.Profil.Pseudo).Profil.ConnectionId = connectionId;
                JurasicRiskGameClient.Get.IsConnectedToLobby = true;
            }
            else
            {
                JurasicRiskGameClient.Get.IsConnectedToLobby = false;
            }
            await JurassicRiskViewModel.Get.LobbyVm.ExitLobby();
            await _partieChatService.ConnectedPartie(JurassicRiskViewModel.Get.LobbyVm.Lobby.Id, JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo);
        }

        private void _chatService_YourTurn(string turnType)
        {
            switch (turnType)
            {
                case "Deploiment":
                    {
                        JurassicRiskViewModel.Get.CarteVm.Tour = new TourPlacement();
                        break;
                    }
            }
        }

        private void _chatService_EndTurn()
        {
            //new tourAttente
        }

        private void _partieChatService_Disconnected()
        {
            JurassicRiskViewModel vm = JurassicRiskViewModel.Get;
            if (JurassicRiskViewModel.Get.JoueurVm.Joueur != null)
            {
                vm.JoueurVm.Joueur.Profil.ConnectionId = String.Empty;
                vm.PartieVm.Partie.Joueurs.Find(j => j.Profil.Pseudo == vm.JoueurVm.Joueur.Profil.Pseudo).Profil.ConnectionId = String.Empty;
            }
                
            JurasicRiskGameClient.Get.IsConnectedToLobby = false;
        }

        private void _partieChatService_Deploiment(int idUnit, int idTerritoire, int playerIndex)
        {
            Carte carte = JurassicRiskViewModel.Get.CarteVm.Carte;
            if (_partie.Joueurs[playerIndex].Profil.Pseudo != JurassicRiskViewModel.Get.JoueurVm.Joueur.Profil.Pseudo)
            {
                _partie.Joueurs[playerIndex].PlaceUnits(_partie.Joueurs[playerIndex].Units[idUnit], carte.GetTerritoire(idTerritoire));
            }
            else
            {
                JurassicRiskViewModel.Get.JoueurVm.Joueur.PlaceUnits(JurassicRiskViewModel.Get.JoueurVm.Joueur.Units[0], carte.GetTerritoire(idTerritoire));
            }
        }
        #endregion
    }
}
