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

        public JurassicRiskViewModel()
        {
            _joueurVm = new JoueurViewModel();
            _carteVm = new CarteViewModel(_joueurVm);
        }
    }
}
