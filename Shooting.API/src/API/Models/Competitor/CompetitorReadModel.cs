namespace API.Models.Competitor {
  using System;

  public class CompetitorReadModel {
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int BirthYear { get; set; }
    public float? Result { get; set; }
  }
}
