using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{

    class Day01 : ASolution
    {
        private List<long> Lines = new List<long>();

        public Day01() : base(01, 2019, "")
        {
            string[] input = Input.SplitByNewline();

            foreach (var row in input)
                Lines.Add(Convert.ToInt64(row));
        }

        protected override string SolvePartOne()
        {
            float sum = 0;

            foreach (var line in Lines)
            {
                sum += (line / 3) - 2;
            }
            
            return Convert.ToString(sum);
        }

        protected override string SolvePartTwo()
        {
            float sum = 0;

            foreach (var line in Lines)
            {
                sum += calculateFuelForFuel(line);
            }
            
            return Convert.ToString(sum);
        }

        private static long calculateFuel(long mass)
        {
            return (mass / 3) - 2;
        }

        private static long calculateFuelForFuel(long fuel)
        {
            long additionalFuel = 0;

            while (fuel > 0)
            {
                fuel = calculateFuel(fuel);
                if (fuel > 0)
                    additionalFuel += fuel;
            }

            return additionalFuel;
        }
    }
}
