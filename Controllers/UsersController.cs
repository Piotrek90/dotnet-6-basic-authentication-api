namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Services;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
    {
        var user = await _userService.Authenticate(model.Username, model.Password);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthenticateModel model)
    {
        User user = new User();
        user.Username = model.Username;
        user.Password = model.Password;
        var resultUser = await _userService.Register(user);


        return CreatedAtAction("Register",resultUser);
    }
}
