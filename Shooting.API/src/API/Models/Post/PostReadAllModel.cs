namespace API.Models.Post {
  using System;

  public class PostReadAllModel {
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
  }
}
