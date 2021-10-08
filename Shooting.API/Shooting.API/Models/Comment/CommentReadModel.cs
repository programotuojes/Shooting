namespace Shooting.API.Models.Comment {
  using System;

  public class CommentReadModel {
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
  }
}
