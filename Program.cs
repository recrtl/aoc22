using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var num = args[0];

        var className = $"Day{num}";
        var assembly = Assembly.GetExecutingAssembly();
        var type = assembly.GetTypes().First(t => t.Name == className);
        var day = Activator.CreateInstance(type) as Day;
        if (day == null) throw new Exception("not a Day");

        var example = File.ReadAllText($"{num}.example.txt");
        var input = File.ReadAllText($"{num}.input.txt");
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