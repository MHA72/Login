namespace Login.Dtos.Info;

public sealed record UserLoginInfo(
    Guid Id,
    string Username,
    string Email,
    string Token
);