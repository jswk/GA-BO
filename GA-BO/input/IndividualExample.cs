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
            IIndividual child1 = new IndividualExample(this.x+((IndividualExample)partner).x);
            IIndividual child2 = new IndividualExample(this.x-((IndividualExample)partner).x); 
            return new Tuple<IIndividual,IIndividual>(child1,child2);
        }

        public IIndividual duplicate()
        {
            return (IndividualExample) this.MemberwiseClone();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            var ind = obj as IIndividual;
            if (ind != null)
            {
                return value().CompareTo(ind.value());
            } else
            {
                throw new ArgumentException("Object is not IIndividual");
            }
        }
    }
}
