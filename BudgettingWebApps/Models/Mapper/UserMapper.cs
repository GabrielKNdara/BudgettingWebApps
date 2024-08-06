using BudgettingWebApps.Models.DbModel;

namespace BudgettingWebApps.Models.Mapper
{
    public class UserMapper
    {
        public static DbUserDto Map(UserDto user)
        {
            return new DbUserDto()
            {
                id = user.Id,
                username = user.UserName,
                passwordhash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                role = user.Role,
                email = user.Email,
                firstname = user.FirstName,
                lastname = user.Surname
                
              //  createdate = DateTime.Now,
            };      
        }
    }
}
