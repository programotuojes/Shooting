namespace Integration.Generators {
  using System;
  using System.Collections.Generic;
  using API.Models.Competition;
  using API.Models.Competitor;
  using FluentAssertions.Extensions;
  using Utils;

  public static class CompetitionGenerator {

    public static CompetitionCreateModel CreateModel(Action<CompetitionCreateModel>? creator = null) {
      var model = new CompetitionCreateModel {
        Name = "Test competition",
        DateFrom = DateTime.UtcNow.Truncate(1.Seconds()),
        DateTo = DateTime.UtcNow.Add(7.Days()).Truncate(1.Seconds()),
        Competitors = new List<CompetitorCreateModel> {
          new() { FirstName = "Alice", LastName = "Alison", BirthYear = 1999 },
          new() { FirstName = "Bob", LastName = "Bobbin", BirthYear = 2000 }
        }
      };

      creator?.Invoke(model);
      return model;
    }
  }
}
