using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace AdventOfCode.Solutions.Year2020
{
    public class AccCalculator
    {
        public static (int value, bool finished) RunInput(Dictionary<int, (string cmd, string op)> operations)
        {
            List<int> keysVisited = new List<int>();
            int accumulator = 0;
            
            for (int i = 0; i < operations.Count; i++)
            {
                if (keysVisited.Contains(i))
                    return (accumulator, false);

                keysVisited.Add(i);

                string cmd = operations[i].cmd;
                string op = operations[i].op;

                switch (cmd)
                {
                    case "nop":
                        break;
                    case "acc":
                        if (op[0] == '+')
                            accumulator += int.Parse(op.Substring(1));

                        else if (op[0] == '-')
                            accumulator -= int.Parse(op.Substring(1));

                        else
                            throw new Exception("Invalid Accumulator exception");
                        break;
                    case "jmp":
                        if (op[0] == '+')
                            i += int.Parse(op.Substring(1)) - 1;

                        else if (op[0] == '-')
                            i -= int.Parse(op.Substring(1)) + 1;

                        else
                            throw new Exception("Invalid Jmp operation");
                        break;
                }
            }

            return (accumulator, true);   
        }

        public static Dictionary<int, (string cmd, string op)> ModOperations(Dictionary<int, (string cmd, string op)> input, Dictionary<string, List<string>> changeables)
        {
            Dictionary<int, (string cmd, string op)> operations = new Dictionary<int, (string cmd, string op)>();
            int accumulator = 0;

            for (int i = 0; i < input.Count; i++)
            {
                operations = new Dictionary<int, (string, string)>(input);
                
                string cmd = input[i].cmd;
                string op = input[i].op;

                bool foundPath = false;

                if (changeables.ContainsKey(cmd))
                {
                    foreach (string changeable in changeables[cmd])
                    {
                        operations[i] = (changeable, op);

                        if (RunInput(operations).finished)
                        {
                            foundPath = true;
                            break;
                        }
                    }
                }

                if (foundPath)
                    break;
            }

            return operations;
        }
    }
}