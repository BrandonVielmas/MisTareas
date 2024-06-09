using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MisTareas.API.Data;
using MisTareas.API.Data.Entities;
using MisTareas.API.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MisTareas.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController(MisTareasContext _context, IConfiguration configuration) : ControllerBase
    {
        private readonly MisTareasContext _context = _context;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost("loggin")]
        public async Task<UserLogginDto> Loggin([FromBody] string email)
        {
            User? user = await _context.User.Include(u => u.Boards).FirstOrDefaultAsync(x => x.Email == email);

            string token = GenerateJwtToken(user.UserName);

            UserLogginDto res = new UserLogginDto
            { 
                UserId = user.Id, 
                Email = user.Email, 
                FullName = user.Name + " " + user.LastName, 
                Boards = BoardDto.BoardToDto(user.Boards),
                Token = token
            };

            return res;
        }

        private string GenerateJwtToken(string userName)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName) }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
