
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MisTareas.API.Data;
using MisTareas.API.Data.Entities;
using MisTareas.API.Helpers;

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
            string userIdClaim = UserHelper.GetUserId();
            return Entities.Where(e => e.UserId == int.Parse(userIdClaim));
        }

        public async Task<T?> GetById(TId id)
        {
            string userIdClaim = UserHelper.GetUserId();
            return await Entities.AsNoTracking()
                .Where(e => e.UserId == int.Parse(userIdClaim))
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
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
            string userIdClaim = UserHelper.GetUserId();
            if (int.Parse(userIdClaim) == value.UserId)
            {
                EntityEntry<T> result = await Entities.AddAsync(value);
                return result.Entity;
            }
            return null;
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
            string userIdClaim = UserHelper.GetUserId();
            if (int.Parse(userIdClaim) == value.UserId)
            {
                Entities.Update(value);
            }
        }
    }
}
