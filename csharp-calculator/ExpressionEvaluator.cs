namespace EventsAndDelegatesTest;

/// <summary>
/// The ExpressionEvaluator class is responsible for evaluating mathematical expressions
/// and generating their string representations.
/// </summary>
public class ExpressionEvaluator
{
    // The calculator instance to use for performing operations
    private readonly Calculator _calculator;
    
    // Default Constructor - Initializes a new instance of the ExpressionEvaluator class.
    public ExpressionEvaluator(Calculator calculator)
    {
        _calculator = calculator;
    }
    
    /// <summary>
    /// Generates a string representation of the current mathematical expression.
    /// </summary>
    /// <param name="expression">The list of expression elements to be represented.</param>
    /// <returns>A string representing the mathematical expression.</returns>
    public string GetCurrentExpression(List<IExpressionElement> expression) 
    {
        string currentExpression = "";
        foreach (IExpressionElement element in expression)
        {
            currentExpression += element.GetStringRepresentation() + " ";
        }
        return currentExpression;
    }
    
    /// <summary>
    /// Evaluates a list of expression elements and calculates the result.
    /// </summary>
    /// <param name="expressionElements">The list of expression elements to evaluate.</param>
    /// <returns>The result of the expression evaluation.</returns>
    public int EvaluateExpression(List<IExpressionElement> expressionElements)
    {
        int result = 0;
        Program.Operation currentOperation = null; // claro asi podria ir usando roden de precedencia, tomar values de 2 en 2
        foreach (IExpressionElement element in expressionElements ) 
        {
            if (element is NumberElement numberElement)
            {
                if (currentOperation != null) 
                {
                    // Apply the current operation if one is set, otherwise, set the result to the number value.
                    result = currentOperation(result, numberElement.Value); 
                }
                else 
                {
                    // Retrieve and set the current operation based on the operator symbol.
                    result = numberElement.Value;
                }
            } 
            else if (element is OperatorElement operatorElement)
            {
                currentOperation = _calculator.Operations[operatorElement.Operator];
            }
        }
        return result;
    } 
}