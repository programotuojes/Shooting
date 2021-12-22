namespace API.Authorization {
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Http;

  public class JwtMiddleware {
    private readonly RequestDelegate next;

    public JwtMiddleware(RequestDelegate next) {
      this.next = next;
    }

    public async Task Invoke(HttpContext context, UserService userService, JwtUtils jwtUtils) {
      if (context.Request.Path == "/API/users") {
        await next(context);
        return;
      }

      var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last() ?? string.Empty;
      var userId = jwtUtils.ValidateJwtToken(token);

      if (userId != null) {
        // Attach user to context on successful JWT validation
        context.Items["User"] = userService.GetById(userId.Value);
      }

      await next(context);
    }
  }
}
