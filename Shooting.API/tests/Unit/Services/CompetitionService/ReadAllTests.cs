namespace Unit.Services.CompetitionService {
  using API.Models.Competition;
  using API.Services;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Xunit;

  public class ReadAllTests {

    [Theory]
    [CustomAutoData]
    public void CompetitionExists_CompetitionReturned(
      DataContext dataContext,
      CompetitionService service,
      Competition competition
    ) {
      // Arrange
      dataContext.Competitions.Add(competition);
      dataContext.SaveChanges();

      // Act
      var result = service.ReadAll();

      // Assert
      result.Should()
        .ContainEquivalentOf(new CompetitionReadAllModel {
          Id = competition.Id,
          Name = competition.Name,
          DateFrom = competition.DateFrom,
          DateTo = competition.DateTo
        });
    }
  }
}
