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
                resultString += readed.stacks[i,0];
            }

            Console.WriteLine("Result: " + resultString);
        }


        private (char[,] stacks, int startOnGoing) Read()
        {
            var content = File.ReadAllLines("Inputs\\day5.txt");
          
            char[,] stacks = new char[CountStacks, 100];
            int end = 0;


            for (int i = 0; i < content.Length; i++) { if (content[i].Contains(" 1   2   3")) { end = i - 1; } }

            for (int s = 0; s < CountStacks; s++)
            {
                int datPos = (s * 4) + 1;
                int pos = 0;

                for (int i = end; i >= 0; i--)
                {
                    if (content[i].Length >= datPos && char.IsLetter(content[i][datPos]))
                    {
                        stacks[s, pos] = content[i][datPos];
                    }
                    pos++;
                }

            }

            return (stacks, end + 3);
        }

        private void Move(ref char[,] stacks, string cmd)
        {
            string[] cmdParts = cmd.Split(' ');
            int count = Convert.ToInt32(cmdParts[1]);
            int sourceStack = Convert.ToInt32(cmdParts[3]) - 1;
            int targetStack = Convert.ToInt32(cmdParts[5]) - 1;

            var sourceStackCount = GetCountOfStack(stacks, sourceStack);
            var targetStackCount = GetCountOfStack(stacks, targetStack);

            for (int i = 0; i < count; i++)
            {
                var value = stacks[sourceStack, sourceStackCount - 1];
                stacks[sourceStack, sourceStackCount - 1] = '\0';
                stacks[targetStack, targetStackCount] = value;

                sourceStackCount--;
                targetStackCount++;
            }


        }

        private int GetCountOfStack(char[,] stacks, int stack)
        {
            var output = 0;

            for (int i = 0; i < stacks.Length; i++)
            {
                if (!char.IsLetter(stacks[stack,i]))
                {
                    break;
                } 
                output++;
            }

           

            return output;
        }


        public void Part2()
        {
            //throw new NotImplementedException();
        }
    }
}
