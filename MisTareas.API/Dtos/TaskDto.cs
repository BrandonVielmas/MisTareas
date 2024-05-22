namespace MisTareas.API.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnId { get; set; }
        public static List<TaskDto> TasksToDto(ICollection<Data.Entities.Task> tasks)
        {
            List<TaskDto> taskDtos = new List<TaskDto>();

            foreach (Data.Entities.Task task in tasks)
            {
                taskDtos.Add(new TaskDto { Id = task.Id, Name = task.Name, Description = task.Description, ColumnId = task.ColumnId });
            }

            return taskDtos;
        }
    }
}
