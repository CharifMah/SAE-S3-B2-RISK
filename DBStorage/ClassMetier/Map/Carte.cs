using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DBStorage.ClassMetier.Map
{
    /// <summary>
    /// Classe du plateau de jeu
    /// </summary>
    public class Carte
    {
        #region Attributes

        private Dictionary<int, Continent> dicoContinents;

        private ITerritoireBase _selectedTerritoire;

        #endregion

        #region Property
        [Required]
        public Dictionary<int, Continent> DicoContinents
        {
            get;
        }
        [Required]
        public ITerritoireBase SelectedTerritoire
        {
            get;
            set;
        }

        #endregion

        public Carte(List<Continent> continent)
        {
            dicoContinents = new Dictionary<int, Continent>();
            for (int i = 0; i < continent.Count; i++)
            {
                dicoContinents.Add(i, continent[i]);
            }
        }
    
        [JsonConstructor]
        public Carte()
        {
        }
    }
}