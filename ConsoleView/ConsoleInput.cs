using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleView
{
    public class ConsoleInput : IConsoleInput
    {
        public string[] GetLineTokens(string prompt, string expectedInput, int tokenCount)
        {
            while (true)
            {
                Console.Write($"{prompt} ({expectedInput}): ");
                var entry = Console.ReadLine();
                var inputs = entry?.Split(new char[] { ' ', ',' });
                if (inputs == null || inputs.Length != tokenCount)
                {
                    Console.WriteLine($"{entry} is not {expectedInput}.  Try again.");
                    continue;

                }
                return inputs;
            }
        }

        public int[] GetIntegers(string prompt, int numberCount, int min = int.MinValue, int max = int.MaxValue)
        {
            var results = new List<int>();
            while (true)
            {
                Console.Write($"{prompt} ({min} - {max}): ");
                var entry = Console.ReadLine()?.Trim();
                var numbers = entry?.Split(new char[] { ' ', ',' });
                try
                {
                    foreach (var number in numbers)
                    {
                        var value = int.Parse(number);
                        if (value < min || value > max) throw new ArgumentException();
                        results.Add(value);
                    }

                    break;
                }
                catch
                {
                    Console.WriteLine($"{entry} is not {numberCount} comma-separated integers between {min} and {max}.  Try again.");
                    continue;
                }
            }

            return results.ToArray();
        }
    }
}
