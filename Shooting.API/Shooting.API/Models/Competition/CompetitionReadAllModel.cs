namespace Shooting.API.Models.Competition {
  using System;

  public class CompetitionReadAllModel {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
  }
}
