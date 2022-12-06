using System;
using System.Linq;
using System.Collections.Generic;

namespace Day05
{
    class Program
    {
        public static string Run(IEnumerable<string> commands, List<List<char>> cranes, bool reverse)
        {
            /* Deep copy */
            cranes = cranes.Select(x => x.Select(y => y).ToList()).ToList();

            foreach(var cmd in commands.Select(x => x.Split(' ').ToList()))
            {

                var count = Int32.Parse(cmd[1]);
                var from = Int32.Parse(cmd[3]) - 1;
                var to = Int32.Parse(cmd[5]) - 1;
                var from_index = cranes[from].Count() - count;

                var data = cranes[from].Skip(from_index);
                if (reverse)
                {
                    data = data.Reverse();
                }
                cranes[to].AddRange(data);
                cranes[from].RemoveRange(from_index, count);
            }

            return String.Join("", cranes.Select(x => x.Last()));
        }

        public static void Main()
        {
            var input = System.IO.File.ReadAllText("./input.txt").Split("\n\n").Select(
                x => x.Split('\n').Where(row => row.Length != 0)
            ).ToList();

            var cranes = input[0].Last().Where(x => x != ' ').Select(
                    _ => new List<char>()
                ).ToList();

            foreach (var row in input[0].Take(input[0].Count() - 1))
            {
                for (int i = 1; i < row.Length; i += 4)
                {
                    if (row[i] != ' ')
                    {
                        cranes[(i-1)/4].Add(row[i]);
                    }
                }
            }
            cranes.ForEach(x => x.Reverse());

            Console.WriteLine($"Part 1: {Program.Run(input[1], cranes, true)}");
            Console.WriteLine($"Part 2: {Program.Run(input[1], cranes, false)}");
        }
    }
}
