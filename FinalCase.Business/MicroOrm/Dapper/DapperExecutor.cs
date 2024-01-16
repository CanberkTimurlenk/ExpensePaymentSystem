using Dapper;
using FinalCase.Schema.Reports;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinalCase.Business.MicroOrm.Dapper;

/// <summary>
/// A static class to execute Dapper operations
/// </summary>
public static class DapperExecutor
{
    /// <summary>
    /// Executes a stored procedure asynchronously
    /// </summary>
    /// <typeparam name="T">The type to be filled with the stored procedure result</typeparam>
    /// <param name="storedProcedure">The stored procedure const</param>
    /// <param name="parameters">The query parameters</param>
    /// <param name="connectionString">The connection string</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>An <see cref="IEnumerable{T}"/> filled with the results of the stored procedure</returns>
    public async static Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string storedProcedure, DynamicParameters parameters,
        string connectionString, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Asynchronously executes a SQL query against a view.
    /// </summary>
    /// <typeparam name="T">The type to be filled with the results.</typeparam>
    /// <param name="view">The name of the view.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> filled with the results of the SQL query against the view.</returns>
    public async static Task<IEnumerable<T>> QueryViewAsync<T>(string view, string connectionString, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        return await connection.QueryAsync<T>(view);
    }
}