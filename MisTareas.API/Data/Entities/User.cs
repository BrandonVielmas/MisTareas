using System.ComponentModel.DataAnnotations;

namespace MisTareas.API.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public virtual ICollection<Board> Boards { get; set; }
        public virtual ICollection<Column> Columns { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
