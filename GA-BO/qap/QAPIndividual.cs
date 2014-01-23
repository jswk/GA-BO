﻿using System;
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

			QAPIndividual firstChild = makeChild(this, qapPartner);
			QAPIndividual secondChild = makeChild(this, qapPartner);

			return new Tuple<algorithm.interfaces.IIndividual, algorithm.interfaces.IIndividual>(firstChild, secondChild);
		}

		// uniform like crossover 
		// http://itc.ktu.lt/itc342/Misev342.pdf
		private QAPIndividual makeChild(QAPIndividual parent1, QAPIndividual parent2)
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

			// Second, the unassigned positions of a permutation are scanned
			// from left to right: for the unassigned position, an item 
			// is chosen randomly, uniformly from those in the 
			// parents if they are not yet included in the child.
			for (int i = 0; i < problemSize; i++)
			{
				if (newPermutation[i] == -1)// no element
				{
					// both parents values are unused
					if (!isUsed[parent1.permutation[i]] && !isUsed[parent2.permutation[i]])
					{
						int fromWhich = random.Next(2);

						int value;
						if (fromWhich == 0)
						{
							value = parent1.permutation[i];
						} 
						else
						{
							value = parent2.permutation[i];
						}

						newPermutation[i] = value;
						isUsed[value] = true;
					}
					else if (!isUsed[parent1.permutation[i]])
					{
						newPermutation[i] = parent1.permutation[i];
						isUsed[newPermutation[i]] = true;
					} 
					else if (!isUsed[parent2.permutation[i]])
					{
						newPermutation[i] = parent2.permutation[i];
						isUsed[newPermutation[i]] = true;
					}
				}
			}

			// Third, remaining items are assigned at random
			List<int> remaining = new List<int>();

			for (int i = 0; i < problemSize; i++)
			{
				if (!isUsed[i])
				{
					remaining.Add(i);
				}
			}

			int[] remainingArray = remaining.ToArray<int>();

			// random shuffle
			remainingArray = remainingArray.OrderBy(x => random.Next()).ToArray<int>();

			int j = 0;
			for (int i = 0; i < problemSize; i++)
			{
				if (newPermutation[i] == -1)
				{
					newPermutation[i] = remainingArray[j];
					isUsed[newPermutation[i]] = true;
					j++;
				}
			}

			return new QAPIndividual(parent1.problem, newPermutation);
		}

        public algorithm.interfaces.IIndividual duplicate()
        {
			return new QAPIndividual(problem, permutation);
        }

	    public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            var ind = obj as QAPIndividual;
            if (ind != null)
            {
                return value().CompareTo(ind.value());
            }
            else
            {
                throw new ArgumentException("Object is not QAPIndividual");
            }
	    }
	}
}
