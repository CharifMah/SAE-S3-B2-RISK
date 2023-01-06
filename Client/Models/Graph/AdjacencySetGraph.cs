using Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Graph
{
    public class AdjacencySetGraph : GraphBase2
    {
        private HashSet<Node> _vertexSet;
        public AdjacencySetGraph(List<ITerritoireBase> _numVertices, bool directed = false) : base(_numVertices, directed)
        {
            this._vertexSet = new HashSet<Node>();
            for (var i = 0; i < _numVertices.Count; i++)
            {
                _vertexSet.Add(new Node(_numVertices[i]));
            }
        }

        public override void AddEdge(ITerritoireBase v1, ITerritoireBase v2, int weight)
        {
            if (v1.ID >= this._numVertices || v2.ID >= this._numVertices || v1.ID < 0 || v2.ID < 0)
                throw new ArgumentOutOfRangeException("Vertices are out of bounds");

            if (weight != 1) throw new ArgumentException("An adjacency set cannot represent non-one edge weights");

            this._vertexSet.ElementAt(v1.ID).AddEdge(v2);

            //In an undirected graph all edges are bi-directional
            if (!this._directed) this._vertexSet.ElementAt(v2.ID).AddEdge(v1);
        }

        public override void Display()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<ITerritoireBase> GetAdjacentVertices(ITerritoireBase territoire)
        {
            if (territoire.ID < 0 || territoire.ID >= this._numVertices) throw new ArgumentOutOfRangeException("Cannot access vertex");
            return this._vertexSet.ElementAt(territoire.ID).GetAdjacentVertices();
        }

        public override int GetEdgeWeight(ITerritoireBase v1, ITerritoireBase v2)
        {
            return 1;
        }
    }
}
