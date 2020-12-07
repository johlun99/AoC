using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{

    class Day07 : ASolution
    {
        public class BagRule
        {
            public string Color;
            public Dictionary<string, int> Contents = new Dictionary<string, int>();
        }

        private Dictionary<string, Dictionary<string, int>> bags = new Dictionary<string, Dictionary<string, int>>();
        private Stopwatch watch = new System.Diagnostics.Stopwatch();
        
        public Day07() : base(07, 2020, "")
        {
            watch.Start();
            string[] rules = Input.SplitByNewline();
            
            foreach (string rule in rules)
            {
                var matches = Regex.Match(rule, @"(\w+ \w+) bags contain (.+)");
                var color = matches.Groups[1].Value;
                Dictionary<string, int> contents = new Dictionary<string, int>();

                foreach (var content in matches.Groups[2].Value.Split(","))
                {
                    if (content.StartsWith("no")) 
                        continue;

                    var contentMatch = Regex.Match(content, @"(\d+) (\w+ \w+)");
                    contents.Add(contentMatch.Groups[2].Value, int.Parse(contentMatch.Groups[1].Value));
                }
                
                bags.Add(color, contents);
            }
        }

        protected override string SolvePartOne()
        {
            return Convert.ToString( bags.Count(d => canHoldGold(d.Key)));
        }

        protected override string SolvePartTwo()
        {
            string answer =  Convert.ToString(GoldCanHold("shiny gold", 0));
            
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            return answer;
        }

        public bool canHoldGold(string bagColor)
        {
            if (bags[bagColor].Any(d => d.Key == "shiny gold")) 
                return true;
            
            if (bags[bagColor].Any(d => d.Key.StartsWith("no"))) 
                return false;

            foreach (var bagRuleContent in bags[bagColor])
            {
                if (canHoldGold(bagRuleContent.Key))
                    return true;
            }

            return false;
        }

        private int GoldCanHold(string color, int counter)
        {
            if (bags[color].Any(d => d.Key.StartsWith("no"))) 
                return counter;
            
            foreach (var content in bags[color])
            {
                counter += content.Value;
                for (int i = 0; i < content.Value; i++)
                    counter += GoldCanHold(content.Key, 0);
            }

            return counter;
        }
    }
}
