using BudgettingWebApps.Models.DbModel;

namespace BudgettingWebApps.Models.Mapper
{
    public class ForgotPasswordMapper
    {
        public static DbForgotPasswordDto Map(ForgotPasswordDto forgotPassword)
        {
            return new DbForgotPasswordDto()
            {
                username = forgotPassword.Username,
                passwordhash = BCrypt.Net.BCrypt.HashPassword(forgotPassword.Password),
            };
        }
    }
}
