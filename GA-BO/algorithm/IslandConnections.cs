using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_BO.algorithm
{
    public class IslandConnections
    {
        DirectedGraph di_graph;
        private bool _debugMode = true; //change to false if sure than args are correct

        public IslandConnections(List<int>[] connection, Island[] islands)
        {
            // create digraf where every vertex is island[i], and connections corresponds to adjacence list given in 'connection'
            
            if (_debugMode == true)
            {
                if (connection.Length != islands.Length)
                {
                    throw new Exception("Both array args must have the same length");
                }

                for (int i = 0; i < connection.Length; i++)
                {
                    if (connection[i].Any(num => num >= connection.Length))
                    {
                        throw new Exception("Found wrong connection, index of island " + i + " out of range");
                    }

                    if (connection[i].Contains(i))
                    {
                        throw new Exception("Found wrong connection, island -> island for island = " + i);
                    }
                }
            }

            di_graph = new DirectedGraph(islands, connection);
        }

        public List<Island> getConnections(Island island)
        {
            //return all X adjacent to island (such as: island -> X)
            return di_graph.GetConnections(island);
        }

    }
}
