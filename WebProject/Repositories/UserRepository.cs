using Dapper;
using ShareModels;
using WebProject.Repositories.Interfaces;
using System.Data;
using WebProject.Data.Interfaces;

namespace WebProject.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionSingleton _dbConnection;

    public UserRepository(IDbConnectionSingleton dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<User?> Add(User item)
    {
        const string sql = @"
        INSERT INTO Users (Username, Password, Email, CreatedAt)
        VALUES (@Username, @Password, @Email, @CreatedAt)
        RETURNING *;
        ";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<User>(sql, item);
    }

    public async Task<bool> Delete(User item)
    {
        const string sql = "DELETE FROM Users WHERE UserId = @UserId";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var rowsAffected = await connection.ExecuteAsync(sql, new { item.UserID });

        return rowsAffected > 0;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        const string sql = "SELECT * FROM Users";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryAsync<User>(sql);
    }

    public async Task<User?> GetByEmail(string email)
    {
        const string sql = "SELECT * FROM Users WHERE Email = @Email";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
    }

    public async Task<User?> GetById(int id)
    {
        const string sql = "SELECT * FROM Users WHERE UserId = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
    }

    public async Task<User?> UpdateAsync(User item)
    {
        const string sql = @"
        UPDATE Users
        SET Username = @Username, Password = @Password, Email = @Email
        WHERE UserId = @UserId
        RETURNING *;
        ";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<User>(sql, item);
    }
}
