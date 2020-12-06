using System;

namespace LinearEquations
{
	public class Rational : ICloneable
	{
		private int _numerator;
		private int _denominator;

		public Rational(int numerator, int denominator)
		{

			if (denominator == 0)
			{
				throw new ArgumentException("Знаменатель не может быть меньше либо равен нулю.");
			}
			else if (denominator < 0)
			{
				_numerator = -numerator;
				_denominator = -denominator;
			}
			else
			{
				_numerator = numerator;
				_denominator = denominator;
			}
			Simplify();
		}

		public Rational(int numerator) : this(numerator, 1) { }

		public static bool operator ==(Rational r1, Rational r2)
		{
			return (r1.Numerator == r2.Numerator) && (r1.Denominator == r2.Denominator);
		}

		public static bool operator !=(Rational r1, Rational r2)
		{
			return (r1.Numerator != r2.Numerator) || (r1.Denominator != r2.Denominator);
		}

		public static Rational operator *(Rational r1, Rational r2)
		{
			return new Rational(r1.Numerator * r2.Numerator, r1.Denominator * r2.Denominator);
		}

		public static Rational operator /(Rational r1, Rational r2)
		{
			if (r1.Numerator < 0 && r2.Numerator < 0)
			{
				r1 = new Rational(Math.Abs(r1.Numerator), r1.Denominator);
				r2 = new Rational(Math.Abs(r2.Numerator), r2.Denominator);
			}

			Rational r = r1 * new Rational(r2.Denominator, r2.Numerator);
			if (r.Numerator == r.Denominator)
			{
				return new Rational(1);
			}
			else
			{
				return r;
			}
		}

		public static Rational operator +(Rational r1, Rational r2)
		{
			return new Rational(r1.Numerator * r2.Denominator + r1.Denominator * r2.Numerator, r1.Denominator * r2.Denominator);
		}

		public static Rational operator -(Rational r1, Rational r2)
		{
			return new Rational(r1.Numerator * r2.Denominator - r1.Denominator * r2.Numerator, r1.Denominator * r2.Denominator);
		}

		public static Rational operator -(Rational r1)
		{
			return new Rational(-r1.Numerator, r1.Denominator);
		}

		public int Numerator { get => _numerator; }

		public int Denominator { get => _denominator; }

		public bool IsInteger() => _denominator == 1;

		override
		public string ToString() => _denominator == 1 ? _numerator.ToString() : _numerator + "/" + _denominator;

		public override bool Equals(object obj)
		{
			if (obj.GetType() == this.GetType())
			{
				return (this == (Rational)obj);
			}

			return false;
		}

		public override int GetHashCode()
		{
			return (_numerator | (_denominator << 16));
		}

		private void Simplify()
		{
			if (Math.Abs(_numerator) == _denominator)
			{
				_numerator = Math.Sign(_numerator) < 0 ? -1 : 1;
				_denominator = 1;
				return;
			}

			int gcd = GreatestCommonDivisor(_numerator, _denominator);

			_numerator /= gcd;
			_denominator /= gcd;
		}

		private int GreatestCommonDivisor(int n, int d)
		{
			n = Math.Abs(n);

			while (n != 0 && d != 0)
			{
				if (n > d)
					n %= d;
				else
					d %= n;
			}

			return n | d;
		}

		public Rational CloneRational()
		{
			return Clone() as Rational;
		}

		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}
