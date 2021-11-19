namespace Unit.Models.Competitor {
  using System.ComponentModel.DataAnnotations;
  using API.Models.Competitor;
  using FluentAssertions;
  using Xunit;

  public class ValidateTests {

    [Theory]
    [InlineData(1799, 1)]
    [InlineData(2000, 0)]
    [InlineData(3000, 1)]
    public void BirthYearSpecified_ExpectedResult(int birthYear, int errorCount) {
      // Arrange
      var model = new CompetitorCreateModel {
        BirthYear = birthYear
      };

      // Act
      var results = model.Validate(new ValidationContext(model));

      // Assert
      results.Should().HaveCount(errorCount);
    }
  }
}
