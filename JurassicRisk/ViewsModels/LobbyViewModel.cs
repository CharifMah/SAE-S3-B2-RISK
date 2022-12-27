using Models.Player;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurassicRisk.ViewsModels
{
    internal class LobbyViewModel
    {
        private ObservableCollection<Profil> _profils
        {
            get; set;
        }
    }
}
