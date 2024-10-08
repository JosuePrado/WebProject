using Microsoft.AspNetCore.Mvc;
using ShareModels;
using WebProject.Data;
using WebProject.Repositories.Interfaces;

namespace WebProject.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserRepository userManager) : ControllerBase
{

    private readonly IUserRepository _userManager = userManager;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var user = await _userManager.GetById(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userManager.GetAll();
        return users == null ? NotFound() : Ok(users);
    }

    [HttpPost()]
    public async Task<IActionResult> Create([FromBody] User user)
    {
        var createdUser = await _userManager.Add(user);
        return createdUser == null ? BadRequest() : Ok(user);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserModel InputUser)
    {
        User user = await _userManager.GetByEmail(InputUser.Email);
        if (user == null)
        {
            return NotFound();
        }
        if (user.Email == InputUser.Email && user.Password == InputUser.Password)
        {
            UserContext.CurrentUserName = user.Username;
            return Ok(UserContext.CurrentUserName);
        }
        return BadRequest();
    }

}
