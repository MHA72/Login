
namespace Login.Dtos.Info;

public sealed record UserInfo(
    Guid Id,
    string Username,
    string Email
);