namespace Notes.Data.EFProvider
{
    public interface IRepositoryReadOnly<T> : IReadRepository<T> where T : class
    {

    }
}