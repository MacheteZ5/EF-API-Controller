using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebChat_API.Models;

namespace WebChat_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public TokenController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [Route("GetToken")]
        [HttpPost]
        public Token GetToken([FromBody] string userName)
        {
            Token token = new();
            token.TokenString = CustomTokenJWT(userName, DateTime.Now.AddMinutes(30));
            token.ExpirationTime = DateTime.Now.AddMinutes(30);
            return token;
        }
        private string CustomTokenJWT(string applicationName, DateTime tokenExpiration)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var _Header = new JwtHeader(_signingCredentials);
            var _Claims = new[] {
                //acá es lo que debemos de modificar las los parametros que vamos a utilizar.
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, applicationName)
            };
            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.Now,
                    expires: tokenExpiration
                );
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );
            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}
