namespace Unit.Services.CompetitionService {
  using System;
  using API.Services;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Xunit;

  public class DeleteTests {

    [Theory]
    [CustomAutoData]
    public void CompetitionExists_DeletedAndReturnsTrue(
      DataContext dataContext,
      CompetitionService service,
      Competition competition
    ) {
      // Arrange
      dataContext.Competitions.Add(competition);
      dataContext.SaveChanges();

      // Act
      var result = service.Delete(competition.Id);

      // Assert
      result.Should().BeTrue();
      dataContext.Competitions.Find(competition.Id).Should().BeNull();
    }

    [Theory]
    [CustomAutoData]
    public void CompetitionDoesNotExist_ReturnsFalse(CompetitionService service) {
      // Arrange

      // Act
      var result = service.Delete(Guid.Empty);

      // Assert
      result.Should().BeFalse();
    }
  }
}
