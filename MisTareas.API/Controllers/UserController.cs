using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MisTareas.API.Data;
using MisTareas.API.Data.Entities;
using MisTareas.API.Dtos;
using MisTareas.API.Repositories.UserRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MisTareas.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController(MisTareasContext context, IUserRepository userRepository, IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly MisTareasContext _context = context;

        //[HttpPost("create-user")]
        //public System.Threading.Tasks.Task CreateUser()
        //{

        //}
        public class UserLoggin
        {
            public string email { get; set; }
            public string password { get; set; }
        }

        [HttpPost("loggin")]
        public async Task<UserLogginDto> Loggin([FromBody] UserLoggin userLoggin)
        {
            User? user = await _context.User
                .AsNoTracking()
                .Where(x => x.Email == userLoggin.email && x.Password == userLoggin.password)
                .FirstOrDefaultAsync();

            string token = GenerateJwtToken(user.Id.ToString());

            UserLogginDto res = new UserLogginDto
            { 
                UserId = user.Id, 
                Email = user.Email, 
                FullName = user.Name + " " + user.LastName, 
                Token = token
            };

            return res;
        }

        private string GenerateJwtToken(string userId)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) }),
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
