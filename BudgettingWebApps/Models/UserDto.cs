

using FluentValidation;

namespace BudgettingWebApps.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Password { get; set; }=string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool active { get; set; }
    }
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator() 
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("your first name is required")
            .MinimumLength(3).WithMessage("name is too short")
            .MaximumLength(25).WithMessage("name is too long!");

            RuleFor(x => x.Surname)
          .NotEmpty().WithMessage("your surname is required")
          .MinimumLength(3).WithMessage("name is too short!")
          .MaximumLength(25).WithMessage("name is too long!");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("email address is required!")
                .EmailAddress().WithMessage("the format of your email address is wrong!");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("a username is required")
                .MinimumLength(3).WithMessage("username is too short!")
                .MaximumLength(50).WithMessage("username is too long");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("a password is required")
                .MinimumLength(10).WithMessage("password is too short")
                .MaximumLength(50).WithMessage("password is too long");
        }
    }
}
