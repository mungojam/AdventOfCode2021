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

var bestBoardResult = boardsWithOrder
    .Select(board => (board, result: GetBestBoardResult(board)))
    .MinBy(boardResult => boardResult.result);

var sumUnmarked = bestBoardResult.board
    .SelectMany(x => x)
    .Where(x => x.order > bestBoardResult.result)
    .Sum(x => x.number);

var winningNumber = called[bestBoardResult.result].number;

var answer1 = sumUnmarked * winningNumber;

PrintAnswer(1, answer1);


int GetBestBoardResult(ImmutableArray<ImmutableArray<(int number, int order)>> boardWithOrder)
{
    var bestRowResult = boardWithOrder.Min(boardRow => boardRow.Max(x => x.order));
    var bestColResult = allCols.Min(boardColNumber => boardWithOrder.Max(x => x[boardColNumber].order));

    var best = Math.Min(bestRowResult, bestColResult);

    return best;
}