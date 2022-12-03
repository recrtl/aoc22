namespace aoc22;

public class Day3 : Day
{
    public string Part1(string input)
    {
        var bags = ParseInput(input);
        return bags.Sum(x => x.IntersectionPriority()).ToString();
    }

    public string Part2(string input)
    {
        var bags = ParseInput(input);
        var badges = new List<char>();
        for (var i = 0; i < bags.Count; i += 3)
        {
            var badge = bags[i].Content()
                .Intersect(bags[i + 1].Content())
                .Intersect(bags[i + 2].Content())
                .Single();
            badges.Add(badge);
        }

        return badges.Sum(x => Priority(x)).ToString();
    }

    private static List<Bag> ParseInput(string input)
    {
        var res = new List<Bag>();
        foreach (var line in input.Split("\n"))
            res.Add(new Bag
            {
                Compartment1 = line.Substring(0, line.Length / 2).ToCharArray(),
                Compartment2 = line.Substring(line.Length / 2).ToCharArray()
            });
        return res;
    }

    private static int Priority(char c)
    {
        if (c >= 'a' && c <= 'z') return c - 'a' + 1;
        if (c >= 'A' && c <= 'Z') return c - 'A' + 27;

        throw new ArgumentOutOfRangeException();
    }

    private class Bag
    {
        public char[] Compartment1, Compartment2;

        public IEnumerable<char> Content()
        {
            return Compartment1.Concat(Compartment2);
        }

        public int IntersectionPriority()
        {
            return Compartment1.Intersect(Compartment2).Sum(x => Priority(x));
        }
    }
}