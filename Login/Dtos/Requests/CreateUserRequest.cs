using System.ComponentModel.DataAnnotations;

namespace Login.Dtos.Requests;

public sealed record CreateUserRequest(
    [Required(AllowEmptyStrings = false)] string Username,
    [Required(AllowEmptyStrings = false)]
    [EmailAddress]
    string Email,
    [Required(AllowEmptyStrings = false)] string Password);