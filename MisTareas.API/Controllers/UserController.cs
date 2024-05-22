using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisTareas.API.Data;
using MisTareas.API.Data.Entities;
using MisTareas.API.Dtos;

namespace MisTareas.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController(MisTareasContext _context) : ControllerBase
    {
        private readonly MisTareasContext _context = _context;

        [HttpPost("loggin")]
        public async Task<UserLogginDto> Loggin([FromBody] string email)
        {
            User? user = await _context.User.Include(u => u.Boards).FirstOrDefaultAsync(x => x.Email == email);

            UserLogginDto res = new UserLogginDto
            { 
                UserId = user.Id, 
                Email = user.Email, 
                FullName = user.Name + " " + user.LastName, 
                Boards = BoardDto.BoardToDto(user.Boards),
            };

            return res;
        }
    }
}
