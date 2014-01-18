using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_BO.algorithm.interfaces
{
    public interface IFactory
    {
        Population createPopulation();
        IIndividual createIndividual(); //maybe it can be deleted, because it's probably used only internally by concrete factories
        Population nextPopulation(Population parent);
    }
}
