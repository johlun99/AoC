using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace AdventOfCode.Solutions.Year2019
{
    public class OpCode
    {
        public static int RunOpCode(List<int> code)
        {
            int index = 0;
            
            while (code[index] != 99)
            {
                int opCode = code[index];
                int steps = 4;

                int value1;
                int value2;
                int targetIndex;

                switch (opCode)
                {
                    case 1:
                        // Add
                        value1 = code[index + 1];
                        value2 = code[index + 2];
                        targetIndex = code[index + 3];
                        
                        if (targetIndex < 0 || targetIndex > code.Count)
                            Console.WriteLine("Couldn't perform addition due to out of index");
                        
                        else if (targetIndex == 224)
                            Console.WriteLine("Just a breakpoint");
                        
                        code[targetIndex] = code[value1] + code[value2];
                        break;
                    case 2:
                        // Multiply
                        value1 = code[index + 1];
                        value2 = code[index + 2];
                        targetIndex = code[index + 3];
                        
                        if (targetIndex < 0 || targetIndex > code.Count)
                            Console.WriteLine("Multplied missed the code index");

                        else if (targetIndex == 224)
                            Console.WriteLine("Just a breakpoint");

                        code[targetIndex] = code[value1] * code[value2];
                        break;
                    case 3:
                        // Read
                        Console.Write("Input: ");
                        string input = Console.ReadLine();

                        targetIndex = code[code[index + 1]];
                        
                        if (targetIndex < 0 || targetIndex > code.Count)
                            Console.WriteLine("Seems like it was here it went to hell");

                        else if (targetIndex == 224)
                            Console.WriteLine("Just a breakpoint");

                        code[targetIndex] = Convert.ToInt32(input);
                        break;
                    case 4:
                        // Write
                        targetIndex = code[code[index + 1]];
                        
                        if (targetIndex < 0 || targetIndex > code.Count)
                            Console.WriteLine("It went wrong here!");

                        else if (targetIndex == 224)
                            Console.WriteLine("Just a breakpoint");

                        Console.WriteLine(code[targetIndex]);
                        break;
                }

                index += steps;
            }

            return code[0];
        }
    }
}