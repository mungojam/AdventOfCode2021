using System.Collections;

var input = LoadInputs(3,
    line =>
        line.Select(
            x => x == '1'
        ).ToBitArray()
);

var numBits = input.First().Length;

PrintAnswer(1, Part1());

int Part1()
{
    var halfSamples = input.Length / 2.0;

    var gammaBits = Enumerable.Range(0, numBits)
        .Select(bitNum => input.Count(sample => sample[bitNum]) >= halfSamples)
        .ToBitArray();

    var epsilonBits = gammaBits.Duplicate().Not();

    var result = gammaBits.ToInteger() * epsilonBits.ToInteger();

    return result;
}

internal static class Extensions
{
    public static BitArray Duplicate(this BitArray array) => new BitArray(array);
    
    public static BitArray ToBitArray(this IEnumerable<bool> array) => new(array.ToArray());

    public static int ToInteger(this BitArray bits)
    {
        var array = new int[1];
        bits.Cast<bool>().Reverse().ToBitArray().CopyTo(array, 0);
        return array[0];
    }
}