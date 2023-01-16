using Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Graph
{
    public abstract class GraphBase2
    {
        protected readonly int __numVertices;
        protected readonly bool _directed;

        public GraphBase2(List<ITerritoireBase> _numVertices, bool directed = false)
        {
            this.__numVertices = _numVertices.Count;
            this._directed = directed;
        }

        public abstract void AddEdge(ITerritoireBase v1, ITerritoireBase v2, int weight);

        public abstract IEnumerable<ITerritoireBase> GetAdjacentVertices(ITerritoireBase v);

        public abstract int GetEdgeWeight(ITerritoireBase v1, ITerritoireBase v2);

        public abstract void Display();

        public int _numVertices { get { return this.__numVertices; } }
    }
}
