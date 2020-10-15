using System;

namespace GuessNumber
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const int upperBounr = 100;
            const int triesNum = 6;
            var random = new Random();
            var number = random.Next(0, upperBounr);
            var count = 0;
            var isExit = false;
            Console.WriteLine("Try to guess number from 0 to 100 or enter 'q' to exit");
            while (!isExit)
            {
                var inputLine = Console.ReadLine();

                if (inputLine == "q")
                {
                    isExit = true;
                    break;
                }
                else
                {
                    if (!int.TryParse(inputLine, out var result))
                    {
                        Console.WriteLine("Enter a number");
                    }
                    else
                    {

                        if (count > triesNum)
                        {
                            Console.WriteLine("You didn't guess");
                        }
                        else
                        {
                            count++;
                            if (result > number)
                            {
                                Console.WriteLine("Enter less number");
                            }
                            else if (result < number)
                            {
                                Console.WriteLine("Enter greater number");
                            }
                            else if (result == number)
                            {
                                Console.WriteLine("You guessed");
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}