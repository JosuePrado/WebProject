using WebProject.Domain;
using WebProject.Events;
using WebProject.Repositories.Interfaces;

namespace WebProject.EventHandlers;

public class NewMessageEventHandler
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChannelRepository _channelRepository;
    private readonly IUserRepository _userRepository;

    public NewMessageEventHandler(IMessageRepository messageRepository, IUserRepository userRepository, IChannelRepository channelRepository)
    {
        _messageRepository = messageRepository;
        _channelRepository = channelRepository;
        _userRepository = userRepository;
    }

    public async void Handle(NewMessageEvent @event)
    {
        var user = await GetUserByEmail(@event.Email);
        if (user == null) return;

        var channel = await GetChannelById(@event.ChannelId);
        if (channel == null) return;

        await CreateMessage(user.UserID, channel.ChannelID, @event.Content);
    }

    private async Task<User?> GetUserByEmail(string email)
    {
        return await _userRepository.GetByEmail(email);
    }

    private async Task<Channel?> GetChannelById(int channelId)
    {
        return await _channelRepository.GetById(channelId);
    }

    private async Task CreateMessage(int userId, int channelId, string content)
    {
        var message = new Message
        {
            Content = content,
            UserId = userId,
            ChannelId = channelId,
            SentAt = DateTime.UtcNow
        };

        await _messageRepository.Add(message);
    }
}
