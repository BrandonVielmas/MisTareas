namespace MisTareas.API.Data.Entities
{
    public class MisTareasBaseEntity<TId>
    {
        public TId Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
