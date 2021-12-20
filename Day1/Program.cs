using static Common.AdventOfCode;

var input = LoadIntInputs(1);

PrintAnswer(1, Part1());
PrintAnswer(2, Part2());

int Part1() => CountAscending(input);
int Part2()
{
    var rollingSum = 
        input.Zip(input.Skip(1), input.Skip(2))
             .Select(x=>x.First + x.Second + x.Third)
             .ToImmutableArray();

    return CountAscending(rollingSum);
}

int CountAscending(ImmutableArray<int> immutableArray)
{
    return immutableArray.Zip(immutableArray.Skip(1)).Count(x => x.Second > x.First);
}

