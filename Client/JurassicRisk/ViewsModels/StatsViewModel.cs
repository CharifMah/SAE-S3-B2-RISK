using Models.Player;
using System;
using System.Collections.ObjectModel;

namespace JurassicRisk.ViewsModels
{
    public class StatsViewModel : observable.Observable
    {

        private ObservableCollection<Statistique> _statistique;

        public StatsViewModel()
        {
            _statistique = new ObservableCollection<Statistique>();
            Random r = new Random();
            for (int i = 0; i < 5; i++)
            {
                Statistique statistique = new Statistique("1" + r.Next(), "3" + r.Next(), "4" + r.Next(), "5" + r.Next(),"6" + r.Next());
                _statistique.Add(statistique);
            }

            NotifyPropertyChanged("SelectedListStatistique");

        }

        public ObservableCollection<Statistique> SelectedListStatistique
        {
            get { return _statistique; }
            set
            {
                _statistique = value;
                NotifyPropertyChanged("SelectedListStatistique");
            }
        }
    }
}
