namespace LoanApp.Application.DTOs;


public record TokenRequestDto(string GrantType, string ClientId, string ClientSecret, string Scope);