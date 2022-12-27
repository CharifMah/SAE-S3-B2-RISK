namespace JurassicRisk.ViewsModels
{
    public class JurassicRiskViewModel
    {

        #region Attributes
        private CarteViewModel _carteVm;
        private JoueurViewModel _joueurVm;
        
        #endregion

        #region Property
        public CarteViewModel CarteVm { get => _carteVm; }
        public JoueurViewModel JoueurVm { get => _joueurVm; }
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
        }
        #endregion
    }
}
