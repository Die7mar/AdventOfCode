using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Challenges
{
    internal class DaySix : IDayChallenge
    {
        public void Part1() => CheckStreamByLength(4);

        public void Part2() => CheckStreamByLength(14);

        private void CheckStreamByLength(int length)
        {
            var inputString = File.ReadAllText("Inputs\\day6.txt");
            int result = 0;
            for (int i = 0; i < inputString.Length; i++)
            {
                List<char> currentSignal = inputString.Substring(i, length).ToList();
                if (currentSignal.GroupBy(x => x).Count() == length) { result = i + length; break; }
            }
            Console.WriteLine("Result: " + result);
        }
    }
}
