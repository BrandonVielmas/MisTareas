using MisTareas.API.Data.Entities;

namespace MisTareas.API.Dtos
{
    public class UserLogginDto
    {
        public int UserId { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public List<BoardDto>? Boards { get; set; }
        public string Token { get; set; }
    }
}
