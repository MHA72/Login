﻿using Login.Dtos.Info;
using Login.Dtos.Requests;
using Login.IRepositories;
using Login.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Login.Application;

public sealed class CreateUserUseCase
{
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IUserRepository _userRepository;

    public CreateUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<UserInfo> Create(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var creatingUser = User.Create(request.Username, request.Email);

        var hashedPassword = _passwordHasher.HashPassword(creatingUser, request.Password);
        creatingUser.PasswordHash = hashedPassword;

        return await _userRepository.AddUser(creatingUser, cancellationToken);
    }
}