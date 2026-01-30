namespace LoanApp.Infrastructure.Services;


public interface IClientStore
{
    Task<Client?> FindAsync(string clientId);
}

public record Client(string ClientId, string SecretHash, string[] AllowedScopes);

public sealed class InMemoryClientStore(IEnumerable<Client> clients) : IClientStore
{
    private readonly Dictionary<string, Client> _clients = clients.ToDictionary(c => c.ClientId, StringComparer.Ordinal);

    public Task<Client?> FindAsync(string clientId)
        => Task.FromResult(_clients.TryGetValue(clientId, out var c) ? c : null);
}