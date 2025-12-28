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
    public class MessageController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("[action]/{chatsListID}")]
        public async Task<IEnumerable<Message>> GetAllChatsListMessages([FromServices] Context dbContext, long chatsListID)
        {
            var messages = await dbContext.message.Where(message => message.ChatsListIDRoom == chatsListID).OrderBy(message => message.DateCreated).ToListAsync();
            return messages ?? new List<Message>();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("[action]/{messageID}")]
        public async Task<Message> GetUserChatsListMessage([FromServices] Context dbContext, long messageID)
        {
            var messages = await dbContext.message.FindAsync(messageID);
            return messages ?? new Message();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> PostCreateMessage([FromServices] Context dbContext, [FromBody] Message message)
        {
            await dbContext.message.AddAsync(message);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("[action]/{messageID}")]
        public async Task<IActionResult> PutUpdateMessageInformation([FromServices] Context dbContext, [FromBody] Message newMessage, long messageID)
        {
            var categoriaActual = await dbContext.message.FindAsync(messageID);
            if (categoriaActual is not null)
            {
                categoriaActual.Content = (newMessage.Content != categoriaActual.Content && newMessage.Content != string.Empty) ? newMessage.Content : categoriaActual.Content;
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("[action]/{messageID}")]
        public async Task<IActionResult> PutDeleteMessage([FromServices] Context dbContext, long messageID)
        {
            var categoriaActual = await dbContext.message.FindAsync(messageID);
            if (categoriaActual is not null)
            {
                categoriaActual.StatusMessage = false;
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
