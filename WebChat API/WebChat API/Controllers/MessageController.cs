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
        [Route("[action]/{firstContactID}")]
        public async Task<IEnumerable<Message>> Get([FromServices] Context dbContext, long firstContactID)
        {
            var chatList = await dbContext.chatsList.FirstOrDefaultAsync(chatList => chatList.ID == firstContactID);
            var secondContacIDRegister = await dbContext.chatsList.FirstOrDefaultAsync(chatsList => chatsList.FirstUser == chatList.SecondUser && chatsList.SecondUser == chatList.FirstUser);
            var secondContactID = secondContacIDRegister.ID;
            var sentMessages = dbContext.message.Where(message => message.ContactID == firstContactID && message.StatusMessageID == 1).OrderBy(message => message.FecTransac);
            var recieveMessages = dbContext.message.Where(message => message.ContactID == secondContactID && message.StatusMessageID == 1).OrderBy(message => message.FecTransac);
            var result = new List<Message>();
            foreach (var mensaje in sentMessages)
            {
                result.Add(mensaje);
            }
            foreach (var mensaje in recieveMessages)
            {
                result.Add(mensaje);
            }
            return result.OrderBy(message => message.ID);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromServices] Context dbContext, [FromBody] Message message)
        {
            await dbContext.message.AddAsync(message);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("[action]/{messageID}")]
        public async Task<IActionResult> Put([FromServices] Context dbContext, [FromBody] Message newMessage, long messageID)
        {
            var categoriaActual = await dbContext.message.FindAsync(messageID);
            if (categoriaActual is not null)
            {
                categoriaActual.Content = (newMessage.Content != categoriaActual.Content && newMessage.Content != string.Empty) ? newMessage.Content : categoriaActual.Content;
                categoriaActual.StatusMessageID = (newMessage.StatusMessageID != categoriaActual.StatusMessageID && newMessage.StatusMessageID != 0) ? newMessage.StatusMessageID : categoriaActual.StatusMessageID;
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
