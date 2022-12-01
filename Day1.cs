namespace aoc22;

public class Day1 : Day
{
    public string Part1(string input)
    {
        var elvesCalories = ParseInput(input);
        return elvesCalories.Max().ToString();
    }

    public string Part2(string input)
    {
        var elvesCalories = ParseInput(input);
        return elvesCalories.OrderByDescending(x => x).Take(3).Sum().ToString();
    }

    private static List<int> ParseInput(string input)
    {
        var elvesCalories = new List<int>();

        var elfCalories = 0;
        foreach (var line in input.Split("\n"))
        {
            if (line == "")
            {
                elvesCalories.Add(elfCalories);
                elfCalories = 0;
                continue;
            }

            elfCalories += int.Parse(line);
        }

        elvesCalories.Add(elfCalories);

        return elvesCalories;
    }
}