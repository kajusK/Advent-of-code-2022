using System;
using System.Linq;
using System.Collections.Generic;

namespace Day02
{
    class Game
    {
        // r->p, p->R, s->p
        public static List<char> defeats = new List<char> {'C', 'A', 'B'};

        public static int GetResult(char oponent, char me)
        {
            int score = me - 'A' + 1;

            if (oponent == me)
            {
                return 3 + score;
            }

            if (Game.defeats[me - 'A'] == oponent)
            {
                return 6 + score;
            }

            return score;
        }

        public static char GetMove(char oponent, char result)
        {
            char[] moves = {
                Game.defeats[oponent - 'A'], // loose
                oponent, // draw
                (char) (Game.defeats.IndexOf(oponent) + 'A') // win
            };

            return moves[result - 'X'];
        }
    }

    class Program
    {
        public static void Main()
        {
            var input = System.IO.File.ReadAllLines("./input.txt").Select(
                    line => line.Split(' ')
                                  .Select(x => x[0])
                                  .ToList()
                ).ToList();

            int part1 = input.Select(
                    x => Game.GetResult(x[0], (char)(x[1] - 'X' + 'A'))
                ).Sum();

            int part2 = input.Select(
                    x => Game.GetResult(x[0], Game.GetMove(x[0], x[1]))
                ).Sum();

            Console.WriteLine("Part 1: " + part1);
            Console.WriteLine("Part 1: " + part2);
        }
    }
}
