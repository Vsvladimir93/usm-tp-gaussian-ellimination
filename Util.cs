using LinearEquations;
using System;
using System.Text.RegularExpressions;

namespace StudentsApp
{
	sealed class Util
	{
		private static Regex rationalFormats = new Regex(@"^[-]?\d{1,3}([/,\.]\d{1,3})?$");

		private Util() { }

		public static int GetNextInt(int min, int max)
		{
			try
			{
				int number = int.Parse(Console.ReadLine());
				if (number < min || number > max)
				{
					Console.WriteLine(" - Пожалуйста введите число от {0} до {1}", min, max);
					return GetNextInt(min, max);
				}
				else
				{
					return number;
				}
			}
			catch (Exception e)
			{
				if (e is FormatException)
				{
					Console.WriteLine(" - Ошибка формата!");
				}
				else if (e is OverflowException)
				{
					Console.WriteLine(" - Число должно быть в диапазоне от {0} до {1}", int.MinValue, int.MaxValue);
				}
				else
				{
					Console.WriteLine(" - Oops!");
				}

				return GetNextInt(min, max);
			}
		}

		public static Rational GetNextRational()
		{
			string result = Console.ReadLine();
			Rational r = null;
			if (!ParseRational(result, out r))
			{
				Console.WriteLine("Неверный формат рационального числа.");
				r = GetNextRational();
			}
			return r;

		}

		private static bool ParseRational(string input, out Rational result)
		{
			int numerator = 0;
			int denominator = 0;
			bool matches = rationalFormats.Matches(input).Count == 1;
			if (matches)
			{
				string[] digits;
				switch (true)
				{
					case true when input.Contains('/'):
						digits = input.Split('/');
						result = new Rational(int.Parse(digits[0]), int.Parse(digits[1]));
						break;
					case true when input.Contains('.'):
						digits = input.Split('.');

						denominator = (int) Math.Pow(10, digits[1].Length);
						numerator = int.Parse(digits[0]) * denominator + int.Parse(digits[1]);

						result = new Rational(numerator, denominator);
						break;
					case true when input.Contains(','):
						digits = input.Split(',');

						denominator = (int)Math.Pow(10, digits[1].Length);
						numerator = int.Parse(digits[0]) * denominator + int.Parse(digits[1]);

						result = new Rational(numerator, denominator);
						break;
					default:
						result = new Rational(int.Parse(input));
						break;
				}

				return matches;
			}
			else
			{
				result = null;
				return matches;
			}
		}

		public static Rational RandomRational(int min, int max)
		{
			int den = new Random().Next(min, max);
			return new Rational(new Random().Next(min, max), den == 0 ? 1 : den);
		}


	}
}
