using System.ComponentModel.DataAnnotations;

namespace MisTareas.API.Data.Entities
{
    public class Column
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Description { get; set; }
        [MaxLength(15)]
        [Required]
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
