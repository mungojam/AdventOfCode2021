using static Common.AdventOfCode;
using static Direction;

var input = LoadInputs(2, line =>
{
    var parts = line.Split(' ');

    Enum.TryParse<Direction>(parts[0], true, out var direction);

    return new CourseCorrection(direction, int.Parse(parts[1]));
});

PrintAnswer(1, Part1());

int Part1()
{
    var initialPosition = new Position();
    
    var finalPosition = input.Aggregate(initialPosition, (position, move) => position + move);

    return finalPosition.Depth * finalPosition.Horizontal;
}



internal record CourseCorrection(Direction Direction, int Quantity)
{
    public PositionChange AsChange() =>
        Direction switch
        {
            Forward => new PositionChange(0, Quantity),
            Down => new PositionChange(Quantity, 0),
            Up => new PositionChange(-Quantity, 0),
            _ => throw new ArgumentOutOfRangeException()
        };
}

internal record PositionChange(int Depth, int Horizontal);

internal record Position(int Depth = 0, int Horizontal = 0)
{
    public static Position operator +(Position position1, CourseCorrection courseCorrection)
    {
        var (depthChange, horizontalChange) = courseCorrection.AsChange();

        return new Position(position1.Depth + depthChange, position1.Horizontal + horizontalChange);
    }
}

enum Direction
{
    Forward,
    Down,
    Up
}