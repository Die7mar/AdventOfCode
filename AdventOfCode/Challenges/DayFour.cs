using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Challenges
{
    internal class DayFour : IDayChallenge
    {

        public void Part1()
        {
            var content = File.ReadAllLines("Inputs\\day4.txt");
            int result = 0;

            foreach (var item in content)
            {
                var lists = GetList(item);
                bool containsFullList = ContainsFullList(lists.listA, lists.listB);

                if (containsFullList) { result++; }
            }

            Console.WriteLine("Result: " + result);
        }

        public void Part2()
        {
            var content = File.ReadAllLines("Inputs\\day4.txt");
            int result = 0;

            foreach (var item in content)
            {
                var lists = GetList(item);
                bool containsFullList = HasAOverLap(lists.listA, lists.listB);

                if (containsFullList) { result++; }
            }

            Console.WriteLine("Result: " + result);
        }

        private (List<int> listA, List<int> listB) GetList(string listRanges)
        {
            var parts = listRanges.Split(',');
            var listA = GetRange(parts[0]);
            var listB = GetRange(parts[1]);

            return (listA, listB);
        }

        private bool ContainsFullList(List<int> listA, List<int> listB)
        {
            List<int> smallerList;
            List<int> biggerList;
            bool containsFullList = true;

            if (listA.Count < listB.Count)
            {
                smallerList = listA;
                biggerList = listB;
            }
            else
            {
                smallerList = listB;
                biggerList = listA;
            }

            foreach (var item in smallerList)
            {
                if (!biggerList.Contains(item))
                {
                    containsFullList = false;
                    break;
                }
            }

            return containsFullList;
        }

        private bool HasAOverLap(List<int> listA, List<int> listB)
        {
            bool hasOverLap = false;

            foreach (var item in listA) { if (listB.Contains(item)) { hasOverLap = true; } }

            return hasOverLap;
        }

        private List<int> GetRange(string range)
        {
            var list = new List<int>();
            string[] rangeParts = range.Split("-");
            int start = Convert.ToInt32(rangeParts[0]);
            int end = Convert.ToInt32(rangeParts[1]);

            for (int i = start; i < end + 1; i++) { list.Add(i); }

            return list;
        }

    }
}
