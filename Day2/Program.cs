using Xapier14.AdventOfCode;

AdventOfCode.SetYearAndDay(2024, 2);

var sample = """
             7 6 4 2 1
             1 2 7 8 9
             9 7 6 2 1
             1 3 2 4 5
             8 6 4 4 1
             1 3 6 7 9
             """.Split('\n').Select(line => line.Trim()).ToArray();
var output1 = 2;
var output2 = 4;

Utility.Assert(Part1, sample, output1);
Utility.Assert(Part2, sample, output2);

var input = AdventOfCode.GetInputLines();
var part1 = Part1(input);
var part2 = Part2(input);
AdventOfCode.SubmitPart1(part1);
AdventOfCode.SubmitPart2(part2);

return;

bool IsSafe(int[] values)
{
    var prev = values[0];
    var increasing = values[1] - values[0] > 0;
    var safe = true;
    for (var i = 1; i < values.Length; ++i)
    {
        var diff = values[i] - prev;
        prev = values[i];
        var isInCorrectDirection = (increasing && diff > 0) || (!increasing && diff < 0);
        var isDifferenceInRange = Math.Abs(diff) is >= 1 and <= 3;
        if (isInCorrectDirection && isDifferenceInRange) continue;
        safe = false;
        break;
    }

    return safe;
}

int[][] Expand(int[] input)
{
    var result = new List<int[]>();
    var buffer = new List<int>();
    for (var i = 0; i < input.Length; ++i)
    {
        buffer.AddRange(input.Where((_, j) => i != j));
        result.Add(buffer.ToArray());
        buffer.Clear();
    }

    return result.ToArray();
}

int Part1(string[] input)
{
    return input.Select(line => line
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray()
    ).Count(IsSafe);
}

int Part2(string[] input)
{
    var good = 0;
    foreach (var line in input)
    {
        var values = line
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
        if (IsSafe(values))
        {
            good += 1;
            continue;
        }
        
        var expandedValues = Expand(values);
        if (expandedValues.Any(IsSafe))
        {
            good += 1;
        }
    }
    return good;
}