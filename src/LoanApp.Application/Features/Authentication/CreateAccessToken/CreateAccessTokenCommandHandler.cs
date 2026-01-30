using LoanApp.Application.Abstractions.Authentication;
using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Domain.Common;

namespace LoanApp.Application.Features.Authentication.CreateAccessToken;

public class CreateAccessTokenCommandHandler(ITokenService tokenService) : ICommandHandler<CreateAccessTokenCommand, CreateAccessTokenResult>
{
    public async Task<Result<CreateAccessTokenResult>> Handle(CreateAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var token = tokenService.CreateAccessToken(request.ClientId);

        if (string.IsNullOrEmpty(token)) return Result<CreateAccessTokenResult>.Failure(new Error("Unable to Generate Access Token", "Unable to Generate Access Token"));

        return new CreateAccessTokenResult(token);
    }
}
