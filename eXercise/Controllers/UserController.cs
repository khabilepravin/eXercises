using eXercise.Entities;
using eXercise.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace eXercise.Controllers
{
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;        
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger,                                    
                                   IUserService userRepository)
        { 
            _logger = logger;            
            _userService = userRepository;
        }

        [HttpGet("user")]
        public async Task<ActionResult<User>> GetUserAsync()
        {
            var user = await _userService.GetUserAsync();

            if (user == null)
            {
                return NotFound("User not found");
            }
            else
            {
                return user;
            }
        }       
    }
}
