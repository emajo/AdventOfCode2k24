Console.WriteLine("Insert the day bro... ");
var day = Console.ReadLine();

switch (day)
{
    case "1":
        Console.WriteLine(Day01.Part(true));
        Console.WriteLine(Day01.Part(false));
        break;

    default:
        Console.WriteLine("Default");
        break;
}