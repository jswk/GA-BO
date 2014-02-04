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
		    permutation = value;
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

        private int pos(int[] t, int element)
		{
			for (int i = 0; i < t.Length; i++)
			{
				if (t[i] == element)
				{
					return i;
				}
			}
			return -1;
		}

        public Tuple<algorithm.interfaces.IIndividual, algorithm.interfaces.IIndividual> crossover(algorithm.interfaces.IIndividual partner)
        {
            QAPIndividual qapPartner = (QAPIndividual)partner; // ugly :/

            //QAPIndividual firstChild = makeChild(this, qapPartner);
            //QAPIndividual secondChild = makeChild(this, qapPartner);

            //return new Tuple<algorithm.interfaces.IIndividual, algorithm.interfaces.IIndividual>(firstChild, secondChild);

            int n = this.problem.ProblemSize;

            int[] firstPermutation = this.permutation;
            int[] secondPermutation = qapPartner.permutation;

            int[][] permutations = new int[2][];
            permutations[0] = firstPermutation;
            permutations[1] = secondPermutation;

            int[] offspring1 = new int[n];
            int[] offspring2 = new int[n];

            for (int i = 0; i < n; i++)
            {
                offspring1[i] = -1;
                offspring2[i] = -1;
            }

            //printArray(offspring1);

            for (int i = 0; i < n; i++)
            {
                //Console.WriteLine(i);
                if (offspring1[i] == -1)
                {
                    //Console.WriteLine(i);
                    int par = random.Next(2);

                    int start1 = permutations[par][i];
                    int start2 = permutations[1 - par][i];

                    int el1 = start1;
                    int el2 = start2;


                    offspring1[i] = el1;
                    offspring2[i] = el2;

                    int pos1 = pos(permutations[1 - par], el1);
                    int pos2 = pos(permutations[par], el2);



                    //Console.WriteLine("" + pos1 + " " + pos2 + " " + el1 + " " + el2 + " " + par + " " + i);
                    //printArray(offspring1);
                    //printArray(offspring2);

                    //Console.WriteLine("new pos1 = " + pos1 + " new pos2 = " + pos2);

                    while (i != pos1)
                    {
                        el1 = permutations[par][pos1];
                        el2 = permutations[1 - par][pos2];

                        offspring1[pos1] = el1;
                        offspring2[pos2] = el2;


                        //Console.WriteLine("" + pos1 + " " + pos2 + " " + el1 + " " + el2);
                        //printArray(offspring1);
                        //printArray(offspring2);

                        pos1 = pos(permutations[1 - par], el1);
                        pos2 = pos(permutations[par], el2);

                        //Console.ReadKey();
                    }
                }
            }

            return new Tuple<algorithm.interfaces.IIndividual, algorithm.interfaces.IIndividual>(new QAPIndividual(problem, offspring1), new QAPIndividual(problem, offspring2));
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
                    QAPIndividual parentOther;
                    if (from == 0)
                    {
                        parent = parent1;
                        parentOther = parent2;
                    }
                    else
                    {
                        parent = parent2;
                        parentOther = parent1;
                    }

                    int pos = i;
                    do
                    {
                        var val = parentOther.permutation[pos];
                        newPermutation[pos] = parent.permutation[pos];
                        for (var j = 0; j < problemSize; j++)
                        {
                            if (parent.permutation[j] != val) continue;
                            pos = j;
                            break;
                        }
                    } while (pos != i);
                }
            }

            return new QAPIndividual(problem, newPermutation);
        }

	    private static Boolean HasDuplicates(IList<int> newPermutation, int problemSize)
	    {
	        for (var i = 0; i < problemSize - 1; i++)
	        {
	            var val = newPermutation[i];
	            for (var j = i + 1; j < problemSize; j++)
	            {
	                if (val == newPermutation[j])
	                {
	                    return true;
	                }
	            }
	        }
	        return false;
	    }

	    private static string GetPermutationString(int problemSize, IList<int> permutation)
        {
            var outstr = "";
            var sep = "";
            for (var i = 0; i < problemSize; i++)
            {
                if (permutation[i] != -1)
                {
                    outstr += sep + permutation[i];
                } else
                {
                    outstr += sep + "-";
                }
                sep = ",";
            }
	        return outstr + "\n";
        }

	    public algorithm.interfaces.IIndividual duplicate()
        {
            var perm = new int[problem.ProblemSize];
            Array.Copy(permutation, perm, problem.ProblemSize);
			return new QAPIndividual(problem, perm);
        }

	    private static IList<QAPIndividual> lok = new List<QAPIndividual>();
	}
}
