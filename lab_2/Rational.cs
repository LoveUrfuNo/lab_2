using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    public struct Rational
    {
        /// Числитель дроби
        public int Numerator { get; set; }

        /// Знаменатель дроби
        public int Denominator { get; set; }

        /// Целая часть числа Z.N:D, Z. получается делением числителя на знаменатель и
        /// отбрасыванием остатка
        public int Base => (int)Math.Truncate((double)Numerator / Denominator);

        /// Дробная часть числа Z.N:D, N:D
        public double Fraction => Numerator / Denominator - Base;

        /// Операция сложения, возвращает новый объект - рациональное число,
        /// которое является суммой чисел c и this
        public Rational Add(Rational c)
        {
            Rational rational = new Rational();
            rational.Numerator = c.Numerator * this.Denominator + this.Numerator * c.Denominator;
            rational.Denominator = c.Denominator * this.Denominator;

            Even();

            return rational;
        }

        /// Операция смены знака, возвращает новый объект - рациональное число,
        /// которое являтеся разностью числа 0 и this
        public Rational Negate()
        {
            Rational rational = new Rational();
            rational.Denominator = this.Denominator;
            rational.Numerator = -this.Numerator;

            Even();

            return rational;
        }

        /// Операция умножения, возвращает новый объект - рациональное число,
        /// которое является результатом умножения чисел x и this
        public Rational Multiply(Rational x)
        {
            Rational rational = new Rational();
            rational.Numerator = x.Numerator * this.Numerator;
            rational.Denominator = x.Denominator * this.Denominator;

            Even();

            return rational;
        }

        /// Операция деления, возвращает новый объект - рациональное число,
        /// которое является результатом деления this на x
        public Rational DivideBy(Rational x)
        {
            Rational rational = new Rational();
            rational.Numerator = this.Numerator * x.Denominator;
            rational.Denominator = this.Denominator * x.Numerator;

            return rational;
        }

        /// Вовзращает строковое представление числа в виде Z.N:D, где
        /// Z — целая часть N и D — целые числа, числитель и знаменатель, N < D
        /// '.' — символ, отличающий целую часть от дробной,
        /// ':' — символ, обозначающий знак деления
        /// если числитель нацело делится на знаменатель, то
        /// строковое представление не отличается от целого числа
        /// (исчезает точка и всё, что после точки)
        /// Если Z = 0, опускается часть представления до точки включительно
        public override string ToString()
        {
            int absNumerator = Math.Abs(Numerator);
            while (absNumerator >= Denominator)
            {
                absNumerator -= Denominator;
            }

            string _base = Math.Abs(Base).ToString();
            _base = _base == "0" ? "" : _base;

            string sign = Numerator < 0 ? "-" : "";
            string fraction = (absNumerator == 0 ? "" : absNumerator + ":" + Denominator);

            string maybeDot = ".";
            if (_base.Equals("") || fraction.Equals(""))
            {
                maybeDot = "";
            }

            return sign + _base + maybeDot + fraction;
        }

        /// Создание экземпляра рационального числа из строкового представления Z.N:D
        /// допускается N > D, также допускается
        /// Строковое представления рационального числа
        /// Результат конвертации строкового представления в рациональное
        /// число
        /// true, если конвертация из строки в число была успешной,
        /// false если строка не соответствует формату
        public static bool TryParse(string input, out Rational rational)
        {
            rational = new Rational();

            string[] numberParts = input.Split('.', ':');
            try
            {
                int minusCounter = 0;
                foreach (string part in numberParts)
                {
                    if (part.Contains('-'))
                    {
                        minusCounter++;
                    }
                }

                if (minusCounter > 1)
                {
                    throw new FormatException();
                }

                int sign = int.Parse(numberParts[0]) >= 0 ? 1 : -1;
                if (input.Contains('.'))
                {
                    rational.Denominator = int.Parse(numberParts[2]);
                    rational.Numerator =
                        int.Parse(numberParts[0]) * rational.Denominator + sign * int.Parse(numberParts[1]);                   
                }
                else
                {
                    if (numberParts.Length == 1)
                    {
                        rational.Numerator = int.Parse(numberParts[0]);
                        rational.Denominator = 1;
                    }
                    else
                    {
                        rational.Denominator = int.Parse(numberParts[1]);
                        rational.Numerator = int.Parse(numberParts[0]);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Проверьте введенные данные. Числа должны иметь вид: Z.N:D или Z");

                return false;
            }
        }

        /// Приведение дроби - сокращаем дробь на общие делители числителя
        /// и знаменателя. Вызывается реализацией после каждой арифметической операции
        private void Even()
        {
            int nod = getNod(Numerator, Denominator);
            if (nod != 0)
            {
                Denominator /= nod;
                Numerator /= nod;
            }
        }

        private int getNod(int x, int y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);

            return y == 0 ? x : getNod(y, x % y);
        }

        public static Rational operator +(Rational x, Rational y)
        {
            Rational rational = new Rational();
            if (y.Denominator != x.Denominator)
            {
                int cDenominator = x.Denominator;
                x.Denominator *= cDenominator;
                x.Numerator *= y.Denominator;
                y.Denominator *= cDenominator;
                y.Numerator *= cDenominator;

                rational.Denominator = y.Denominator;
            }
            else
            {
                rational.Denominator = y.Denominator;
            }

            rational.Numerator = y.Numerator + x.Numerator;
            rational.Even();

            return rational;
        }

        public static Rational operator *(Rational x, Rational y)
        {
            Rational rational = new Rational();
            rational.Denominator = x.Denominator * y.Denominator;
            rational.Numerator = x.Numerator * y.Numerator;
            rational.Even();

            return rational;
        }

        public static Rational operator /(Rational x, Rational y)
        {
            Rational rational = new Rational();
            rational.Numerator = x.Numerator * y.Denominator;
            rational.Denominator = x.Denominator * y.Numerator;
            rational.Even();

            return rational;
        }

        public static Rational operator -(Rational x, Rational y)
        {
            return x + y.Negate();
        }
    }
}
