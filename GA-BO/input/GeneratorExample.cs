using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.algorithm.interfaces;

namespace GA_BO.input
{
    class GeneratorExample : IGenerator
    {

        public IIndividual genarate()
        {
            Random rnd = new Random();
            return new IndividualExample(rnd.Next(1, 100));
        }
    }
}
