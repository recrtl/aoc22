namespace aoc22;

public class Day6 : Day
{
    public string Part1(string rawInput)
    {
        var index = GetFirstDistinctSequenceIndex(rawInput, 4);
        return (index + 1).ToString();
    }

    public string Part2(string rawInput)
    {
        var index = GetFirstDistinctSequenceIndex(rawInput, 14);
        return (index + 1).ToString();
    }

    private int GetFirstDistinctSequenceIndex(string input, int length)
    {
        for (var i = length - 1; i < input.Length; i++)
        {
            var disinctCount = input.Skip(i + 1 - length).Take(length).Distinct().Count();
            if (disinctCount == length) return i;
        }

        throw new Exception();
    }
}