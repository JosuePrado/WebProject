using Microsoft.AspNetCore.Mvc;
using WebProject.Domain;
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
        return createdUser == null ? BadRequest() : NotFound();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] string Email, string Password)
    {
        User user = await _userManager.GetByEmail(Email);
        if (user == null)
        {
            return NotFound();
        }
        if (user.Email == Email && user.Password == Password)
        {
            return Ok();
        }
        return BadRequest();
    }

}
