using AdventOfCode;
using AdventOfCode.Challenges;


while (true)
{
    Console.WriteLine("Enter the Date or type c for cancel");
    string input = Console.ReadLine();

    if (input.ToLower() == "c") { break; }

    bool isNumber = int.TryParse(input, out int selectedDate);

    if (!isNumber) { Console.WriteLine("Please tippe a valid number!"); }

    IDayChallenge challenge = null;

    switch (selectedDate)
    {
        case 1:
            challenge = new DayOne();
            break;
        case 2:
            challenge = new DayTwo(); 
            break;
        case 3:
            challenge = new DayThree();
            break;
        case 4:
            challenge = new DayFour();
            break;
        case 5:
            challenge = new DayFive();
            break;
        case 6:
            challenge = new DaySix();
            break;
        case 7:
            challenge = new DaySeven();
            break;
        default:
            Console.WriteLine("No DATA");
            continue;
    }

    challenge.Part1();
    challenge.Part2();
}


