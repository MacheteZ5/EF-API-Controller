using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebChat_API.Contexts;

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
            var user = await dbContext.user.FirstOrDefaultAsync(user => user.UserName == userName && user.StatusUserID == 1);
            var exists = (user is not null) ? true : false;
            return exists;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("[action]/{userName}")]
        public async Task<Models.User> GetActiveUser([FromServices] Context dbContext, string userName)
        {
            var user = await dbContext.user.FirstOrDefaultAsync(user => user.UserName == userName && user.StatusUserID == 1);
            return user ?? new Models.User();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> PostCreateUser([FromServices] Context dbContext, [FromBody] Models.User user)
        {
            await dbContext.user.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("[action]/{username}")]
        public async Task<IActionResult> PutUpdateUserInformation([FromServices] Context dbContext, [FromBody] Models.User user, string username)
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
        public async Task<IActionResult> PutUpdateUserStatus([FromServices] Context dbContext, string username)
        {
            var actualUser = await dbContext.user.FindAsync(username);
            if (actualUser is not null)
            {
                actualUser.StatusUserID = 2;
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
