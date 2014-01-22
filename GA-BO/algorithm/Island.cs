using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GA_BO.input;
using GA_BO.algorithm.interfaces;
using System.Collections.Concurrent;

namespace GA_BO.algorithm
{
    public class Island
    {
        private IslandSupervisor supervisor;
        private IFactory factory;
        private Population currentPopulation;
        private Thread islandThread;
        private bool keepGoing=false;
        private IslandConfiguration configuration;
        private ConcurrentQueue<IIndividual> arrivingIndividuals; //implementation needs to be thread safe

        public Island(IFactory factory, IslandConfiguration configuration,IslandSupervisor supervisor)
        {
            this.islandThread = new Thread(new ThreadStart(run)); //http://stackoverflow.com/questions/1923512/threading-does-c-sharp-have-an-equivalent-of-the-java-runnable-interface
            this.factory = factory;
            this.configuration = configuration;
            this.supervisor = supervisor;
            this.arrivingIndividuals = new ConcurrentQueue<IIndividual>();
            Console.WriteLine("Island created");
        }

        public void beginEvolution()
        {
            this.keepGoing = true;
            islandThread.Start(); 
        }

        public void stopEvolution()
        {
            this.keepGoing = false;
        }

        public IIndividual getBest()
        {
            IIndividual bestIndividual = null;
            lock (currentPopulation)
            {
                foreach (IIndividual ind in currentPopulation.individuals)
                {   
                    if (bestIndividual == null)
                    {
                        bestIndividual = ind;
                    }
                    if (ind.value() < bestIndividual.value())
                    {
                        bestIndividual = ind;
                    }
                }
            }
            //get best from current population
            return bestIndividual.duplicate();
        }


        public void welcomeNewIndividuals(List<IIndividual> arrivals)
        {
            //add arrivals to arrivingIndividuals queue
            foreach (IIndividual ind in arrivals)
            {
                arrivingIndividuals.Enqueue(ind);
            }
        }


        private void swapPopulation(IIndividual individual) // change the worst individual in population to another individual
        {
            IIndividual worstIndividual = null;
            lock (currentPopulation)
            {
                foreach (IIndividual ind in currentPopulation.individuals)
                {
                    if (worstIndividual == null)
                    {
                        worstIndividual = ind;
                    }
                    if (ind.value() > worstIndividual.value())
                    {
                        worstIndividual = ind;
                    }
                }
                var i = currentPopulation.individuals.IndexOf(worstIndividual);
                currentPopulation.individuals[i] = individual; //changing the worst for new. 
                //but using this way we could add a lot of weak Individuals and change only one in population if addaing individuals are weak than existing on the island
            }
         }

        private List<IIndividual> getBestsToExchange()
        {
            List<IIndividual> tmp;
            lock (currentPopulation)
            {
                tmp = currentPopulation.individuals.OrderBy(o => o.value()).ToList();
            }
            List<IIndividual> result = new List<IIndividual>();
            for (int i = 0; i < configuration.bestIndividualsToExchangeNo; i++)
            {
                result.Add(tmp[i]);
            }
            return result;
            }

        private void run()
        {   
            currentPopulation = factory.createPopulation();
            while (keepGoing)
            {
               IIndividual outResult = null;
               while(arrivingIndividuals.TryDequeue(out outResult))
               {
                   swapPopulation(outResult);
               } // change individuals for new 

               List<IIndividual> bestsToExchange = getBestsToExchange();
               supervisor.exchangeIndividuals(this,bestsToExchange);
                // sending a best part of population to supervisior
               lock (currentPopulation)
               {
                   currentPopulation = factory.nextPopulation(currentPopulation);
               }
                // it produces start population first, then tries to rechange population using indiviudals from queue.
               // then produce next generation of population
                // produce new populations using factory, exchange best individuals with supervisor, add arriving individuals to population...
            }
        }
    }
}
