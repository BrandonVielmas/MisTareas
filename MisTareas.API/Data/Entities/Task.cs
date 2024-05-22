using System.ComponentModel.DataAnnotations;

namespace MisTareas.API.Data.Entities
{
    public class Task
    {
        public int Id { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Description { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ColumnId { get; set; }
        public virtual Column? Column { get; set; }
    }
}
