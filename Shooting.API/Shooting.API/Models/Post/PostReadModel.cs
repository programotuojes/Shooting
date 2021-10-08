namespace Shooting.API.Models.Post {
  using System;
  using System.Collections.Generic;
  using Comment;

  public class PostReadModel {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public ICollection<CommentReadModel> Comments { get; set; } = new List<CommentReadModel>();
  }
}
