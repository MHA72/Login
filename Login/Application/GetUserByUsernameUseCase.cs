using Login.IRepositories;
using Login.Models.User;

namespace Login.Application;

public class GetUserByUsernameUseCase
{
    private readonly IUserRepository _userRepository;

    public GetUserByUsernameUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<User?>
        Get(string username, CancellationToken cancellationToken = default) =>
        _userRepository.GetUserByUsername(username, cancellationToken);
}