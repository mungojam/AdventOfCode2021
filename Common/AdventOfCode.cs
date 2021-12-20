using System.Collections.Immutable;
using System.Reflection;

namespace Common;

public static class AdventOfCode
{
    private static readonly string AssemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

    private static readonly string InputsFolder = Path.Combine(AssemblyFolder, "Inputs");
    
    public static ImmutableArray<int> LoadIntInputs(int day) => 
        LoadInputs(day, int.Parse);
    
    public static ImmutableArray<T> LoadInputs<T>(int day, Func<string, T> inputSelector) => 
        File.ReadAllLines(Path.Combine(InputsFolder, $"Day{day}.txt"))
            .Select(inputSelector)
            .ToImmutableArray();

    public static void PrintAnswer(int part, object obj)
    {
        Console.WriteLine($"Part {part} answer is:");
        Console.Write(obj);
        Console.ReadLine();
    }
    
}