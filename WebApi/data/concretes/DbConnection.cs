using System.Data;

using Microsoft.Extensions.Options;

using Npgsql;
using WebProject.Data.Interfaces;

namespace WebProject.Data.Concretes;

public class DbConnection : IDbConnectionSingleton
{
    private readonly DatabaseOptions _options;

    public DbConnection(IOptions<DatabaseOptions> options)
    {
        _options = options.Value;
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_options.DefaultConnection);
        await connection.OpenAsync();
        return connection;
    }
}