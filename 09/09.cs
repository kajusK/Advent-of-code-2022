using System;
using System.Linq;
using System.Collections.Generic;

namespace Day09
{
    class Program
    {
        public static (int x, int y) MoveHead((int x, int y) head, char dir)
        {
            switch (dir)
            {
                case 'U': head.y--; break;
                case 'D': head.y++; break;
                case 'L': head.x--; break;
                default: head.x++; break;
            }

            return head;
        }

        public static (int x, int y) MoveTail((int x, int y) tail, (int x, int y) head)
        {
            var tail_x_dist = head.x - tail.x;
            var tail_y_dist = head.y - tail.y;
            if (Math.Abs(tail_x_dist) <= 1 && Math.Abs(tail_y_dist) <= 1)
            {
                return tail;
            }

            if (Math.Abs(tail_x_dist) == 2) { tail_x_dist /= 2; }
            if (Math.Abs(tail_y_dist) == 2) { tail_y_dist /= 2; }

            return (x: tail.x + tail_x_dist, y: tail.y + tail_y_dist);
        }

        public static int Run(IEnumerable<List<string>> input, List<(int x, int y)> tails)
        {
            var head = (x: 0, y: 0);
            var visited = new List<(int, int)> { head };

            foreach (var step in input)
            {
                var dir = step[0][0];
                var dist = Int32.Parse(step[1]);

                for (int i = 0; i < dist; i++)
                {
                    head = Program.MoveHead(head, dir);
                    tails = tails.Select(
                            (tail, pos) => Program.MoveTail(tail, pos == 0 ? head : tails[pos - 1])
                        ).ToList();
                    visited.Add(tails.Last());
                }
            }

            return visited.Distinct().Count();
        }

        public static void Main()
        {
            var input = System.IO.File.ReadAllLines("./input.txt").Select(
                    line => line.Split(' ').ToList()
                );
            var tails = Enumerable.Range(0, 9).Select(x => (x: 0, y: 0)).ToList();

            Console.WriteLine($"Part 1: {Program.Run(input, new List<(int, int)> { tails.First() })}");
            Console.WriteLine($"Part 2: {Program.Run(input, tails)}");
        }
    }
}
