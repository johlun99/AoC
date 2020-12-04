using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day04 : ASolution
    {
        private List<List<(string, string)>> passports = new List<List<(string, string)>>();

        public Day04() : base(04, 2020, "")
        {
            string[] inputs = Input.Split("\n\n");

            foreach (var row in inputs)
            {
                string[] line = row.SplitByNewline();
                List<(string, string)> fields = new List<(string, string)>();

                foreach (var field in line)
                {
                    List<string> fieldsInRow = new List<string>(field.Split(' '));
                    foreach (var f in fieldsInRow)
                        fields.Add((f.Split(':')[0], f.Split(':')[1]));
                }
                
                passports.Add(fields);
            }
        }

        protected override string SolvePartOne()
        {
            int validPassports = 0;

            foreach (List<(string, string)> passport in passports)
                if (ValidatePassport(passport))
                    validPassports++;
            
            return Convert.ToString(validPassports);
        }

        protected override string SolvePartTwo()
        {
            int validPassports = 0;

            foreach (List<(string, string)> passport in passports)
                if (ValidatePassport(passport, true))
                    validPassports++;
            
            return Convert.ToString(validPassports);
        }

        private bool ValidatePassport(List<(string, string)> passport, bool checkValues = false)
        {
            bool passportIsValid = true;
            
            if (passport.Count < 7)
                passportIsValid = false;

            else
            {
                int validFields = 0;
                
                foreach ((string, string) field in passport)
                {
                    if (field.Item1.Equals("byr"))
                    {
                        if (!checkValues)
                            validFields++;
                        
                        else if (int.TryParse(field.Item2, out int n))
                        {
                            int year = Convert.ToInt32(field.Item2);
                            if (year >= 1920 && year <= 2002)
                                validFields++;
                        }
                    }
                    
                    else if (field.Item1.Equals("iyr"))
                    {
                        if (!checkValues)
                            validFields++;
                        
                        else if (int.TryParse(field.Item2, out int n))
                        {
                            int year = Convert.ToInt16(field.Item2);
                            if (year >= 2010 && year <= 2020)
                                validFields++;
                        }
                    }
                    
                    else if (field.Item1.Equals("eyr"))
                    {
                        if (!checkValues)
                            validFields++;
                        
                        else if (int.TryParse(field.Item2, out int n))
                        {
                            int year = Convert.ToInt16(field.Item2);
                            if (year >= 2020 && year <= 2030)
                                validFields++;
                        }
                    }
                    
                    else if (field.Item1.Equals("hgt"))
                    {

                        if (!checkValues)
                        {
                            validFields++;
                            continue;
                        }

                        var match = Regex.Match(field.Item2, @"(\d+)(\w+)");
                        int height = Convert.ToInt16(match.Groups[1].Value);
                        string unit = match.Groups[2].Value;

                        if (unit == "cm")
                        {
                            if (height >= 150 && height <= 193)
                                validFields++;
                        }

                        else if (unit == "in")
                        {
                            if (height >= 59 && height <= 76)
                                validFields++;
                        }
                    }                                                                          
                    
                    else if (field.Item1.Equals("hcl"))
                    {
                        if (!checkValues)
                        {
                            validFields++;
                            continue;
                        }
                        
                        bool isValid = true;

                        if (field.Item2[0] == '#' && field.Item2.Length == 7)
                        {
                            for (int i = 1; i < field.Item2.Length; i++)
                            {
                                if (Char.IsLetter(field.Item2, i) &&
                                    field.Item2[i] < 'a' &&
                                    field.Item2[i] > 'f')
                                {
                                    isValid = false;
                                }
                                else if (Char.IsDigit(field.Item2, i))
                                {
                                    int num = Convert.ToInt32(field.Item2[i]);

                                    if (num < 0)
                                        isValid = false;
                                }
                            }
                        }
                        else
                            isValid = false;

                        if (isValid)
                            validFields++;
                    }
                    
                    else if (field.Item1.Equals("ecl"))
                    {
                        if (!checkValues)
                        {
                            validFields++;
                            continue;
                        }

                        switch (field.Item2)
                        {
                            case "amb":
                            case "blu":
                            case "brn":
                            case "gry":
                            case "grn":
                            case "hzl":
                            case "oth":
                                validFields++;
                                break;
                        }
                    }
                    
                    else if (field.Item1.Equals("pid"))
                    {
                        if (!checkValues)
                            validFields++;
                        
                        else if (field.Item2.Length == 9 &&
                            int.TryParse(field.Item2, out int n))
                        {
                            validFields++;
                        }
                    }
                }

                passportIsValid = validFields >= 7;
            }

            return passportIsValid;
        }
    }
}
