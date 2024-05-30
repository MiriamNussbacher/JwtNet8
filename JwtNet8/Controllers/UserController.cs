using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop5783.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ILogger<UserController> _logger;
        const string filePath = "users.txt";
        IUserService _userService;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _logger = logger;
            _logger.LogInformation("logedtvytvyt");
            _userService = userService;
           
        }


        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get([FromQuery] string email, [FromQuery] string password)
        {
            var foundUser = await _userService.getUser(email, password);
            if (foundUser == null)
                return NoContent();
            else
                Response.Cookies.Append("X-Access-Token", foundUser.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

                return Ok(foundUser);
        }
    

    // GET api/<UserController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
            var user = await _userService.getUserById(id);   
                if (user == null)
                    return NoContent();
                else
                    return Ok(user);

        }

    // POST api/<UserController>
    [HttpPost]
    public async Task<ActionResult<User>> Post([FromBody] User user)
    {
            if(await _userService.saveUser(user)!=null)
                return NoContent();
            return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);

    }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [Authorize]
    public async Task Put(int id, [FromBody] User userToUpdate)
    {
            await _userService.updateUser(id, userToUpdate);
    }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
}
