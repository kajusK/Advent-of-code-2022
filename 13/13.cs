using System;
using System.Linq;
using System.Collections.Generic;

namespace Day13
{
    class Data
    {
        public List<Data> data = new List<Data> ();
        public int final = 0;
        public bool is_final = false;
        public string origin = "";

        public Data(int data)
        {
            this.final = data;
            this.is_final = true;
        }

        public Data(string data)
        {
            this.origin = data;

            int pos = 1;
            while (pos < data.Length)
            {
                if (data[pos] == '[')
                {
                    int depth = 1;
                    int end = pos + 1;
                    while (depth != 0) {
                        if (data[end] == ']') { depth--; }
                        if (data[end] == '[') { depth++; }
                        end++;
                    }
                    this.data.Add(new Data(data.Substring(pos, end - pos)));
                    pos = end;
                }
                else if (Char.IsDigit(data[pos]))
                {
                    int end = pos;
                    while (Char.IsDigit(data[++end])) { ; }
                    this.data.Add(new Data(Int32.Parse(data.Substring(pos, end - pos))));
                    pos = end;
                }
                pos++;
            }
        }

        public int CompareTo(Data right)
        {
            var left = this;

            if (left.is_final && right.is_final)
            {
                if (left.final > right.final) { return 1; }
                if (left.final < right.final) { return -1; }
                return 0;
            }

            if (left.is_final) { left = new Data($"[{left.final}]"); }
            if (right.is_final) { right = new Data($"[{right.final}]"); }

            var cmp = left.data.Zip(right.data, (a, b) => a.CompareTo(b)).Where(x => x != 0);
            if (cmp.Count() != 0) { return cmp.First(); }

            if (left.data.Count() < right.data.Count()) { return -1; }
            if (left.data.Count() > right.data.Count()) { return 1; }
            return 0;
        }
    }

    class Program
    {
        public static void Main()
        {
            var pairs = System.IO.File.ReadAllText("./input.txt").Trim().Split("\n\n").Select(
                    block => block.Split('\n')
                ).Select(
                    pair => pair.Select(line => new Data(line)).ToList()
                );

            var part1 = pairs.Select(
                    pair => pair[0].CompareTo(pair[1])
                ).Select((x, i) => x < 0 ? i + 1 : 0).Sum();

            var data = pairs.SelectMany(x => x).ToList();
            data.Add(new Data("[[2]]"));
            data.Add(new Data("[[6]]"));
            data.Sort((a, b) => a.CompareTo(b));

            var pos_a = data.FindIndex(x => x.origin == "[[2]]") + 1;
            var pos_b = data.FindIndex(x => x.origin == "[[6]]") + 1;

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {pos_a*pos_b}");
        }
    }
}
