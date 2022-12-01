using System;
using System.Linq;
using System.Collections.Generic;

namespace AoC2022
{
    class Day01
    {
        public static void Main()
        {
            string[] input = System.IO.File.ReadAllLines("./input.txt");
            List<int> elves = new List<int>();

            int sum = 0;
            int pos = 0;

            foreach (string line in input)
            {
                if (line.Length == 0)
                {
                    elves.Add(sum);
                    pos++;
                    sum = 0;
                    continue;
                }

                sum += Int32.Parse(line);
            }

            elves.Sort();
            elves.Reverse();
            Console.WriteLine("Part 1: " + elves[0]);
            Console.WriteLine("Part 2: " + elves.Take(3).Sum());
        }
    }
}
