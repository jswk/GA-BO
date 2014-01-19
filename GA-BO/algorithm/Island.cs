using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GA_BO.input;
using GA_BO.algorithm.interfaces;
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
        private Queue<Island> arrivingIndividuals; //implementation needs to be thread safe

        public Island(IFactory factory, IslandConfiguration configuration)
        {
            this.islandThread = new Thread(new ThreadStart(run)); //http://stackoverflow.com/questions/1923512/threading-does-c-sharp-have-an-equivalent-of-the-java-runnable-interface
            this.factory = factory;
            this.configuration = configuration;
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
            //zaloz locka !! 
            IIndividual bestIndividual = null;
            foreach(IIndividual ind in currentPopulation.individuals)
            {
                if (ind.value() > bestIndividual.value())
                {
                    bestIndividual = ind;
                }
            }
            //get best from current population
            return bestIndividual;
        }

        public void welcomeNewIndividuals(List<IIndividual> arrivals)
        {
            //add arrivals to arrivingIndividuals queue
        }

        private void run()
        {
            while (keepGoing)
            {
                // produce new populations using factory, exchange best individuals with supervisor, add arriving individuals to population...
            }
        }
    }
}
