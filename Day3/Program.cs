using System.Text.RegularExpressions;
using Xapier14.AdventOfCode;

AdventOfCode.SetYearAndDay(2024, 3);

var sample1 = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
var sample2 = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
var output1 = 161;
var output2 = 48;

Utility.Assert(Part1, sample1, output1);
Utility.Assert(Part2, sample2, output2);

var input = AdventOfCode.GetInputText();

var part1 = Part1(input);
var part2 = Part2(input);

AdventOfCode.SubmitPart2(part2);
return;

long Part1(string input)
{
    var mulGroups = Regex.Matches(input, @"mul\((\d+),(\d+)\)")
        .Select(match => (Convert.ToInt64(match.Groups[1].Value), Convert.ToInt64(match.Groups[2].Value)))
        .ToArray();
    
    return mulGroups.Sum((mulGroup) => mulGroup.Item1 * mulGroup.Item2);
};


long Part2(string input)
{
    (string Match, long Value1, long Value2)[] instructions = Regex
        .Matches(input, @"(?:mul\((\d+),(\d+)\))|(?:do\(\))|(?:don't\(\))")
        .Select(match => (
            match.Value,
            match.Groups[1].Length > 0
                ? Convert.ToInt64(match.Groups[1].Value)
                : 0,
            match.Groups[2].Length > 0
                ? Convert.ToInt64(match.Groups[2].Value)
                : 0
            )
        )
        .ToArray();

    var product = 0L;
    var enabled = true;
    foreach (var (match, val1, val2) in instructions)
    {
        if (match == "do()")
        {
            enabled = true;
            continue;
        }

        if (match == "don't()")
        {
            enabled = false;
            continue;
        }
        if (enabled)  product += val1 * val2;
    }

    return product;
};