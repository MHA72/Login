using Login.Dtos.Info;
using Login.Dtos.Requests;
using Login.IRepositories;

namespace Login.Application;

public sealed class GetUsersUseCase
{
    private readonly IUserRepository _userRepository;

    public GetUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<(List<UserInfo>, int)>
        Get(QueryParameter parameter, CancellationToken cancellationToken = default) =>
        _userRepository.GetUsers(parameter, cancellationToken);
}