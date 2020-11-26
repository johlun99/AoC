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
            string[] input = Input.SplitByNewline();

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
            List<(int, int)> firstCoordinates = GetPathCoordinates(firstWire);
            List<(int, int)> secondCoordinates = GetPathCoordinates(secondWire);

            IEnumerable<(int, int)> intersections = firstCoordinates.Intersect(secondCoordinates);
            
            return Convert.ToString(GetShortestNumberOfStepsToIntersection(intersections, firstCoordinates, secondCoordinates));
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
                if (Math.Abs(intersection.Item1) + Math.Abs(intersection.Item2) < Math.Abs(closestToHome.Item1) + Math.Abs(closestToHome.Item2))
                    closestToHome = intersection;

            return Convert.ToInt32(Math.Abs(closestToHome.Item2) + Math.Abs(closestToHome.Item1));
        }

        protected int GetShortestNumberOfStepsToIntersection(IEnumerable<(int, int)> intersections, List<(int, int)> firstPath, List<(int, int)> secondPath)
        {
            int shortest = 0;

            foreach ((int, int) intersection in intersections)
            {
                int steps1 = 0;
                int steps2 = 0;
                int distance;
                
                foreach (var coordinate in firstPath)
                {
                    steps1++;
                    if (coordinate == intersection)
                        break;
                }

                foreach (var coordinate in secondPath)
                {
                    steps2++;
                    if (coordinate == intersection)
                        break;
                }

                distance = steps1 + steps2;

                if (shortest == 0)
                    shortest = distance;
                
                else if (distance < shortest)
                    shortest = distance;
            }

            return shortest;
        }
    }
}
