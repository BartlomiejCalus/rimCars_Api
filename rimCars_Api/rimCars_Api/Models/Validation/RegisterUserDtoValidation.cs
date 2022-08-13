using FluentValidation;
using rimCars_Api.Entities;

namespace rimCars_Api.Models.Validation
{
    public class RegisterUserDtoValidation : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidation(SalonsDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            
            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailUsed = dbContext.Users.Any(u => u.Email == value);

                    if (emailUsed)
                    {
                        context.AddFailure("Email", "Email is taken");
                    }

                });
        }
    }
}
