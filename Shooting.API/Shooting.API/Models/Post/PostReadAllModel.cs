namespace Shooting.API.Models.Post {
  using System;

  public class PostReadAllModel {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
  }
}
