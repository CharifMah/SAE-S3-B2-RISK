using Models.Map;
using Models.Player;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Models.Fight
{
    public class Combat : ICombat
    {
        public Combat(List<IUnit> attaquant, List<IUnit> defenseur, ITerritoireBase territoireAttaquant, ITerritoireBase cible, Joueur assaillant, Joueur victime)
        {
            DerouleCombat(attaquant, defenseur, territoireAttaquant, cible, assaillant, victime);
        }

        public void DerouleCombat(List<IUnit> attaquant, List<IUnit> defenseur, ITerritoireBase territoireAttaquant, ITerritoireBase cible, Joueur assaillant, Joueur victime)
        {
            int nbAttaque = Attaquer(attaquant);
            int nbDefense = Defendre(defenseur);
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

        /// <summary>
        /// Recupère le nombre d'attaquant choisi par le joueur
        /// </summary>
        /// <param name="attaquant">La liste des unités envoyé en attaque</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int Attaquer(List<IUnit> attaquant)
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

        /// <summary>
        /// Recupère le nombre de défenseur choisi par le joueur ciblé
        /// </summary>
        /// <param name="defenseur"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int Defendre(List<IUnit> defenseur)
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

        /// <summary>
        /// Simule un ou plusieurs lancés de dé à 6 faces
        /// </summary>
        /// <param name="nombreDes">Connu par le return de la méthode Attaquer ou Défendre suivant l'avancement de l'attaque</param>
        /// <returns>Renvoie les résultats de chaque dés lancés</returns>
        public List<int> LancerDes(int nombreDes)
        {
            List<int> res = new List<int>();
            Des dice = new Des(6);
            for (int i = 0; i < nombreDes; i++)
            {
                res.Add(dice.Roll());
            }
            return res;
        }

        /// <summary>
        /// Tris les lancés en prenant le plus grand de l'attaquant et celui du défenseur.
        /// Réitère l'opération pour chaque dés d'attaque
        /// </summary>
        /// <param name="attaqueScore">Chaque lancer de l'attaquant</param>
        /// <param name="defenseScore">Chaque lancer du défenseur</param>
        /// <returns>Tuple de liste d'entier contenant les valeurs triés</returns>
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

        /// <summary>
        /// Compare chaque résultat de dés précédemment triés par ordre décroissant
        /// </summary>
        /// <param name="lanceAttaque">Liste des résultats des dés d'attaque par ordre décroissant</param>
        /// <param name="lanceDefense">Liste des résultats des dés de défense par ordre décroissant</param>
        /// <returns>Le nombre d'attaque réussie et le nombre total d'attaque</returns>
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

        /// <summary>
        /// Retire les unités en fonction des résultats des dés (-1 unités pour le défenseur pour chaque attaque réussie / 
        /// -1 unités * (nbAttaque-attaqueReussie) pour l'attaquant)
        /// </summary>
        /// <param name="attaqueReussie"></param>
        /// <param name="nbAttaque"></param>
        /// <param name="territoireAttaquant"></param>
        /// <param name="cible"></param>
        public void RemoveUnits(int attaqueReussie, int nbAttaque, ITerritoireBase territoireAttaquant, ITerritoireBase cible)
        {
            List<IUnit> troupe = cible.Units;
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
