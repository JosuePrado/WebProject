using WebProject.Domain;

namespace WebProject.Repositories.Interfaces;

public interface IMessageRepository : IBaseRepository<Message>
{
    Task<IEnumerable<Message>> GetAllByChannelId(int channelId);
}
