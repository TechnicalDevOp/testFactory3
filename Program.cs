using PumpTestFactory.Models;
using PumpTestFactory.Services;

namespace PumpTestFactory;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Industrial Pump Test Factory ===");
        Console.WriteLine();

        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PumpTestFactory <pump_type> <operation>");
            Console.WriteLine("Example: PumpTestFactory pump opened");
            Console.WriteLine();
            Console.WriteLine("Available pump types: pump, compressor, valve");
            Console.WriteLine("Available operations: opened, close, start, stop, test");
            return;
        }

        string pumpType = args[0].ToLower();
        string operation = args[1].ToLower();

        Console.WriteLine($"Initializing {pumpType} for operation: {operation}");
        Console.WriteLine();

        var testRunner = new PumpTestRunner();
        testRunner.RunTests(pumpType, operation);
    }
}