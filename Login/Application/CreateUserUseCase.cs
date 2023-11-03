using Login.Dtos.Info;
using Login.Dtos.Requests;
using Login.IRepositories;
using Login.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Login.Application;

public sealed class CreateUserUseCase
{
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IBackgroundTaskQueue _queue;
    private readonly IDriveService _driveService;



    public CreateUserUseCase(IUserRepository userRepository, IBackgroundTaskQueue queue, IDriveService driveService)
    {
        _userRepository = userRepository;
        _queue = queue;
        _driveService = driveService;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<UserInfo> Create(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var creatingUser = User.Create(request.Username, request.Email);

        var hashedPassword = _passwordHasher.HashPassword(creatingUser, request.Password);
        creatingUser.PasswordHash = hashedPassword;
        
        var userInfo = await _userRepository.AddUser(creatingUser, cancellationToken);
        
        _queue.QueueBackgroundWorkItem(async token =>
        {
            await _driveService.CreateSheet(userInfo.Id, userInfo.Email, userInfo.Username, token);
        });

        return userInfo;
    }
}