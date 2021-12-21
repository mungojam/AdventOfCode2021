var inputs = LoadInputs(5, line =>
{
    var pointStrings = line.Split(" -> ");

    var point1 = Point.Parse(pointStrings[0]);
    var point2 = Point.Parse(pointStrings[1]);

    return new Line(point1, point2);
});

var allPoints = inputs.SelectMany(line => line.AllPointsFromSimpleLines).ToImmutableArray();

var pointHits = allPoints
    .GroupBy(x => x)
    .Select(x => (point: x.Key, hitCount: x.Count()))
    .ToImmutableArray();

var dangerousPoints = pointHits.Where(x => x.hitCount >= 2).ToImmutableArray();

var answer1 = dangerousPoints.Length;

PrintAnswer(1, answer1);

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
    public ImmutableArray<Point> AllPointsFromSimpleLines => (Point1, Point2) switch
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