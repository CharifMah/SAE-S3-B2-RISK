using Models.Map;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Models
{
    public class Combat : ICombat
    {
        public Combat(List<Unite> attaquant, List<Unite> defenseur, TerritoireBase territoireAttaquant, TerritoireBase cible, Joueur assaillant, Joueur victime)
        {
            DerouleCombat(attaquant, defenseur, territoireAttaquant, cible, assaillant, victime);
        }

        public void DerouleCombat(List<Unite> attaquant, List<Unite> defenseur, TerritoireBase territoireAttaquant, TerritoireBase cible, Joueur assaillant, Joueur victime)
        {
            int nbAttaque = Attaquer(attaquant, cible, assaillant);
            int nbDefense = Defendre(defenseur, territoireAttaquant, victime);
            List<int> resAttaque = LancerDes(nbAttaque);
            List<int> resDefense = LancerDes(nbDefense);
            (List<int>, List<int>) res = TriLances(resAttaque, resDefense);
            resAttaque = res.Item1;
            resDefense = res.Item2;
            (int, int) comparaison = CompareLances(resAttaque, resDefense);
            int attaqueReussie = comparaison.Item1;
            int nombreAttaque = comparaison.Item2;
            RemoveUnits(attaqueReussie, nombreAttaque, territoireAttaquant, cible);
        }

        public int Attaquer(List<Unite> attaquant, TerritoireBase cible, Joueur assaillant)
        {
            if (cible.Team != assaillant.Equipe)
            {
                switch (attaquant.Count)
                {
                    case 0:
                        throw new Exception("You can't attack without unit on your territory !");
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    case 3:
                        return 3;
                    default:
                        return -1;
                }
            }
            else
            {
                throw new Exception("You can't attack your territory !");
            }
        }

        public int Defendre(List<Unite> defenseur, TerritoireBase territoireAttaquant, Joueur j)
        {
            switch (defenseur.Count)
            {
                case 0:
                    throw new Exception("You can't attack without unit on your territory !");
                case 1:
                    return 1;
                case 2:
                    return 2;
                default:
                    return -1;
            }
        }

        public List<int> LancerDes(int nombreDes)
        {
            List<int> res = new List<int>();
            Random rand = new Random();
            for (int i = 0; i < nombreDes; i++)
            {
                res.Add(rand.Next(1, 6));
            }
            return res;
        }

        public (List<int>, List<int>) TriLances(List<int> attaqueScore, List<int> defenseScore)
        {
            (int, int) max = (attaqueScore[0], 0);
            for (int i = 0; i < attaqueScore.Count; i++)
            {
                if (attaqueScore[i] > max.Item1)
                {
                    (int, int) temp = max;
                    int temp2 = attaqueScore[i];

                    attaqueScore[i] = max.Item1;
                    attaqueScore[max.Item2] = temp2;

                    max.Item1 = attaqueScore[i];
                    max.Item2 = i;
                }
            }
            for (int i = 0; i < defenseScore.Count; i++)
            {
                if (defenseScore[i] > max.Item1)
                {
                    (int, int) temp = max;
                    int temp2 = defenseScore[i];

                    defenseScore[i] = max.Item1;
                    defenseScore[max.Item2] = temp2;

                    max.Item1 = defenseScore[i];
                    max.Item2 = i;
                }
            }

            return (attaqueScore, defenseScore);
        }

        public (int, int) CompareLances(List<int> lanceAttaque, List<int> lanceDefense)
        {
            int attaqueReussie = 0;
            int nbAttaque = 0;
            if (lanceAttaque.Count < lanceDefense.Count)
            {
                for (int i = 0; i < lanceAttaque.Count; i++)
                {
                    if (lanceAttaque[i] > lanceDefense[i])
                    {
                        attaqueReussie++;
                    }
                    nbAttaque++;
                }
            }
            else
            {
                for (int i = 0; i < lanceDefense.Count; i++)
                {
                    if (lanceAttaque[i] > lanceDefense[i])
                    {
                        attaqueReussie++;
                    }
                    nbAttaque++;
                }
            }
            return (attaqueReussie, nbAttaque);
        }

        public void RemoveUnits(int attaqueReussie, int nbAttaque, TerritoireBase territoireAttaquant, TerritoireBase cible)
        {
            List<Unite> troupe = cible.Units;
            for (int i = 0; i < attaqueReussie; i++)
            {
                cible.RemoveUnit(troupe[0]);
            }
            troupe = territoireAttaquant.Units;
            for (int i = 0; i < nbAttaque - attaqueReussie; i++)
            {
                territoireAttaquant.RemoveUnit(troupe[0]);
            }
        }
    }
}
