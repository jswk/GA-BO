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

            return new Population() { individuals = children };
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

        protected abstract List<IIndividual> crossover(List<IIndividual> parents);

        protected abstract List<IIndividual> selection(List<IIndividual> parent);
    }
}
