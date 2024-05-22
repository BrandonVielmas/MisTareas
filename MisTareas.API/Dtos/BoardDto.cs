using MisTareas.API.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MisTareas.API.Dtos
{
    public class BoardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public static List<BoardDto> BoardToDto(ICollection<Board> boards)
        {
            List<BoardDto> boardDtos = new List<BoardDto>();

            foreach (var board in boards)
            {
                boardDtos.Add(new BoardDto { Id = board.Id, Description = board.Description, Name = board.Name });
            }

            return boardDtos;
        }
    }
}
