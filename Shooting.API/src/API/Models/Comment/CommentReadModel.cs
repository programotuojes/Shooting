namespace API.Models.Comment {
  using System;

  public class CommentReadModel {
    public Guid Id { get; set; }
    public string Body { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
  }
}
