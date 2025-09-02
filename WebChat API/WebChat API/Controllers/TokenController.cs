using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public Models.Token GetToken([FromBody] Models.User user)
        {
            var token = new Models.Token();
            var applicationName = user.UserName;
            var expirationDateTime = DateTime.Now.AddMinutes(30);
            token.TokenString = CustomTokenJWT(applicationName, expirationDateTime);
            token.expirationTime = expirationDateTime;
            return token;
        }
        private string CustomTokenJWT(string ApplicationName, DateTime token_expiration)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!));
            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var _Header = new JwtHeader(_signingCredentials);
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, ApplicationName)
            };
            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.Now, //cuanto va a durar el token
                    expires: token_expiration // cuando va a expirar el token.
                );
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );
            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}
