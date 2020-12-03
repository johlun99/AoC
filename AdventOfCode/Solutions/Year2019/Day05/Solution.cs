using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{

    class Day05 : ASolution
    {
        private readonly List<int> code = new List<int>();

        public Day05() : base(05, 2019, "")
        {
            string[] input = Input.Split(',');

            foreach (string row in input)
                code.Add(Convert.ToInt32(row));
        }

        protected override string SolvePartOne()
        {
            List<int> copy = new List<int>(code);

            return Convert.ToString(OpCode.RunOpCode(copy));
        }

        protected override string SolvePartTwo()
        {
            return null;
        }
    }
}
