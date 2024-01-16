using ApiDevBP.Models;
using ApiDevBP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiDevBP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService) : base(logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser([FromBody] UserModel user)
        {
            try
            {
                return GetObjectResult(await _userService.CreateUser(user).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return GetErrorObjectResult(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return GetObjectResult(await _userService.GetUsers().ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return GetErrorObjectResult(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel user)
        {
            try
            {
                return GetObjectResult(await _userService.UpdateUser(user).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return GetErrorObjectResult(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] UserModel user)
        {
            try
            {
                return GetObjectResult(await _userService.DeleteUser(user).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return GetErrorObjectResult(ex);
            }
        }

    }
}
