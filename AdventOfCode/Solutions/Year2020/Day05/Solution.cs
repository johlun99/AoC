using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day05 : ASolution
    {
        readonly List<string> seatCodes;
        private List<int> seatIds = new List<int>();

        private Dictionary<int, int> grid = new Dictionary<int, int>();
        
        public Day05() : base(05, 2020, "")
        {
            seatCodes = new List<string>(Input.SplitByNewline());
            GenerateGrid();
            GetSeatIds();
        }

        protected override string SolvePartOne()
        {
            int highestId = 0;

            foreach (string seatCode in seatCodes)
            {
                var values = GetSeatValue(seatCode);

                if (values.id > highestId)
                    highestId = values.id;
            }
            
            return Convert.ToString(highestId);
        }

        protected override string SolvePartTwo()
        {
            for (int i = 2; i < seatIds.Count; i++)
            {
                if (seatIds[i] - seatIds[i - 1] == 2)
                {
                    return Convert.ToString(seatIds[i] - 1);
                }
            }

            return null;
        }

        private (int row, int column, int id) GetSeatValue(string seatCode)
        {
            
            (int min, int high) yRange = (0, 127);
            (int min, int high) xRange = (0, 7);

            for (int i = 0; i < 7; i++)
            {
                int distance = Math.Abs((yRange.min - yRange.high) / 2) + 1;
                
                switch (seatCode[i])
                {
                    case 'F':
                        yRange.high -= distance;
                        break;
                    case 'B' :
                        yRange.min += distance;
                        break;
                }
            }

            if (yRange.min != yRange.high)
                throw new Exception("The y values doesn't match");

            for (int i = 7; i < 10; i++)
            {
                int distance = Math.Abs((xRange.min - xRange.high) / 2) + 1;
                    
                switch (seatCode[i])
                {
                    case 'L':
                        xRange.high -= distance;
                        break;
                    case 'R':
                        xRange.min+= distance;
                        break;
                }
            }

            if (xRange.min != xRange.high)
                throw new Exception("X values doesn't match");
            
            
            int id = (yRange.min * 8) + xRange.min;

            return (yRange.min, xRange.min, id);
        }

        private void GenerateGrid()
        {
            foreach (string seatCode in seatCodes)
            {
                (int row, int column, int id) seat = GetSeatValue(seatCode);

                grid[seat.row] = seat.column;
            }
        }

        private void GetSeatIds()
        {
            foreach (string seatCode in seatCodes)
            {
                seatIds.Add(GetSeatValue(seatCode).id);
            }
            
            seatIds.Sort();
        }
    }
}
