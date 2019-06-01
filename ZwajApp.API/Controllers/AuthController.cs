using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZwajApp.API.Data;
using ZwajApp.API.Dtos;
using ZwajApp.API.Models;

namespace ZwajApp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody]UserFoRegisterDto userFoRegisterDto) //[ApiController]
        public async Task<IActionResult> Register(UserFoRegisterDto userFoRegisterDto)
        {
            // Validation
            // if(!ModelState.IsValid) return BadRequest(ModelState); //[ApiController]
            userFoRegisterDto.Username = userFoRegisterDto.Username.ToLower();
            if (await _repo.UserExists(userFoRegisterDto.Username))
                return BadRequest("هذا المستخدم مسجل من قبل");
            var userToCreate = new User
            {
                Username = userFoRegisterDto.Username
            };
            var createdUser = await _repo.Register(userToCreate, userFoRegisterDto.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
            if (userFromRepo == null) return Unauthorized();

            // Claims 
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.Username)
            };
            // Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            // Credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            // tokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }


    }

}
