namespace Shooting.Database.Entities {
  using System;
  using System.Collections.Generic;

  public class Competition {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public ICollection<Competitor> Competitors { get; set; } = new List<Competitor>();
  }
}
