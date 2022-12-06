using System;
using System.Linq;
using System.Collections.Generic;

namespace Day06
{
    class Program
    {
        public static int FindMarker(string data, int length)
        {
            for (int i = 0; i < (data.Length - length); i++)
            {
                if (data.Substring(i, length).Distinct().Count() == length)
                {
                    return i + length;
                }
            }
            return -1;
        }

        public static void Main()
        {
            var input = System.IO.File.ReadAllText("./input.txt");

            Console.WriteLine($"Part 1: {FindMarker(input, 4)}");
            Console.WriteLine($"Part 2: {FindMarker(input, 14)}");
        }
    }
}
