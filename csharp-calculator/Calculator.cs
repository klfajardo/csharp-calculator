namespace EventsAndDelegatesTest;

/// <summary>
/// Represents a simple calculator capable of performing basic arithmetic operations.
/// </summary>
public class Calculator
{
    // Dictionary mapping arithmetic operation symbols to their corresponding delegate implementations.
    public readonly Dictionary<string, Program.Operation> Operations = new() {
        { "+", Add },
        { "-", Subtract },
        { "*", Multiply },
        // I can add additional operations in the future.
    };
    
    // Adds two integers and returns the result
    private static int Add(int num1, int num2) {
        return (num1 + num2);
    }

    // Subtracts the second integer from the first and returns the result
    private static int Subtract(int num1, int num2) {
        return (num1 - num2);
    }

    // Multiplies two integers and returns the result
    private static int Multiply(int num1, int num2) {
        return (num1 * num2);
    }
}