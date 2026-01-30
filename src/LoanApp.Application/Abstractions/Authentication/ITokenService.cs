namespace LoanApp.Application.Abstractions.Authentication;

public interface ITokenService
{
    string Create(string identityKey);
    string CreateAccessToken(string identityKey);
}
