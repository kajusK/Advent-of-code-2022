using System;
using System.Linq;

namespace Day04
{
    class Program
    {
        public static bool IsContained(int[] data)
        {
            return (data[0] <= data[2] && data[1] >= data[3])
                || (data[2] <= data[0] && data[3] >= data[1]);
        }

        public static bool IsOverlaped(int[] data)
        {
            return (data[0] <= data[2] && data[1] >= data[2])
                || (data[0] <= data[3] && data[1] >= data[3])
                || (data[2] <= data[0] && data[3] >= data[0])
                || (data[2] <= data[1] && data[3] >= data[1]);
        }

        public static void Main()
        {
            var input = System.IO.File.ReadAllLines("./input.txt").Select(
                    line => line.Split(',', '-').Select(Int32.Parse).ToArray()
                );

            var part1 = input.Where(Program.IsContained).Count();
            var part2 = input.Where(Program.IsOverlaped).Count();

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 1: {part2}");
        }
    }
}
