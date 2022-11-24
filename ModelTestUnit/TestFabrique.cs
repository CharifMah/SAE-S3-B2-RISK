using Models.Fabriques.FabriqueUnite;
using Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTestUnit
{
    public class TestFabrique
    {
        [Fact]
        public void CreationBrachiosaure()
        {
            FabriqueUniteBase fabrique = new FabriqueUniteBase();
            UniteBase b1 = fabrique.Create("brachiosaure");
            Assert.IsType<Brachiosaure>(b1);
        }
    }
}
