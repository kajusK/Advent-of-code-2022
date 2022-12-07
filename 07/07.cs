using System;
using System.Linq;
using System.Collections.Generic;

namespace Day07
{
    public class Dir
    {
        public string Name;
        public int Size = 0;
        private List<Dir> Subdirs = new List<Dir> ();
        private Dir Parent;

        public Dir(string name, Dir parent)
        {
            this.Name = name;
            this.Parent = parent;
        }

        public Dir Chdir(string name)
        {
            if (name == "..")
            {
                return this.Parent;
            }

            if (name == "/")
            {
                if (this.Name == name) { return this; }
                return this.Parent.Chdir(name);
            }

            if (!this.Subdirs.Any(x => x.Name == name))
            {
                this.Subdirs.Add(new Dir(name, this));
            }
            return this.Subdirs.Where(x => x.Name == name).First();
        }

        public void AddFiles(IEnumerable<string> ls)
        {
            this.Size = ls.Select(x => x.Split(' ')[0]).Where(
                x => x.Length != 0 && x != "dir").Select(Int32.Parse).Sum();
        }

        public int GenerateDirSizes()
        {
            this.Size += this.Subdirs.Select(x => x.GenerateDirSizes()).Sum();
            return this.Size;
        }
    }

    class Program
    {
        public static void Main()
        {
            var input = System.IO.File.ReadAllText("./input.txt").Trim().Split("$ ").Select(
                    x => x.Split('\n')
                );

            var current = new Dir("/", null);
            var dirs = new HashSet<Dir>();

            foreach (var data in input)
            {
                var cmd = data[0].Split(' ');
                dirs.Add(current);

                if (cmd[0] == "ls")
                {
                    current.AddFiles(data.Skip(1));
                }
                else if (cmd[0] == "cd")
                {
                    current = current.Chdir(cmd[1]);
                }
            }

            current = current.Chdir("/");
            current.GenerateDirSizes();

            int needed = 30000000 - (70000000 - current.Size);
            var sizes = dirs.ToList().Select(x => x.Size).Where(x => x > needed).ToList();
            sizes.Sort();

            int part1 = dirs.ToList().Where(x => x.Size <= 100000).Select(x => x.Size).Sum();
            var part2 = sizes.First();

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }
    }
}
