using System;
using System.Linq;
using System.Collections.Generic;

namespace Day12
{
    class Program
    {
        public static List<(int x, int y)> FindLetter(char letter, List<List<char>> map)
        {
            var res = new List<(int x, int y)> ();

            for (int y = 0; y < map.Count(); y++)
            {
                for (int x = 0; x < map[0].Count(); x++)
                {
                    if (letter == map[y][x])
                    {
                        res.Add((x, y));
                    }
                }
            }
            return res;
        }

        public static int FindShortest((int x, int y) start, (int x, int y) end, List<List<char>> map)
        {
            var dirs = new List<(int x, int y)> {(0, 1), (0, -1), (-1, 0), (1, 0) };
            var visited = map.Select(line => line.Select(x => Int32.MaxValue).ToList()).ToList();
            var to_visit = new List<(int x, int y)> { start };
            visited[start.y][start.x] = 0;

            while (to_visit.Count() != 0)
            {
                var pos = to_visit.First();
                to_visit = to_visit.Skip(1).ToList();

                var cur_val = visited[pos.y][pos.x];
                var cur_letter = map[pos.y][pos.x];

                foreach (var dir in dirs)
                {
                    var next_pos = (x: pos.x + dir.x, y: pos.y + dir.y);
                    if (next_pos.x < 0
                        || next_pos.x >= visited[0].Count()
                        || next_pos.y < 0
                        || next_pos.y >= visited.Count())
                    {
                        continue;
                    }

                    var next_val = visited[next_pos.y][next_pos.x];
                    var next_letter = map[next_pos.y][next_pos.x];

                    if (next_val < cur_val + 1) { continue; }
                    if ((int)next_letter > (int)cur_letter + 1) { continue; }
                    if (to_visit.Contains(next_pos)) { continue; }

                    visited[next_pos.y][next_pos.x] = cur_val + 1;
                    to_visit.Add(next_pos);
                }
            }

            return visited[end.y][end.x];
        }

        public static void Main()
        {
            var map = System.IO.File.ReadAllText("./input.txt").Trim().Split("\n").Select(
                    line => line.ToList()
                ).ToList();

            var start = Program.FindLetter('S', map).First();
            var end = Program.FindLetter('E', map).First();
            map[start.y][start.x] = 'a';
            map[end.y][end.x] = 'z';

            var part1 = Program.FindShortest(start, end, map);
            var part2 = Program.FindLetter('a', map).Select(
                    pos => Program.FindShortest(pos, end, map)
                ).Min();

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }
    }
}
