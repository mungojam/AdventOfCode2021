

using Common;

var input = AdventOfCode.LoadInputs(1);

var answer = input.Zip(input.Skip(1)).Count(x => x.Second > x.First);

AdventOfCode.PrintAnswer(answer);