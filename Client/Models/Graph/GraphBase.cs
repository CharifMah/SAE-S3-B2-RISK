using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Graph
{
    public abstract class GraphBase
    {
        protected readonly int __numVertices;
        protected readonly bool _directed;

        public GraphBase(int _numVertices, bool directed = false)
        {
            this.__numVertices = _numVertices;
            this._directed = directed;
        }

        public abstract void AddEdge(int v1, int v2, int weight);

        public abstract IEnumerable<int> GetAdjacentVertices(int v);

        public abstract int GetEdgeWeight(int v1, int v2);

        public abstract void Display();

        public int _numVertices { get { return this.__numVertices; } }
    }
}
