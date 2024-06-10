using System.ComponentModel.DataAnnotations;

namespace MisTareas.API.Data.Entities
{
    public class Board : MisTareasBaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        //public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Column> Columns { get; set; }
    }
}
