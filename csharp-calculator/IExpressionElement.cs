namespace EventsAndDelegatesTest;

/// <summary>
/// Defines a common interface for elements that can be part of a mathematical expression.
/// </summary>
public interface IExpressionElement
{
   /// <summary>
   /// Retrieves the string representation of the expression element.
   /// </summary>
   /// <returns>A string that represents the current element.</returns>
   string GetStringRepresentation();
}