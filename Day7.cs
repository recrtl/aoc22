namespace aoc22;

public class Day7 : Day
{
    public string Part1(string rawInput)
    {
        var root = ParseInput(rawInput);
        return root.GetAllSubdirectories().Select(x => x.Size()).Where(x => x < 100000).Sum().ToString();
    }

    public string Part2(string rawInput)
    {
        var root = ParseInput(rawInput);
        var needToFree = 30000000 - (70000000 - root.Size());

        return root.GetAllSubdirectories().Select(x => x.Size()).Where(x => x > needToFree).OrderBy(x => x).First().ToString();
    }

    private static Directory ParseInput(string input)
    {
        var root = new Directory(null);

        var currentDir = root;
        var entries = input.Split("$ ");
        foreach (var entry in entries)
        {
            var lines = entry.Split("\n");
            var command = lines[0];

            if (command.StartsWith("cd"))
            {
                var dir = command.Substring(3);
                switch (dir)
                {
                    case "/":
                        currentDir = root;
                        break;
                    case "..":
                        currentDir = currentDir.Parent;
                        break;
                    default:
                        currentDir = currentDir.Dirs[dir];
                        break;
                }

                continue;
            }

            // ls
            foreach (var line in lines.Skip(1))
            {
                if (line == "") continue;

                if (line.StartsWith("dir "))
                {
                    var dirName = line.Replace("dir ", "");
                    currentDir.Dirs.Add(dirName, new Directory(currentDir));
                    continue;
                }

                var parts = line.Split(" ");
                currentDir.Files.Add(parts[1], int.Parse(parts[0]));
            }
        }

        return root;
    }

    public class Directory
    {
        public Dictionary<string, Directory> Dirs;
        public Dictionary<string, int> Files;
        public Directory Parent;

        public Directory(Directory parent)
        {
            Dirs = new Dictionary<string, Directory>();
            Files = new Dictionary<string, int>();
            Parent = parent;
        }

        public IEnumerable<Directory> GetAllSubdirectories()
        {
            return Dirs.Values.Concat(
                Dirs.Values.SelectMany(x => x.GetAllSubdirectories())
            );
        }

        public int Size()
        {
            return Files.Sum(x => x.Value)
                   + Dirs.Values.Sum(x => x.Size());
        }
    }
}