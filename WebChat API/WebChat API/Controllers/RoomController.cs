using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebChat_API.Contexts;
using WebChat_API.Models;

namespace WebChat_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("[action]/{userName}/{chatsListID}")]
        public async Task<Room> GetActiveRoom([FromServices] Context dbContext, string userName, long chatsListID)
        {
            var room = await dbContext.room.FirstOrDefaultAsync(room => room.UserName == userName && room.ChatsListID == chatsListID);
            return room ?? new Room();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("[action]/{userName}/{chatsListID}")]
        public async Task<IActionResult> PostCreateRoom([FromServices] Context dbContext, [FromBody] Room room)
        {
            await dbContext.room.AddAsync(room);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("[action]/{userName}/{chatsListID}")]
        public async Task<IActionResult> DeleteUserNameChatsListIDRoom([FromServices] Context dbContext, string userName, long chatsListID)
        {
            var actualUserChatListRoom = await dbContext.room.FindAsync(userName, chatsListID);
            if (actualUserChatListRoom is not null)
            {
                dbContext.Remove(actualUserChatListRoom);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
