using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.input;
using GA_BO.algorithm.interfaces;

namespace GA_BO.algorithm
{
    public class IslandSupervisor
    {
        private IslandConnections connections;
        private Island[] islands;

        public IslandSupervisor(GlobalConfiguration configuration)
        {
            //create all Islands with concrete Factories depending on EvolutionStrategy
        }

        public IIndividual getResult()
        {
            // begin evolution,wait some time, stop Evolution, return best of all
            return null;
        }

        void exchangeIndividuals(Island from, List<IIndividual> individuals)
        {
            //send to connected islands
        }


    }
}
