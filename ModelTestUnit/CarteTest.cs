

using JurassicRisk.ViewsModels;
using Models.Map;
using System.Linq;

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
            
        }
    }
}
