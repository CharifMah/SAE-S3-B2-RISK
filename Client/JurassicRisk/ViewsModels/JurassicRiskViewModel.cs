using Models;
using Models.Services;
using Models.Tours;
using System.Threading.Tasks;

namespace JurassicRisk.ViewsModels
{
    public class JurassicRiskViewModel
    {

        #region Attributes
        private CarteViewModel _carteVm;
        private JoueurViewModel _joueurVm;
        private LobbyViewModel _lobbyVm;
        private TaskCompletionSource<bool> _clickWaitTask;
        private SignalRLobbyService _chatService;
        #endregion

        #region Property
        public CarteViewModel CarteVm { get => _carteVm; }
        public JoueurViewModel JoueurVm { get => _joueurVm; }
        public LobbyViewModel LobbyVm { get => _lobbyVm; set => _lobbyVm = value; }
        #endregion

        #region Singleton
        private static JurassicRiskViewModel _instance;
        public static JurassicRiskViewModel Get
        {
            get
            {
                if (_instance == null)
                    _instance = new JurassicRiskViewModel();
                return _instance;
            }
        }



        private JurassicRiskViewModel()
        {
            _joueurVm = new JoueurViewModel();
            _carteVm = new CarteViewModel(_joueurVm);
            _lobbyVm = new LobbyViewModel();

            _chatService = JurasicRiskGameClient.Get.ChatService;
            _chatService.YourTurn += _chatService_YourTurn;
            _chatService.EndTurn += _chatService_EndTurn;

        }
        #endregion

        public void _chatService_YourTurn(string turnType)
        {
            switch (turnType)
            {
                case "placement":
                    {
                        new TourPlacement();
                        break;
                    }
            }
        }

        public void _chatService_EndTurn()
        {
            //new tourAttente
        }

    }
}
