using Login.Dtos.Info;

namespace Login.Dtos.Response;

public sealed record GetUsersResponse(List<UserInfo> Users, int Total);