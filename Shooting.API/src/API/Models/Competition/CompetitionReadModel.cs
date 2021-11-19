namespace API.Models.Competition {
  using System;
  using System.Collections.Generic;
  using Competitor;

  public class CompetitionReadModel {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public ICollection<CompetitorReadModel> Competitors { get; set; } = new List<CompetitorReadModel>();
  }
}
