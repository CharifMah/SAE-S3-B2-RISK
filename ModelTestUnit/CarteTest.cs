

using JurassicRisk.ViewsModels;
using Models.Map;

namespace ModelTestUnit
{
    public class CarteTest
    {

        [Fact]
        public void TestTerritoireCreation()
        {
            UriParser.Register(new GenericUriParser(GenericUriParserOptions.GenericAuthority), "pack", -1);
            ViewModelCarte MC = new ViewModelCarte();
            Assert.Equal(6, MC.Carte.Continent.Count);
        }

        [Fact]
        public void TestTeams()
        {
            List<Continent> continent = new List<Continent>();
            List<TerritoireBase> territoires = new List<TerritoireBase>();
            for (int y = 0; y < 5; y++)
            {
                TerritoireBase territoire = new TerritoireBase();
                territoire.Team = Models.Teams.ROUGE;
                territoires.Add(territoire);

            }
            for (int i = 0; i < 5; i++)
            {

                continent.Add(new Continent(territoires));

            }
            Carte carte = new Carte(continent);
            Assert.Equal(carte.Continent.Values.ElementAt(0).Territores.ElementAt(0).Value.Team, Models.Teams.ROUGE);


        }
        [Fact]
        public void TestDoublons()
        {
            List<Continent> continent = new List<Continent>();
            List<Continent> continent1 = new List<Continent>();

            List<TerritoireBase> territoires = new List<TerritoireBase>();
            Dictionary<int, Continent> dicoContinents;
            dicoContinents = new Dictionary<int, Continent>();

            for (int i = 0; i < 2; i++)
            {
                continent.Add(new Continent(territoires));
                continent1.Add(new Continent(territoires));

            }

            Carte carte = new Carte(continent);
            Carte carte1 = new Carte(continent);

            Assert.NotEqual(carte.Continent.Values.ElementAt(0).Territores.ElementAt(0).Key, carte1.Continent.Values.ElementAt(0).Territores.ElementAt(0).Key);


        }
    }
}
