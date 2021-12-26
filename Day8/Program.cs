var inputs = LoadInputs(8, line =>
{
    var sampleAndChallenge = line.Split('|').Select(x => x.Trim()).ToImmutableArray();

    var (samples, challenge) = (ParseWord(sampleAndChallenge[0]), ParseWord(sampleAndChallenge[1]));

    static ImmutableArray<ImmutableArray<char>> ParseWord(string text) =>
        text.Split(' ')
            .Select(letter => letter.ToUpper().ToImmutableArray())
            .ToImmutableArray();

    return (samples, challenge);
});

var digitSpecs = ImmutableArray.Create(
    ImmutableArray.Create('a', 'b', 'c', 'e', 'f', 'g'),
    ImmutableArray.Create('c', 'f'),
    ImmutableArray.Create('a', 'c', 'd', 'e', 'g'),
    ImmutableArray.Create('a', 'c', 'd', 'f', 'g'),
    ImmutableArray.Create('b', 'c', 'd', 'f'),
    ImmutableArray.Create('a', 'b', 'd', 'f', 'g'),
    ImmutableArray.Create('a', 'b', 'd', 'e', 'f', 'g'),
    ImmutableArray.Create('a', 'c', 'f'),
    ImmutableArray.Create('a', 'b', 'c', 'd', 'e', 'f', 'g'),
    ImmutableArray.Create('a', 'b', 'c', 'd', 'f', 'g')
).Select(x => new DigitSpec(x))
    .ToImmutableArray();

var uniqueSegmentCounts = digitSpecs
                                                .GroupBy(x => x.SegmentCount)
                                                .Where(x => x.Count() == 1)
                                                .Select(x=>x.Key)
                                                .ToImmutableArray();

var challengeDigits = inputs.SelectMany(input => input.challenge).ToImmutableArray();

var part1 = uniqueSegmentCounts.SelectMany(count => challengeDigits.Where(digit => digit.Length == count)).Count();

PrintAnswer(1, part1);

internal record DigitSpec(ImmutableArray<char> Segments)
{
    public int SegmentCount => Segments.Length;
}