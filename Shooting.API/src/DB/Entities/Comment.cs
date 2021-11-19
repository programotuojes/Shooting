namespace DB.Entities {
  using System;

  public class Comment {
    public Guid Id { get; set; }
    public string Body { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public Guid CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;
    public Guid PostId { get; set; }
  }
}
