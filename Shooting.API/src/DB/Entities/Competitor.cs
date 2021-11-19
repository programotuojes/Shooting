namespace DB.Entities {
  using System;

  public class Competitor {
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int BirthYear { get; set; }
    public float? Result { get; set; }
    public Competition Competition { get; set; } = null!;
  }
}
