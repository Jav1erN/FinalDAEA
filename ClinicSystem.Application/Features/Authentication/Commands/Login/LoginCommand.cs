using MediatR;
using ClinicSystem.Application.Common.Models;

namespace ClinicSystem.Application.Features.Authentication.Commands.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<Result<LoginResponse>>
{
    public class Handler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        public async Task<Result<LoginResponse>> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            // ⚠️ lógica real se conecta después (DbContext + JWT)
            if (request.Email == "admin@test.com" && request.Password == "123456")
            {
                return Result<LoginResponse>.Success(new LoginResponse
                {
                    Token = "FAKE_JWT_TOKEN"
                });
            }

            return Result<LoginResponse>.Failure("Invalid credentials");
        }
    }
}