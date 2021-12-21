var startingAges = LoadInputs(
    6, x=>x.Split(',').Select(int.Parse).ToImmutableArray()
    ).Single();

var answer1 = GetFinalState(80).Length;

PrintAnswer(1, answer1);

ImmutableArray<int> GetFinalState(int numDays) =>
    Range(1, numDays)
        .Aggregate(startingAges, (ages, _) => NextDay(ages));

ImmutableArray<int> NextDay(ImmutableArray<int> ages)
{
    var simpleParents = ages.Select(age => age - 1).ToImmutableArray();
    var childrenCount = simpleParents.Count(x => x == -1);
    var children = Enumerable.Repeat(8, childrenCount);

    var actualParents = simpleParents.Select(age => age == -1 ? 6 : age);

    return actualParents.Concat(children).ToImmutableArray();
}