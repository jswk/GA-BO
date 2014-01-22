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
            var size = _iconfig.selectionSize;
            var totalFitness = individuals.Aggregate(0, (acc, next) => next.value() + acc);
            var outInd = new List<IIndividual>();
            // selecting random pool of individuals with probability based on their fitness
            for (var i = 0; i < size; i++)
            {
                var rand = _rand.NextDouble()*totalFitness;
                var sum = 0;
                foreach (var individual in individuals)
                {
                    // we're moving a window by individual size
                    // to a point, where the random number is in the range
                    if (sum < rand && rand < sum + individual.value())
                    {
                        outInd.Add(individual.duplicate());
                        break;
                    }
                    sum += individual.value();
                }
            }
            return outInd;
        }
    }
}
