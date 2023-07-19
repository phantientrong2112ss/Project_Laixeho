using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KGProject_1.Models.Entities;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace KGProject_1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenAController : ControllerBase
    {
        private readonly GPChauffeurContext dbContext;
        private string Secret;

        public TokenAController(GPChauffeurContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            Secret = configuration["AppSettings:Secret"];

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] Account model)
        {
            string username = model.Username.ToString();
            Account user = (Account)dbContext.Accounts.Where(item => item.Username == model.Username && item.Password == model.Password).FirstOrDefault();
            // return null if user not found
            if (user == null)
            {
                return Ok(null);
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Username),
                    new Claim(ClaimTypes.Role,user.Role),
                    new Claim(ClaimTypes.Surname,user.Email),
                    new Claim(ClaimTypes.DenyOnlyWindowsDeviceGroup,user.Password)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tmp = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tmp);
            return Ok(new { UserId = user.Id, Hoten = user.Name, Token = token });
        }
    }
}
