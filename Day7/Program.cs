var horizontalPositions = LoadInputs(
    7, x => x.Split(',').Select(int.Parse).ToImmutableArray()
).Single().OrderBy(x => x).ToImmutableArray();

var count = horizontalPositions.Length;

//get the median for the minimum absolute distance, explained well here:
//https://math.stackexchange.com/a/113336
var idealHorizontalPosition = horizontalPositions[count / 2];

var fuel = horizontalPositions.Sum(pos => Abs(pos - idealHorizontalPosition));

PrintAnswer(1, fuel);