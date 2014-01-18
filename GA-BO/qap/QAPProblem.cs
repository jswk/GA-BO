using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_BO.qap
{
	class QAPProblem
	{
		private int problemSize;

		private int[][] w;

		private int[][] d;

		public QAPProblem(int problemSize, int[][] w, int[][] d)
		{
			this.problemSize = problemSize;
			this.w = w;
			this.d = d;
		}

		public int ProblemSize
		{
			get { return problemSize; }
		}

		public int[][] W
		{
			get { return w; }
		}

		public int[][] D
		{
			get { return d; }
		}
	}
}
