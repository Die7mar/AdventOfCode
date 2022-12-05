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
        public void Part1() => MoveIt(Mover.CreateMover9000);
       
        public void Part2() => MoveIt(Mover.CreateMover9001);

        private void MoveIt(Mover mover)
        {

            var readedInput = ReadFile();
            var containers = ReadContainer(readedInput.containers);

            switch (mover)
            {
                case Mover.CreateMover9000:
                    readedInput.cmd.ToList().ForEach(x => CreateMover9000(ref containers, x));
                    break;
                case Mover.CreateMover9001:
                    readedInput.cmd.ToList().ForEach(x => CreateMover9001(ref containers, x));
                    break;
            }

            string resultString = "";

            for (int i = 0; i < CountStacks; i++) { resultString += containers[i].Last(); }

            Console.WriteLine("Result: " + resultString);
        }


        private (string[] containers, string[] cmd) ReadFile()
        {
            string content = File.ReadAllText("Inputs\\day5.txt");
            string[] parts = content.Split("\r\n\r\n");

            string[] containers = parts[0].Split("\n");
            string[] cmd = parts[1].Split("\n");

            return (containers, cmd);
        }


        private List<List<char>> ReadContainer(string[] container)
        {
            var content = File.ReadAllLines("Inputs\\day5.txt");

            List<List<char>> stacks = new List<List<char>>();

            for (int s = 0; s < CountStacks; s++)
            {
                stacks.Add(new List<char>());

                int datPos = (s * 4) + 1;
                for (int i = 0; i <= container.Count() - 1; i++)
                {
                    if (content[i].Length >= datPos && char.IsLetter(content[i][datPos])) { stacks[s].Add(content[i][datPos]); }
                }
            }

            stacks.ForEach(x => x.Reverse());

            return stacks;
        }

        private void CreateMover9000(ref List<List<char>> stacks, string cmdRow)
        {
            CMD cmd = new CMD(cmdRow);

            for (int i = 0; i < cmd.Count; i++)
            {
                var value = stacks[cmd.SourceStack][stacks[cmd.SourceStack].Count - 1];
                stacks[cmd.SourceStack].RemoveAt(stacks[cmd.SourceStack].Count - 1);
                stacks[cmd.TargetStack].Add(value);
            }
        }

        private void CreateMover9001(ref List<List<char>> stacks, string cmdRow)
        {
            CMD cmd = new CMD(cmdRow);

            int startMove = stacks[cmd.SourceStack].Count - cmd.Count;
            int endMove = stacks[cmd.SourceStack].Count - startMove;

            var value = stacks[cmd.SourceStack].GetRange(startMove, endMove);
            stacks[cmd.SourceStack].RemoveRange(startMove, endMove);
            stacks[cmd.TargetStack].AddRange(value);
        }

        class CMD
        {

            public int Count { get; set; }
            public int SourceStack { get; set; }
            public int TargetStack { get; set; }

            public CMD(string cmdRow)
            {
                string[] cmdParts = cmdRow.Split(' ');
                Count = Convert.ToInt32(cmdParts[1]);
                SourceStack = Convert.ToInt32(cmdParts[3]) - 1;
                TargetStack = Convert.ToInt32(cmdParts[5]) - 1;
            }
        }

        enum Mover
        {
            CreateMover9000,
            CreateMover9001
        }

    }
}
