﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.input;
using GA_BO.algorithm.interfaces;
using GA_BO.algorithm.enums;

namespace GA_BO.algorithm
{
    public class IslandSupervisor
    {
        private IslandConnections connections;
        private Island[] islands;
        private GlobalConfiguration configuration;

        public IslandSupervisor(GlobalConfiguration configuration)
        {
            this.configuration=configuration;
            this.islands = new Island[configuration.configurations.Count];
            int i=0;

            //create islands
            foreach(IslandConfiguration islandConfiguration in configuration.configurations)
            {
                IFactory factory=null;
                switch(islandConfiguration.evolutionStrategy)
                {
                    case EvolutionStrategy.Roulette:
                        factory=new RouletteFactory(configuration,islandConfiguration);
                        break;
                    case EvolutionStrategy.Stochastic:
                        factory=new StochasticFactory(configuration, islandConfiguration);
                        break;
                    default:
                        throw new MissingMemberException("Undefined factory for selected strategy!");
                }
                this.islands[i++]=new Island(factory,islandConfiguration,this);
            }
            //create connections
            this.connections = new IslandConnections(configuration.connections, this.islands);
            
        }

        public IIndividual getResult()
        {
            //start
            foreach (Island island in islands)
                island.beginEvolution();
            
            //wait till end
            System.Threading.Thread.Sleep(configuration.evolutionTimeInSeconds * 1000);
            
            //stop
            foreach (Island island in islands)
                island.stopEvolution();
            
            //return best
            IIndividual best = islands[0].getBest();
            foreach (Island island in islands)  
                if(island.getBest().value()>best.value()) //todo check if it should be > or <
                    best=island.getBest();
            return best;
        }

        public void exchangeIndividuals(Island from, List<IIndividual> individuals)
        {
            foreach(Island island in connections.getConnections(from))
                island.welcomeNewIndividuals(individuals);
        }


    }
}
