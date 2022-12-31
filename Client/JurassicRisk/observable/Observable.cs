using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace JurassicRisk.observable
{
    /// <summary> Classe abstraite pour les objets observables.</summary>
    /// <Author>Charif</Author>
    public abstract class Observable : INotifyPropertyChanged
    {
        /// <summary> Evénement de modification d'une property </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary> Méthode à appler pour avertir d'une modification </summary>
        /// <param name="propertyName">Nom de la property modifiée (authomatiquement déterminé si appelé directement dans le setter une property) </param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
