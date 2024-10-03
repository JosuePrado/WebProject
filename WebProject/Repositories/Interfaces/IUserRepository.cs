using WebProject.Domain;

namespace WebProject.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmail(string email);
}