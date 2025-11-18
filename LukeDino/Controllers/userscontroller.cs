using FirebaseAdmin.Auth;
using LukeDino.Classes.Dtos;
using LukeDino.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LukeDino.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class userscontroller(IUserService userService, IDinoService dinoService) : ControllerBase
    {
        private FirebaseToken FirebaseUser
        {
            get
            {
                return HttpContext.Items["FirebaseUser"] as FirebaseToken;
            }
        }

        /// <summary>
        /// Gets all userss
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var result = await userService.GetUsersAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Confirm that a user is member, adds a random dino
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPut("{userId}/confirm")]
        public async Task<IActionResult> ConfirmUser(int userId)
        {
            try
            {

                await userService.ConfirmUserAsync(userId);                
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Adds a new user
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> AddUserAsync(UserDto form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await userService.CreateUserAsync(form);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }

    }
}
