using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScarlettBeautyLab.Models;
using ScarlettBeautyLab.Services;

namespace ScarlettBeautyLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }
        // GET: api/Users
        [HttpGet(Name = nameof(GetVisibleUsers))]
        public async Task<ActionResult<List<User>>> GetVisibleUsers()
        {
            //TODO: Authorization check. Is the user an admin?
            var users = await _userService.GetUsersAsync();
            return users;
        }

        [Authorize]
        [HttpGet("api/[controller]/currentuser", Name = nameof(GetCurrentUser))]
        public async Task<ActionResult<string>> GetCurrentUser()
        {
            //TODO: Authorization check. Is the user an admin?
            return "current user";
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        [HttpPost(Name = nameof(RegisterUser))]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterForm form)
        {
            var (succeeded, message) = await _userService.CreateUserAsync(form);
            if (succeeded) return Created(
                Url.Link(nameof(UsersController.GetVisibleUsers), null), //TODO: link to userino
                null);

            return BadRequest(new ApiError
            {
                Message = "Registration failed.",
                Detail = message
            });
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
