using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Domain.Common;

namespace LoanApp.Application.Features.Authentication.CreateAccessToken;

public record CreateAccessTokenCommand(string GrantType, string ClientId, string ClientSecret, string[] Scopes) : ICommand<CreateAccessTokenResult>;
public record CreateAccessTokenResult(string AccessToken);