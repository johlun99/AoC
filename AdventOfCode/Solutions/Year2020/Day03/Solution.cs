using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day03 : ASolution
    {
        private readonly List<string> map;
        
        public Day03() : base(03, 2020, "")
        {
            map = new List<string>(Input.SplitByNewline());
        }

        protected override string SolvePartOne()
        {
            return Convert.ToString(countSlope(3, 1));
        }

        protected override string SolvePartTwo()
        {
            int val = countSlope(1, 1);
            int val1 = countSlope(3, 1);
            int val2 = countSlope(5, 1);
            int val3 = countSlope(7, 1); 
            int val4 = countSlope(1, 2);

            long result = val;

            result *= val1;
            result *= val2;
            result *= val3;
            result *= val4;

            return Convert.ToString(result);
        }

        private int countSlope(int rightStep, int downStep)
        {
            List<string> copy = new List<string>(map);
            
            int x = rightStep;
            int y = downStep;
            int treeCounter = 0;

            while (y < copy.Count)
            {
                if (x >= copy[y].Length)
                    x -= copy[y].Length;

                if (copy[y][x] == '#')
                    treeCounter++;
                
                x += rightStep;
                y += downStep;
            }

            return treeCounter;
        }
    }
}
