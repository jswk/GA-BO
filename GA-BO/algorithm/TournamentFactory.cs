using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.algorithm.interfaces;
using GA_BO.input;

namespace GA_BO.algorithm
{
    class TournamentFactory : AbstractFactory
    {
        public TournamentFactory(GlobalConfiguration config, IslandConfiguration iconfig) : base(config, iconfig)
        {
        }

        protected override List<IIndividual> selection(List<IIndividual> individuals)
        {
            var pressure = 0.95; // would be nice to be able to put in config
            var selected = new List<IIndividual>();
            var size = _iconfig.selectionSize;
            for (var i = 0; i < size; i++)
            {
                var one = individuals.ElementAt(_rand.Next(individuals.Count));
                var two = individuals.ElementAt(_rand.Next(individuals.Count));
                var better = one.CompareTo(two) < 0 ? one : two;
                var worse  = one.CompareTo(two) > 0 ? two : one;
                selected.Add((_rand.NextDouble() < pressure) ? better.duplicate() : worse.duplicate());
            }
            return selected;
        }
    }
}
