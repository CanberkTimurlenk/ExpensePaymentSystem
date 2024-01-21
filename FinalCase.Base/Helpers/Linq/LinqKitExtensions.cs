using LinqKit;
using System.Linq.Expressions;

namespace FinalCase.Base.Helpers.Linq;

public static class LinqKitExtensions
{
    /// <summary>
    /// Adds a condition to the specified predicate based on the given boolean condition.
    /// If the condition is true, the provided expression is combined with the existing predicate using the AND operator;
    /// otherwise, the existing predicate is returned without any modification.
    /// </summary>
    /// <typeparam name="T">The type of the entity for which the predicate is being built.</typeparam>
    /// <param name="predicate">The current predicate to be modified.</param>
    /// <param name="condition">The boolean condition that determines whether the expression should be added to the predicate.</param>
    /// <param name="expression">The expression to be added to the predicate if the condition is true.</param>
    /// <returns>The modified predicate.</returns>
    public static ExpressionStarter<T> AddIf<T>(this ExpressionStarter<T> predicate, bool condition, Expression<Func<T, bool>> expression)
        => condition ? predicate.And(expression) : predicate;
}
