using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_BO.qap
{
	class QAPIndividual : algorithm.interfaces.IIndividual
	{
		private QAPProblem problem;

		public int[] permutation;

		private Random random = new Random();

		public QAPIndividual(QAPProblem problem, int[] value)
		{
			this.problem = problem;
			this.permutation = value;
		}

        public override int GetHashCode()
        {
            int hash = 0;
            for (int i = 0; i < problem.ProblemSize; i++)
                hash += i * permutation[i];
            return hash;
        }

        public int value()
		{
			int cost = 0;
			for (int i = 0; i < problem.ProblemSize; i++)
			{
				for (int j = 0; j < problem.ProblemSize; j++)
				{
					cost += (problem.W[i][j] * problem.D[permutation[i]][permutation[j]]);
				}
			}

			return cost;
		}

		// swaps two elements in permutation
		public void mutate()
		{
			int firstIndex = random.Next(problem.ProblemSize);

			// It should draw from range [0,firstIndex)u(firstIndex,problemSize)
			// so let subtract 1 from each element of right range, draw from range
			// [0, problemSize - 1] and after drawing, if secondIndex is equal or 
			// greater than first, add 1
			int secondIndex = random.Next(problem.ProblemSize - 1);
			if (secondIndex >= firstIndex)
			{
				secondIndex += 1;
			}

			int tmp = permutation[firstIndex];
			permutation[firstIndex] = permutation[secondIndex];
			permutation[secondIndex] = tmp;
        }

        public Tuple<algorithm.interfaces.IIndividual, algorithm.interfaces.IIndividual> crossover(algorithm.interfaces.IIndividual partner)
        {
            QAPIndividual qapPartner = (QAPIndividual)partner; // ugly :/

            Console.WriteLine("one");
            QAPIndividual firstChild = CXCrossover(this, qapPartner);
            Console.WriteLine("two");
            QAPIndividual secondChild = CXCrossover(this, qapPartner);
            Console.WriteLine("return");

            return new Tuple<algorithm.interfaces.IIndividual, algorithm.interfaces.IIndividual>(firstChild, secondChild);
        }

        private QAPIndividual CXCrossover(QAPIndividual parent1, QAPIndividual parent2)
        {
            int problemSize = parent1.problem.ProblemSize;
            int[] newPermutation = new int[problemSize];
            bool[] isUsed = new bool[problemSize];

            // First, all items assigned to the same position in both parents are copied to this position in the child. 
            for (int i = 0; i < problemSize; i++)
            {
                newPermutation[i] = -1;// no element

                if (parent1.permutation[i] == parent2.permutation[i])
                {
                    newPermutation[i] = parent1.permutation[i];
                    isUsed[newPermutation[i]] = true;
                }
            }


            for (int i = 0; i < newPermutation.Length; i++)
            {
                if (newPermutation[i] == -1)
                {
                    int from = random.Next(2);
                    QAPIndividual parent;
                    if (from == 0)
                    {
                        parent = parent1;
                    }
                    else
                    {
                        parent = parent2;
                    }

                    int startPos = i;
                    int v = parent.permutation[startPos];

                    newPermutation[startPos] = v;

                    int pos = v;
                    while (pos != i)
                    {
                        v = parent.permutation[pos];
                        newPermutation[pos] = v;
                        pos = v;
                    }

                    newPermutation[pos] = parent.permutation[pos];
                }
            }

            return new QAPIndividual(problem, newPermutation);
        }

        public algorithm.interfaces.IIndividual duplicate()
        {
			return new QAPIndividual(problem, permutation);
        }
	}
}
