using Login.Application;
using Login.Dtos.Requests;
using Login.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers;

[Controller]
[Route("[Controller]")]
public class UserController : Controller
{
    private readonly CreateUserUseCase _createUserUseCase;
    private readonly GetUsersUseCase _getUsersUseCase;

    public UserController(CreateUserUseCase createUserUseCase, GetUsersUseCase getUsersUseCase)
    {
        _createUserUseCase = createUserUseCase;
        _getUsersUseCase = getUsersUseCase;
    }

    [HttpGet("")]
    public async Task<ActionResult<GetUsersResponse>> Do([FromQuery] QueryParameter parameter, CancellationToken cancellationToken)
    {
        var (users, total) = await _getUsersUseCase.Get(parameter, cancellationToken);
        return Ok(new GetUsersResponse(users, total));
    }

    [HttpPost("")]
    public async Task<ActionResult<CreateUsersResponse>> Do([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _createUserUseCase.Create(request, cancellationToken);
        return Ok(new CreateUsersResponse(user));
    }
}