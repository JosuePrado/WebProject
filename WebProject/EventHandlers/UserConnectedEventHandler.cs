using ShareModels;
using WebProject.Events;
using WebProject.Repositories.Interfaces;

namespace WebProject.EventHandlers;

public class UserConnectedEventHandler
{
    private readonly IUserRepository _userRepository;

    public UserConnectedEventHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async void Handle(UserConnectedEvent @event)
    {
        var user = await GetUserByEmail(@event.Username);
        if (user == null) return;
    }

    private async Task<User?> GetUserByEmail(string email)
    {
        return await _userRepository.GetByEmail(email);
    }

}
