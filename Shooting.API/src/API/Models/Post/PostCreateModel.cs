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

    [MinLength(2)]
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Url]
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string ImageLabel { get; set; } = string.Empty;
  }
}
