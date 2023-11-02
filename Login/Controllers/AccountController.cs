using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Login.Application;
using Login.Dtos.Info;
using Login.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Login.Controllers;

[Controller]
[Route("[Controller]")]
public class AccountController : Controller
{
    private readonly LoginUseCase _loginUseCase;
    private readonly IConfiguration _configuration;
    private readonly GetUserByUsernameUseCase _getUserByUsernameUseCase;


    public AccountController(LoginUseCase loginUseCase, IConfiguration configuration,
        GetUserByUsernameUseCase getUserByUsernameUseCase)
    {
        _loginUseCase = loginUseCase;
        _configuration = configuration;
        _getUserByUsernameUseCase = getUserByUsernameUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<UserLoginInfo>> Login([FromBody] UserLoginRequest request,
        CancellationToken cancellationToken)
    {
        if (await _loginUseCase.CheckLogin(request, cancellationToken))
        {
            var user = await _getUserByUsernameUseCase.Get(request.Username, cancellationToken);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]!),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new Claim("UserId", user!.Id.ToString()),
                new Claim("Username", user.Username),
                new Claim("Email", user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["JWT:ExpirationHour"]!)),
                signingCredentials: signIn);

            return Ok(new UserLoginInfo(user.Id, user.Username, user.Email,
                new JwtSecurityTokenHandler().WriteToken(token)));
        }

        return Unauthorized();
    }
}