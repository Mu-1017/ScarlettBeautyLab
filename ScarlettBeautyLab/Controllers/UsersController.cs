using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
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
        private readonly IAuthorizationService _authorizationService;

        public UsersController(IUserService userService, IAuthorizationService authorizationService)
        {
            this._userService = userService;
            this._authorizationService = authorizationService;
        }
        // GET: api/Users
        [Authorize]
        [HttpGet(Name = nameof(GetVisibleUsers))]
        public async Task<ActionResult<List<User>>> GetVisibleUsers()
        {
            if(User.Identity.IsAuthenticated)
            {
                var canSeeEveryone = await _authorizationService.AuthorizeAsync(User, "ViewAllUsersPolicy");
                if (canSeeEveryone.Succeeded)
                {
                    //Admin
                    return await _userService.GetUsersAsync();
                }
                else
                {
                    //Guest
                    var users = new List<User>();
                    var user = await _userService.GetUserAsync(User);
                    users.Add(user);
                    return users;

                }
            }

            return BadRequest(new OpenIdConnectResponse
            {
                Error = OpenIdConnectConstants.Errors.InvalidGrant,
                ErrorDescription = "The user is not logged in."
            });
        }

        [HttpGet("currentuser")]
        [ProducesResponseType(401)]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var user = await _userService.GetUserAsync(User);
            if(user == null)
            {
                return BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "The user does not exist."
                });
            }

            return user;
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
                Url.Link(nameof(UsersController.GetVisibleUsers), null), //TODO: link to userinfo
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
