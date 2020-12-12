using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day11 : ASolution
    {
        private readonly List<(int x, int y)> _neighbours = new List<(int x, int y)>
        {
            (-1, 1), (0, 1), (1, 1),
            (-1, 0),         (1, 0),
            (-1, -1), (0, -1), (1, -1)
        };

        private List<string> _lines;
        private List<Tile> _states;
        
        public enum Tile
        {
            Floor,
            Empty,
            Full
        }

        public Day11() : base(11, 2020, "")
        {
           _lines = new List<string>(Input.SplitByNewline());
        }

        protected override string SolvePartOne()
        {
            var states = _lines.Select(x => x.Select(c => c switch
            {
                'L' => Tile.Empty,
                '#' => Tile.Full,
                '.' => Tile.Floor
            }).ToList()).ToList();

            int count = 0;
            
            while (true)
            {
                count++;
                var oldState = states.Select(l => l.Select(x => x).ToList()).ToList();
                var newState = CheckRules(states);
                var same = true;
                for (int y = 0; y < newState.Count(); y++)
                {
                    for (int x = 0; x < newState.First().Count(); x++)
                    {
                        if (newState[y][x] != oldState[y][x])
                        {
                            same = false;
                        }
                    }
                }

                if (same)
                {
                    return Convert.ToString(newState.Sum(l => l.Count(x => x == Tile.Full)));
                }

                states = newState;
            }
            
            return null;
        }

        protected override string SolvePartTwo()
        {
            var states = _lines.Select(x => x.Select(c => c switch
            {
                'L' => Tile.Empty,
                '#' => Tile.Full,
                '.' => Tile.Floor
            }).ToList()).ToList();

            int count = 0;
            
            while (true)
            {
                count++;
                var oldState = states.Select(l => l.Select(x => x).ToList()).ToList();
                var newState = CheckUpdatedRules(states);
                var same = true;
                for (int y = 0; y < newState.Count(); y++)
                {
                    for (int x = 0; x < newState.First().Count(); x++)
                    {
                        if (newState[y][x] != oldState[y][x])
                        {
                            same = false;
                        }
                    }
                }

                if (same)
                {
                    return Convert.ToString(newState.Sum(l => l.Count(x => x == Tile.Full)));
                }

                states = newState;
            }
        }
        
        public static List<List<Tile>> CheckRules(List<List<Tile>> lines)
        {
            var newState = lines.Select(l => l.Select(c => c).ToList()).ToList();

            for (int y = 0; y < lines.Count(); y++)
            {
                var line = lines[y];
                for (int x = 0; x < lines.First().Count(); x++)
                {
                    var upAllowed = y > 0;
                    var downAllowed = y <= lines.Count()-2;
                    var leftAllowed = x > 0;
                    var rightAllowed = x <= line.Count()-2;

                    var up = upAllowed && lines[y - 1][x] == Tile.Full;
                    var down = downAllowed && lines[y + 1][x] == Tile.Full;
                    var left = leftAllowed && lines[y][x-1] == Tile.Full;
                    var right = rightAllowed && lines[y][x+1] == Tile.Full;

                    var upLeft = upAllowed && leftAllowed && lines[y - 1][x - 1] == Tile.Full;
                    var upRight = upAllowed && rightAllowed && lines[y - 1][x + 1] == Tile.Full;
                    var downLeft = downAllowed && leftAllowed && lines[y + 1][x - 1] == Tile.Full;
                    var downRight = downAllowed && rightAllowed && lines[y + 1][x + 1] == Tile.Full;

                    var neighbours = new [] {up, down, left, right, upLeft, upRight, downLeft, downRight};
                    
                    var existing = newState[y][x];
                    var all = neighbours.All(x => x);
                    var adjacent = neighbours.Count(x => x);
                    var none = neighbours.All(x => !x);

                    if (existing == Tile.Full && adjacent >= 4)
                    {
                        newState[y][x] = Tile.Empty;
                    } else if (existing == Tile.Empty && none)
                    {
                        newState[y][x] = Tile.Full;
                    };
                }
            }

            return newState;
        }
        
        private static List<List<Tile>> CheckUpdatedRules(List<List<Tile>> lines)
        {
            var newState = lines.Select(l => l.Select(c => c).ToList()).ToList();

            
            for (int y = 0; y < lines.Count(); y++)
            {
                var line = lines[y];
                for (int x = 0; x < lines.First().Count(); x++)
                {
                    Tile FirstSeat(int xdir, int ydir)
                    {
                        var xi = x + xdir;
                        var yi = y + ydir;

                        bool OOB()
                        {
                            return xi < 0 || yi < 0 || xi >= lines.First().Count() || yi >= lines.Count();
                        }
                        
                        while (!OOB())
                        {
                            var spot = lines[yi][xi];
                            if (spot != Tile.Floor)
                            {
                                return spot;
                            }

                            xi += xdir;
                            yi += ydir;
                        }

                        return Tile.Floor;
                    }
                    
                    var up = FirstSeat(0, -1) == Tile.Full;
                    var down =  FirstSeat(0, 1) == Tile.Full;
                    var left =  FirstSeat(-1, 0) == Tile.Full;
                    var right =  FirstSeat(1, 0) == Tile.Full;

                    var upLeft =  FirstSeat(-1, -1) == Tile.Full;
                    var upRight =  FirstSeat(1, -1) == Tile.Full;
                    var downLeft =  FirstSeat(-1, 1) == Tile.Full;
                    var downRight =  FirstSeat(1, 1) == Tile.Full;

                    var neighbours = new [] {up, down, left, right, upLeft, upRight, downLeft, downRight};
                    
                    var existing = newState[y][x];
                    var all = neighbours.All(x => x);
                    var adjacent = neighbours.Count(x => x);
                    var none = neighbours.All(x => !x);

                    if (existing == Tile.Full && adjacent >= 5)
                    {
                        newState[y][x] = Tile.Empty;
                    } else if (existing == Tile.Empty && none)
                    {
                        newState[y][x] = Tile.Full;
                    };
                }
            }

            return newState;
        }
    }
}
