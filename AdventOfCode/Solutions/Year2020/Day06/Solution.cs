using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day06 : ASolution
    {
        private List<string> groupsAnswers;

        public Day06() : base(06, 2020, "")
        {
            groupsAnswers = new List<string>(Input.Split("\n\n"));
        }

        protected override string SolvePartOne()
        {
            List<string> individualAnswers;
            List<char> groupOptions;

            int answerCounter = 0;
            
            foreach (string groupsAnswer in groupsAnswers)
            {
                individualAnswers = new List<string>(groupsAnswer.SplitByNewline());
                groupOptions = new List<char>();

                foreach (string individualAnswer in individualAnswers)
                {
                    foreach (char answer in individualAnswer)
                    {
                        if (!groupOptions.Contains(answer))
                            groupOptions.Add(answer);
                    }
                }

                answerCounter += groupOptions.Count;
            }
            
            return Convert.ToString(answerCounter);
        }

        protected override string SolvePartTwo()
        {
            List<string> individualAnswers;
            List<char> alreadyUsedAnswers;
            
            int answerCounter = 0;

            foreach (string groupAnswers in groupsAnswers)
            {
                individualAnswers = new List<string>(groupAnswers.SplitByNewline());
                alreadyUsedAnswers = new List<char>();

                int groupCount = individualAnswers.Count;
                    
                for (int i = 0; i < groupCount; i++)
                {
                    foreach (char answer in individualAnswers[i])
                    {
                        if (alreadyUsedAnswers.Contains(answer))
                            continue;
                        
                        alreadyUsedAnswers.Add(answer);
                        
                        int uniqueAnswer = 1;
                        
                        for (int j = 0; j < groupCount; j++)
                        {
                            if (i == j)
                                continue;

                            if (individualAnswers[j].Contains(answer))
                                uniqueAnswer++;
                        }
                        
                        if (uniqueAnswer == groupCount)
                            answerCounter++;
                    }
                }
            }
            
            return Convert.ToString(answerCounter);
        }
    }
}
