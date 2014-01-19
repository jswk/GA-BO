using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_BO.algorithm
{
    class DirectedGraph
    {
        private Island[] _islands;
        private List<int>[] _connections;

        public DirectedGraph(Island[] islands, List<int>[] connection)
        {
            this._islands = islands;
            this._connections = connection;
        }

        public List<Island> GetConnections(Island island)
        {
            if (!_islands.Contains(island))
            {
                throw new Exception("Island does not exist");
            }

            int index = Array.IndexOf(_islands, island);

            if (!_connections[index].Any())
            {
                return null;
            }

            List<Island> toReturn = new List<Island>();
            foreach (int i in _connections[index])
            {
                toReturn.Add(_islands[i]);
            }
            return toReturn;
        }
    }
}
