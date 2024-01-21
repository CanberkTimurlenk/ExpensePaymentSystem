using Dapper;
using FinalCase.Data.Constants.DbObjects;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace FinalCase.BackgroundJobs.MicroOrm.Dapper;

/// <summary>
/// A static class to execute Dapper operations
/// </summary>
public static class DapperExecutor
{
    /// <summary>
    /// Executes a stored procedure asynchronously.
    /// </summary>
    /// <typeparam name="T">The type to be filled with the stored procedure result</typeparam>
    /// <param name="storedProcedure">The stored procedure const</param>
    /// <param name="parameters">The query parameters</param>
    /// <param name="connectionString">The connection string</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>An <see cref="IEnumerable{T}"/> Filled with the results of the stored procedure</returns>
    public async static Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string storedProcedure, DynamicParameters parameters,
        string connectionString, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Executes a stored procedure asynchronously.
    /// </summary>    
    /// <param name="storedProcedure">The stored procedure const</param>
    /// <param name="parameters">The query parameters</param>
    /// <param name="connectionString">The connection string</param>
    /// <param name="cancellationToken">Cancellation Token</param>    
    public async static Task ExecuteStoredProcedureAsync(string storedProcedure, DynamicParameters parameters, string connectionString, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Executes SQL query against a view asynchronously.
    /// </summary>
    /// <typeparam name="T">The type to be filled with the view results.</typeparam>
    /// <param name="view">The name of the view.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> filled with the results of the SQL query against the view.</returns>
    public async static Task<IEnumerable<T>> QueryViewAsync<T>(string view, string connectionString, CancellationToken cancellationToken = default)
    {
        if (!IsViewNameValid(view)) // To prevent a possible SQL injection, since the parameter is a string
            throw new ArgumentException("Invalid view name");

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        return await connection.QueryAsync<T>($"SELECT * FROM {view}");
    }

    /// <summary>
    /// Executes a SQL query against a view.
    /// </summary>
    /// <typeparam name="T">The type to be filled with the results.</typeparam>
    /// <param name="view">The name of the view.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> filled with the results of the SQL query against the view.</returns>
    public static IEnumerable<T> QueryView<T>(string view, string connectionString)
    {
        if (!IsViewNameValid(view)) // To prevent a possible SQL injection, since the parameter is a string
            throw new ArgumentException("Invalid view name");

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        return connection.Query<T>($"SELECT * FROM {view}");
    }

    /// <summary>
    /// Checks if the provided view name is valid by comparing it to the values
    /// of the public and static fields in the Views class.
    /// </summary>
    /// <param name="view">The view name to be validated.</param>
    /// <returns>True if the view name is valid; otherwise, false.</returns>
    public static bool IsViewNameValid(string view)
    {
        var fields = typeof(Views).GetFields(BindingFlags.Public | BindingFlags.Static);
        // Gets all the values of the fields in the Views class

        List<string> values = fields.Select(field => (string)field.GetValue(null)).ToList();

        return values.Any(value => value.Equals(view));
    }
}