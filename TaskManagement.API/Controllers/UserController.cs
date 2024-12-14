using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interface.Services;
using FluentValidation;


namespace TaskManagement.API.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                var createdUser = await _userService.CreateUserAsync(user);
                return CreatedAtAction(nameof(CreateUser), new { id = createdUser.Id }, createdUser);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
    
}