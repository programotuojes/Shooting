namespace API.Models.Post {
  using System;
  using System.Collections.Generic;
  using Comment;

  public class PostReadModel {
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ImageLabel { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public ICollection<CommentReadModel> Comments { get; set; } = new List<CommentReadModel>();
  }
}
