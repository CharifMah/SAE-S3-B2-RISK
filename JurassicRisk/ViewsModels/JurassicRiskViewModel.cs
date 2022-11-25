using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurassicRisk.ViewsModels
{
    public class JurassicRiskViewModel
    {
        private CarteViewModel _carteVm;

        public JurassicRiskViewModel()
        {
            _carteVm = new CarteViewModel();
        }

        public CarteViewModel CarteVm { get => _carteVm; }
    }
}
