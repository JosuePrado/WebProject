using System.Data;

namespace WebProject.Data.Interfaces;

public interface IDbConnectionSingleton 
{
    Task<IDbConnection> CreateConnectionAsync();
}