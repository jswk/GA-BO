﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.algorithm.interfaces;

namespace GA_BO.input
{
    public class GlobalConfiguration
    {
        public IGenerator generator;
        public List<int>[] connections;
        public List<IslandConfiguration> configurations;
        public Boolean maximize;
        public int evolutionTimeInSeconds;
    }
}
