namespace Shooting.API.Models.Competition {
  using System;
  using System.Collections.Generic;
  using Competitor;

  public class CompetitionReadModel {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public ICollection<CompetitorReadModel> Competitors { get; set; } = new List<CompetitorReadModel>();
  }
}
