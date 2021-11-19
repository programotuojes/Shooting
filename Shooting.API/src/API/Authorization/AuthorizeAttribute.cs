namespace API.Authorization {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using DB.Entities;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.Filters;

  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class AuthorizeAttribute : Attribute, IAuthorizationFilter {
    private readonly IList<Role> roles;

    public AuthorizeAttribute(params Role[] roles) {
      this.roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context) {
      // Skip authorization if action is decorated with [AllowAnonymous] attribute
      var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

      if (allowAnonymous)
        return;

      // Authorization
      var user = (User?) context.HttpContext.Items["User"];
      if (user == null || roles.Any() && !roles.Contains(user.Role)) {
        // Not logged in or role not authorized
        context.Result = new JsonResult(new { message = "Unauthorized" })
          { StatusCode = StatusCodes.Status401Unauthorized };
      }
    }
  }
}
