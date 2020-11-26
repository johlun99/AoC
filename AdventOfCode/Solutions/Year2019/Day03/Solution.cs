using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{

    class Day03 : ASolution
    {
        private readonly List<string> firstWire = new List<string>();
        private readonly List<string> secondWire = new List<string>();

        public Day03() : base(03, 2019, "")
        {
            //string[] input = Input.SplitByNewline();
            string[] input = "R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83".SplitByNewline();

            firstWire = new List<string>(input[0].Split(','));
            secondWire = new List<string>(input[1].Split(','));
        }

        protected override string SolvePartOne()
        {
            List<(int, int)> firstCoordinates = GetPathCoordinates(firstWire);
            List<(int, int)> secondCoordinates = GetPathCoordinates(secondWire);

            IEnumerable<(int, int)> intersections = firstCoordinates.Intersect(secondCoordinates);

            return Convert.ToString(GetClosestManhatanDistance(intersections));
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        protected List<(int, int)> GetPathCoordinates(List<string> wire)
        {
            List<(int, int)> coordinates = new List<(int, int)>();

            int x = 0;
            int y = 0;

            foreach (var turn in wire)
            {
                char dir = Convert.ToChar(turn.Substring(0, 1));
                int steps = Convert.ToInt32(turn.Substring(1));

                for (int i = 0; i < steps; i++)
                {
                    if (dir == 'U')
                        y++;

                    else if (dir == 'D')
                        y--;

                    else if (dir == 'R')
                        x++;

                    else if (dir == 'L')
                        x--;

                    else
                    {
                        throw new Exception($"Invalid direction '{dir}'");
                    }

                    coordinates.Add((x, y));
                }
            }

            return coordinates;
        }

        protected int GetClosestManhatanDistance(IEnumerable<(int, int)> intersections)
        {
            (int, int) closestToHome = intersections.First();

            foreach ((int, int) intersection in intersections)
            {
                if (intersection.Item1 + intersection.Item2 < closestToHome.Item1 + intersection.Item2)
                    closestToHome = intersection;
            }
            
            return Convert.ToInt32(Math.Abs(closestToHome.Item2) + Math.Abs(closestToHome.Item1) * Math.Sqrt(2));
        }
}
}
