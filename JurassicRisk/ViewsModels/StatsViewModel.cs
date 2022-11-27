using Models.Player;
using System.Collections.ObjectModel;

namespace JurassicRisk.ViewsModels
{
    public class StatsViewModel : observable.Observable
    {

        private ObservableCollection<Statistique> _statistique;

        public StatsViewModel()
        {
            _statistique = new ObservableCollection<Statistique>();

            for (int i = 0; i < 5; i++)
            {
                Statistique statistique = new Statistique($"Adam{i}");
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
