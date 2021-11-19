namespace API.Models.Competition {
  using System;

  public class CompetitionReadAllModel {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
  }
}
