
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MisTareas.API.Data;
using MisTareas.API.Data.Entities;

namespace MisTareas.API.Repositories
{
    public abstract class GenericRepository<T, TId> : IGenericRepository<T, TId>
        where T : MisTareasBaseEntity<TId>
        where TId : IEquatable<TId>
    {
        private readonly MisTareasContext _context;
        protected DbSet<T> Entities => _context.Set<T>();

        protected GenericRepository(MisTareasContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return Entities;
        }

        public async Task<T?> GetById(TId id)
        {
            return await Entities.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<bool> HardDelete(TId id)
        {
            T? entity = await this.GetById(id);

            if (entity == null)
            {
                return false;
            }

            Entities.Remove(entity);
            return true;
        }

        public async Task<T> Insert(T value)
        {
            EntityEntry<T> result = await Entities.AddAsync(value);
            return result.Entity;
        }

        public async Task<bool> SoftDelete(TId id)
        {
            T? entity = await this.GetById(id);

            if(entity == null)
            {
                return false;
            }

            entity.IsDelete = true;
            entity.UpdateTime = DateTime.UtcNow;

            this.Update(entity);
            return true;
        }

        public void Update(T value)
        {
            Entities.Update(value);
        }
    }
}
