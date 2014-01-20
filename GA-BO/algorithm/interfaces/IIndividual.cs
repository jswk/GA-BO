using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_BO.algorithm.interfaces
{
    public interface IIndividual
    {
        int value();
        void mutate();
        Tuple<IIndividual, IIndividual> crossover(IIndividual partner); //http://stackoverflow.com/questions/166089/what-is-c-sharp-analog-of-c-stdpair

        IIndividual duplicate();
    }
}
