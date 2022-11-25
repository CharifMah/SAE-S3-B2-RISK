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
        #endregion
        #region Property
        public CarteViewModel CarteVm { get => _carteVm; }
        #endregion

        public JurassicRiskViewModel()
        {
            _carteVm = new CarteViewModel();
        }


    }
}
