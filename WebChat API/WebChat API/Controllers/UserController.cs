using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebChat_API.Contexts;
using WebChat_API.Models;

namespace WebChat_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("[action]/{userName}")]
        public async Task<bool> GetIsActiveUser([FromServices] Context dbContext, string userName)
        {
            var user = await dbContext.user.FirstOrDefaultAsync(user => user.UserName == userName && user.StatusID == 1);
            return (user is not null) ? true : false;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[action]/{userName}/{password}")]
        public async Task<bool> GetIsActiveUserCredentials([FromServices] Context dbContext, string userName, string password)
        {
            var user = await dbContext.user.FirstOrDefaultAsync(user => user.UserName == userName && user.Password == password && user.StatusID == 1);
            return (user is not null) ? true : false;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("[action]/{userName}")]
        public async Task<User> GetActiveUser([FromServices] Context dbContext, string userName)
        {
            var user = await dbContext.user.FirstOrDefaultAsync(user => user.UserName == userName && user.StatusID == 1);
            return user ?? new User();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> PostCreateUser([FromServices] Context dbContext, [FromBody] User user)
        {
            await dbContext.user.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("[action]/{username}")]
        public async Task<IActionResult> PutUpdateUserInformation([FromServices] Context dbContext, [FromBody] User user, string username)
        {
            var actualUser = await dbContext.user.FindAsync(username);
            if (actualUser is not null)
            {
                actualUser.Password = user.Password;
                actualUser.FirstName = user.FirstName;
                actualUser.LastName = user.LastName;
                actualUser.Birthdate = user.Birthdate;
                actualUser.Email = user.Email;
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("[action]/{username}")]
        public async Task<IActionResult> PutDeleteUserStatus([FromServices] Context dbContext, string username)
        {
            var actualUser = await dbContext.user.FindAsync(username);
            if (actualUser is not null)
            {
                actualUser.StatusID = 2;
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("[action]/{username}")]
        public async Task<IActionResult> PutReactiveUserStatus([FromServices] Context dbContext, string username)
        {
            var actualUser = await dbContext.user.FindAsync(username);
            if (actualUser is not null)
            {
                actualUser.StatusID = 1;
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
