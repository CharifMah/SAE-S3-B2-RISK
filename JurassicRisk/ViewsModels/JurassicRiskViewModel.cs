using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
