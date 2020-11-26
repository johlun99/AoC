using System;
using System.Collections.Generic;

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
                int value1 = code[index + 1];
                int value2 = code[index + 2];
                int targetIndex = code[index + 3];

                if (opCode == 1)
                {
                    code[targetIndex] = code[value1] + code[value2];
                }

                else if (opCode == 2)
                {
                    code[targetIndex] = code[value1] * code[value2];
                }

                else
                {
                    throw new Exception($"Wrongful opcode: {opCode}");
                }

                index += 4;
            }

            return code[0];
        }
    }
}