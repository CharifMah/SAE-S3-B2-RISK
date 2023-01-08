using Models;
using System.Windows;
using static JurassicRisk.ViewsModels.CarteViewModel;

namespace JurassicRisk.ViewsModels
{
    public class JurassicRiskViewModel : observable.Observable
    {

        #region Attributes

        private JoueurViewModel _joueurVm;
        private LobbyViewModel _lobbyVm;

        #endregion

        #region Property

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

            _lobbyVm = new LobbyViewModel();
  
        }

 

        #endregion


      

        public void DestroyVm()
        {
            _instance = null;
        }

    }
}
