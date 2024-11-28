using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisTareas.API.Data;
using MisTareas.API.Data.Entities;
using MisTareas.API.Repositories.BoardRepository;

namespace MisTareas.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    public class BoardController(MisTareasContext context, IBoardRepository boardRepository) : ControllerBase
    {
        private readonly MisTareasContext _context = context;
        private readonly IBoardRepository _boardRepository = boardRepository;

        [HttpGet("board-by-userId")]
        public async Task<Board> GetBoardByUserId([FromQuery] int boardId, [FromQuery] int userId)
        {
            Board? board = await _context.Board
                .Include(b => b.Columns)
                .ThenInclude(c => c.Tasks)
                .Where(b => b.Id == boardId && b.UserId == userId)
                .FirstOrDefaultAsync();

            return board;
        }

        [HttpGet]
        public async Task<List<Board>> GetBoards()
        {
            return await _boardRepository.GetAll().AsNoTracking().ToListAsync();
        }
    }
}
