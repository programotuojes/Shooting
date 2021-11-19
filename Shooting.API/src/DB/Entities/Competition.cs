namespace DB.Entities {
  using System;
  using System.Collections.Generic;

  public class Competition {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public ICollection<Competitor> Competitors { get; set; } = new List<Competitor>();
  }
}
