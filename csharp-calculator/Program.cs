namespace EventsAndDelegatesTest;
/*
 * Things to improve in the program.
 * Code:
 * 1. DRY (Don't Repeat Yourself)
 * 2. Avoid Hard-Coding
 * 3. Single Responsibility Principle (SRP)
 * 4. Code Readability and Clarity <<<
 * 5. Commenting and Documentation <<<
 * 6. Error Handling and Logging <<<
 * 7. Unit Testing and Test Coverage ?
 * 8. Performance Efficiency < could be interesting...
 * 9. Code Consistency <<<
 */

/// <summary>
/// The main entry class for the calculator application.
/// This class initializes necessary components and handles the main execution loop.
/// </summary>
public static class Program
{
    // Static instances of application components
    private static readonly Calculator Calculator = new();
    public static HistoryManager? HistoryManager;
    public static ConsoleManager? ConsoleManager;
    private static ExpressionEvaluator? _expressionEvaluator;
    
    // Delegate types definitions
    public delegate int Operation(int num1, int num2); // Delegate for defining an operation taking two integers and returning an integer.
    public delegate bool Comparator<T>(string input);  // Delegate for comparing an input string and returning a boolean value.
    private delegate void CalculationCompletedEventHandler(string expression, int result);
    
    // Events - Event triggered when a calculation is completed
    private static event CalculationCompletedEventHandler? CalculationCompleted;
    
    
    // Main entry point of the application //
    // Initializes the application components and starts the main execution loop
    public static void Main(string[] args) 
    { 
        // Initialization of application components
        HistoryManager = new HistoryManager();
        ConsoleManager = new ConsoleManager();
        _expressionEvaluator = new ExpressionEvaluator(Calculator);
       
        InitializeEventSubscriptions();

        // Main application loop
        while (true)
        { 
            // Display instructions and a countdown before performing calculations
            ConsoleManager.PrintInstructionsAndCountdown(14000,1000);
            // Perform calculation after countdown
            PerformCalculation();
        }
        // ReSharper disable once FunctionNeverReturns
    }

    // Initializes event subscriptions for the application.
    private static void InitializeEventSubscriptions()
    {
        // Subscribing methods to the Calculation Completed event
        CalculationCompleted += HistoryManager.AddEntry; 
        CalculationCompleted += ConsoleManager.PrintCalculationResult;
    }

    // Handles the logic for performing a calculation.
    // This includes user input for numbers and operations, and triggering the calculation completed event.
    private static void PerformCalculation()
    {
        // Instantiate the delegate
        Operation operationDelegate;
        
        // Delegates for input validation (with lambda expression)
        Comparator<string> checkNumber = (input) => Int32.TryParse(input, out int result);
        Comparator<string> checkOperation = (input) => Calculator.Operations.ContainsKey(input);
        
        // List for storing elements of the mathematical expression
        List<IExpressionElement> expressionElements = new List<IExpressionElement>();
        
        // Loop for user input
        int numberCount = 1;
        int maxExpressionLength = 2;
        while (numberCount <= maxExpressionLength) // Limit of numbers for expression
        {
            int number = Int32.Parse(ConsoleManager.GetValidInput(checkNumber, "numberPrompt", numberCount, _expressionEvaluator.GetCurrentExpression(expressionElements)));
            expressionElements.Add(new NumberElement(number));
            if (numberCount == maxExpressionLength) break;
            
            string operation = ConsoleManager.GetValidInput(checkOperation, "operationPrompt", numberCount, _expressionEvaluator.GetCurrentExpression(expressionElements));
            if (operation == "enter" || numberCount == 10) break; // Break the loop to perform the calculation
            expressionElements.Add(new OperatorElement(operation));
            
            numberCount++;
        }
        // Store and evaluate the final expression
        string finalExpression = _expressionEvaluator.GetCurrentExpression(expressionElements);
        int expressionResult = _expressionEvaluator.EvaluateExpression(expressionElements);
        
        // Invoke the calculation completed event
        OnCalculationCompleted(finalExpression, expressionResult);
    }

    // Invokes the CalculationCompleted event with the given expression and result.
    private static void OnCalculationCompleted(string expression, int result)
    {
        CalculationCompleted?.Invoke(expression, result); 
    }
    
    // private static void SaveCalculationResultInHistory(string expression, int result)
    // {
    //     HistoryManager.AddEntry(expression, result);
    // }
}


