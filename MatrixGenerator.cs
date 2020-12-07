using StudentsApp;
using System;

namespace LinearEquations
{
	public class MatrixGenerator
	{

		public Rational[][] GenerateMatrix()
		{
			Rational[][] matrix;
			int size = AskForMatrixSize();
			if (AskIfNeedMatrixGeneration())
			{
				matrix = GenerateRandomMatrix(size);
			}
			else
			{
				matrix = ManualMatrixInput(size);
			}

			Console.WriteLine(new string('_', Console.WindowWidth));
			Console.WriteLine("Система линейных равнений");

			for (int i = 0; i < matrix.Length; i++) 
			{
				for (int j = 0; j < matrix[i].Length; j++)
				{
					if (j == matrix.Length)
					{
						Console.Write(matrix[i][j]);
					}
					else
					{
						char op = j == matrix.Length - 1 ? '=' : '+';
						Console.Write("({0})X{1} {2} ",matrix[i][j], j + 1, op);
					}
					
				}
				Console.Write("\n");
			}

			Console.WriteLine();
			Console.WriteLine(new string('_', Console.WindowWidth));

			return matrix;
		}

		private int AskForMatrixSize()
		{
			Console.WriteLine("Введите размер системы линейных уравнений: ");
			return Util.GetNextInt(2, 50);
		}

		private bool AskIfNeedMatrixGeneration()
		{
			Console.WriteLine("Выберите способ генерации коэффициентов:");
			Console.WriteLine("1) Вручную.");
			Console.WriteLine("2) Генерация случайных коэффициентов.");
			return Util.GetNextInt(1, 2) == 2;
		}

		private Rational[][] GenerateRandomMatrix(int size)
		{
			Rational[][] matrix = new Rational[size][];
			for (int row = 0; row < size; row++)
			{
				matrix[row] = new Rational[size + 1];
				for (int col = 0; col < size + 1; col++)
				{
					matrix[row][col] = Util.RandomRational(-3, 3);
				}
			}
			return matrix;
		}


		private Rational[][] ManualMatrixInput(int size)
		{
			Rational[][] matrix = new Rational[size][];
			for (int row = 0; row < size; row++)
			{
				matrix[row] = new Rational[size + 1];
				for (int col = 0; col < size + 1; col++)
				{
					if (col == size)
					{
						Console.WriteLine("Введите результат {0} уравнения: ", row + 1);
					}
					else
					{
						Console.WriteLine("Введите {0} коэффициент для {1} уравнения: ", col + 1, row + 1);
					}
					matrix[row][col] = Util.GetNextRational();
				}
			}
			return matrix;
		}

	}
}
