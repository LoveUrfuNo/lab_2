using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab_2
{
    class Program
    {
        public const string ADD = "add";
        public const string SUB = "sub";
        public const string MULTIPLY = "mul";
        public const string DIVIDEBY = "div";
        public const string EXIT = "exit";

        public const int USER_COMMAND_NUMBER = 0;
        public const int FIRST_RATIONAL_NUMBER = 1;
        public const int SECOND_RATIONAL_NUMBER = 2;

        public static void Main(string[] args)
        {
            Console.WriteLine("Привет! Я жду команд!");
            Console.Write(" >> ");

            ProgramCycle();
        }

        private static void ProgramCycle()
        {
            bool isProgramRunning = true;
            while (isProgramRunning)
            {
                string commandLine = Console.ReadLine();
                string[] commandLineParts = commandLine.ToLower().Split(' ');

                Rational firstRational = new Rational();
                Rational secondRational = new Rational();
                try
                {
                    if (!commandLineParts[USER_COMMAND_NUMBER].Equals(EXIT))
                    {
                        bool isFirstRationalCorrect = Rational.TryParse(
                            commandLineParts[FIRST_RATIONAL_NUMBER], out firstRational);
                        bool isSecondRationalCorrect = Rational.TryParse(
                            commandLineParts[SECOND_RATIONAL_NUMBER], out secondRational);
                        if (!(isFirstRationalCorrect || isSecondRationalCorrect))
                        {
                            throw new FormatException();
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Введите корректное кол-во чисел");
                    Console.Write(" >> ");

                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Неверный формат введеных чисел");
                    Console.Write(" >> ");

                    continue;
                }

                Rational result = new Rational();
                switch (commandLineParts[USER_COMMAND_NUMBER])
                {
                    case ADD:
                        result = firstRational.Add(secondRational);
                        break;

                    case SUB:
                        result = firstRational.Add(secondRational.Negate());
                        break;

                    case MULTIPLY:
                        result = firstRational.Multiply(secondRational);
                        break;

                    case DIVIDEBY:
                        result = firstRational.DivideBy(secondRational);
                        break;

                    case EXIT:
                        isProgramRunning = false;
                        Console.WriteLine("Пока");
                        break;

                    default:
                        Console.WriteLine("Вы ввели некоректную команду");
                        break;
                }

                if (isProgramRunning)
                {
                    if (result.Numerator != 0 && result.Denominator != 0)
                    {
                        Console.WriteLine(result);
                    }
                    Console.Write(" >> ");
                }
            }
        }
    }
}
