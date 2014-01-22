using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.algorithm.interfaces;
using GA_BO.algorithm.enums;

namespace GA_BO.input
{
    public class IslandConfiguration
    {
        public EvolutionStrategy evolutionStrategy;
        public double mutationProbability;
        public double crossoverProbability;
        public int populationSize;
        public int selectionSize;
        public int bestIndividualsToExchangeNo;
    }
}
