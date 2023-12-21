namespace EventsAndDelegatesTest;

/// <summary>
/// Manages console interactions including displaying prompts and handling user input.
/// </summary>
public class ConsoleManager
{
    private static Dictionary<string, string>? _prompts;
    private static Dictionary<string, string>? _keywords;
    
    // Default Constructor - Initializes a new instance of the ConsoleManager class.
    // Sets up prompts and keywords used in the console interface.
    public ConsoleManager()
    {
        // Dictionary initialization for prompts
        _prompts = new Dictionary<string,string>() {
            { "operationPrompt", "Enter the operation (+,-,*): " }, // string input
            { "numberPrompt", "Enter number {0}: " }, // int numCount
            { "resultHistoryPrompt", "{0} Equation: {1} = {2}" }, // historyCount, wholeExpression, result (guardarlo todo en una sola variable no?)
            { "expressionPrompt", "Expression: {0} \n" }, // string wholeExpression 
            { "expressionResultPrompt", "Expression: {0}= {1}\n" }, // string wholeExpression + result
            { "resultPrompt", "Result: {0}\n" }, // string result
            { "pressAnyKeyPrompt", "\nPress any key to continue..." }, 
            { "instructionsPrompt", "Instructions - Reminder: \n- Type 'enter' to perform the calculation at any moment. \n- Type 'hist' to show the history. \n- Type 'exit' to exit the program. \n- Please note that mathematical precedence has not been implemented, so entering operations out of order may lead to incorrect results! :(" }, 
            { "waitingForSeconds", " \nWaiting for {0} seconds. Press any key to continue." }, // int seconds
            { "historyPrompt", "\n{0}. Expression: {1}, Result: {2}" }, // string expression, int result
        };
        
        // Dictionary initialization for special command keywords
        _keywords = new Dictionary<string, string>()
        {
            { "enterKeyword", "enter" },
            { "exitKeyword", "exit" },
            { "historyKeyword", "hist" },
        }; 
    }

    /// <summary>
    /// Displays instructions and a countdown before performing calculations.
    /// </summary>
    /// <param name="waitTime">The total wait time in milliseconds.</param>
    /// <param name="interval">The interval for countdown updates in milliseconds.</param>
    public void PrintInstructionsAndCountdown(int waitTime, int interval) 
    {
        for (int remaining = waitTime; remaining > 0; remaining -= interval)
        { 
            Console.Clear();
            ShowFormattedPrompt("instructionsPrompt");
            ShowFormattedPrompt("waitingForSeconds", remaining / 1000);
               
            // Break the countdown if a key is pressed
            if (Console.KeyAvailable) {
                // Clear the input buffer
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(intercept: true); // Read and discard the key
                }
                break;
            }
            Thread.Sleep(interval);
        }
    }
    
    public void PrintCalculationResult(string expression, int result)
    {
        Console.Clear();
        ShowFormattedPrompt("expressionPrompt", expression);
        ShowFormattedPrompt("resultPrompt", result);
        Thread.Sleep(1000);
        ShowFormattedPrompt("pressAnyKeyPrompt");
        Console.ReadKey(); 
    }
    
    // I feel like the following method could be improved.
    /* Things I could check and improve:
     1. DRY (Don't Repeat Yourself)
     2. Avoid Hard-Coding
     3. Single Responsibility Principle (SRP)
     4. Code Readability and Clarity <<<
     */
    /// <summary>
    /// Obtains valid user input based on a specified comparator and prompt.
    /// </summary>
    /// <param name="comparator">A delegate that defines the criteria for valid input.</param>
    /// <param name="prompt">The prompt key to display to the user.</param>
    /// <param name="numberCount">The current count of numbers entered, used in formatting certain prompts.</param>
    /// <param name="expression">The current mathematical expression, used in formatting certain prompts.</param>
    /// <returns>The valid input from the user.</returns> 
    public string GetValidInput(Program.Comparator<string> comparator, string prompt, int numberCount, string expression) 
    {
        while (true) 
        {
            Console.Clear();
            ShowFormattedPrompt("expressionPrompt", expression); 
            ShowFormattedPrompt(prompt, numberCount);
            
            string input = Console.ReadLine()!.Trim();
            
            if (TryHandleSpecialCommand(input, prompt, out bool shouldContinue))
            {
                if (!shouldContinue) 
                    return input.Equals(_keywords["enterKeyword"], StringComparison.OrdinalIgnoreCase) ? _keywords["enterKeyword"] : string.Empty;
                continue;
            }
            
            if (!comparator(input)) { 
                Console.WriteLine("Invalid input. Please try again."); 
                Thread.Sleep(2000);
                continue;
            }
            
            // Valid Input
            return input; 
        }
    }

    /// <summary>
    /// Handles special command inputs like 'exit' or 'history'.
    /// </summary>
    /// <param name="input">The user input to evaluate.</param>
    /// <param name="prompt">The current prompt displayed to the user.</param>
    /// <param name="shouldContinue">Out parameter indicating whether to continue the input loop.</param>
    /// <returns>True if the input matches a special command, otherwise false.</returns>
    private bool TryHandleSpecialCommand(string input, string prompt, out bool shouldContinue)
    {
        shouldContinue = true;
       
        if (input.Equals(_keywords["enterKeyword"], StringComparison.OrdinalIgnoreCase)) {
            if (prompt.Equals("operationPrompt"))
            {
                shouldContinue = false;
                return true; 
            }
            
            Console.WriteLine("Invalid input. You can't use the 'enter' keyword while entering numbers.");
            Thread.Sleep(2000); 
            return true;
        } 
        else if (input.Equals(_keywords["exitKeyword"], StringComparison.OrdinalIgnoreCase))
        {
            Environment.Exit(0); // Exit program
        } 
        else if (input.Equals(_keywords["historyKeyword"], StringComparison.OrdinalIgnoreCase))
        {
            Program.HistoryManager.PrintHistory(); 
            return true;
        }
       
        // Continue with the normal flow
        shouldContinue = false;
        return false;
    }
    
    /// <summary>
    /// Retrieves and formats a prompt based on a prompt key and optional formatting arguments. Private method.
    /// </summary>
    /// <param name="promptKey">The key for the prompt to retrieve.</param>
    /// <param name="args">Optional formatting arguments.</param>
    /// <returns>The formatted prompt string.</returns>
    private static string GetFormattedPrompt(string promptKey, params object[] args) {
        if (!_prompts.TryGetValue(promptKey, out var prompt)) return "Invalid prompt key";
        return string.Format(prompt, args);
    }

    /// <summary>
    /// Displays a formatted prompt to the console. Public method.
    /// </summary>
    /// <param name="promptKey">The key for the prompt to display.</param>
    /// <param name="args">Optional formatting arguments.</param>
    public void ShowFormattedPrompt(string promptKey, params object[] args) {
        string formattedPrompt = GetFormattedPrompt(promptKey, args);
        Console.Write(formattedPrompt);
    }
}