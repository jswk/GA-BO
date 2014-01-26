using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.algorithm.interfaces;
using GA_BO.input;

namespace GA_BO.algorithm
{
    class StochasticFactory :AbstractFactory
    {
        public StochasticFactory(GlobalConfiguration config, IslandConfiguration iconfig) : base(config, iconfig)
        {
        }

        protected override List<IIndividual> selection(List<IIndividual> individuals)
        {
            var size = _iconfig.selectionSize;
            var totalFitness = individuals.Aggregate(0.0, (acc, next) => getIndividualFitness(next) + acc);
            var step = totalFitness / size;
            var start = _rand.NextDouble()*step;
            var indOut = new List<IIndividual>();
            var threshold = 0.0;
            foreach (var ind in individuals) // they don't have to be sorted
            {
                /*
                 * |        |         |       |    |   |  | | |
                 *    ^          ^          ^          ^ <- uniformly spaced
                 *    first one randomly chosen from [0, totalFitness / size]
                 *               then skip by totalFitness / size
                 *  less elitist than Roulette which promotes the best individuals the most
                 */
                while (start < threshold + getIndividualFitness(ind))
                {
                    indOut.Add(ind.duplicate());
                    start += step;
                }
                threshold += getIndividualFitness(ind);
            }
            return indOut;
        }
    }
}
