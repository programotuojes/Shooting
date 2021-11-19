namespace DB.Entities {
  using System;

  public class User {
    public Guid Id { get; set; }
    public Role Role { get; set; } = Role.User;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
  }
}
