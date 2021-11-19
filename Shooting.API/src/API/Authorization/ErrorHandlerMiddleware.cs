namespace API.Authorization {
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Text.Json;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Http;

  public class ErrorHandlerMiddleware {
    private readonly RequestDelegate next;

    public ErrorHandlerMiddleware(RequestDelegate next) {
      this.next = next;
    }

    public async Task Invoke(HttpContext context) {
      try {
        await next(context);
      } catch (Exception error) {
        var response = context.Response;
        response.ContentType = "application/json";

        response.StatusCode = error switch {
          AppException => (int)HttpStatusCode.BadRequest,
          KeyNotFoundException => (int)HttpStatusCode.NotFound,
          _ => (int)HttpStatusCode.InternalServerError
        };

        var result = JsonSerializer.Serialize(new { message = error.Message });
        await response.WriteAsync(result);
      }
    }
  }
}
