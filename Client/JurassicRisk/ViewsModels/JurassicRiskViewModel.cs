namespace JurassicRisk.ViewsModels
{
    public class JurassicRiskViewModel
    {

        #region Attributes
        private CarteViewModel _carteVm;
        private JoueurViewModel _joueurVm;
        private LobbyViewModel _lobbyVm;
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
        }

        public void DestroyVm()
        {
            _instance = null;
        }

        #endregion
    }
}
