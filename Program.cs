using MemoryLeak;
using System.Runtime;

Dictionary<string, string> dict = new Dictionary<string, string>();

GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;

Console.WriteLine("Starting!");
Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false).ToSize(MemoryConverter.SizeUnits.GB));
Console.WriteLine("Check memory.");
Console.ReadLine();

CreateGarbage(10_000_000);
ClearGarbage();
CreateGarbage(30_000_000);
ClearGarbage();
CreateGarbage(5_000_000);
ClearGarbage();

void CreateGarbage(int amount)
{
    for (int i = 0; i < amount; i++)
    {
        dict.Add($"Key{i}", $"Test{i}");
    }

    Console.WriteLine($"Finished loading {amount.ToString("n0")} into memory.");
    Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false).ToSize(MemoryConverter.SizeUnits.GB));
    Console.WriteLine("Check memory.");
    Console.ReadLine();
}

void ClearGarbage()
{
    Console.WriteLine($"Max generation: {GC.MaxGeneration}");
    dict.Clear();
    GC.Collect();
    GC.WaitForPendingFinalizers();
    
    Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false).ToSize(MemoryConverter.SizeUnits.GB));
    Console.WriteLine("Check memory.");
    Console.ReadLine();
}
