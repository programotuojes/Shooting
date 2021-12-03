namespace Unit.Services.PostService {
  using System;
  using API.Models.Post;
  using API.Services;
  using Customizations;
  using DB.Entities;
  using FluentAssertions;
  using FluentAssertions.Extensions;
  using Xunit;

  public class CreateTests {

    [Theory]
    [CustomAutoData]
    public void NewPost_ReturnsCreatedPost(
      PostService service,
      PostCreateModel model,
      User user
    ) {
      // Arrange
      var dateTimeBefore = DateTime.UtcNow;

      // Act
      var result = service.Create(model, user.Id);

      // Assert
      result.CreatedOn.Should().BeCloseTo(dateTimeBefore, 10.Seconds());
      result.Title.Should().Be(model.Title);
      result.Body.Should().Be(model.Body);
      result.CreatedBy.Should().Be(user.Username);
    }
  }
}
