using Models.Fabriques.FabriqueUnite;
using Models.Player;
using IUnit = Models.Units.IUnit;
using Profil = Models.Player.Profil;
using Teams = Models.Teams;
using TerritoireBase = Models.Map.TerritoireBase;
using UniteBase = Models.Units.UniteBase;

namespace ModelTestUnit
{
    public class TestTroupe
    {
        [Fact]
        public void TestPositionnement()
        {
            TerritoireBase t1 = new TerritoireBase(0);
            t1.Team = Teams.ROUGE;
            TerritoireBase t2 = new TerritoireBase(0);
            t2.Team = Teams.VERT;
            Joueur j1 = new Joueur(new Profil("test", "qsd"), Teams.NEUTRE);
            j1.Team = Teams.VERT;

            FabriqueUniteBase f = new FabriqueUniteBase();
            List<IUnit> renforts = new List<IUnit>();
            for (int i = 0; i < 10; i++)
            {
                UniteBase u = new UniteBase();
                renforts.Add(u);
                j1.Units.Add(u);
            }

            j1.AddUnits(renforts, t2);
            Assert.Equal(t2.Team, j1.Team);
            Assert.Equal(1, j1.Units.Count);
            Assert.Equal(renforts.Count, t2.Units.Count);
        }
    }
}
