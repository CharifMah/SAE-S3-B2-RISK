using Models.Graph;
using Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTestUnit
{
    public class TestGraph
    {
        [Fact]
        public void TestGraphNode()
        {
            var tr1 = new TerritoireDecorator(new TerritoireBase(0), 0, 0, 0, 0, "");
            var tr2 = new TerritoireDecorator(new TerritoireBase(1), 0, 0, 0, 0, "");
            var tr3 = new TerritoireDecorator(new TerritoireBase(2), 0, 0, 0, 0, "");
            var tr4 = new TerritoireDecorator(new TerritoireBase(3), 0, 0, 0, 0, "");
            var l = new List<ITerritoireBase>
            {
                tr1,
                tr2,
                tr3,
                tr4
            };
            var adj = new AdjacencySetGraph(l);
            adj.AddEdge(tr1, tr2, 1);
            adj.AddEdge(tr1, tr3, 1);
            var adjacent = adj.GetAdjacentVertices(tr1);

            Assert.Equal(0, adj.GetAdjacentVertices(tr4).Count());
            Assert.Equal(2, adj.GetAdjacentVertices(tr1).Count());
            Assert.Equal(0, adj.GetAdjacentVertices(tr2).First().ID);
        }
    }
}
