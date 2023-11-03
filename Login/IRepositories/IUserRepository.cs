using Login.Dtos.Info;
using Login.Dtos.Requests;
using Login.Models.User;

namespace Login.IRepositories;

public interface IUserRepository
{
    Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken = default);
    Task<User?> GetUserById(Guid userId, CancellationToken cancellationToken = default);
    Task AddLogin(Guid userId, CancellationToken cancellationToken = default);
    Task DeleteAllLogins(Guid userId, CancellationToken cancellationToken = default);
    Task<(List<UserInfo>, int)> GetUsers(QueryParameter parameter, CancellationToken cancellationToken = default);
    void AddUserSync(User user);
    Task<UserInfo> AddUser(User user, CancellationToken cancellationToken = default);
    Task SetAuthenticatedUser(Guid id);
    User GetAuthenticatedUser();
}