using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Challenges
{
    internal class DayOne : IDayChallenge
    {

        public void Part1()
        {
            List<int> elfen = GetElfenSum();

            var result = elfen.OrderByDescending(x => x).ToList()[0];
            Console.WriteLine("Result Part 1 " + result);
        }

        public void Part2()
        {
            List<int> elfen = GetElfenSum();

            var sortedList = elfen.OrderByDescending(x => x).ToList();
            var result = sortedList[0] + sortedList[1] + sortedList[2];
            Console.WriteLine("Result Part 2 " + result);
        }

        public List<int> GetElfenSum()
        {
            var content = File.ReadAllLines("Inputs\\day1.txt");
            List<int> elfen = new List<int>();
            var currentElf = 0;

            foreach (var row in content)
            {
                if (string.IsNullOrEmpty(row))
                {
                    elfen.Add(currentElf);
                    currentElf = 0;
                }
                else { currentElf += Convert.ToInt32(row); }
            }

            return elfen.OrderByDescending(x => x).ToList();
        }
    }
}
