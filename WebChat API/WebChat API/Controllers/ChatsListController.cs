using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebChat_API.Contexts;
using WebChat_API.Models;

namespace WebChat_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsListController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet] 
        [Route("[action]/{userName}")]
        public IQueryable<ChatsList> GetChatList([FromServices] Context dbContext, string userName)
        {
            var chatsList = dbContext.chatsList.Where(chatsList => chatsList.FirstUser == userName).OrderBy(x => x.SecondUser);
            return chatsList;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> PostCreateChatsList([FromServices] Context dbContext, [FromBody] ChatsList chatsList)
        {
            await dbContext.chatsList.AddAsync(chatsList);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("[action]/{firstUserName}/{secondUserName}")]
        public async Task<IActionResult> DeleteChatsList([FromServices] Context dbContext, string firstUserName, string secondUserName)
        {
            var actualChatsList = await dbContext.chatsList.FirstOrDefaultAsync(chat => chat.FirstUser == firstUserName && chat.SecondUser == secondUserName);
            if (actualChatsList is not null)
            {
                dbContext.Remove(actualChatsList.ID);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
