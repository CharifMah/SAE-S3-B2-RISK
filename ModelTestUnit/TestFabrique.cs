using Models.Fabriques.FabriqueUnite;

namespace ModelTestUnit
{
    public class TestFabrique
    {
        [Fact]
        public void CreationBrachiosaurus()
        {
            FabriqueUniteBase fabrique = new FabriqueUniteBase();
            //UniteBase b1 = fabrique.Create("Brachiosaurus");
            //Assert.IsType<Brachiosaurus>(b1);
        }
    }
}
