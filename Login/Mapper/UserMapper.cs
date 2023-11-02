using Login.Dtos.Info;
using Login.Models.User;

namespace Login.Mapper;

public static class UserMapper
{
    public static UserInfo ToUserInfo(this User user) => new(user.Id, user.Username, user.Email);
}