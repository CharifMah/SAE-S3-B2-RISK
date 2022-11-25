using Models;
using Models.Exceptions;
using Models.Fabriques.FabriqueUnite;
using Models.Joueur;
using Models.Map;
using Models.Player;
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
            Joueur j1 = new Joueur(new Profil("test","qsd"),new List<UniteBase>() { new UniteBase()},Teams.NEUTRE);
            j1.Equipe = Teams.VERT;

            FabriqueUniteBase f = new FabriqueUniteBase();
            List<UniteBase> renforts = new List<UniteBase>();
            for(int i =0; i < 10; i++)
            {
                UniteBase u = new UniteBase();
                renforts.Add(u);
                j1.Troupe.Add(u);
            }

            Assert.Throws<NotYourTerritoryException>(()=> j1.PositionnerTroupe(renforts, t1));
            j1.PositionnerTroupe(renforts, t2);

            renforts.Add(f.Create("brachiosaure"));
            Assert.Throws<NotEnoughUniteBasexception>(() => j1.PositionnerTroupe(renforts, t2));
        }
    }
}
