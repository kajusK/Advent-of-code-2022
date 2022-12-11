using System;
using System.Linq;
using System.Collections.Generic;

namespace Day11
{
    class Monkey
    {
        public List<long> items;
        public List<string> operation;
        public int test;
        public int true_target;
        public int false_target;

        public Monkey(List<string> input)
        {
            this.items = input[1].Split(": ").Last().Split(", ").Select(Int64.Parse).ToList();
            this.operation = input[2].Split("new = ").Last().Split(' ').ToList();
            this.test = Int32.Parse(input[3].Split(' ').Last());
            this.true_target = Int32.Parse(input[4].Split(' ').Last());
            this.false_target = Int32.Parse(input[5].Split(' ').Last());
        }

        public long GetWorryLevel(long old)
        {
            long left = this.operation[0] == "old" ? old : Int64.Parse(this.operation[0]);
            long right = this.operation[2] == "old" ? old : Int64.Parse(this.operation[2]);
            long level = -1;

            if (this.operation[1] == "+") { level = left + right; }
            else if (this.operation[1] == "*") { level = left * right; }
            return level;
        }

        public int GetThrowTarget(long level)
        {
            return level % this.test == 0 ? this.true_target : this.false_target;
        }
    }

    class Program
    {
        public static long Run(List<Monkey> monkeys, int rounds, Func<long, long> modify_level)
        {
            var inspects = monkeys.Select(x => 0L).ToList();

            for (int round = 0; round < rounds; round++)
            {
                foreach (var monkey in monkeys)
                {
                    inspects[monkeys.IndexOf(monkey)] += monkey.items.Count();
                    foreach (var item in monkey.items)
                    {
                        var level = modify_level(monkey.GetWorryLevel(item));
                        monkeys[monkey.GetThrowTarget(level)].items.Add(level);
                    }
                    monkey.items = new List<long> ();
                }
            }

            inspects.Sort();
            inspects.Reverse();
            return inspects[0]*inspects[1];
        }

        public static void Main()
        {
            var data = System.IO.File.ReadAllText("./input.txt").Split("\n\n").Select(
                    input => input.Split('\n').ToList()
                );

            /* divisors are primes, multiply all divisors and you've got the highest needed number */
            long supernumber = data.Select(x => new Monkey(x)).Select(
                    x => x.test
                ).Aggregate((a, x) => a*x);

            var part1 = Program.Run(data.Select(x => new Monkey(x)).ToList(), 20, x => (long)(x / 3));
            var part2 = Program.Run(data.Select(x => new Monkey(x)).ToList(), 10000, x => x % supernumber);

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }
    }
}
