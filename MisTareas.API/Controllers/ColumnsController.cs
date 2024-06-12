using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisTareas.API.Data;
using MisTareas.API.Data.Entities;
using MisTareas.API.Dtos;
using MisTareas.API.Repositories.ColumnRepository;

namespace MisTareas.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly MisTareasContext _context;
        private readonly IColumnRepository _columnRepository;

        public ColumnsController(MisTareasContext _context, IColumnRepository _columnRepository)
        {
            this._context = _context;
            this._columnRepository = _columnRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Column>> GetAll()
        {
            return await _columnRepository.GetAll().ToListAsync();

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
