var startingAges = LoadInputs(
    6, x => x.Split(',').Select(int.Parse).ToImmutableArray()
).Single();

var possibleAges = Repeat(0L, 9).ToImmutableArray();

var startingAgeBuckets = startingAges.Aggregate(
    possibleAges,
    (bucketedAges, age) => bucketedAges.SetItem(age, bucketedAges[age] + 1)
);

var answer1 = GetFinalState(80).Sum();

PrintAnswer(1, answer1);

var answer2 = GetFinalState(256).Sum();

PrintAnswer(2, answer2);

ImmutableArray<long> GetFinalState(int numDays) =>
    Range(1, numDays)
        .Aggregate(startingAgeBuckets, (ageBuckets, _) => NextDay(ageBuckets));

ImmutableArray<long> NextDay(ImmutableArray<long> ageBuckets)
{
    var childrenCount = ageBuckets[0];

    var shiftedAges = ageBuckets.Skip(1).ToImmutableArray().Add(childrenCount);

    var withParentsBackIn = shiftedAges.SetItem(6, shiftedAges[6] + childrenCount);
    
    return withParentsBackIn;
}