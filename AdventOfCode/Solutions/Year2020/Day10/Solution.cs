using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day10 : ASolution
    {
        private static List<int> input = new List<int>();

        public Day10() : base(10, 2020, "")
        {
            string[] stringInput = Input.SplitByNewline();

            foreach (string row in stringInput)
            {
                input.Add(int.Parse(row));
            }
            
            input.Sort();
        }

        protected override string SolvePartOne()
        {
            (int singles, int tripples) values = GetDifference();
            return Convert.ToString(values.singles * values.tripples);
        }

        protected override string SolvePartTwo()
        {
            return Convert.ToString(FindArrangements(0));
        }

        private static (int singles, int tripples) GetDifference()
        {
            (int singles, int tripples) values = (1, 1);

            for (int i = 0; i < input.Count - 1; i++)
            {
                if (input[i] + 1 == input[i + 1])
                    values.singles++;
                
                else if (input[i] + 3 == input[i + 1])
                    values.tripples++;
            }
            
            return values;
        }

        private static List<int> TryToRemoveIndex(List<int> list)
        {
            for (int i = list.Count - 2; i > 0; i--)
            {
                if (i < list.Count - 2 && list[i + 2] - list[i] <= 2)
                {
                    list.RemoveAt(i + 1);
                    return list;
                }
            }
            
            for (int i = list.Count - 2; i > 0; i--)
            {
                if (i < list.Count - 2 && list[i + 2] - list[i] <= 3)
                {
                    list.RemoveAt(i + 1);
                    return list;
                }
            }

            return new List<int>();
        }

        private long FindArrangements(int index)
        {
            Console.WriteLine(index);
            
            if (index == input.Count - 1)
                return 1;

            long total = 0;
            int nextIndex = index + 1;

            while (nextIndex < input.Count && input[nextIndex] - input[index] <= 3)
            {
                total += FindArrangements(nextIndex);
                nextIndex += 1;
            }

            return total;
        }
    }
}
