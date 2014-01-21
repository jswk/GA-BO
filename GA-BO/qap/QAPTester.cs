using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GA_BO.algorithm;
using GA_BO.input;

namespace GA_BO.qap
{
	public class QAPTester
	{
		private string testPath = "../../qap/problems/";

		private string resultPath = "results/";

		private QAPProblem problemFromFile(string testName)
		{
			string fileName = testPath + testName;
			var lines = System.IO.File.ReadAllLines(fileName);
			int n = Int32.Parse(lines[0]);

			int[][] w = new int[n][];
			int[][] d = new int[n][];
			for (int i = 0; i < n; i++)
			{
				w[i] = new int[n];
				d[i] = new int[n];
			}

			char[] separators = { ' ', '\t' };

			for (int i = 2; i < n + 2; i++)
			{
				string[] numbers = lines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
				for (int j = 0; j < numbers.Length; j++)
				{
					w[i - 2][j] = Convert.ToInt32(numbers[j]);
				}
			}

			for (int i = n + 3; i < 2 * n + 3; i++)
			{
				string[] numbers = lines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
				for (int j = 0; j < numbers.Length; j++)
				{
					d[i - n - 3][j] = Convert.ToInt32(numbers[j]);
				}
			}

			return new QAPProblem(n, w, d);
		}

		private void saveResult(int result, string file)
		{
			if (!Directory.Exists(resultPath))
			{
				Directory.CreateDirectory(resultPath);
			}
			System.IO.File.WriteAllText(resultPath + file, "" + result + "\n");
		}

		// results in "qap/results/testName"
		public void test(string testName)
		{
			QAPProblem problem = problemFromFile(testName);
			GlobalConfiguration globalConfig = new GlobalConfiguration();
			// TODO wybrac odpowiednia konfiguracje
			//IslandSupervisor supervisor = new IslandSupervisor(globalConfig);
			//int result = supervisor.getResult().value();
			int result = 5;
			saveResult(result, testName);
		}

		// results in "qap/results"
		public void allTest()
		{
			var files = Directory.GetFiles(testPath);
			foreach (var filePath in files)
			{
				var splited = filePath.Split(new char[] {'/'});
				var fileName = splited[splited.Length - 1];
				Console.WriteLine(fileName);
				test(fileName);
			}
		}
	}
}
