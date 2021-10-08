namespace Shooting.Database.Entities {
  using System;
  using System.Collections.Generic;

  public class Post {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid CreatedById { get; set; }
    public User CreatedBy { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
  }
}
