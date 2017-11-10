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
        public const string HELP = "help";
        public const string EXIT = "exit";

        public const int USER_COMMAND_NUMBER = 0;
        public const int FIRST_RATIONAL_NUMBER = 1;
        public const int SECOND_RATIONAL_NUMBER = 2;

        public const string HELP_MESSAGE =
            "\tadd - сложение\n\tsub - вычитание\n\tmul - умножение\n\tdiv - деление\n" +
            "\tСессия работы программы может выглядеть примерно так:\n" +
            "\t >> add 5:7 1.4:5\n\t >> 2.18:35\n\t >> div 5:7 9:5\n\t >> 25:63";

        public static string[] COMMANDS_WITHOUT_NEED_FOR_NUMBER = new string[] { EXIT, HELP };

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
                    if (!COMMANDS_WITHOUT_NEED_FOR_NUMBER.Contains(commandLineParts[USER_COMMAND_NUMBER]))
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
                        result = firstRational + secondRational;
                        break;

                    case SUB:
                        result = firstRational - secondRational;
                        break;

                    case MULTIPLY:
                        result = firstRational * secondRational;
                        break;

                    case DIVIDEBY:
                        result = firstRational / secondRational;
                        break;

                    case HELP:
                        Console.WriteLine(HELP_MESSAGE);
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
                        if (result.Numerator == 0)
                        {   
                            Console.WriteLine(result.ToString());
                        }
                        else
                        {
                            Console.WriteLine(result.ToString());
                        }
                    }
                    Console.Write(" >> ");
                }
            }
        }
    }
}
