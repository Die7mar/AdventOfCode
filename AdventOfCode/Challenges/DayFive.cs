using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Challenges
{
    internal class DayFive : IDayChallenge
    {
        const int CountStacks = 9;
        public void Part1()
        {
            //throw new NotImplementedException();
            var readed = Read();

            var content = File.ReadAllLines("Inputs\\day5.txt");
            for (int i = readed.startOnGoing; i < content.Length; i++)
            {
                Move(ref readed.stacks, content[i]);
            }

            string resultString = "";

            for (int i = 0; i < CountStacks; i++)
            {
                resultString += readed.stacks[i][0];
            }

            Console.WriteLine("Result: " + resultString);
        }


        private (List<List<char>> stacks, int startOnGoing) Read()
        {
            var content = File.ReadAllLines("Inputs\\day5.txt");

            List<List<char>> stacks = new List<List<char>>();
            int end = 0;


            for (int i = 0; i < content.Length; i++) { if (content[i].Contains(" 1   2   3")) { end = i - 1; } }

            for (int s = 0; s < CountStacks; s++)
            {
                stacks.Add(new List<char>());

                int datPos = (s * 4) + 1;
                for (int i = 0; i <= end; i++)
                {
                    if (content[i].Length >= datPos && char.IsLetter(content[i][datPos]))
                    {
                        stacks[s].Add(content[i][datPos]);
                    }
                }

            }

            foreach (var item in stacks) { item.Reverse(); }

            return (stacks, end + 3);
        }

        private void Move(ref List<List<char>> stacks, string cmd)
        {
            string[] cmdParts = cmd.Split(' ');
            int count = Convert.ToInt32(cmdParts[1]);
            int sourceStack = Convert.ToInt32(cmdParts[3]) - 1;
            int targetStack = Convert.ToInt32(cmdParts[5]) - 1;


            for (int i = 0; i < count; i++)
            {
                var value = stacks[sourceStack][stacks[sourceStack].Count - 1];
                stacks[sourceStack].RemoveAt(stacks[sourceStack].Count - 1);
                stacks[targetStack].Add(value);
            }


        }

 
        public void Part2()
        {
            //throw new NotImplementedException();
        }
    }
}
