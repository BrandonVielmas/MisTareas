using MisTareas.API.Data.Entities;

namespace MisTareas.API.Dtos
{
    public class ColumnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public static List<ColumnDto> ColumnsToDto(List<Column> columns)
        {
            List<ColumnDto> columnDtos = new List<ColumnDto>();

            foreach (Column column in columns)
            {
                columnDtos.Add(new ColumnDto { Id = column.Id, Name = column.Name, Description = column.Description, Tasks = TaskDto.TasksToDto(column.Tasks) });
            }

            return columnDtos;
        }
    }
}
