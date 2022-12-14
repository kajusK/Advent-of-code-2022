using System;
using System.Linq;
using System.Collections.Generic;

namespace Day14
{
    class Program
    {
        public static int Simulate((int x, int y) init_pos, HashSet<(int x, int y)> map, int floor_level, bool hard_floor = false)
        {
            var dirs = new List<(int x, int y)> {(0, 1), (-1, 1), (1, 1)};
            var pos = init_pos;
            var count = 0;

            map = map.ToHashSet(); // quick and dirty copy

            while (pos.y < floor_level)
            {
                var prev = pos;

                foreach (var dir in dirs)
                {
                    var next = (x: pos.x + dir.x, y: pos.y + dir.y);
                    if (!map.Contains(next) && (!hard_floor || next.y < floor_level))
                    {
                        prev = pos;
                        pos = next;
                        break;
                    }
                }
                if (prev.x == pos.x && prev.y == pos.y)
                {
                    count++;
                    map.Add(pos);
                    if (pos.y == 0) {
                        break;
                    }
                    pos = init_pos;
                }
            }

            return count;
        }

        public static void Main()
        {
            var paths = System.IO.File.ReadAllText("./input.txt").Trim().Split("\n").Select(
                    line => line.Split(" -> ").Select(x => x.Split(',')
                    ).Select(x => (x: Int32.Parse(x[0]), y: Int32.Parse(x[1])))
                );

            var map = new HashSet<(int x, int y)> ();
            foreach (var path in paths)
            {
                var start = path.First();

                foreach (var end in path.Skip(1))
                {
                    var step_x = end.x.CompareTo(start.x);
                    var step_y = end.y.CompareTo(start.y);

                    map.Add(start);
                    while (start.x != end.x || start.y != end.y)
                    {
                        start.x += step_x;
                        start.y += step_y;
                        map.Add(start);
                    }
                    start = end;
                }
            }

            var floor_level = map.Select(x => x.y).Max();

            var part1 = Program.Simulate((500, 0), map, floor_level);
            var part2 = Program.Simulate((500, 0), map, floor_level + 2, true);

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }
    }
}
