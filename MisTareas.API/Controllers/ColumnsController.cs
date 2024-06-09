using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisTareas.API.Data;
using MisTareas.API.Data.Entities;
using MisTareas.API.Dtos;

namespace MisTareas.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly MisTareasContext _context;

        public ColumnsController(MisTareasContext _context)
        {
            this._context = _context;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<Column>> GetAll()
        {
            return await _context.Column.ToListAsync();
        }
        [HttpGet("get-columns-by-board")]
        public async System.Threading.Tasks.Task<List<ColumnDto>> GetColumnsByTask([FromQuery] int userId, [FromQuery] int boardId)
        {
            List<Column> columns = await _context.Column.Include(c => c.Tasks).Where(c => c.UserId == userId && c.BoardId == boardId).ToListAsync();

            return ColumnDto.ColumnsToDto(columns);
        }

        [HttpGet("{idColumn}")]
        public async Task<Column?> GetById(int idColumn)
        {
            return await _context.Column.FirstOrDefaultAsync(item => item.Id == idColumn);
        }

    }
}
