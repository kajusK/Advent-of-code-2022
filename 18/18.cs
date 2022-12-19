using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Day18
{
    class Program
    {
        public static void Main()
        {
            var cubes = System.IO.File.ReadAllText("./input.txt").Trim().Split('\n').Select(
                    line => line.Split(',').Select(Int32.Parse).ToList()
                ).Select(
                    cube => (x: cube[0], y: cube[1], z: cube[2])
                ).ToHashSet();

            var dirs = new List<(int x, int y, int z)> {
                (x: 1, y: 0, z: 0),
                (x: -1, y: 0, z: 0),
                (x: 0, y: 1, z: 0),
                (x: 0, y: -1, z: 0),
                (x: 0, y: 0, z: 1),
                (x: 0, y: 0, z: -1),
            };

            int part1 = 0;
            foreach (var cube in cubes)
            {
                foreach (var dir in dirs)
                {
                    var next = (x: cube.x + dir.x, cube.y + dir.y, cube.z + dir.z);

                    if (!cubes.Contains(next)) {
                        part1++;
                    }
                }
            }

            var max = (
                x: cubes.Select(item => item.x).Max() + 1,
                y: cubes.Select(item => item.y).Max() + 1,
                z: cubes.Select(item => item.z).Max() + 1
                );

            var min = (
                x: cubes.Select(item => item.x).Min() - 1,
                y: cubes.Select(item => item.y).Min() - 1,
                z: cubes.Select(item => item.z).Min() - 1
                );

            var to_check = new HashSet<(int x, int y, int z)> { min };
            var visited = new HashSet<(int x, int y, int z)> {};
            var part2 = 0;

            while (to_check.Count() != 0)
            {
                var pos = to_check.First();
                to_check = to_check.Skip(1).ToHashSet();
                visited.Add(pos);

                foreach (var dir in dirs)
                {
                    var next = (x: pos.x + dir.x, y: pos.y + dir.y, z: pos.z + dir.z);

                    if (next.x > max.x || next.y > max.y || next.z > max.z ||
                        next.x < min.x || next.y < min.y || next.z < min.z) { continue; }
                    if (visited.Contains(next)) { continue; }
                    if (cubes.Contains(next))
                    {
                        part2++;
                        continue;
                    }
                    to_check.Add(next);
                }
            }

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }
    }
}
