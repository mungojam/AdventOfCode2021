var horizontalPositions = LoadInputs(
    7, x => x.Split(',').Select(int.Parse).ToImmutableArray()
).Single().OrderBy(x => x).ToImmutableArray();

var count = horizontalPositions.Length;

//get the median for the minimum absolute distance, explained well here:
//https://math.stackexchange.com/a/113336
var idealHorizontalPosition = horizontalPositions[count / 2];

var fuel = horizontalPositions.Sum(pos => Abs(pos - idealHorizontalPosition));

PrintAnswer(1, fuel);

// Part 2 does not appear to be so simple as the
// fuel is now based on a complex formulae: 0.5(distance^2 + abs(distance))
// so we'll just search for it starting between the mean and the median and assume that we won't hit 
// a local minimum

var part2 = FindMostEfficientPoint();

PrintAnswer(2, part2);

(int currentGuess, double fuel) FindMostEfficientPoint()
{
    var currentGuess = (int)Round((idealHorizontalPosition + horizontalPositions.Average()) / 2);
    var fuel = CalculateTotalFuel(currentGuess);
    var nextJump = 1;

    while (true)
    {
        var alteredGuess1 = currentGuess + nextJump;
        var alteredFuel1 = CalculateTotalFuel(alteredGuess1);

        if (alteredFuel1 < fuel)
        {
            currentGuess = alteredGuess1;
            fuel = alteredFuel1;
            continue;
        }

        var alteredGuess2 = currentGuess - nextJump;
        var alteredFuel2 = CalculateTotalFuel(alteredGuess2);

        if (alteredFuel2 < fuel)
        {
            currentGuess = alteredGuess2;
            fuel = alteredFuel2;
            nextJump *= -1;
            continue;
        }

        return (currentGuess, fuel);
    }
}

double CalculateTotalFuel(int testPosition)
{
    return 0.5 *
           horizontalPositions.Sum(pos => Pow(pos - testPosition, 2) + Abs(pos - testPosition));
}