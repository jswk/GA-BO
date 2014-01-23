using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GA_BO.algorithm;
using GA_BO.input;
using GA_BO.algorithm.enums;

namespace GA_BO.qap
{
	public class QAPTester
	{
		private string testPath = "../../qap/problems/";

		private string resultPath = "results/";

		private QAPProblem problemFromFile(string testName)
		{
			string fileName = testPath + testName;
	        var text = System.IO.File.ReadAllText(fileName);
            char[] separators = { ' ', '\t', '\n', '\r' };
            var numbers = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int n = Int32.Parse(numbers[0]);

			int[][] w = new int[n][];
			int[][] d = new int[n][];
			for (int i = 0; i < n; i++)
			{
				w[i] = new int[n];
				d[i] = new int[n];
			}


            for (int i = 0; i < n; i++)
			{
			
                for (int j = 0; j < n; j++)
                    {
                        w[i][j] = Convert.ToInt32(numbers[1 + (i * n) + j]);
                        d[i][j] = Convert.ToInt32(numbers[1 + (n * n) + (i * n) + j]);
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

            //configuration
			GlobalConfiguration globalConfig = new GlobalConfiguration();
            globalConfig.configurations = new List<IslandConfiguration>();
            globalConfig.configurations.Add(new IslandConfiguration(algorithm.enums.EvolutionStrategy.Tournament, 0.01, 0.1, 20, 10, 1));
            globalConfig.configurations.Add(new IslandConfiguration(algorithm.enums.EvolutionStrategy.Tournament, 0.02, 0.05, 30, 10, 1));
            globalConfig.configurations.Add(new IslandConfiguration(algorithm.enums.EvolutionStrategy.Tournament, 0.05, 0.02, 20, 10, 1));
            globalConfig.configurations.Add(new IslandConfiguration(algorithm.enums.EvolutionStrategy.Tournament, 0.1, 0.01, 30, 10, 1));
            globalConfig.connections = new List<int>[globalConfig.configurations.Count];
            for(int i=0;i<globalConfig.configurations.Count;i++)
                globalConfig.connections[i] = new List<int>();
            //globalConfig.connections[0].Add(3);
            //globalConfig.connections[1].Add(3);
            //globalConfig.connections[2].Add(3);
            globalConfig.evolutionTimeInSeconds = 25;
            globalConfig.generator = new QAPGenerator(problem);

			IslandSupervisor supervisor = new IslandSupervisor(globalConfig);
			QAPIndividual result =(QAPIndividual) supervisor.getResult();
            foreach(int i in result.permutation)
                Console.WriteLine(i.ToString() + " ");
			//int result = 5;
			saveResult(result.value(), testName);
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
