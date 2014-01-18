using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.algorithm.interfaces;

namespace GA_BO.input
{
    public class IndividualExample : IIndividual
    {
        public int x;

        public IndividualExample(int x)
        {
            this.x = x;
        }

        public int value()
        {
            return x*x;
        }

        public void mutate()
        {
            this.x=x+1;
        }

        public Tuple<IIndividual, IIndividual> crossover(IIndividual partner)
        {
            IIndividual child1 = new IndividualExample(this.x+((IndividualExample)partner).x;
            IIndividual child2 = new IndividualExample(this.x-((IndividualExample)partner).x; 
            return new Tuple<IIndividual,IIndividual>(child1,child2);
        }
    }
}
