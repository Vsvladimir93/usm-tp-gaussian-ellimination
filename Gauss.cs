using System;
using System.Collections.Generic;
using System.Linq;

namespace LinearEquations
{
	public class Gauss
	{

		public enum Result { CONTINUE, HAS_SOLUTION, MANY_SOLUTIONS, NO_SOLUTION }

		public KeyValuePair<Result, Rational[]> Solve(Rational[][] initialMatrixState)
		{
			Rational[][] matrix = initialMatrixState.Select(row =>
						row.Select(rational => rational.CloneRational()).ToArray()
					).ToArray();

			Result res = Result.CONTINUE;

			for (int index = 0; index < matrix.Length; index++)
			{
				PrintMatrix(matrix);
				res = TransformColumnToZero(matrix, index, index);
				if (res != Result.CONTINUE)
					break;
			}

			PrintMatrix(matrix);

			if (res != Result.CONTINUE)
			{
				return KeyValuePair.Create(res, new Rational[] { });
			}
			else
			{
				return CheckSolution(initialMatrixState, matrix);
			}
		}



		// Приводим коэффициент строки матрциы к единице
		private Result TransformCoeficientToOne(Rational[][] matrix, int row, int col)
		{
			Rational tmp = matrix[row][col];

			if (tmp.Numerator == 0)
				return Result.CONTINUE;

			for (int i = col; i < matrix[row].Length; i++)
			{
				matrix[row][i] /= tmp;
			}

			return Result.CONTINUE;
		}


		// Приводим коэффициенты каждой колонки матрицы к нулю
		// И каждый опорный коефициент матрицы к единице
		private Result TransformColumnToZero(Rational[][] matrix, int row, int col)
		{
			Result res = Result.CONTINUE;
			if (matrix[row][col] != new Rational(1))
				res = TransformCoeficientToOne(matrix, row, col);

			if (res != Result.CONTINUE)
				return res;

			for (int rowI = 0; rowI < matrix.Length; rowI++)
			{
				// Пропускаем текущую строку
				if (rowI == row) continue;

				// Берем опорный коэффициент с противоположным знаком
				Rational pivot = -matrix[rowI][col];

				for (int colI = col; colI < matrix[rowI].Length; colI++)
				{
					// Умножаем опорный коэффициент на каждый элемент каждой строки исключая текущую (row) и складываем
					// Тем самым мы получаем единицу в опорном коэффициенте сохраняя соотношение матрицы
					matrix[rowI][colI] += matrix[row][colI] * pivot;
				}
			}

			return res;
		}

		private KeyValuePair<Result, Rational[]> CheckSolution(Rational[][] initialState, Rational[][] transformedMatrix)
		{
			KeyValuePair<Result, Rational> rowResult = KeyValuePair.Create(Result.CONTINUE, new Rational(0));
			for (int row = 0; row < initialState.Length; row++)
			{
				rowResult = CheckRow(initialState, transformedMatrix, row);
				if (rowResult.Key != Result.CONTINUE)
					break;
			}

			return KeyValuePair.Create(
				rowResult.Key == Result.CONTINUE ? Result.HAS_SOLUTION : rowResult.Key,
				transformedMatrix.Select(row => row[row.Length - 1]).ToArray());
		}

		private KeyValuePair<Result, Rational> CheckRow(Rational[][] initialState, Rational[][] transformedMatrix, int row)
		{
			Rational rowResult = new Rational(0);
			for (int col = 0; col < initialState[row].Length - 1; col++)
			{
				rowResult += initialState[row][col] * transformedMatrix[col][transformedMatrix[row].Length - 1];
			}

			if (rowResult == new Rational(0))
			{
				return KeyValuePair.Create(Result.MANY_SOLUTIONS, rowResult);
			}
			else if (rowResult == initialState[row][initialState[row].Length - 1])
			{
				return KeyValuePair.Create(Result.CONTINUE, rowResult);
			}
			else
			{
				return KeyValuePair.Create(Result.NO_SOLUTION, rowResult);
			}
		}

		private void PrintMatrix(Rational[][] matrix)
		{
			Console.WriteLine(new string('_', matrix.Length * 10));
			char delimiter = ' ';
			for (int i = 0; i < matrix.Length; i++)
			{
				for (int j = 0; j < matrix[i].Length; j++)
				{
					delimiter = (j == 0 || j == matrix.Length) ? '|' : ' ';
					string column = string.Format("{0} {1,10}", delimiter, matrix[i][j]);
					Console.Write(column);
				}
				Console.Write("|\n");
			}
			Console.WriteLine(new string('_', matrix.Length * 10));
			Console.WriteLine();
		}

	}
}
