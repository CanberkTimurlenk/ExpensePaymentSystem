using LinqKit;
using System.Linq.Expressions;

namespace FinalCase.Base.Helpers.Linq;

public static class LinqHelper
{
    /// <summary>
    /// Adds a condition to the predicate if the condition is true.
    /// </summary>
    /// <typeparam name="T">Type of the entity</typeparam>
    /// <param name="condition">The condition to check before adding the expression to the predicate</param>
    /// <param name="predicate">The predicate to which the expression will be added</param>
    /// <param name="expression">The expression to add to the predicate if the condition is true</param>
    /// <returns>The updated predicate</returns>
    public static ExpressionStarter<T> AddCondition<T>(bool condition, ExpressionStarter<T> predicate, Expression<Func<T, bool>> expression)

        => condition ? predicate.And(expression) : predicate;
}
