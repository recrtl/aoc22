namespace aoc22;

public class Day8 : Day
{
    public string Part1(string rawInput)
    {
        var forest = ParseInput(rawInput);
        return forest.VisibleTrees().Cast<bool>().Count(x => x).ToString();
    }

    public string Part2(string rawInput)
    {
        var forest = ParseInput(rawInput);
        return forest.GetMaxVisibility().ToString();
    }

    private static Forest ParseInput(string input)
    {
        var lines = input.Split("\n");
        var f = new Forest(lines[0].Length, lines.Length);

        for (var r = 0; r < lines.Length; r++)
        {
            var line = lines[r];
            for (var c = 0; c < line.Length; c++) f.Trees[c, r] = int.Parse(line[c].ToString());
        }

        return f;
    }

    public class Forest
    {
        private readonly int Height;
        private readonly int Width;
        public int[,] Trees;

        public Forest(int width, int height)
        {
            Trees = new int[width, height];
            Width = width;
            Height = height;
        }

        public bool[,] VisibleTrees()
        {
            var visibles = new bool[Width, Height];

            for (var r = 0; r < Height; r++)
            {
                var maxHeight = -1;
                for (var c = 0; c < Width; c++)
                {
                    if (Trees[c, r] > maxHeight) visibles[c, r] = true;
                    maxHeight = Math.Max(maxHeight, Trees[c, r]);
                }
            }

            for (var r = 0; r < Height; r++)
            {
                var maxHeight = -1;
                for (var c = Width-1; c >=0; c--)
                {
                    if (Trees[c, r] > maxHeight) visibles[c, r] = true;
                    maxHeight = Math.Max(maxHeight, Trees[c, r]);
                }
            }

            for (var c = 0; c < Width; c++)
            {
                var maxHeight = -1;
                for (var r = 0; r < Height; r++)
                {
                    if (Trees[c, r] > maxHeight) visibles[c, r] = true;
                    maxHeight = Math.Max(maxHeight, Trees[c, r]);
                }
            }

            for (var c = 0; c < Width; c++)
            {
                var maxHeight = -1;
                for (var r = Width-1; r >=0; r--)
                {
                    if (Trees[c, r] > maxHeight) visibles[c, r] = true;
                    maxHeight = Math.Max(maxHeight, Trees[c, r]);
                }
            }

            return visibles;
        }

        public int GetMaxVisibility()
        {
            var max = 0;
            for (var r = 0; r < Height; r++)
            {
                for (var c = 0; c < Width; c++)
                {
                    max = Math.Max(max, GetTreeVisibility(c, r));
                }
            }

            return max;
        }
        
        public int GetTreeVisibility(int c, int r)
        {
            var height = Trees[c, r];
            var viewDistances = new int[4];
            
            for (var i = r-1; i >= 0; i--)
            {
                viewDistances[0]++;
                if (height <= Trees[c, i]) break;
            }
            
            for (var i = r+1; i < Height; i++)
            {
                viewDistances[1]++;
                if (height <= Trees[c, i]) break;
            }
            
            for (var i = c-1; i >= 0; i--)
            {
                viewDistances[2]++;
                if (height <= Trees[i, r]) break;
            }
            
            for (var i = c+1; i < Height; i++)
            {
                viewDistances[3]++;
                if (height <= Trees[i, r]) break;
            }

            return viewDistances[0] * viewDistances[1] * viewDistances[2] * viewDistances[3];
        }
    }
}