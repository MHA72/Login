using Login.Dtos.Requests;
using Login.IRepositories;
using Login.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Login.Application;

public class LoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher;

    public LoginUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<bool> CheckLogin(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsername(request.Username, cancellationToken);
        if (user == null) return false;

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        return verificationResult switch
        {
            PasswordVerificationResult.Failed => false,
            PasswordVerificationResult.Success => true,
            _ => false
        };
    }
}