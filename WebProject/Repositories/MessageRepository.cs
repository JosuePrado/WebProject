using Dapper;
using WebProject.Data.Interfaces;
using WebProject.Domain;
using WebProject.Repositories.Interfaces;

namespace WebProject.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly IDbConnectionSingleton _dbConnection;

    public MessageRepository(IDbConnectionSingleton dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Message?> Add(Message item)
    {
        const string sql = @"
        INSERT INTO Messages (ChannelId, UserId, Content, SentAt)
        VALUES (@ChannelId, @UserId, @Content, @SentAt)
        RETURNING *
        ";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Message>(sql, item);
    }

    public async Task<bool> Delete(Message item)
    {
        const string sql = "DELETE FROM Messages WHERE MessageId = @MessageId";

        using var connection = await _dbConnection.CreateConnectionAsync();
        var rowsAffected = await connection.ExecuteAsync(sql, new { item.MessageId });

        return rowsAffected > 0;
    }

    public async Task<IEnumerable<Message>> GetAll()
    {
        const string sql = "SELECT * FROM Messages";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryAsync<Message>(sql);
    }

    public async Task<IEnumerable<Message>> GetAllByChannelId(int channelId)
    {
        const string sql = "SELECT * FROM Messages WHERE ChannelId = @ChannelId";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryAsync<Message>(sql, new { ChannelId = channelId });
    }

    public async Task<Message?> GetById(int id)
    {
        const string sql = "SELECT * FROM Messages WHERE MessageId = @Id";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Message>(sql, new { Id = id });
    }

    public async Task<Message?> UpdateAsync(Message item)
    {
        const string sql = @"
        UPDATE Messages
        SET ChannelId = @ChannelId, UserId = @UserId, Content = @Content, SentAt = @SentAt
        WHERE MessageId = @MessageId
        RETURNING *
        ";

        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Message>(sql, item);
    }
}
