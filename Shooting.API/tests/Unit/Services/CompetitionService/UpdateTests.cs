namespace Unit.Services.CompetitionService {
  using System;
  using API.Models.Competition;
  using API.Services;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Xunit;

  public class UpdateTests {

    [Theory]
    [CustomAutoData]
    public void CompetitionExists_ReturnsUpdatedCompetition(
      DataContext dataContext,
      CompetitionService service,
      Competition competition,
      CompetitionCreateModel model
    ) {
      // Arrange
      var nameBefore = competition.Name;
      var dateFromBefore = competition.DateFrom;
      var dateToBefore = competition.DateTo;

      dataContext.Competitions.Add(competition);
      dataContext.SaveChanges();

      // Act
      var result = service.Update(competition.Id, model);

      // Assert
      result.Should().NotBeNull();
      result!.Name.Should().NotBe(nameBefore);
      result.DateFrom.Should().NotBe(dateFromBefore);
      result.DateTo.Should().NotBe(dateToBefore);

      result.Should()
        .BeEquivalentTo(new CompetitionReadModel {
          Id = competition.Id,
          Name = model.Name,
          DateFrom = model.DateFrom!.Value,
          DateTo = model.DateTo!.Value
        });
    }

    [Theory]
    [CustomAutoData]
    public void CompetitionDoesNotExist_ReturnsNull(
      CompetitionService service,
      CompetitionCreateModel model
    ) {
      // Arrange

      // Act
      var result = service.Update(Guid.Empty, model);

      // Assert
      result.Should().BeNull();
    }
  }
}
