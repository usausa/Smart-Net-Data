namespace Smart.Data
{
    public interface IPaged
    {
        int Page { get; }

        bool HasPrev { get; }

        bool HasNext { get; }

        int TotalPage { get; }
    }
}
