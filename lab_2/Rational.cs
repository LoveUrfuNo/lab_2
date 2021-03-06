﻿using System;
using System.Linq;

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
        public int Base => Numerator / Denominator;

        /// Дробная часть числа Z.N:D, N:D
        public int Fraction => Numerator % Denominator;

        private Rational Negate()
        {
            Rational negate = new Rational();
            negate.Numerator = -Numerator;
            negate.Denominator = Denominator;

            return negate;
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
            if (Base != 0 && Fraction != 0)
            {
                return Base + "." + Math.Abs(Fraction) + ":" + Denominator;
            }
            else if (Fraction != 0)
            {
                return Numerator + ":" + Denominator;
            }

            return Base.ToString();

            //string _base = Math.Abs(Base).ToString();
            //_base = _base == "0" ? "" : _base;

            //string sign = Numerator < 0 ? "-" : "";
            //string fraction = (absNumerator == 0 ? "" : absNumerator + ":" + Denominator);

            //string maybeDot = ".";
            //if (_base.Equals("") || fraction.Equals(""))
            //{
            //    maybeDot = "";
            //}

            //return sign + _base + maybeDot + fraction;
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

            try
            {
                string[] numberParts = input.Split('.', ':');

                if (input.LastIndexOf("-") > 0)
                {
                    return false;
                }

                if (input.Where(symbol => symbol == '.').Count() > 1 ||
                    input.Where(symbol => symbol == ':').Count() > 1 ||
                    input.IndexOf('.') > input.IndexOf(':'))
                {
                    return false;
                }

                int sign = int.Parse(numberParts[0]) >= 0 ? 1 : -1;
                if (numberParts.Length == 3)
                {
                    rational.Denominator = int.Parse(numberParts[2]);
                    rational.Numerator =
                        int.Parse(numberParts[0]) * rational.Denominator + sign * int.Parse(numberParts[1]);
                }
                else if (numberParts.Length == 2)
                {
                    rational.Denominator = int.Parse(numberParts[1]);
                    rational.Numerator = int.Parse(numberParts[0]);
                }
                else
                {
                    rational.Numerator = int.Parse(numberParts[0]);
                    rational.Denominator = 1;
                }

                return true;
            }
            catch (Exception)
            {
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

            rational.Numerator = x.Numerator * y.Denominator + y.Numerator * x.Denominator;
            rational.Denominator = x.Denominator * y.Denominator;

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

        public static implicit operator Rational(int x)
        {
            return new Rational
            {
                Numerator = x,
                Denominator = x
            };
        }

        public static explicit operator int(Rational x)
        {
            return x.Base;
        }
    }
}
