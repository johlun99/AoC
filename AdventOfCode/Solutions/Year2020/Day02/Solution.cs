using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day02 : ASolution
    {
        private readonly List<string> passwordInput;
        public Day02() : base(02, 2020, "")
        {
            passwordInput = new List<string>(Input.SplitByNewline());
        }

        protected override string SolvePartOne()
        {
            List<string> copy = new List<string>(passwordInput);
            
            int validPasswords = 0;

            foreach (string row in copy)
            {
                int letterCount = 0;
                
                string numsPart = row.Split(' ')[0];
                char letter = row.Split(' ')[1][0];
                string password = row.Split(' ')[2];
                
                int min = Convert.ToInt32(numsPart.Split('-')[0]);
                int high = Convert.ToInt32(numsPart.Split('-')[1]);

                foreach (char c in password)
                {
                    letterCount = c == letter ? letterCount + 1 : letterCount;

                    if (letterCount > high)
                        break;
                }

                if (letterCount <= high && letterCount >= min)
                    validPasswords++;
            }
            
            return Convert.ToString(validPasswords);
        }

        protected override string SolvePartTwo()
        {
            List<string> copy = new List<string>(passwordInput);
            
            int validPasswords = 0;

            foreach (string row in copy)
            {
                bool valid = false;
                
                string numsPart = row.Split(' ')[0];
                char letter = row.Split(' ')[1][0];
                string password = row.Split(' ')[2];
                
                int min = Convert.ToInt32(numsPart.Split('-')[0]) - 1;
                int high = Convert.ToInt32(numsPart.Split('-')[1]) - 1;

                if (password.Length > high && password.Length > min)
                {
                    valid = password[min] == letter || password[high] == letter;

                    if (password[min] == letter && password[high] == letter)
                        valid = false;
                }

                else if (password.Length > min)
                    valid = password[min] == letter;
                
                if (valid)
                    validPasswords++;
            }
            
            return Convert.ToString(validPasswords);
        }
    }
}
