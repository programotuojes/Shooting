namespace Unit.Services.CompetitionService {
  using API.Models.Competition;
  using API.Services;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Xunit;

  public class ReadTests {

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
      var result = service.Read(competition.Id);

      // Assert
      result.Should()
        .BeEquivalentTo(new CompetitionReadModel {
          Id = competition.Id,
          Name = competition.Name,
          DateFrom = competition.DateFrom,
          DateTo = competition.DateTo
        });
    }
  }
}
