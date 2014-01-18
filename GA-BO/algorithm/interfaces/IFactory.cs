using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_BO.algorithm.interfaces
{
    interface IFactory
    {
        public Population createPopulation();
        public IIndividual createIndividual(); //maybe it can be deleted, because it's probably used only internally by concrete factories
        public Population nextPopulation(Population parent);
    }
}
