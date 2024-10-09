using Dapper;
using WebProject.Domain;
using WebProject.Domain.Entities;
using WebProject.Repositories.Interfaces;
using System.Data;
using WebProject.Data.Interfaces;

namespace WebProject.Repositories;

public class ChannelRepository : IChannelRepository
{
    private readonly IDbConnectionSingleton _dbConnection;

    public ChannelRepository(IDbConnectionSingleton dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Channel?> Add(Channel item)
    {
        const string sql = @"
        INSERT INTO Channels (ChannelName, IsPrivate, CreatedAt)
        VALUES (@ChannelName, @IsPrivate, @CreatedAt)
        RETURNING *;
        ";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Channel>(sql, item);
    }

    public async Task<bool> Delete(Channel item)
    {
        const string sql = "DELETE FROM Channels WHERE ChannelId = @ChannelID";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var rowsAffected = await connection.ExecuteAsync(sql, new { item.ChannelID });

        return rowsAffected > 0;
    }

    public async Task<IEnumerable<Channel>> GetAll()
    {
        const string sql = "SELECT * FROM Channels";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryAsync<Channel>(sql);
    }

    public async Task<Channel?> GetById(int id)
    {
        const string sql = "SELECT * FROM Channels WHERE ChannelId = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Channel>(sql, new { Id = id });
    }

    public async Task<bool> JoinMemberToChannel(ChannelMember channelMember)
    {
        const string sql = @"
        INSERT INTO ChannelMembers (ChannelId, UserId, JoinedAt)
        VALUES (@ChannelID, @UserID, @JoinedAt)
        ON CONFLICT (ChannelId, UserId) DO NOTHING; -- Prevents duplicate entries
        ";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var rowsAffected = await connection.ExecuteAsync(sql, channelMember);

        return rowsAffected > 0;
    }

    public async Task<Channel?> UpdateAsync(Channel item)
    {
        const string sql = @"
        UPDATE Channels
        SET ChannelName = @ChannelName, IsPrivate = @IsPrivate
        WHERE ChannelId = @ChannelID
        RETURNING *;
        ";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Channel>(sql, item);
    }
}
