using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day08 : ASolution
    {
        private readonly Dictionary<int, (string cmd, string op)> input = new Dictionary<int, (string cmd, string op)>();

        public Day08() : base(08, 2020, "")
        {
            string[] lines = Input.SplitByNewline();

            for (int i = 0; i < lines.Length; i++)
            {
                string cmd = lines[i].Substring(0, 3);
                string op = lines[i].Substring(4);
                
                input.Add(i, (cmd, op));
            }
        }

        protected override string SolvePartOne()
        {
            int accumulator = AccCalculator.RunInput(input).value;
            
            return Convert.ToString(accumulator);
        }

        protected override string SolvePartTwo()
        {
            Dictionary<int, (string cmd, string op)> operations = new Dictionary<int, (string cmd, string op)>(input);
            List<int> keysVisited = new List<int>();

            int accumulator = 0;

            Dictionary<string, List<string>> changeables = new Dictionary<string, List<string>>();
            
            changeables.Add("nop", new List<string>{"jmp"});
            changeables.Add("jmp", new List<string>{"nop"});

            operations = AccCalculator.ModOperations(operations, changeables);

            accumulator = AccCalculator.RunInput(operations).value;

            return Convert.ToString(accumulator);
        }
    }
}
