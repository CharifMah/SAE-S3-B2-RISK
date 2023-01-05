using Models.Fabriques.FabriqueUnite;
using Models.Units;

namespace ModelTestUnit
{
    public class TestFabrique
    {
        [Fact]
        public void CreationBrachiosaurus()
        {
            FabriqueUniteBase fabrique = new FabriqueUniteBase();
            UniteBase b1 = fabrique.Create("Brachiosaurus");
            Assert.IsType<Brachiosaurus>(b1);
        }
    }
}
