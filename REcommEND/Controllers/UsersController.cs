using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using REcommEND.Models;
using REcommEND.Services;

namespace REcommEND.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    /*EnableCors annotation is an important point here, mainly because it sets the CORS policy this
     * controller must use, the one to be configured soon. Without it, request calls from a different origin
     * (the React application, in this case) won’t be allowed by the server;*/
    [EnableCors("ReactPolicy")]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;
        public UsersController(UserService userService)
        {
            this.userService = userService;
        }
        // GET api/users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return userService.GetAll();
        }
        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(userService.GetById(id));
        }
        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            return CreatedAtAction("Get", new { id = user.Id }, userService.Create(user));
        }
        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            userService.Update(id, user);
            return NoContent();
        }
        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            userService.Delete(id);
            return NoContent();
        }
        public override NoContentResult NoContent()
        {
            return base.NoContent();
        }
    }
}
