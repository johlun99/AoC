using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{

    class Day04 : ASolution
    {
        readonly int FirstValue;
        readonly int SecondValue;

        public Day04() : base(04, 2019, "")
        {
            string[] values = Input.Split('-');

            FirstValue = Convert.ToInt32(values[0]);
            SecondValue = Convert.ToInt32(values[1]);
        }

        protected override string SolvePartOne()
        {
            return Convert.ToString(GetNumberOfPossiblePassword(FirstValue, SecondValue));
        }

        protected override string SolvePartTwo()
        {
            return Convert.ToString(GetNumberOfPossiblePassword(FirstValue, SecondValue, true));
        }

        private int GetNumberOfPossiblePassword(int low, int high, bool doubleNumConstraint = false)
        {
            int counter = 0;
            
            for (int i = low; i <= high; i++)
            {
                bool possible = false;
                int[] password = i.ToString().Select(o => Convert.ToInt32(o)).ToArray();

                if (password.Length != 6)
                    break;
                
                for (int j = 1; j < password.Length; j++)
                {
                    if (password[j] < password[j - 1])
                    {
                        possible = false;
                        break;
                    }

                    if (doubleNumConstraint && !ContainsOnlyDoubleAtSomePoint(password))
                    {
                        possible = false;
                        break;
                    }
                    
                    if (password[j] == password[j - 1])
                        possible = true;
                }

                if (possible)
                    counter++;
            }

            return counter;
        }

        private bool ContainsOnlyDoubleAtSomePoint(int[] password)
        {
            int length = password.Length;
            bool passes = false;

            if (length > 3)
            {
                for (int i = 2; i < length - 1; i++)
                {
                    if (password[i] == password[i - 1] &&
                        password[i] != password[i - 2] &&
                        password[i] != password[i + 1])
                    {
                        passes = true;
                        break;
                    }
                }
            }

            if (!passes && password[0] == password[1] && password[0] != password[2])
                passes = true;
            
            else if (!passes && password[length - 1] == password[length - 2] &&
                     password[length - 1] != password[length - 3])
                passes = true;

            return passes;
        }
    }
}
