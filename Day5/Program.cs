var inputs = LoadInputs(5, line =>
{
    var pointStrings = line.Split(" -> ");

    var point1 = Point.Parse(pointStrings[0]);
    var point2 = Point.Parse(pointStrings[1]);

    return new Line(point1, point2);
});

var allPointsSimple = inputs.SelectMany(line => line.AllPointsSimple).ToImmutableArray();
var allPoints = inputs.SelectMany(line => line.AllPoints).ToImmutableArray();

var dangerousPoints1 = DangerousPoints(allPointsSimple);
var dangerousPoints2 = DangerousPoints(allPoints);

var answer1 = dangerousPoints1.Length;
var answer2 = dangerousPoints2.Length;

PrintAnswer(1, answer1);
PrintAnswer(2, answer2);

static ImmutableArray<(Point point, int hitCount)> DangerousPoints(ImmutableArray<Point> points)
{
    var pointHits = points
        .GroupBy(x => x)
        .Select(x => (point: x.Key, hitCount: x.Count()))
        .ToImmutableArray();

    var dangerousPoints = pointHits.Where(x => x.hitCount >= 2).ToImmutableArray();
    return dangerousPoints;
}

internal record Point(int X, int Y)
{
    internal static Point Parse(string pointString)
    {
        var values = pointString.Split(',').Select(int.Parse).ToImmutableArray();
        return new Point(values[0], values[1]);
    }
}

internal record Line(Point Point1, Point Point2)
{
    public ImmutableArray<Point> AllPoints => AllPointsSimple switch
    {
        var x when x.Any() => x,
        _ =>
            (Point1, Point2) switch
            {
                var ((x1, y1), (x2, y2)) when Abs(x2 - x1) == Abs(y2 - y1) => Range(0, Abs(y2 - y1) + 1)
                    .Select(offset => new Point(x1 + Sign(x2 - x1) * offset, y1 + Sign(y2 - y1) * offset))
                    .ToImmutableArray(),
                _ => throw new ApplicationException("Didn't expect such a tricky line")
            }
    };


    public ImmutableArray<Point> AllPointsSimple => (Point1, Point2) switch
    {
        var ((x1, y1), (x2, y2)) when x1 == x2 => Range(Min(y1, y2), Abs(y2 - y1) + 1)
            .Select(y => new Point(x1, y))
            .ToImmutableArray(),
        var ((x1, y1), (x2, y2)) when y1 == y2 => Range(Min(x1, x2), Abs(x2 - x1) + 1)
            .Select(x => new Point(x, y1))
            .ToImmutableArray(),
        _ => ImmutableArray<Point>.Empty
    };
}