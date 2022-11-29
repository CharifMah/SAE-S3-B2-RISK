using DBStorage.ClassMetier;
using DBStorage.ClassMetier.Map;
using DBStorage.ClassMetier.Units;
using Models;
using Models.Combat;
using Models.Fabriques.FabriqueUnite;
using Models.Map;
using Models.Player;
using Models.Units;

namespace ModelTestUnit
{
    public class TestCombat
    {
        [Fact]
        public void Combat()
        {
            Joueur j1 = new Joueur(new Profil("sqd","sqd"),new List<IUnit>() { new UniteBase(),new UniteBase()},Teams.ROUGE);;
            j1.Equipe = Teams.ROUGE;
            TerritoireBase t1 = new TerritoireBase(1);
            t1.Team = Teams.ROUGE;

            Joueur j2 = new Joueur(new Profil("sqdd", "sqdd"), new List<IUnit>() { new UniteBase(), new UniteBase() }, Teams.ROUGE);
            j2.Equipe = Teams.BLEU;
            TerritoireBase t2 = new TerritoireBase(2);
            t2.Team = Teams.BLEU;

            FabriqueUniteBase f = new FabriqueUniteBase();
            for (int i = 0; i < 5; i++)
            {
                var unit = f.Create("Brachiosaurus");
                j1.AddUnit(unit);
                var unit2 = f.Create("Brachiosaurus");
                j2.AddUnit(unit2);
            }

            List<IUnit> attaquants = new List<IUnit>();
            List<IUnit> defenseurs = new List<IUnit>();
            t1.Units = attaquants;
            t2.Units = defenseurs;
            t1.Units.Add(f.Create("Brachiosaurus"));

            for (int i = 0; i < 3; i++)
            {
                attaquants.Add(j1.Troupe[i]);
            }
            for (int i = 0; i < 2; i++)
            {
                defenseurs.Add(j2.Troupe[i]);
            }

            FakeCombat c = new FakeCombat(attaquants, defenseurs, t1, t2, j1, j2);
            Assert.Equal(2, attaquants.Count);
            Assert.Equal(1, defenseurs.Count);
        }
    }
}
