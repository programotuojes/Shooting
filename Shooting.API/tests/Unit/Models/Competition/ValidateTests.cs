namespace Unit.Models.Competition {
  using System;
  using System.ComponentModel.DataAnnotations;
  using API.Models.Competition;
  using AutoFixture.Xunit2;
  using FluentAssertions;
  using FluentAssertions.Extensions;
  using Xunit;

  public class ValidateTests {

    [Theory]
    [AutoData]
    public void DateFromBeforeDateTo_NoError(CompetitionCreateModel model) {
      // Arrange
      model.DateFrom = DateTime.UtcNow;
      model.DateTo = DateTime.UtcNow.Add(10.Days());

      // Act
      var results = model.Validate(new ValidationContext(model));

      // Assert
      results.Should().BeEmpty();
    }

    [Theory]
    [AutoData]
    public void DateFromAfterDateTo_Error(CompetitionCreateModel model) {
      // Arrange
      model.DateFrom = DateTime.UtcNow;
      model.DateTo = DateTime.UtcNow.Subtract(10.Days());

      // Act
      var results = model.Validate(new ValidationContext(model));

      // Assert
      results.Should().HaveCount(1);
    }
  }
}
