using System.Security.Principal;

namespace BudgettingWebApps.Models.DbModel
{
    public class DbForgotPasswordDto
    {
        public string username { get; set; } = string.Empty;
        public string passwordhash { get; set; } = string.Empty;
    }
}
