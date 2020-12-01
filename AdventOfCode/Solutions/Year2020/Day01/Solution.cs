using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day01 : ASolution
    {
        private readonly List<int> input = new List<int>();
        public Day01() : base(01, 2020, "")
        {
            string[] data = Input.SplitByNewline();

            foreach (var row in data)
                input.Add(Convert.ToInt32(row));
        }

        protected override string SolvePartOne()
        {
            List<int> copy = new List<int>(input);

            for (int i = 0; i < copy.Count; i++)
            {
                for (int j = i; j < copy.Count; j++)
                {
                    if (copy[i] + copy[j] == 2020)
                    {
                        return Convert.ToString(copy[i] * copy[j]);
                    }
                }
            }
            
            return null;
        }

        protected override string SolvePartTwo()
        {
            List<int> copy = new List<int>(input);

            for (int i = 0; i < copy.Count; i++)
            {
                for (int j = i; j < copy.Count; j++)
                {
                    for (int x = j; x < copy.Count; x++)
                    {
                        if (copy[i] + copy[j] + copy[x] == 2020)
                        {
                            return Convert.ToString(copy[i] * copy[j] * copy[x]);
                        }
                    }
                }
            }
            
            return null;
        }
    }
}
