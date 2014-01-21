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
        private GlobalConfiguration _config;
        private IslandConfiguration _iconfig;

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
            var newIndividuals = merge(parents, children);
            newIndividuals = mutate(newIndividuals);

            return new Population() {individuals = newIndividuals};
        }

        protected abstract List<IIndividual> mutate(List<IIndividual> individuals);

        protected abstract List<IIndividual> merge(List<IIndividual> parents, List<IIndividual> children);

        protected abstract List<IIndividual> crossover(List<IIndividual> parents);

        protected abstract List<IIndividual> selection(List<IIndividual> parent);
    }
}
