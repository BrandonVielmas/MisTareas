using MisTareas.API.Data;

namespace MisTareas.API.Repositories.BoardRepository
{
    public class BoardRepository : GenericRepository<Data.Entities.Board, int>, IBoardRepository
    {
        public BoardRepository(MisTareasContext context) : base(context)
        {
        }
    }
}
