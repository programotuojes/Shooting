namespace Shooting.API.Models.Competition {
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using Competitor;

  public class CompetitionCreateModel {
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public ICollection<CompetitorCreateModel> Competitors { get; set; } = new List<CompetitorCreateModel>();
  }
}
