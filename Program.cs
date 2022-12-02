using System.Reflection;

namespace aoc22;

internal class Program
{
    private const string ClassPrefix = "Day";

    private static void Main(string[] args)
    {
        var types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass && t.Name.StartsWith(ClassPrefix))
            .OrderBy(t => t.Name)
            .ToArray();

        var dayNum = types.Length;
        if (args.Length >= 1) dayNum = int.Parse(args[0]);

        var dayClass = types[dayNum - 1];
        Console.WriteLine($"=={dayClass.Name}==");
        var day = Activator.CreateInstance(dayClass) as Day;
        if (day == null) throw new Exception("not a Day");

        var example = File.ReadAllText($"{dayNum}.example.txt");
        var input = File.ReadAllText($"{dayNum}.input.txt");
        Console.WriteLine("Part 1:");
        Console.WriteLine("Example: " + day.Part1(example));
        Console.WriteLine("Actual: " + day.Part1(input));
        Console.WriteLine("Part 2:");
        Console.WriteLine("Example: " + day.Part2(example));
        Console.WriteLine("Actual: " + day.Part2(input));
    }
}

internal interface Day
{
    string Part1(string input);
    string Part2(string input);
}