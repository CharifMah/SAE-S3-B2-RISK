using Models;
using Models.Map;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTestUnit
{
    public class TestCombat
    {
        [Fact]
        public void Combat()
        {
            Joueur j1 = new Joueur();
            j1.Equipe = Teams.ROUGE;
            TerritoireBase t1 = new TerritoireBase(1);
            t1.Team = Teams.ROUGE;

            Joueur j2 = new Joueur();
            j2.Equipe = Teams.BLEU;
            TerritoireBase t2 = new TerritoireBase(2);
            t2.Team = Teams.BLEU;

            FabriqueUnite f = new FabriqueUnite();
            for(int i = 0; i < 5; i++)
            {
                var unit = f.Create("brachiosaure");
                j1.AddUnit(unit);
                var unit2 = f.Create("brachiosaure");
                j2.AddUnit(unit2);
            }

            List<Unite> attaquants = new List<Unite>();
            List<Unite> defenseurs = new List<Unite>();
            t1.Units = attaquants;
            t2.Units = defenseurs;
            t1.Units.Add(f.Create("brachiosaure"));

            for (int i = 0; i < 3; i++)
            {
                attaquants.Add(j1.Troupe[i]);
            }
            for(int i = 0; i < 2; i++)
            {
                defenseurs.Add(j2.Troupe[i]);
            }

            FakeCombat c = new FakeCombat(attaquants, defenseurs, t1, t2, j1, j2);
            Assert.Equal(2, attaquants.Count);
            Assert.Equal(1, defenseurs.Count);
        }
    }
}
