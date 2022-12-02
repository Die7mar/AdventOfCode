using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Challenges
{
    internal class DayTwo : IDayChallenge
    {
        //        Result: 10994
        //        Result: 12526
        public void Part1()
        {
            var matches = ReadPuzzle();
            int points = 0;

            foreach (var match in matches)
            {
                var theMatch = GetMatch(match);
                points += GetWinner(theMatch.op, theMatch.Choose);
                points += GetChoosingPoints(theMatch.Choose);
            }

            Console.WriteLine("Result: " + points);
        }


        public void Part2()
        {
            var matches = ReadPuzzle();
            int points = 0;
            foreach (var match in matches)
            {
                var theMatch = GetMatch(match);
                var elfSay = ConvertToElfSay(theMatch.Choose);
                var result = GetWhatINeed(theMatch.op, elfSay);

                switch (elfSay)
                {
                    case ElfSay.Lose: points += 0; break;
                    case ElfSay.Draw: points += 3; break;
                    case ElfSay.Win: points += 6; break; default:
                        break;
                }

                points += GetChoosingPoints(result.choose);
            }

            Console.WriteLine("Result: " + points);
        }
      
        public enum Choose
        {
            Rock,
            Paper,
            Scissors
        }

        public enum ElfSay
        {
            Lose = 'X',
            Draw = 'Y',
            Win = 'Z'
        }

        public int GetWinner(Choose opponent, Choose me)
        {
            int points = -1;
            if (opponent == me) { points = 3; }

            if (Choose.Rock == opponent && Choose.Paper == me) { points = 6; }
            if (Choose.Rock == opponent && Choose.Scissors == me) { points = 0; }

            if (Choose.Paper == opponent && Choose.Scissors == me) { points = 6; }
            if (Choose.Paper == opponent && Choose.Rock == me) { points = 0; }

            if (Choose.Scissors == opponent && Choose.Rock == me) { points = 6; }
            if (Choose.Scissors == opponent && Choose.Paper == me) { points = 0; }

            //Console.WriteLine($"Op:{opponent} Choose: {Choose} => {points}");

            if (points == -1) { Console.WriteLine("ERROR"); }

            return points;
        }

        public (Choose choose, int points) GetWhatINeed(Choose opponent, ElfSay elfSay)
        {
            Choose choose = Choose.Paper;
            int points = -1;

            if (ElfSay.Win == elfSay)
            {
                points = 6;
                if (Choose.Rock == opponent) { choose = Choose.Paper; }
                if (Choose.Paper == opponent) { choose = Choose.Scissors; }
                if (Choose.Scissors == opponent) { choose = Choose.Rock; }
            }
            else if (ElfSay.Lose == elfSay)
            {
                points = 0;
                if (Choose.Rock == opponent) { choose = Choose.Scissors; }
                if (Choose.Paper == opponent) { choose = Choose.Rock; }
                if (Choose.Scissors == opponent) { choose = Choose.Paper; }
            }
            else if (ElfSay.Draw == elfSay) { choose = opponent; points = 3; }
            else { Console.WriteLine("ERROR"); }

            //Console.WriteLine($"ElfSay:\t{elfSay}\tOp Choose: {opponent}\t{Choose}");           

            return (choose, points);
        }

        public ElfSay ConvertToElfSay(Choose Choose)
        {
            ElfSay elfSay = new ElfSay();
            switch (Choose)
            {
                case Choose.Rock: elfSay = ElfSay.Lose; break;
                case Choose.Paper: elfSay = ElfSay.Draw; break;
                case Choose.Scissors: elfSay = ElfSay.Win; break;
                default: break;
            }

            return elfSay;
        }


        private Choose GetChoose(char value, bool isOpponend = false)
        {
            Choose choose = new Choose();

            if (isOpponend)
            {
                switch (value)
                {
                    case 'A': choose = Choose.Rock; break;
                    case 'B': choose = Choose.Paper; break;
                    case 'C': choose = Choose.Scissors; break;
                }
            }
            else
            {
                switch (value)
                {
                    case 'X': choose = Choose.Rock; break;
                    case 'Y': choose = Choose.Paper; break;
                    case 'Z': choose = Choose.Scissors; break;
                    default: break;
                }
            }

            return choose;
        }


        public int GetChoosingPoints(Choose Choose)
        {
            int points = 0;
            switch (Choose)
            {
                case Choose.Rock: points = 1; break;
                case Choose.Paper: points = 2; break;
                case Choose.Scissors: points = 3; break;
                default: break;
            }

            //Console.WriteLine("ChoosingPoints: " + points);
            return points;
        }

        public string[] ReadPuzzle() { return File.ReadAllLines("Inputs\\day2.txt"); }

        public (Choose op, Choose Choose) GetMatch(string match)
        {
            var parts = match.Split(' ');
            Choose op = GetChoose(parts[0][0],true);
            Choose Choose = GetChoose(parts[1][0]);

            return (op, Choose);
        }

    }
}
