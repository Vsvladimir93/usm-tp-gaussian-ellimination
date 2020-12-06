using System;
using System.Collections.Generic;
using static LinearEquations.Gauss;

namespace LinearEquations
{
	class Program
	{
		static void Main(string[] args)
		{

			MatrixGenerator matrixGenerator = new MatrixGenerator();

			Rational[][] matrix = matrixGenerator.GenerateMatrix();

			Gauss gauss = new Gauss();
			KeyValuePair<Result, Rational[]> result = gauss.Solve(matrix);

			Console.WriteLine(new string('_', Console.WindowWidth));

			switch (result.Key)
			{
				case Result.MANY_SOLUTIONS:
					Console.WriteLine("Имеет множество решений.");
					break;
				case Result.HAS_SOLUTION:
					Console.WriteLine("Решение:");
					for (int i = 1; i <= result.Value.Length; i++)
					{
						Console.Write("x{0} = {1}\t", i, result.Value[i - 1]);
					}
					Console.WriteLine();
					break;
				case Result.NO_SOLUTION:
					Console.WriteLine("Не имеет решения.");
					break;
			}

			Console.WriteLine(new string('_', Console.WindowWidth));

		}
	}
}
