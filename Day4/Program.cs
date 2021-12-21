var inputBlocks = LoadInputBlocks(4);

var called = inputBlocks
    .First().Single().Split(',')
    .Select((s, i) => (number: int.Parse(s), order: i))
    .ToImmutableArray();

// Use this to find when each number was called so we can flag all called numbers and their orders
// as we create the grids. No need to simulate the real bingo process.
// Can't be certain this is actually quicker..
var calledLookup = called
    .ToImmutableDictionary(x => x.number, x => x.order);

ImmutableArray<ImmutableArray<int>> ParseGrid(ImmutableArray<string> lines) =>
    lines.Select(ParseGridRow).ToImmutableArray();

ImmutableArray<int> ParseGridRow(string line) =>
    line.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse).ToImmutableArray();

var boards = inputBlocks.Skip(1).Select(ParseGrid).ToImmutableArray();

var numCols = boards.First().First().Length;

var allCols = Enumerable.Range(0, numCols).ToImmutableArray();

var boardsWithOrder = boards.Select(
    AddOrderToBoard
).ToImmutableArray();

ImmutableArray<ImmutableArray<(int number, int order)>> AddOrderToBoard(
    ImmutableArray<ImmutableArray<int>> board) =>
    board.Select(boardRow =>
        boardRow.Select(cell => (number: cell, order: calledLookup.GetValueOrDefault(cell, int.MaxValue)))
            .ToImmutableArray()).ToImmutableArray();

var boardResults = boardsWithOrder
    .Select(board => (board, result: GetBestBoardResult(board)))
    .ToImmutableArray();

var (winningBoard, bestScore) = boardResults
    .MinBy(boardResult => boardResult.result);

var (lastWinningBoard, worstScore) = boardResults
    .Where(x => x.result < int.MaxValue)
    .MaxBy(x => x.result);

var answer1 = BoardScore(winningBoard, bestScore);
var answer2 = BoardScore(lastWinningBoard, worstScore);

PrintAnswer(1, answer1);
PrintAnswer(2, answer2);


int GetBestBoardResult(ImmutableArray<ImmutableArray<(int number, int order)>> boardWithOrder)
{
    var bestRowResult = boardWithOrder.Min(boardRow => boardRow.Max(x => x.order));
    var bestColResult = allCols.Min(boardColNumber => boardWithOrder.Max(x => x[boardColNumber].order));

    var best = Math.Min(bestRowResult, bestColResult);

    return best;
}

int BoardScore(ImmutableArray<ImmutableArray<(int number, int order)>> board, int boardResult)
{
    var sumUnmarked = board
        .SelectMany(x => x)
        .Where(x => x.order > boardResult)
        .Sum(x => x.number);

    var winningNumber = called[boardResult].number;

    return sumUnmarked * winningNumber;
}