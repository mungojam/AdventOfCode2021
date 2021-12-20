var input = LoadInputs(3,
    line =>
        line.Select(
            x => x == '1'
        ).ToBitArray()
);

var numBits = input.First().Length;

var allBits = Range(0, numBits)
                                       .ToImmutableArray();


PrintAnswer(1, Part1());
PrintAnswer(2, Part2());

int Part1()
{
    

    var gammaBits = allBits
        .Select(bitNum => IsMostlyTrue(bitNum, input))
        .ToBitArray();

    var epsilonBits = gammaBits.Duplicate().Not();
    
    var result = gammaBits.ToInteger() * epsilonBits.ToInteger();

    return result;
}

bool IsMostlyTrue(int bitNum, ImmutableArray<BitArray> samples)
{
    var halfSamples = samples.Length / 2.0;
    return samples.Count(sample => sample[bitNum]) >= halfSamples;
}

bool IsMostly(bool value, int bitNum, ImmutableArray<BitArray> samples)
{
    return IsMostlyTrue(bitNum, samples) == value;
}

int Part2()
{
    var oxygen = DerivedCalc(true);
    var co2 = DerivedCalc(false);

    return oxygen * co2;

    int DerivedCalc(bool lookFor)
    {
        var singleSampleNumber = allBits.Aggregate(
            input, 
            (remainingSamples, bitNum) =>
            {
                if (remainingSamples.Length == 1)
                {
                    return remainingSamples;
                }
                
                var target = IsMostly(lookFor, bitNum, remainingSamples);
                
                return remainingSamples
                    .Where(sample => sample[bitNum] == target)
                    .ToImmutableArray();
                
            }).Single();

        var answer = singleSampleNumber.ToInteger();
        
        return answer;
    }
}

internal static class Extensions
{
    public static BitArray Duplicate(this BitArray array) => new(array);
    
    public static BitArray ToBitArray(this IEnumerable<bool> array) => new(array.ToArray());

    public static int ToInteger(this BitArray bits)
    {
        var array = new int[1];
        bits.Cast<bool>().Reverse().ToBitArray().CopyTo(array, 0);
        return array[0];
    }
}