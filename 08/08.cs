using System;
using System.Linq;
using System.Collections.Generic;

namespace Day08
{
    class Program
    {
        public static int GetScore(IEnumerable<int> trees, int height)
        {
            var score = 0;
            foreach (var tree in trees)
            {
                score++;
                if (tree >= height) { break; }
            }
            return score;
        }

        public static void Main()
        {
            var input = System.IO.File.ReadAllLines("./input.txt").Select(
                    x => x.Select(y => Int32.Parse(y.ToString())).ToList()
                ).ToList();

            var scores = new List<int>();
            var visible = new List<bool>();

            for (var y = 0; y < input.Count(); y++)
            {
                for (var x = 0; x < input[y].Count(); x++)
                {
                    var tree = input[y][x];

                    var dirs = new List<IEnumerable<int>> {
                            input[y].Take(x).Reverse(),
                            input[y].Skip(x+1),
                            input.Select(t => t[x]).Take(y).Reverse(),
                            input.Select(t => t[x]).Skip(y+1),
                        };

                    visible.Add(dirs.Select(
                            dir => dir.All(height => height < tree)
                        ).Contains(true));

                    scores.Add(dirs.Select(
                            dir => Program.GetScore(dir, tree)
                        ).Aggregate((acc, scr) => acc*scr));
                }
            }

            Console.WriteLine($"Part 1: {visible.Count(x => x)}");
            Console.WriteLine($"Part 2: {scores.Max()}");
        }
    }
}
