namespace EventsAndDelegatesTest;

/// <summary>
/// Represents an operator in a mathematical expression, such as '+', '-', or '*'.
/// </summary>
public class OperatorElement : IExpressionElement
{
   public string Operator { get; }
   
   // Initializes a new instance of the OperatorElement class with a specified operator
   // param "op" -> The operator symbol for this expression element
   public OperatorElement(string op)
   {
       Operator = op;
   }

   // Retrieves the string representation of the operator element
   public string GetStringRepresentation()
   {
       return Operator;
   }
}