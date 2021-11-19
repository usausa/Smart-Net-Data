namespace Smart.Data;

using System.Collections.Generic;

public sealed class NamedDbProviderSelector : IDbProviderSelector
{
    private readonly Dictionary<string, IDbProvider> providers = new();

    public void AddProvider(string name, IDbProvider provider)
    {
        providers[name] = provider;
    }

    public IDbProvider GetProvider(object parameter)
    {
        var key = (string)parameter;
        if (providers.TryGetValue(key, out var provider))
        {
            return provider;
        }

        throw new KeyNotFoundException($"Provider not found. name=[{key}]");
    }
}
