namespace MisTareas.API.Repositories
{
    public interface IGenericRepository<T, TId>
    {
        Task<T> Insert(T value);
        Task<T?> GetById(TId id);
        IQueryable<T> GetAll();
        void Update (T value);
        Task<bool> SoftDelete(TId id);
        Task<bool> HardDelete(TId id);
    }
}
