using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Challenges
{
    internal class DayThree : IDayChallenge
    {
        public void Part1()
        {
            var rucksacks = File.ReadAllLines("Inputs\\day3.txt");
            var points = 0;

            foreach (var rucksack in rucksacks)
            {
                var rucksackParts = GetTheTwoRucksack(rucksack);
                var doubleItems = GetDoubleItems(rucksackParts.rucksackA, rucksackParts.rucksackB);
                points += GetPoints(doubleItems);
            }
            Console.WriteLine("Result: " + points);
        }

        private List<char> GetDoubleItems(string rucksackA, string rucksackB)
        {
            List<char> chars = new List<char>();

            foreach (var itemValue in rucksackA)
            {
                foreach (var currentItemInB in rucksackB)
                {
                    if (itemValue == currentItemInB)
                    {
                        chars.Add(itemValue);
                    }
                }
            }

            return chars.Distinct().ToList();
        }

        private List<char> GetDoubleItems(string rucksackA, string rucksackB, string rucksackC)
        {
            List<char> chars = new List<char>();

            foreach (var itemValue in rucksackA)
            {
                bool isInB = false;
                bool isInC = false;
                foreach (var currentItemInB in rucksackB) { if (itemValue == currentItemInB) { isInB = true; } }

                foreach (var currentItemInC in rucksackC) { if (itemValue == currentItemInC) { isInC = true; } }

                if (isInB && isInC) { chars.Add(itemValue); }
            }

            return chars.Distinct().ToList();
        }


        public int GetPoints(List<char> items)
        {
            int output = 0;
            const int AsciiTableLowerCases = 97;
            const int AsciiTableUpperCases = 65;
            const int startPointsLowerCases = 1;
            const int startPointsUpperCases = 27;

            foreach (var item in items)
            {
                if (char.IsLower(item)) { output += (int)item - AsciiTableLowerCases + startPointsLowerCases; }
                else { output += (int)item - AsciiTableUpperCases + startPointsUpperCases; }
            }
            //Console.WriteLine("Points: " + output);

            return output;
        }

        private (string rucksackA, string rucksackB) GetTheTwoRucksack(string input)
        {
            var splitCount = input.Length / 2;

            string rucksackA = input.Substring(0, splitCount);
            string rucksackB = input.Substring(splitCount, input.Length - splitCount);

            //Console.WriteLine($"{rucksackParts.rucksackA} {rucksackParts.rucksackB}");

            return (rucksackA, rucksackB);
        }

        public void Part2()
        {
            var rucksacks = File.ReadAllLines("Inputs\\day3.txt");
            var points = 0;

            for (int i = 0; i < rucksacks.Count();)
            {
                string[] Group = new string[] { rucksacks[i], rucksacks[i + 1], rucksacks[i + 2] };

                var doubleItems = GetDoubleItems(Group[0], Group[1], Group[2]);
                points += GetPoints(doubleItems);

                i += 3;
            }
            Console.WriteLine("Result: " + points);
        }
    }
}
