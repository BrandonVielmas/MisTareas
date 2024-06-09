using Microsoft.EntityFrameworkCore;
using MisTareas.API.Data;
using MisTareas.API.Data.Entities;

namespace MisTareas.API.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        public UserRepository(MisTareasContext context) : base(context)
        {
        }
    }
}
