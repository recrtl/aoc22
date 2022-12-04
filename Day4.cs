namespace aoc22;

public class Day4 : Day
{
    public string Part1(string input)
    {
        var pairs = ParseInput(input);
        return pairs.Count(x => x.FullyOverlap()).ToString();
    }

    public string Part2(string input)
    {
        var pairs = ParseInput(input);
        return pairs.Count(x => x.Overlap()).ToString();
    }

    private static List<Pair> ParseInput(string input)
    {
        var res = new List<Pair>();
        foreach (var line in input.Split("\n"))
        {
            var pairs = line.Split(",");
            var minMax1 = pairs[0].Split("-");
            var minMax2 = pairs[1].Split("-");
            res.Add(new Pair
            {
                A1 = new Assignement
                {
                    Min = int.Parse(minMax1[0]),
                    Max = int.Parse(minMax1[1])
                },
                A2 = new Assignement
                {
                    Min = int.Parse(minMax2[0]),
                    Max = int.Parse(minMax2[1])
                }
            });
        }

        return res;
    }

    private struct Pair
    {
        public Assignement A1, A2;

        public bool FullyOverlap()
        {
            if (A1.Min == A2.Min || A1.Max == A2.Max) return true;

            if (A1.Min < A2.Min)
                return A2.Max < A1.Max;
            return A1.Max < A2.Max;
        }

        public bool Overlap()
        {
            var low = A1;
            var high = A2;
            if (A1.Min > A2.Min)
            {
                low = A2;
                high = A1;
            }

            return high.Min <= low.Max;
        }
    }

    private struct Assignement
    {
        public int Min, Max;
    }
}