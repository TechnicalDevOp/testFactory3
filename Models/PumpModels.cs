namespace PumpTestFactory.Models;

public class PumpTestResult
{
    public string TestName { get; set; } = string.Empty;
    public bool Passed { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public string? Notes { get; set; }
}

public class PumpConfiguration
{
    public string Type { get; set; } = string.Empty;
    public string Operation { get; set; } = string.Empty;
    public double MaxPressure { get; set; }
    public double MaxFlow { get; set; }
    public double MaxTemperature { get; set; }
    public int TestDurationSeconds { get; set; }
}

public enum PumpType
{
    Pump,
    Compressor,
    Valve
}

public enum Operation
{
    Open,
    Close,
    Start,
    Stop,
    Test
}