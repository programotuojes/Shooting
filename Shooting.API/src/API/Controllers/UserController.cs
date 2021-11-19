namespace API.Controllers {
  using System.Collections.Generic;
  using Authorization;
  using DB.Entities;
  using Microsoft.AspNetCore.Mvc;
  using Models.Users;

  [Authorize]
  [ApiController]
  [Route("API/users")]
  public class UserController : ControllerBase {
    private readonly UserService userService;

    public UserController(UserService userService) {
      this.userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult<AuthResponseModel> Register(AuthRequestModel model) {
      return Ok(userService.CreateAccount(model));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<AuthResponseModel> Login(AuthRequestModel model) {
      var response = userService.Authenticate(model);
      return Ok(response);
    }

    [Authorize(Role.Admin)]
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll() {
      var users = userService.GetAll();
      return Ok(users);
    }
  }
}
