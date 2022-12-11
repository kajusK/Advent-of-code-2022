using System;
using System.Linq;
using System.Collections.Generic;

namespace Day10
{
    class Program
    {
        public static bool Visible(int x, int cycle)
        {
            int pos = (cycle - 1) % 40;
            return Math.Abs(x - pos) <= 1;
        }

        public static void Main()
        {
            var input = System.IO.File.ReadAllLines("./input.txt").Select(
                    line => line.Split(' ').ToList()
                ).ToList();

            int x = 1;
            var req_cycles = new List<int> {20, 60, 100, 140, 180, 220};

            int cycles = 0;
            var screen = "";
            int strength = 0;
            int pos = 0;
            bool addx = false;

            while (pos < input.Count())
            {
                cycles++;

                if (req_cycles.Contains(cycles))
                {
                    strength += x*cycles;
                }
                screen += Program.Visible(x, cycles) ? "#" : ".";
                if (cycles % 40 == 0)
                {
                    screen += '\n';
                }

                /* Process instructions */
                if (addx)
                {
                    addx = false;
                    x += Int32.Parse(input[pos][1]);
                    pos++;
                }
                else if (input[pos][0] == "noop")
                {
                    pos++;
                }
                else if (input[pos][0] == "addx")
                {
                    addx = true;
                }
            }


            Console.WriteLine($"Part 1: {strength}");
            Console.WriteLine($"Part 2:\n{screen}");
        }
    }
}
