namespace Shooting.Database {
  using System;
  using System.Collections.Generic;
  using Entities;

  public static class MockData {
    public static User User1 { get; set; } = new() {
      Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
      Username = "First user"
    };

    public static User User2 { get; set; } = new() {
      Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
      Username = "Second user"
    };

    public static Comment Comment1 { get; set; } = new() {
      Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
      Content = "Comment one",
      CreatedBy = User1,
      CreatedById = User1.Id,
      CreatedOn = DateTime.UtcNow
    };

    public static Comment Comment2 { get; set; } = new() {
      Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
      Content = "Comment two",
      CreatedBy = User2,
      CreatedById = User2.Id,
      CreatedOn = DateTime.UtcNow
    };

    public static Post Post1 { get; set; } = new() {
      Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
      Title = "Post title one",
      Body = "Post body one",
      CreatedById = User1.Id,
      CreatedBy = User1,
      CreatedOn = DateTime.UtcNow,
      Comments = new List<Comment> { Comment1, Comment2 }
    };

    public static Post Post2 { get; set; } = new() {
      Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
      Title = "Post title two",
      Body = "Post body two",
      CreatedById = User2.Id,
      CreatedBy = User2,
      CreatedOn = DateTime.UtcNow,
      Comments = new List<Comment> { Comment2 }
    };

    public static Competitor Competitor1 { get; set; } = new() {
      FirstName = "Gustas",
      LastName = "Klevinskas"
    };

    public static Competitor Competitor2 { get; set; } = new() {
      FirstName = "Greta",
      LastName = "Ramonaitė"
    };

    public static Competition Competition1 { get; set; } = new() {
      Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
      Name = "Local Championship",
      Date = DateTime.UtcNow,
      Competitors = new List<Competitor> { Competitor1, Competitor2 }
    };

    public static Competition Competition2 { get; set; } = new() {
      Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
      Name = "Baltic Cup 2021",
      Date = DateTime.UtcNow,
      Competitors = new List<Competitor> { Competitor1 }
    };

    public static ICollection<Post> Posts { get; set; } = new List<Post> { Post1, Post2 };
    public static ICollection<Comment> Comments { get; set; } = new List<Comment> { Comment1, Comment2 };

    public static ICollection<Competition> Competitions { get; set; } = new List<Competition> { Competition1, Competition2 };
  }
}
