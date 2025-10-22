# Industrial Pump Test Factory

A C# console application that simulates industrial pump testing scenarios. The application takes command-line parameters to specify the pump type and operation, then runs a series of dummy tests to simulate real-world industrial pump testing.

## Features

- **Multiple Pump Types**: Supports pumps, compressors, and valves
- **Various Operations**: Open, close, start, stop, and comprehensive test operations
- **Realistic Test Simulation**: Includes pressure, flow rate, temperature, vibration, and seal integrity tests
- **Operation-Specific Tests**: Additional tests based on the requested operation
- **Detailed Reporting**: Comprehensive test results with pass/fail status and recommendations
- **Console-Based**: No APIs or endpoints - pure console application

## Usage

```bash
dotnet run <pump_type> <operation>
```

### Examples

```bash
# Test opening a pump
dotnet run pump open

# Test starting a compressor
dotnet run compressor start

# Run comprehensive tests on a valve
dotnet run valve test

# Test closing a pump
dotnet run pump close
```

### Available Parameters

**Pump Types:**
- `pump` - Centrifugal Pump
- `compressor` - Rotary Compressor  
- `valve` - Ball Valve

**Operations:**
- `open` - Test opening operation
- `close` - Test closing operation
- `start` - Test startup operation
- `stop` - Test shutdown operation
- `test` - Run comprehensive test suite

## Test Types

The application runs various tests depending on the pump type and operation:

### Base Tests (All Operations)
- **Pressure Test**: Measures operating pressure
- **Flow Rate Test**: Measures fluid flow rate
- **Temperature Test**: Monitors operating temperature
- **Vibration Test**: Detects mechanical vibrations
- **Seal Integrity Test**: Checks for leaks

### Operation-Specific Tests

**Open Operation:**
- Opening Time Test
- Full Open Position Test

**Close Operation:**
- Closing Time Test
- Leak Test (Closed)

**Start Operation:**
- Startup Time Test
- Motor Current Test

**Stop Operation:**
- Shutdown Time Test
- Coast Down Test

**Test Operation:**
- Efficiency Test
- Power Consumption Test
- Noise Level Test

## Sample Output

```
=== Industrial Pump Test Factory ===

Initializing pump for operation: open

📋 Running tests for Centrifugal Pump - Open operation
⚙️  Max Pressure: 150 PSI | Max Flow: 500 GPM | Max Temp: 180°F

🔧 Running Pressure Test... ✅ PASSED
   📝 Within normal range (< 150 PSI)
🔧 Running Flow Rate Test... ✅ PASSED
   📝 Flow rate nominal
🔧 Running Temperature Test... ❌ FAILED
   📝 Temperature exceeds safe limits!
🔧 Running Vibration Test... ✅ PASSED
   📝 Vibration within acceptable limits
🔧 Running Seal Integrity Test... ✅ PASSED
   📝 Seal integrity excellent
🔧 Running Opening Time Test... ✅ PASSED
🔧 Running Full Open Position Test... ✅ PASSED

============================================================
🔍 TEST SUMMARY REPORT
============================================================
📊 Total Tests: 7
✅ Passed: 6
❌ Failed: 1
📈 Pass Rate: 85.7%

📋 DETAILED RESULTS:
------------------------------------------------------------
✅ Pressure Test          |    89.23 PSI      | PASSED
✅ Flow Rate Test         |   342.18 GPM      | PASSED
❌ Temperature Test       |   198.45 °F       | FAILED
✅ Vibration Test         |     3.21 mm/s     | PASSED
✅ Seal Integrity Test    |    97.84 %        | PASSED
✅ Opening Time Test      |    45.67 seconds  | PASSED
✅ Full Open Position Test|    89.23 %        | PASSED
------------------------------------------------------------
⚠️  WARNING: Some tests failed. Equipment may require maintenance.
⏱️  Total test duration: 6247ms
```

## Building and Running

### Native .NET
1. **Prerequisites**: .NET 8.0 SDK
2. **Build**: `dotnet build`
3. **Run**: `dotnet run <pump_type> <operation>`

### Docker
1. **Prerequisites**: Docker installed
2. **Build and Run**: `./docker-build-and-run.sh`
3. **Manual Docker Commands**:
   ```bash
   # Build the image
   docker build -t pump-test-factory .
   
   # Run examples
   docker run --rm pump-test-factory pump open
   docker run --rm pump-test-factory compressor start
   docker run --rm pump-test-factory valve test
   ```

### Docker Compose
```bash
# Build and run with default command (pump open)
docker-compose up

# Run with custom parameters
docker-compose run pump-test-factory compressor start
docker-compose run pump-test-factory valve test
```

## Project Structure

```
PumpTestFactory/
├── Models/
│   └── PumpModels.cs          # Data models and enums
├── Services/
│   └── PumpTestRunner.cs      # Main test execution logic
├── Program.cs                 # Entry point and command-line handling
├── PumpTestFactory.csproj     # Project configuration
└── README.md                  # This file
```

## Technical Details

- **Framework**: .NET 8.0
- **Language**: C# 12
- **Architecture**: Console application with service layer
- **Testing**: Simulated with randomized results and realistic failure rates
- **No External Dependencies**: Pure .NET implementation

The application simulates realistic industrial testing scenarios with:
- 85% average pass rate for tests
- Realistic value ranges for different pump types
- Operation-specific test variations
- Detailed failure analysis and recommendations