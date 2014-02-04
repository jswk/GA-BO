using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.algorithm.interfaces;
using GA_BO.input;

namespace GA_BO.algorithm
{
    class TruncationFactory : AbstractFactory   //based on LinearRankedFactory
    {
        public TruncationFactory(GlobalConfiguration config, IslandConfiguration iconfig)
            : base(config, iconfig)
        {
        }

        protected override List<IIndividual> selection(List<IIndividual> individuals)
        {
            var selected = new List<IIndividual>();
            var size = _iconfig.selectionSize;
            double trunc = 0.75;  // 0.5 - 0.9 :would be nice to be able to put in config
            int newLength = (int)(individuals.Count * trunc);
            if (newLength <= size)
                newLength = size;
            individuals.Sort(MyCompare);
            for (int i = 0; i < size; i++)
            {
                double d = _rand.NextDouble();
                int j = -1;
                int c = newLength;
                while (d > 0)
                {
                    double k = (double)(2 * c) / (newLength * (newLength + 1));
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
