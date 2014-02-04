 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.algorithm.interfaces;

namespace GA_BO.algorithm
{
    public class Population
    {
        public List<IIndividual> individuals;
        
        public bool containsIndividual(IIndividual ind)
        {
            foreach (var i in individuals)
                if (i.GetHashCode() == ind.GetHashCode())
                    return true;
            return false;
        }
    }
}
