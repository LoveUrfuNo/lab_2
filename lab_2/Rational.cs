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
            Rational result = new Rational();
            result.Numerator = c.Numerator * this.Denominator + this.Numerator * c.Denominator;
            result.Denominator = c.Denominator * this.Denominator;

            Even();

            return result;
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
            int numerator = Numerator;
            while (numerator >= Denominator)
            {
                numerator -= Denominator;
            }

            string fraction = (numerator == 0 ? "" : "." + numerator + ":" + Denominator);
            string result = Base == 0 ? fraction.Replace(".", "") : Base + fraction;

            return result;
        }

        /// Создание экземпляра рационального числа из строкового представления Z.N:D
        /// допускается N > D, также допускается
        /// Строковое представления рационального числа
        /// Результат конвертации строкового представления в рациональное
        /// число
        /// true, если конвертация из строки в число была успешной,
        /// false если строка не соответствует формату
        public static bool TryParse(string input, out Rational result)
        {
            result = new Rational();

            if (input.Contains('-'))
            {
                Console.WriteLine("Can't parse negative value");
                
                return false;
            }

            string[] numberParts = input.Split('.', ':');
            try
            {
                if (input.Contains('.'))
                {
                    result.Denominator = int.Parse(numberParts[2]);
                    result.Numerator =
                        int.Parse(numberParts[0]) * result.Denominator + int.Parse(numberParts[1]);
                }
                else
                {
                    result.Denominator = int.Parse(numberParts[1]);
                    result.Numerator = int.Parse(numberParts[0]);
                }

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Проверьте введенные данные");

                return false;
            }
        }

        /// Приведение дроби - сокращаем дробь на общие делители числителя
        /// и знаменателя. Вызывается реализацией после каждой арифметической операции
        private void Even()
        {
            if (Numerator == 0 || Denominator == 0)
            {
                return;
            }

            int nod = getNod(Numerator, Denominator);
            Numerator = Numerator / nod;
            Denominator = Denominator / nod;
        }

        private int getNod(int x, int y)
        {
            return y == 0 ? x : getNod(y, x % y);
        }
    }
}
