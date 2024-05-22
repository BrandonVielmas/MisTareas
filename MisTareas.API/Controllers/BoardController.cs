using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisTareas.API.Data;
using MisTareas.API.Data.Entities;

namespace MisTareas.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BoardController(MisTareasContext context) : ControllerBase
    {
        private readonly MisTareasContext _context = context;

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
    }
}
