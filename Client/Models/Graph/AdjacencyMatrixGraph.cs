using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Graph
{
    public class AdjacencyMatrixGraph : GraphBase
    {
        int[,] Matrix;

        public AdjacencyMatrixGraph(int _numVertices, bool directed = false) : base(_numVertices, directed)
        {
            GenerateEmptyMatrix(_numVertices);
        }

        public override void AddEdge(int v1, int v2, int weight)
        {
            if (v1 >= this.__numVertices || v2 >= this.__numVertices || v1 < 0 || v2 < 0)
                throw new ArgumentOutOfRangeException("Vertices are out of bounds");

            if (weight < 1) throw new ArgumentException("Weight cannot be less than 1");

            this.Matrix[v1, v2] = weight;

            //In an undirected graph all edges are bi-directional
            if (!this._directed) this.Matrix[v2, v1] = weight;
        }

        public override void Display()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<int> GetAdjacentVertices(int v)
        {
            if (v < 0 || v >= this.__numVertices) throw new ArgumentOutOfRangeException("Cannot access vertex");

            List<int> adjacentVertices = new List<int>();
            for (int i = 0; i < this.__numVertices; i++)
            {
                if (this.Matrix[v, i] > 0)
                    adjacentVertices.Add(i);
            }
            return adjacentVertices;
        }

        public override int GetEdgeWeight(int v1, int v2)
        {
            return this.Matrix[v1, v2];
        }

        private void GenerateEmptyMatrix(int _numVertices)
        {
            this.Matrix = new int[_numVertices, _numVertices];
            for (int row = 0; row < _numVertices; row++)
            {
                for (int col = 0; col < _numVertices; col++)
                {
                    Matrix[row, col] = 0;
                }
            }
        }
    }
}
