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
        public bool GetActiveChatList([FromServices] Context dbContext, string userName)
        {
            var chatlist = dbContext.chatsList.Where(chatsList => chatsList.Name == userName && chatsList.Active).OrderBy(x => x.Name);
            return (chatlist is not null) ? true : false;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("[action]/{userName}")]
        public IQueryable<ChatsList> GetChatList([FromServices] Context dbContext, string userName)
        {
            return dbContext.chatsList.Where(chatsList => chatsList.Name == userName && chatsList.Active).OrderBy(x => x.Name);
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
        [HttpPut]
        [Route("[action]/{name}")]
        public async Task<IActionResult> PutUpdateChatsListInformation([FromServices] Context dbContext, [FromBody] ChatsList newChatsList, string name)
        {
            var actualChatsList = await dbContext.chatsList.FirstOrDefaultAsync(chatsList => chatsList.Name == name && chatsList.Active);
            if (actualChatsList is not null)
            {
                actualChatsList.Name = newChatsList.Name;
                actualChatsList.Description = newChatsList.Description;
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("[action]/{name}")]
        public async Task<IActionResult> PutDeleteChatsList([FromServices] Context dbContext, string name)
        {
            var actualChatsList = await dbContext.chatsList.FirstOrDefaultAsync(chatsList => chatsList.Name == name && chatsList.Active);
            if (actualChatsList is not null)
            {
                actualChatsList.Active = false;
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("[action]/{name}")]
        public async Task<IActionResult> DeleteChatsList([FromServices] Context dbContext, string name)
        {
            var actualChatsList = await dbContext.chatsList.FirstOrDefaultAsync(chat => chat.Name == name);
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
