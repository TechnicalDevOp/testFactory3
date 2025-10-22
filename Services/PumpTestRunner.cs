using PumpTestFactory.Models;

namespace PumpTestFactory.Services;

public class PumpTestRunner
{
    private readonly Random _random = new();
    private readonly Dictionary<string, PumpConfiguration> _configurations;

    public PumpTestRunner()
    {
        _configurations = InitializeConfigurations();
    }

    public void RunTests(string pumpType, string operation)
    {
        var config = GetConfiguration(pumpType, operation);
        if (config == null)
        {
            Console.WriteLine($"❌ Unsupported pump type '{pumpType}' or operation '{operation}'");
            return;
        }

        Console.WriteLine($"📋 Running tests for {config.Type} - {config.Operation} operation");
        Console.WriteLine($"⚙️  Max Pressure: {config.MaxPressure} PSI | Max Flow: {config.MaxFlow} GPM | Max Temp: {config.MaxTemperature}°F");
        Console.WriteLine();

        var tests = GenerateTestsForOperation(config);
        var results = new List<PumpTestResult>();

        foreach (var test in tests)
        {
            Console.Write($"🔧 Running {test.TestName}... ");
            
            // Simulate test execution time
            Thread.Sleep(_random.Next(500, 1500));
            
            var result = ExecuteTest(test, config);
            results.Add(result);

            var statusIcon = result.Passed ? "✅" : "❌";
            Console.WriteLine($"{statusIcon} {result.Status}");
            
            if (!string.IsNullOrEmpty(result.Notes))
            {
                Console.WriteLine($"   📝 {result.Notes}");
            }
        }

        DisplayTestSummary(results);
    }

    private Dictionary<string, PumpConfiguration> InitializeConfigurations()
    {
        return new Dictionary<string, PumpConfiguration>
        {
            ["pump_opened"] = new() { Type = "Centrifugal Pump", Operation = "Opened", MaxPressure = 150, MaxFlow = 500, MaxTemperature = 180, TestDurationSeconds = 30 },
            ["pump_close"] = new() { Type = "Centrifugal Pump", Operation = "Close", MaxPressure = 150, MaxFlow = 500, MaxTemperature = 180, TestDurationSeconds = 15 },
            ["pump_start"] = new() { Type = "Centrifugal Pump", Operation = "Start", MaxPressure = 150, MaxFlow = 500, MaxTemperature = 180, TestDurationSeconds = 45 },
            ["pump_stop"] = new() { Type = "Centrifugal Pump", Operation = "Stop", MaxPressure = 150, MaxFlow = 500, MaxTemperature = 180, TestDurationSeconds = 20 },
            ["pump_test"] = new() { Type = "Centrifugal Pump", Operation = "Test", MaxPressure = 150, MaxFlow = 500, MaxTemperature = 180, TestDurationSeconds = 60 },
            
            ["compressor_opened"] = new() { Type = "Rotary Compressor", Operation = "Opened", MaxPressure = 300, MaxFlow = 200, MaxTemperature = 220, TestDurationSeconds = 25 },
            ["compressor_close"] = new() { Type = "Rotary Compressor", Operation = "Close", MaxPressure = 300, MaxFlow = 200, MaxTemperature = 220, TestDurationSeconds = 20 },
            ["compressor_start"] = new() { Type = "Rotary Compressor", Operation = "Start", MaxPressure = 300, MaxFlow = 200, MaxTemperature = 220, TestDurationSeconds = 50 },
            ["compressor_stop"] = new() { Type = "Rotary Compressor", Operation = "Stop", MaxPressure = 300, MaxFlow = 200, MaxTemperature = 220, TestDurationSeconds = 25 },
            ["compressor_test"] = new() { Type = "Rotary Compressor", Operation = "Test", MaxPressure = 300, MaxFlow = 200, MaxTemperature = 220, TestDurationSeconds = 90 },
            
            ["valve_opened"] = new() { Type = "Ball Valve", Operation = "Opened", MaxPressure = 600, MaxFlow = 1000, MaxTemperature = 400, TestDurationSeconds = 10 },
            ["valve_close"] = new() { Type = "Ball Valve", Operation = "Close", MaxPressure = 600, MaxFlow = 1000, MaxTemperature = 400, TestDurationSeconds = 10 },
            ["valve_start"] = new() { Type = "Ball Valve", Operation = "Start", MaxPressure = 600, MaxFlow = 1000, MaxTemperature = 400, TestDurationSeconds = 15 },
            ["valve_stop"] = new() { Type = "Ball Valve", Operation = "Stop", MaxPressure = 600, MaxFlow = 1000, MaxTemperature = 400, TestDurationSeconds = 15 },
            ["valve_test"] = new() { Type = "Ball Valve", Operation = "Test", MaxPressure = 600, MaxFlow = 1000, MaxTemperature = 400, TestDurationSeconds = 30 }
        };
    }

    private PumpConfiguration? GetConfiguration(string pumpType, string operation)
    {
        var key = $"{pumpType}_{operation}";
        return _configurations.TryGetValue(key, out var config) ? config : null;
    }

    private List<PumpTestResult> GenerateTestsForOperation(PumpConfiguration config)
    {
        var baseTests = new List<PumpTestResult>
        {
            new() { TestName = "Pressure Test", Unit = "PSI" },
            new() { TestName = "Flow Rate Test", Unit = "GPM" },
            new() { TestName = "Temperature Test", Unit = "°F" },
            new() { TestName = "Vibration Test", Unit = "mm/s" },
            new() { TestName = "Seal Integrity Test", Unit = "%" }
        };

        // Add operation-specific tests
        switch (config.Operation.ToLower())
        {
            case "opened":
                baseTests.Add(new PumpTestResult { TestName = "Opening Time Test", Unit = "seconds" });
                baseTests.Add(new PumpTestResult { TestName = "Full Open Position Test", Unit = "%" });
                break;
            case "close":
                baseTests.Add(new PumpTestResult { TestName = "Closing Time Test", Unit = "seconds" });
                baseTests.Add(new PumpTestResult { TestName = "Leak Test (Closed)", Unit = "ml/min" });
                break;
            case "start":
                baseTests.Add(new PumpTestResult { TestName = "Startup Time Test", Unit = "seconds" });
                baseTests.Add(new PumpTestResult { TestName = "Motor Current Test", Unit = "Amps" });
                break;
            case "stop":
                baseTests.Add(new PumpTestResult { TestName = "Shutdown Time Test", Unit = "seconds" });
                baseTests.Add(new PumpTestResult { TestName = "Coast Down Test", Unit = "seconds" });
                break;
            case "test":
                baseTests.Add(new PumpTestResult { TestName = "Efficiency Test", Unit = "%" });
                baseTests.Add(new PumpTestResult { TestName = "Power Consumption Test", Unit = "kW" });
                baseTests.Add(new PumpTestResult { TestName = "Noise Level Test", Unit = "dB" });
                break;
        }

        return baseTests;
    }

    private PumpTestResult ExecuteTest(PumpTestResult test, PumpConfiguration config)
    {
        var startTime = DateTime.Now;
        
        // Simulate different test scenarios with realistic values
        var passRate = 0.85; // 85% pass rate for realistic simulation
        var passed = _random.NextDouble() > (1 - passRate);

        test.Passed = passed;
        test.Status = passed ? "PASSED" : "FAILED";

        // Generate realistic test values based on test type
        switch (test.TestName.ToLower())
        {
            case "pressure test":
                test.Value = _random.NextDouble() * config.MaxPressure * (passed ? 0.8 : 1.1);
                test.Notes = passed ? $"Within normal range (< {config.MaxPressure} PSI)" : $"Exceeds maximum pressure limit!";
                break;
                
            case "flow rate test":
                test.Value = _random.NextDouble() * config.MaxFlow * (passed ? 0.9 : 0.5);
                test.Notes = passed ? $"Flow rate nominal" : $"Flow rate below minimum threshold";
                break;
                
            case "temperature test":
                test.Value = _random.NextDouble() * config.MaxTemperature * (passed ? 0.7 : 1.2);
                test.Notes = passed ? $"Operating temperature normal" : $"Temperature exceeds safe limits!";
                break;
                
            case "vibration test":
                test.Value = _random.NextDouble() * 10 * (passed ? 0.5 : 2);
                test.Notes = passed ? $"Vibration within acceptable limits" : $"Excessive vibration detected";
                break;
                
            case "seal integrity test":
                test.Value = _random.NextDouble() * 100;
                test.Passed = test.Value > 95;
                test.Status = test.Passed ? "PASSED" : "FAILED";
                test.Notes = test.Passed ? $"Seal integrity excellent" : $"Potential seal leakage detected";
                break;
                
            default:
                // Generic test values for operation-specific tests
                test.Value = _random.NextDouble() * 100;
                if (!passed)
                {
                    test.Notes = $"Test failed - requires maintenance attention";
                }
                break;
        }

        test.Duration = DateTime.Now - startTime;
        test.Value = Math.Round(test.Value, 2);

        return test;
    }

    private void DisplayTestSummary(List<PumpTestResult> results)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 60));
        Console.WriteLine("🔍 TEST SUMMARY REPORT");
        Console.WriteLine(new string('=', 60));

        var passed = results.Count(r => r.Passed);
        var failed = results.Count - passed;
        var passRate = (double)passed / results.Count * 100;

        Console.WriteLine($"📊 Total Tests: {results.Count}");
        Console.WriteLine($"✅ Passed: {passed}");
        Console.WriteLine($"❌ Failed: {failed}");
        Console.WriteLine($"📈 Pass Rate: {passRate:F1}%");
        Console.WriteLine();

        Console.WriteLine("📋 DETAILED RESULTS:");
        Console.WriteLine(new string('-', 60));

        foreach (var result in results)
        {
            var icon = result.Passed ? "✅" : "❌";
            Console.WriteLine($"{icon} {result.TestName,-25} | {result.Value,8:F2} {result.Unit,-8} | {result.Status}");
        }

        Console.WriteLine(new string('-', 60));
        
        if (failed > 0)
        {
            Console.WriteLine("⚠️  WARNING: Some tests failed. Equipment may require maintenance.");
        }
        else
        {
            Console.WriteLine("🎉 All tests passed! Equipment is operating within normal parameters.");
        }

        var totalDuration = results.Sum(r => r.Duration.TotalMilliseconds);
        Console.WriteLine($"⏱️  Total test duration: {totalDuration:F0}ms");
    }
}