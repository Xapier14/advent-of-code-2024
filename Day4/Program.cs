using Xapier14.AdventOfCode;

AdventOfCode.SetYearAndDay(2024, 4);

var sample = """
             MMMSXXMASM
             MSAMXMSMSA
             AMXSXMAAMM
             MSAMASMSMX
             XMASAMXAMM
             XXAMMXXAMA
             SMSMSASXSS
             SAXAMASAAA
             MAMMMXMMMM
             MXMXAXMASX
             """.Split('\n').Select(line => line.ToCharArray()).ToArray();
var output1 = 18;
var output2 = 9;

Utility.Assert(Part1, sample, output1);
Utility.Assert(Part2, sample, output2);

var input = AdventOfCode.GetInputLines().Select((line) => line.ToCharArray()).ToArray();

var part1 = Part1(input);
var part2 = Part2(input);

AdventOfCode.SubmitPart1(part1);
AdventOfCode.SubmitPart2(part2);
return;

int Part1(char[][] grid)
{
    List<(int X, int Y)> startingPoints = [];
    const string pattern = "XMAS";

    for (var y = 0; y < grid.Length; ++y)
    {
        for (var x = 0; x < grid[y].Length; ++x)
        {
            if (grid[y][x] == 'X')
                startingPoints.Add((x, y));
        }
    }

    var count = 0;
    foreach (var startingPoint in startingPoints)
    {
        (int X, int Y)[] scanDirections = [
            (-1, -1),   (0, -1),    (1, -1),
            (-1, 0),    /* Center */    (1, 0),
            (-1, 1),    (0, 1),     (1, 1)
        ];
        foreach (var dir in scanDirections)
        {
            var letterIndex = 1;
            var currentX = startingPoint.X + dir.X;
            var currentY = startingPoint.Y + dir.Y;
            var found = false;
            while (letterIndex < pattern.Length &&
                   currentX >= 0 && currentX< grid[0].Length &&
                   currentY >= 0 && currentY < grid.Length)
            {
                if (grid[currentY][currentX] != pattern[letterIndex])
                    break;
                if (letterIndex == pattern.Length - 1)
                {
                    found = true;
                    break;
                }

                currentX += dir.X;
                currentY += dir.Y;
                letterIndex += 1;
            }

            if (found)
                count += 1;
        }
    }

    return count;
}

bool IsXMas(char[][] grid, (int X, int Y) coord)
{
    if (coord.X <= 0 || coord.X >= grid[0].Length - 1 ||
        coord.Y <= 0 || coord.Y >= grid.Length - 1)
        return false;
    
    (int X, int Y) topLeft = (coord.X - 1, coord.Y - 1);
    (int X, int Y) topRight = (coord.X + 1, coord.Y - 1);
    (int X, int Y) bottomLeft = (coord.X - 1, coord.Y + 1);
    (int X, int Y) bottomRight = (coord.X + 1, coord.Y + 1);

    (char TopLeft, char TopRight, char BottomLeft, char BottomRight)[] patterns =
    [
        ('M', 'S', 'M', 'S'),
        ('S', 'M', 'S', 'M'),
        ('M', 'M', 'S', 'S'),
        ('S', 'S', 'M', 'M'),
    ];
    
    return patterns.Any(pattern =>
        grid[topLeft.Y][topLeft.X] == pattern.TopLeft &&
        grid[topRight.Y][topRight.X] == pattern.TopRight &&
        grid[bottomLeft.Y][bottomLeft.X] == pattern.BottomLeft &&
        grid[bottomRight.Y][bottomRight.X] == pattern.BottomRight);
}

int Part2(char[][] grid)
{
    List<(int X, int Y)> startingPoints = [];

    for (var y = 0; y < grid.Length; ++y)
    {
        for (var x = 0; x < grid[y].Length; ++x)
        {
            if (grid[y][x] == 'A')
                startingPoints.Add((x, y));
        }
    }

    return startingPoints.Count(point => IsXMas(grid, point));
}