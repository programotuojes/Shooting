namespace Shooting.API.Models.Competition {
  using System;
  using System.Collections.Generic;
  using Competitor;

  public class CompetitionCreateModel {
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public ICollection<CompetitorCreateModel> Competitors { get; set; } = new List<CompetitorCreateModel>();
  }
}
