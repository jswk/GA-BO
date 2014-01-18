using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_BO.algorithm
{
    public interface IIndividual
    {
        public int value();
        public void mutate();
        public Tuple<IIndividual, IIndividual> crossover(IIndividual partner); //http://stackoverflow.com/questions/166089/what-is-c-sharp-analog-of-c-stdpair
    }
}
