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

        [HttpPost("/api/v1/Tasks/add-by-columnId")]
        public async Task<Data.Entities.Task> AddByColumnId([FromBody] InsertTaskDto task)
        {
            Data.Entities.Task newTask = new Data.Entities.Task();
            newTask.Name = task.name;
            newTask.Description = task.description;
            newTask.ColumnId = task.columnId;
            newTask.UserId = task.userId;

            await _context.AddAsync(newTask);
            await _context.SaveChangesAsync();

            return newTask;
        }

        public class InsertTaskDto
        {
            public string name { get; set; }
            public string description { get; set; }
            public int columnId { get; set; }
            public int userId { get; set; }
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
