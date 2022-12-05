namespace aoc22;

public class Day5 : Day
{
    public string Part1(string rawInput)
    {
        var input = ParseInput(rawInput);
        input.SolveCrateMover9000();
        var chars = input.Stacks.Select(x => x.Peek()).ToArray();
        return new string(chars);
    }

    public string Part2(string rawInput)
    {
        var input = ParseInput(rawInput);
        input.SolveCrateMover9001();
        var chars = input.Stacks.Select(x => x.Peek()).ToArray();
        return new string(chars);
    }

    private static Input ParseInput(string input)
    {
        var res = new Input();
        var parts = input.Split("\n\n");

        var stacksLines = parts[0].Split("\n");
        var nbStacks = (stacksLines.Last().Length + 1) / 4;
        for (var i = 0; i < nbStacks; i++)
        {
            var letterIndex = i * 4 + 1;
            var stack = new Stack();

            for (var nbLine = stacksLines.Length - 2; nbLine >= 0; nbLine--)
            {
                var letter = stacksLines[nbLine].ToCharArray()[letterIndex];
                if (letter == ' ') break;
                stack.Push(letter);
            }

            res.Stacks.Add(stack);
        }

        foreach (var line in parts[1].Split("\n"))
        {
            var words = line.Split(" ");
            res.Moves.Add(new Move
            {
                Qty = int.Parse(words[1]),
                From = int.Parse(words[3]) - 1,
                To = int.Parse(words[5]) - 1
            });
        }

        return res;
    }

    public class Input
    {
        public List<Move> Moves = new();
        public List<Stack> Stacks = new();

        public void SolveCrateMover9000()
        {
            foreach (var move in Moves)
                for (var i = 0; i < move.Qty; i++)
                {
                    var c = Stacks[move.From].Pop();
                    Stacks[move.To].Push(c);
                }
        }

        public void SolveCrateMover9001()
        {
            foreach (var move in Moves)
            {
                var buf = new List<char>();
                for (var i = 0; i < move.Qty; i++)
                {
                    var c = Stacks[move.From].Pop();
                    buf.Insert(0, c);
                }

                foreach (var c in buf) Stacks[move.To].Push(c);
            }
        }
    }

    public class Stack : Stack<char>
    {
    }

    public class Move
    {
        public int Qty, From, To;
    }
}