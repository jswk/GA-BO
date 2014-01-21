using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_BO.qap
{
	class QAPGenerator : algorithm.interfaces.IGenerator
	{
		private Random random = new Random();

		private QAPProblem problem;

		public QAPGenerator(QAPProblem problem)
		{
			this.problem = problem;
		}

		private int[] randomPermutation(int size)
		{
			int[] permutation = new int[size];
			for (int i = 0; i < size; i++)
			{
				permutation[i] = i;
			}
			return permutation.OrderBy(x => random.Next()).ToArray<int>();
		}

		public algorithm.interfaces.IIndividual generate()
		{
			return new QAPIndividual(problem, randomPermutation(problem.ProblemSize));
		}
	}
}
