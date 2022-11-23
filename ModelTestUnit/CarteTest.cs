

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
            Assert.Equal(6, MC.Carte.DicoContinents.Count);
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
            Assert.Equal(carte.DicoContinents.Values.ElementAt(0).DicoTerritoires.ElementAt(0).Value.Team, Models.Teams.ROUGE);


        }
        //[Fact]
        //public void TestID()
        //{
        //    List<Continent> continent = new List<Continent>();
        //    Dictionary<int, TerritoireBase> dicoTerritoires = new Dictionary<int, TerritoireBase>();
        //    Dictionary<int, Continent> dicoContinent = new Dictionary<int, Continent>();
        //    continent.Add(new Continent(new List<TerritoireBase>() { new TerritoireBase() }));
        //    continent.Add(new Continent(new List<TerritoireBase>() { new TerritoireBase() }));

        //    for (int i = 0; i < continent.Count - 1; i++)
        //    {

        //    }
        //    Carte carte = new Carte(continent);
        //    Assert.NotEqual(carte.DicoContinents.Values.ElementAt(0).DicoTerritoires.ElementAt(0).Key, carte.DicoContinents.Values.ElementAt(1).DicoTerritoires.ElementAt(1).Key);


        //}
    }
}
