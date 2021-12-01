using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2021
{

    class Day01 : ASolution
    {
        List<int> input = new List<int>();
        public Day01() : base(01, 2021, "")
        {
            string[] data = Input.SplitByNewline();

            foreach (var row in data)
                input.Add(Convert.ToInt32(row));
        }

        protected override string SolvePartOne()
        {
            int countIncreases = 0;

            for (int i = 1; i < input.Count; i++)
                if (input[i] > input[i - 1])
                    countIncreases++;

            return countIncreases.ToString();
        }

        protected override string SolvePartTwo()
        {
            int countIncreases = 0;

            for (int i = 0; i < input.Count - 3; i++) {
                int a = input[i] + input[i + 1] + input[i + 2];
                int b = input[i + 1] + input[i + 2] + input[i + 3];

                if (a < b) 
                    countIncreases++;
            }

            return countIncreases.ToString();
        }
    }
}
