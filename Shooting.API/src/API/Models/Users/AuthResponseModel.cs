namespace API.Models.Users {
  using System;
  using DB.Entities;

  public class AuthResponseModel {

    public AuthResponseModel(User user, string token) {
      Id = user.Id;
      Username = user.Username;
      Role = user.Role;
      Token = token;
    }

    public Guid Id { get; set; }
    public string Username { get; set; }
    public Role Role { get; set; }
    public string Token { get; set; }
  }
}
