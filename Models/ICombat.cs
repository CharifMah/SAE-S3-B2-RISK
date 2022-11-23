using Models.Map;
using Models.Units;

namespace Models
{
    public interface ICombat
    {
        int Attaquer(List<Unite> attaquant, TerritoireBase cible, Joueur assaillant);
        (int, int) CompareLances(List<int> lanceAttaque, List<int> lanceDefense);
        int Defendre(List<Unite> defenseur, TerritoireBase territoireAttaquant, Joueur j);
        void DerouleCombat(List<Unite> attaquant, List<Unite> defenseur, TerritoireBase territoireAttaquant, TerritoireBase cible, Joueur assaillant, Joueur victime);
        List<int> LancerDes(int nombreDes);
        void RemoveUnits(int attaqueReussie, int nbAttaque, TerritoireBase territoireAttaquant, TerritoireBase cible);
        (List<int>, List<int>) TriLances(List<int> attaqueScore, List<int> defenseScore);
    }
}