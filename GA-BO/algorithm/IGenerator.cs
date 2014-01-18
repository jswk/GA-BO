using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_BO.algorithm
{
    public interface IGenerator
    {
        public IIndividual genarate();
    }
}
