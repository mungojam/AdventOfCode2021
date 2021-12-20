using static Common.AdventOfCode;
using static Direction;

var input = LoadInputs(2, line =>
{
    var parts = line.Split(' ');

    Enum.TryParse<Direction>(parts[0], true, out var direction);

    return new CourseCorrection(direction, int.Parse(parts[1]));
});

PrintAnswer(1, Part1());
PrintAnswer(2, Part2());

int Part1()
{
    var initialPosition = new Position();

    var (depth, horizontal) = input.Aggregate(initialPosition, ApplyNaiveCourseCorrection);

    return depth * horizontal;
}

int Part2()
{
    var initialPosition = new PositionAndAim();

    var (depth, horizontal, _) = input.Aggregate(initialPosition, ApplyCourseCorrection);

    return depth * horizontal;
}

static Position ApplyNaiveCourseCorrection(Position position, CourseCorrection courseCorrection)
{
    var (depthChange, horizontalChange, _) = courseCorrection.AsNaiveChange();

    var (depth, horizontal) = position;
    return new Position(depth + depthChange, horizontal + horizontalChange);
}

static PositionAndAim ApplyCourseCorrection(PositionAndAim position, CourseCorrection courseCorrection)
{
    var (direction, quantity) = courseCorrection;
    
    var (depthChange, horizontalChange, aimChange) = direction switch
    {
        Forward => new PositionChange(quantity * position.Aim, quantity, 0),
        Down => new PositionChange(0, 0, quantity),
        Up => new PositionChange(0, 0, -quantity),
        _ => throw new ArgumentOutOfRangeException()
    };

    var (depth, horizontal, aim) = position;
    return new PositionAndAim(depth + depthChange, horizontal + horizontalChange, aim + aimChange);
}

internal record CourseCorrection(Direction Direction, int Quantity)
{
    public PositionChange AsNaiveChange() =>
        Direction switch
        {
            Forward => new PositionChange(0, Quantity, 0),
            Down => new PositionChange(Quantity, 0, 0),
            Up => new PositionChange(-Quantity, 0, 0),
            _ => throw new ArgumentOutOfRangeException()
        };
}



internal record PositionChange(int Depth, int Horizontal, int Aim);

internal record PositionAndAim(int Depth = 0, int Horizontal = 0, int Aim = 0);

internal record Position(int Depth = 0, int Horizontal = 0);


enum Direction
{
    Forward,
    Down,
    Up
}