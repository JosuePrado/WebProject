using WebProject.Domain;
using WebProject.Domain.Entities;

namespace WebProject.Repositories.Interfaces;

public interface IChannelRepository : IBaseRepository<Channel>
{
    Task<bool>JoinMemberToChannel(ChannelMember channelMember);
}