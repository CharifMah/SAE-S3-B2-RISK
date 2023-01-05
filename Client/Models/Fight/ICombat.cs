using Models.Map;
using Models.Player;
using Models.Units;

namespace Models.Fight
{
    public interface ICombat
    {
        public int Attaquer(List<IUnit> attaquant, ITerritoireBase cible, Joueur assaillant);
        public (int, int) CompareLances(List<int> lanceAttaque, List<int> lanceDefense);
        public int Defendre(List<IUnit> defenseur, ITerritoireBase territoireAttaquant, Joueur j);
        public void DerouleCombat(List<IUnit> attaquant, List<IUnit> defenseur, ITerritoireBase territoireAttaquant, ITerritoireBase cible, Joueur assaillant, Joueur victime);
        public List<int> LancerDes(int nombreDes);
        public void RemoveUnits(int attaqueReussie, int nbAttaque, ITerritoireBase territoireAttaquant, ITerritoireBase cible);
        public (List<int>, List<int>) TriLances(List<int> attaqueScore, List<int> defenseScore);
    }
}