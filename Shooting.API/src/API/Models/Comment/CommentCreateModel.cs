namespace API.Models.Comment {
  using System.ComponentModel.DataAnnotations;

  public class CommentCreateModel {
    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Body { get; set; } = string.Empty;
  }
}
