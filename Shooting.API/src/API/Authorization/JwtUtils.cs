namespace API.Authorization {
  using System;
  using System.IdentityModel.Tokens.Jwt;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;
  using DB.Entities;
  using Microsoft.Extensions.Options;
  using Microsoft.IdentityModel.Tokens;

  public class JwtUtils {
    private readonly JwtConfig jwtConfig;

    public JwtUtils(IOptions<JwtConfig> jwtConfig) {
      this.jwtConfig = jwtConfig.Value;
    }

    public string GenerateJwtToken(User user) {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
        Expires = DateTime.UtcNow.AddHours(12),
        SigningCredentials =
          new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }

    public Guid? ValidateJwtToken(string token) {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);
      try {
        tokenHandler.ValidateToken(token,
          new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,

            // Set ClockSkew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            ClockSkew = TimeSpan.Zero
          },
          out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

        // Return user id from JWT token if validation successful
        return userId;
      } catch {
        // Return null if validation fails
        return null;
      }
    }
  }
}
