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

    default:
        Console.WriteLine("Default");
        break;
}