namespace EventsAndDelegatesTest;

/// <summary>
/// Represents a numeric element in a mathematical expression
/// </summary>
public class NumberElement : IExpressionElement
{
   public int Value { get; }

   // Initializes a new instance of the NumberElement class with a specified value
   // param "value" -> The numeric value of this expression element
   public NumberElement(int value)
   {
      Value = value;
   }

   // Retrieves the string representation of the numeric element
   public string GetStringRepresentation()
   {
      return Value.ToString();
   }
}