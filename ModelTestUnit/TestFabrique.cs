using Models.Fabrique.FabriqueUnite;
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
            FabriqueUnite fabrique = new FabriqueUnite();
            Unite b1 = fabrique.Create("brachiosaure");
            Assert.IsType<Brachiosaure>(b1);
        }
    }
}
