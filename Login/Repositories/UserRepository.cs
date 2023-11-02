using Login.Context;
using Login.Dtos.Info;
using Login.Dtos.Requests;
using Login.IRepositories;
using Login.Mapper;
using Login.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Login.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LoginContext _authContext;

    public UserRepository(LoginContext authContext)
    {
        _authContext = authContext;
    }

    public async Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken)
    {
        return await _authContext.Users!.FirstOrDefaultAsync(user => user.Username == username, cancellationToken);
    }

    public async Task<User?> GetUserById(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _authContext.Users!.FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
    }

    public async Task AddLogin(Guid userId, CancellationToken cancellationToken = default)
    {
        var login = new UserLogin
        {
            UserId = userId
        };
        await _authContext.UserLogins!.AddAsync(login, cancellationToken);
        await _authContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAllLogins(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _authContext.Users!.FirstAsync(user => user.Id == userId, cancellationToken);
        user.IsDeleted = true;
        user.UpdateTime = DateTime.Now;
        await _authContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<(List<UserInfo>, int)> GetUsers(QueryParameter parameter, CancellationToken cancellationToken = default)
    {
        var queryableUser = _authContext.Users!.AsQueryable();

        var total = await queryableUser.CountAsync(cancellationToken);

        if (parameter.Skip != 0) queryableUser = queryableUser.Skip(parameter.Skip);
        if (parameter.Take != 0) queryableUser = queryableUser.Take(parameter.Take);

        return (await queryableUser.Select(user => user.ToUserInfo()).ToListAsync(cancellationToken), total);
    }

    public void AddUserSync(User user)
    {
        _authContext.Users!.Add(user);
        _authContext.SaveChanges();
    }

    public async Task<UserInfo> AddUser(User user, CancellationToken cancellationToken = default)
    {
        await _authContext.Users!.AddAsync(user, cancellationToken);
        await _authContext.SaveChangesAsync(cancellationToken);
        return GetUserByUsername(user.Username, cancellationToken).Result!.ToUserInfo();
    }
}