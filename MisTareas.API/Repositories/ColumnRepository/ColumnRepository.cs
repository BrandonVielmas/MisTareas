using MisTareas.API.Data;
using MisTareas.API.Data.Entities;

namespace MisTareas.API.Repositories.ColumnRepository
{
    public class ColumnRepository : GenericRepository<Column, int>, IColumnRepository
    {
        public ColumnRepository(MisTareasContext context) : base(context)
        {
        }
    }
}
