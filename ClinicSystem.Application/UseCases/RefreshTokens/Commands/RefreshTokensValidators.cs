using FluentValidation;

namespace ClinicSystem.Application.UseCases.RefreshTokens.Commands;

public class CreateRefreshTokenValidator : AbstractValidator<CreateRefreshTokenCommand>
{
    public CreateRefreshTokenValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.TokenHash).NotEmpty();
    }
}
public class UpdateRefreshTokenValidator : AbstractValidator<UpdateRefreshTokenCommand>
{
    public UpdateRefreshTokenValidator()
    {
        RuleFor(x => x.RefreshTokenId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.TokenHash).NotEmpty();
    }
}
