using JurassicRisk.observable;
using Models.Joueur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurassicRisk.ViewsModels
{
    public class JoueurViewModel : observable.Observable
    {
        private Joueur _joueur;

        public Joueur Joueur
        {
            get { return _joueur; }
        }

        public JoueurViewModel()
        {
        }
    }
}
