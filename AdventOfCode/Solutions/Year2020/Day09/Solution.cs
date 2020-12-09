using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day09 : ASolution
    {
        private static List<long> numbers = new List<long>();

        public Day09() : base(09, 2020, "")
        {
            string[] input = Input.SplitByNewline();

            foreach (string row in input)
            {
                numbers.Add(long.Parse(row));
            }
        }

        protected override string SolvePartOne()
        {
            return Convert.ToString(FindFirstIncompatible(25));
        }

        protected override string SolvePartTwo()
        {
            long searchFor = FindFirstIncompatible(25);

            (long high, long low) nums = findRange(searchFor);
            
            return Convert.ToString(nums.high + nums.low);
        }

        private static long FindFirstIncompatible(int memoryLength)
        {
            for (int currentIndex = memoryLength; currentIndex < numbers.Count; currentIndex++)
            {
                bool foundMatch = false;
                
                for (int i = currentIndex - memoryLength; i < currentIndex; i++)
                {
                    for (int j = i + 1; j < currentIndex; j++)
                    {
                        if (numbers[i] + numbers[j] == numbers[currentIndex])
                        {
                            foundMatch = true;
                            break;
                        }
                    }

                    if (foundMatch)
                        break;
                }

                if (!foundMatch)
                    return numbers[currentIndex];
            }

            return -1;
        }

        private static (long low, long high) findRange(long searchFor)
        {
            int low = 0;
            int high = 0;
            
            for (int i = 0; i < numbers.Count; i++)
            {
                long currentSum = numbers[i];
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    currentSum += numbers[j];

                    if (currentSum == searchFor)
                    {
                        low = i;
                        high = j;
                    }
                }

                if (low > 0)
                    break;
            }

            long smallest = numbers[high];
            long biggest = numbers[low];

            if (low > 0 && high > 0)
            {
                for (int i = low; i < high; i++)
                {
                    if (smallest > numbers[i])
                        smallest = numbers[i];

                    if (biggest < numbers[i])
                        biggest = numbers[i];
                }

                return (smallest, biggest);
            }
            
            return (-1, -1);
        }
    }
}
