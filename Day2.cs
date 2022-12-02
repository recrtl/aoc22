namespace aoc22;

public class Day2 : Day
{
    public string Part1(string input)
    {
        var rounds = ParseInput1(input);
        rounds.ForEach(x => x.Solve());
        return rounds.Sum(x => x.Score()).ToString();
    }

    public string Part2(string input)
    {
        var rounds = ParseInput2(input);
        rounds.ForEach(x => x.Solve());
        return rounds.Sum(x => x.Score()).ToString();
    }

    private static Choice GetLoseAgainst(Choice input)
    {
        switch (input)
        {
            case Choice.Rock:
                return Choice.Scissors;
            case Choice.Paper:
                return Choice.Rock;
            case Choice.Scissors:
                return Choice.Paper;
            default:
                throw new ArgumentOutOfRangeException(nameof(input), input, null);
        }
    }

    private static Choice GetWinAgainst(Choice input)
    {
        switch (input)
        {
            case Choice.Rock:
                return Choice.Paper;
            case Choice.Paper:
                return Choice.Scissors;
            case Choice.Scissors:
                return Choice.Rock;
            default:
                throw new ArgumentOutOfRangeException(nameof(input), input, null);
        }
    }

    private static Choice ParseChoice(string v)
    {
        switch (v)
        {
            case "A":
            case "X":
                return Choice.Rock;
            case "B":
            case "Y":
                return Choice.Paper;
            case "C":
            case "Z":
                return Choice.Scissors;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static List<Round> ParseInput1(string input)
    {
        var res = new List<Round>();
        foreach (var line in input.Split("\n"))
        {
            var splits = line.Split(" ");
            res.Add(new Round
            {
                ElfChoice = ParseChoice(splits[0]),
                MyChoice = ParseChoice(splits[1])
            });
        }

        return res;
    }

    private static List<Round> ParseInput2(string input)
    {
        var res = new List<Round>();
        foreach (var line in input.Split("\n"))
        {
            var splits = line.Split(" ");
            res.Add(new Round
            {
                ElfChoice = ParseChoice(splits[0]),
                Result = parseResult(splits[1])
            });
        }

        return res;
    }

    private static Result? parseResult(string v)
    {
        switch (v)
        {
            case "X":
                return Result.Lose;
            case "Y":
                return Result.Draw;
            case "Z":
                return Result.Win;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private enum Choice
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    private enum Result
    {
        Lose = 0,
        Draw = 3,
        Win = 6
    }

    private class Round
    {
        public Choice ElfChoice;
        public Choice? MyChoice;
        public Result? Result;

        public void Solve()
        {
            if (!Result.HasValue)
                Result = GetResult();
            else
                MyChoice = GetMyChoice();
        }


        public int Score()
        {
            if (!MyChoice.HasValue || !Result.HasValue) throw new Exception("call Solve()");

            return (int)MyChoice + (int)Result.Value;
        }

        private Result GetResult()
        {
            if (MyChoice == ElfChoice) return Day2.Result.Draw;

            if (ElfChoice == GetWinAgainst(MyChoice!.Value)) return Day2.Result.Lose;

            return Day2.Result.Win;
        }

        private Choice GetMyChoice()
        {
            switch (Result)
            {
                case Day2.Result.Draw:
                    return ElfChoice;
                case Day2.Result.Lose:
                    return GetLoseAgainst(ElfChoice);
                case Day2.Result.Win:
                    return GetWinAgainst(ElfChoice);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}