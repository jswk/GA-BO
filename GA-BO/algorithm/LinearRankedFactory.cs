using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.algorithm.interfaces;
using GA_BO.input;


namespace GA_BO.algorithm
{
    class LinearRankedFactory : AbstractFactory
    {
        public LinearRankedFactory(GlobalConfiguration config, IslandConfiguration iconfig)
            : base(config, iconfig)
        {
        }

        protected override List<IIndividual> selection(List<IIndividual> individuals)
        {
            var selected = new List<IIndividual>();
            var size = _iconfig.selectionSize;
            individuals.Sort(MyCompare);
            for (int i = 0; i < size; i++)
            {
                double d = _rand.NextDouble();
                int j = -1;
                int c = individuals.Count;
                while (d > 0)
                {
                    double k = (double)(2 * c) / (individuals.Count * (individuals.Count + 1));
                    d -= k;
                    j++;
                    c--;
                }
                selected.Add(individuals[j].duplicate());
            }


            return selected;
        }

        private static int MyCompare(IIndividual x, IIndividual y)
        {
            return x.value().CompareTo(y.value());
        }
    }
}
