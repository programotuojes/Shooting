namespace Unit.Services.CompetitionService {
  using API.Models.Competition;
  using API.Services;
  using Customizations;
  using FluentAssertions;
  using Xunit;

  public class CreateTests {
    [Theory]
    [CustomAutoData]
    public void CreateNewCompetition_ReturnsCreatedCompetition(
      CompetitionService service,
      CompetitionCreateModel model
    ) {
      // Arrange

      // Act
      var result = service.Create(model);

      // Assert
      result.Name.Should().Be(model.Name);
      result.DateFrom.Should().Be(model.DateFrom);
      result.DateTo.Should().Be(model.DateTo);
    }
  }
}
