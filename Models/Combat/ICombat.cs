using Models.Map;
using Models.Player;
using Models.Units;

namespace Models.Combat
{
    public interface ICombat
    {
        public int Attaquer(List<UniteBase> attaquant, TerritoireBase cible, Joueur assaillant);
        public (int, int) CompareLances(List<int> lanceAttaque, List<int> lanceDefense);
        public int Defendre(List<UniteBase> defenseur, TerritoireBase territoireAttaquant, Joueur j);
        public void DerouleCombat(List<UniteBase> attaquant, List<UniteBase> defenseur, TerritoireBase territoireAttaquant, TerritoireBase cible, Joueur assaillant, Joueur victime);
        public List<int> LancerDes(int nombreDes);
        public void RemoveUnits(int attaqueReussie, int nbAttaque, TerritoireBase territoireAttaquant, TerritoireBase cible);
        public (List<int>, List<int>) TriLances(List<int> attaqueScore, List<int> defenseScore);
    }
}