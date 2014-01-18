using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.algorithm.interfaces;

namespace GA_BO.input
{
    class IslandConfiguration
    {
        public EvolutionStrategy evolutionStrategy;
        public double mutationProbability;
        public double crossoverProbability;
        public int populationSize;
        public int bestIndividualsToExchangeNo;
    }
}
