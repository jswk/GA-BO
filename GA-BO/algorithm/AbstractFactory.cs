using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.algorithm.interfaces;
using GA_BO.input;

namespace GA_BO.algorithm
{
    abstract class AbstractFactory : IFactory
    {
        protected Random _rand = new Random();

        protected GlobalConfiguration _config;
        protected IslandConfiguration _iconfig;

        public AbstractFactory(GlobalConfiguration config, IslandConfiguration iconfig)
        {
            _config = config;
            _iconfig = iconfig;
        }

        public Population createPopulation()
        {
            var individuals = new List<IIndividual>();
            for (int i = 0; i < _iconfig.populationSize; i++)
            {
                individuals.Add(_config.generator.generate());
            }
            return new Population() {individuals = individuals};
        }



        public Population nextPopulation(Population parent)
        {
            var parents = selection(parent.individuals);
            var children = crossover(parents);
            children = mutate(children);

            var childrenPopulation = new Population() { individuals = children.OrderByDescending(o => getIndividualFitness(o)).ToList() };

            return childrenPopulation;
        }

        protected virtual List<IIndividual> mutate(List<IIndividual> individuals)
        {
            foreach (var individual in individuals)
            {
                if (_rand.NextDouble() < _iconfig.mutationProbability)
                {
                    individual.mutate();
                }
            }
            return individuals;
        }
        

        protected virtual List<IIndividual> crossover(List<IIndividual> parents)
        {
            var needed = _iconfig.populationSize - parents.Count;
            var children = new List<IIndividual>();
            while (needed > 0)
            {
                var ind1 = parents.ElementAt(_rand.Next(parents.Count));
                var ind2 = ind1;
                while (ind2 == ind1)
                {
                    ind2 = parents.ElementAt(_rand.Next(parents.Count));
                }
                
                var result = ind1.crossover(ind2);

                children.Add(result.Item1);
                needed--;
                if (needed == 1) break;
                children.Add(result.Item2);
                needed--;
            }
            children.AddRange(parents);
            return children;
        }

        protected abstract List<IIndividual> selection(List<IIndividual> individuals);

        protected double getIndividualFitness(IIndividual ind)
        {
            // assumes fitness > 0
            return (_config.maximize) ? ind.value() : 1.0 / ind.value();
        }
    }
}
