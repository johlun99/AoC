using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{

    class Day02 : ASolution
    {
        readonly List<int> code = new List<int>();

        public Day02() : base(02, 2019, "")
        {
            string[] lines = Input.Split(',');

            foreach (var line in lines)
            {
                code.Add(Convert.ToInt32(line));
            }
        }

        protected override string SolvePartOne()
        {
            List<int> copy = new List<int>(code);
            
            copy[1] = 12;
            copy[2] = 2;
            return Convert.ToString(OpCode.RunOpCode(copy));
        }

        protected override string SolvePartTwo()
        {
            for (int noun = 0; noun < 99; noun++)
            {
                for (int verb = 0; verb < 99; verb++)
                {
                    List<int> copy = new List<int>(code);
                    copy[1] = noun;
                    copy[2] = verb;

                    int output = OpCode.RunOpCode(copy);

                    if (output == 19690720)
                        return Convert.ToString(100 * noun + verb);
                }
            }

            return "Fail";
        }
    }
}
