using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MisTareas.API.Data;

namespace MisTareas.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TasksController(MisTareasContext _context) : ControllerBase
    {
        private readonly MisTareasContext _context = _context;

        [HttpPost]
        public async Task<Data.Entities.Task> Insert(Data.Entities.Task task)
        {
            EntityEntry<Data.Entities.Task> insertTask = await _context.AddAsync(task);
            await _context.SaveChangesAsync();

            return insertTask.Entity;
        }

        [HttpDelete("{idTask}")]
        public async Task DeleteById(int idTask)
        {
            Data.Entities.Task? task = await _context.Task.FirstOrDefaultAsync(x => x.Id == idTask);

            if (task != null)
            {
                _context.Task.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        [HttpPut("/api/v1/Tasks/update-task-of-column")]
        public async Task UpdateTaskOfColumn([FromQuery] int idTask, [FromQuery] int idColumn)
        {
            Data.Entities.Task? task = await _context.Task.FirstOrDefaultAsync(x => x.Id == idTask);

            if(task != null)
            {
                task.ColumnId = idColumn;
                await _context.SaveChangesAsync();
            }
        }
    }
}
