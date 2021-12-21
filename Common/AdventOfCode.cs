using System.Reflection;

namespace Common;

public static class AdventOfCode
{
    private static readonly string AssemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

    private static readonly string InputsFolder = Path.Combine(AssemblyFolder, "Inputs");
    
    public static ImmutableArray<int> LoadIntInputs(int day) => 
        LoadInputs(day, int.Parse);
    
    public static ImmutableArray<T> LoadInputs<T>(int day, Func<string, T> inputSelector) => 
        LoadInputRows(day)
            .Select(inputSelector)
            .ToImmutableArray();

    private static ImmutableArray<string> LoadInputRows(int day)
        => File.ReadAllLines(Path.Combine(InputsFolder, $"Day{day}.txt")).ToImmutableArray();

    public static ImmutableArray<ImmutableArray<string>> LoadInputBlocks(int day)
    {
        var blocksBuilder = ImmutableArray.CreateBuilder<ImmutableArray<string>>();

        var blockBuilder = ImmutableArray.CreateBuilder<string>();
        
        foreach (var line in LoadInputRows(day))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                blocksBuilder.Add(blockBuilder.ToImmutable());
                blockBuilder.Clear();
            }
            else
            {
                blockBuilder.Add(line);
            }
        }

        if (blockBuilder.Any())
        {
            blocksBuilder.Add(blockBuilder.ToImmutable());
            blockBuilder.Clear();
        }

        return blocksBuilder.ToImmutable();
    }

    public static void PrintAnswer(int part, object obj)
    {
        Console.WriteLine($"Part {part} answer is:");
        Console.Write(obj);
        Console.ReadLine();
    }
    
}