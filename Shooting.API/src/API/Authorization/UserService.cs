namespace API.Authorization {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using DB;
  using DB.Entities;
  using Models.Users;
  using BCryptNet = BCrypt.Net.BCrypt;

  public class UserService {
    private readonly DataContext dataContext;
    private readonly JwtUtils jwtUtils;

    public UserService(DataContext dataContext, JwtUtils jwtUtils) {
      this.dataContext = dataContext;
      this.jwtUtils = jwtUtils;
    }

    public AuthResponseModel CreateAccount(AuthRequestModel model) {
      var exists = dataContext.Users.Any(x => x.Username == model.Username);
      if (exists) throw new AppException("Username is taken");

      var user = new User {
        Username = model.Username,
        Password = BCryptNet.HashPassword(model.Password)
      };

      dataContext.Users.Add(user);
      dataContext.SaveChanges();

      var jwtToken = jwtUtils.GenerateJwtToken(user);
      return new AuthResponseModel(user, jwtToken);
    }

    public AuthResponseModel Authenticate(AuthRequestModel model) {
      var user = dataContext.Users.SingleOrDefault(x => x.Username == model.Username);

      if (user == null || !BCryptNet.Verify(model.Password, user.Password))
        throw new AppException("Username or password is incorrect");

      // Authentication successful so generate jwt token
      var jwtToken = jwtUtils.GenerateJwtToken(user);

      return new AuthResponseModel(user, jwtToken);
    }

    public User GetById(Guid id) {
      var user = dataContext.Users.Find(id);
      if (user == null) throw new KeyNotFoundException("User not found");
      return user;
    }
  }
}
