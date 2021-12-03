namespace API.Models.Post {
  using System.ComponentModel.DataAnnotations;

  public class PostCreateModel {
    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MinLength(2)]
    [MaxLength(10_000)]
    public string Body { get; set; } = string.Empty;
  }
}
