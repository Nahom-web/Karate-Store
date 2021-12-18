using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using nhH60Services.Models;

namespace nhH60Services.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ASPUsersController : ControllerBase {

        // GET: api/ASPUsers
        [HttpGet]
        public async Task<ActionResult<AspNetUser>> ASPUsers(string? Email) {
            AspNetUser Users = new AspNetUser();

            try {
                if (Email != null) {
                    var UserFound = await Users.GetUser(Email);
                    if (UserFound.IsManager()) {
                        return UserFound;
                    } else {
                        return BadRequest();
                    }
                } else {
                    return NoContent();
                }
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }


    }
}
