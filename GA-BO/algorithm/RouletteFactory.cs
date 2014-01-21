using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.algorithm.interfaces;
using GA_BO.input;

namespace GA_BO.algorithm
{
    class RouletteFactory : AbstractFactory
    {
        public RouletteFactory(GlobalConfiguration config, IslandConfiguration iconfig) : base(config, iconfig)
        {
        }

        protected override List<IIndividual> selection(List<IIndividual> individuals)
        {
            var sum = individuals.Aggregate(0, (acc, next) => next.value() + acc);
            // selecting random pool of individuals with probability based on their fitness
            var selected = from el in individuals
                           where el.value() < _rand.Next(sum)
                           select el;
            return new List<IIndividual>(selected);
        }

        protected override List<IIndividual> crossover(List<IIndividual> parents)
        {
            int needed = _iconfig.populationSize - parents.Count;
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
    }
}
