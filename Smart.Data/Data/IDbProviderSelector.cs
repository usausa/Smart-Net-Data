namespace Smart.Data
{
    public interface IDbProviderSelector
    {
        IDbProvider GetProvider(object parameter);
    }
}
