namespace EventsAndDelegatesTest;

/// <summary>
/// Manages the history of calculations, storing and displaying previous expressions and their results.
/// </summary>
public class HistoryManager
{
    // A list of tuples to hold the history of expressions and their results.
    private static readonly List<(string expression, int result)> History = new();
    
    // Adds a new entry to the history.
    public void AddEntry(string expression, int result)
    {
       // Add the new expression and result as a tuple to the history.
       History.Add((expression, result)); 
    }
    
    // Prints the entire history of calculations to the console.
    public void PrintHistory()
    {
        Console.Clear();
        Console.Write("History:");
        
        // Iterate through all history entries and print them.
        for (int i = 0; i < History.Count; i++)
        {
            // Deconstruct the tuple into expression and result.
            var (expression, result) = History[i];
            Program.ConsoleManager.ShowFormattedPrompt("historyPrompt", i + 1, expression, result );
        }
        Console.WriteLine("");
        Program.ConsoleManager.ShowFormattedPrompt("pressAnyKeyPrompt");
        Console.ReadKey();
    } 
}