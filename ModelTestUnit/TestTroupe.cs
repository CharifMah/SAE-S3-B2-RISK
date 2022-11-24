using Models;
using Models.Exceptions;
using Models.Map;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTestUnit
{
    public class TestTroupe
    {
        [Fact]
        public void TestPositionnement()
        {
            TerritoireBase t1 = new TerritoireBase(0);
            t1.Team = Models.Teams.ROUGE;
            TerritoireBase t2 = new TerritoireBase(0);
            t2.Team = Models.Teams.VERT;
            Joueur j1 = new Joueur(new Profil("test","azerty"),new List<Unite>() { new Unite()},Teams.NEUTRE);
            j1.Equipe = Teams.VERT;

            FabriqueUnite f = new FabriqueUnite();
            List<Unite> renforts = new List<Unite>();
            for(int i =0; i < 10; i++)
            {
                Unite u = new Unite();
                renforts.Add(u);
                j1.Troupe.Add(u);
            }

            Assert.Throws<NotYourTerritoryException>(()=> j1.PositionnerTroupe(renforts, t1));
            j1.PositionnerTroupe(renforts, t2);

            renforts.Add(f.Create("brachiosaure"));
            Assert.Throws<NotEnoughUnitException>(() => j1.PositionnerTroupe(renforts, t2));
        }
    }
}
