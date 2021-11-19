namespace API.Models.Users {
  using System.ComponentModel.DataAnnotations;

  public class AuthRequestModel {
    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(4)]
    [MaxLength(200)]
    public string Password { get; set; } = string.Empty;
  }
}
