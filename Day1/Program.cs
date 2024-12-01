using Xapier14.AdventOfCode;

AdventOfCode.SetYearAndDay(2024, 1);

var sample = """
             3   4
             4   3
             2   5
             1   3
             3   9
             3   3
             """.Split("\r\n");
var output1 = 11;
var output2 = 31;

var input = AdventOfCode.GetInputLines();

Utility.Assert(Part1, sample, output1);
Utility.Assert(Part2, sample, output2);

var part1 = Part1(input);
var part2 = Part2(input);
AdventOfCode.SubmitPart1(part1);
AdventOfCode.SubmitPart2(part2);
return;

(int[] list1, int[] list2) ParseLists(string[] input)
{
    var list1 = new int[input.Length];
    var list2 = new int[input.Length];

    for (var i = 0; i < input.Length; ++i)
    {
        var values = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim()).ToArray();
        list1[i] = int.Parse(values[0]);
        list2[i] = int.Parse(values[1]);
    }

    return (list1, list2);
}

int Part1(string[] input)
{
    var (list1, list2) = ParseLists(input);

    Array.Sort(list1);
    Array.Sort(list2);

    return list1.Select((t, i) => Math.Abs(t - list2[i])).Sum();
}

int Part2(string[] input)
{
    var (list1, list2) = ParseLists(input);
    var freqMap = new Dictionary<int, int>();

    foreach (var i in list2)
    {
        freqMap.Remove(i, out var freq);
        freqMap.Add(i, freq + 1);
    }

    var similarityScore = 0;
    foreach (var i in list1)
    {
        freqMap.TryGetValue(i, out var freq);
        similarityScore += i * freq;
    }

    return similarityScore;
}