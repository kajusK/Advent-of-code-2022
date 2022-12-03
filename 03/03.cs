using System;
using System.Collections.Generic;
using System.Linq;

namespace Day03
{
    class Program
    {
        public static int GetValue(IEnumerable<char> data)
        {
            return data.Select(
                    x => Char.IsLower(x) ? x - 'a' + 1 : x - 'A' + 27
                ).Sum();
        }

        public static void Main()
        {
            var input = System.IO.File.ReadAllLines("./input.txt");

            var part1 = input.Select(
                    line => line.Substring(0, line.Length/2)
                                .Intersect(line.Substring(line.Length/2))
                                .First()
                );

            var groups = input.Select(
                    (s, i) => input.Skip(i*3).Take(3)
                ).Where(x => x.Any());

            var part2 = groups.Select(
                    group => group.Aggregate(
                        group.First().AsEnumerable(),
                        (a, b) => a.Intersect(b)
                    ).First()
                );

            Console.WriteLine($"Part 1: {Program.GetValue(part1)}");
            Console.WriteLine($"Part 2: {Program.GetValue(part2)}");
        }
    }
}
