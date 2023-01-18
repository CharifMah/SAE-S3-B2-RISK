using Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Graph
{
    public class Node
    {
        private readonly int VertexId;
        private readonly HashSet<ITerritoireBase> AdjacencySet;

        public Node(ITerritoireBase territoire)
        {
            this.VertexId = territoire.ID;
            this.AdjacencySet = new HashSet<ITerritoireBase>();
        }

        public void AddEdge(ITerritoireBase territoire)  
        {
            if (this.VertexId == territoire.ID)
                throw new ArgumentException("The vertex cannot be adjacent to itself");
            this.AdjacencySet.Add(territoire);
        }

        public HashSet<ITerritoireBase> GetAdjacentVertices()
        {
            return this.AdjacencySet;
        }
    }
}
