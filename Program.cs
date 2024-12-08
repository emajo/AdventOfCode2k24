Console.WriteLine("Insert the day bro... ");
var day = Console.ReadLine();

switch (day)
{
    case "1":
        Console.WriteLine(Day01.Part(true));
        Console.WriteLine(Day01.Part(false));
        break;
    case "2":
        Console.WriteLine(Day02.Part(true));
        Console.WriteLine(Day02.Part(false));
        break;
    case "3":
        Console.WriteLine(Day03.Part(true));
        Console.WriteLine(Day03.Part(false));
        break;
    case "4":
        Console.WriteLine(Day04.Part(true));
        Console.WriteLine(Day04.Part(false));
        break;
    case "5":
        Console.WriteLine(Day05.Part(true));
        Console.WriteLine(Day05.Part(false));
        break;
    case "6":
        Console.WriteLine(Day06.Part(true));
        Console.WriteLine(Day06.Part(false));
        break;
    case "7":
        Console.WriteLine(Day07.Part(true));
        Console.WriteLine(Day07.Part(false));
        break;
    case "8":
        Console.WriteLine(Day08.Part(true));
        Console.WriteLine(Day08.Part(false));
        break;

    default:
        Console.WriteLine("Default");
        break;
}