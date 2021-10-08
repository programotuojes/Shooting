namespace Shooting.Database.Entities {
  using System;

  public class Comment {
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid CreatedById { get; set; }
    public User CreatedBy { get; set; }
  }
}
