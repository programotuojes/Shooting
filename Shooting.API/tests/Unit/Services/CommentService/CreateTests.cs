namespace Unit.Services.CommentService {
  using System;
  using API.Models.Comment;
  using API.Services;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Xunit;

  public class CreateTests {

    [Theory]
    [CustomAutoData]
    public void PostExists_ReturnsCreatedComment(
      DataContext dataContext,
      CommentService service,
      CommentCreateModel model,
      Post post,
      User user
    ) {
      // Arrange
      dataContext.Posts.Add(post);
      dataContext.SaveChanges();

      // Act
      var result = service.Create(post.Id, model, user.Id);

      // Assert
      result.Should().NotBeNull();
      result!.Body.Should().Be(model.Body);
      result.CreatedBy.Should().Be(user.Username);
    }

    [Theory]
    [CustomAutoData]
    public void PostDoesNotExist_ReturnsNull(
      CommentService service,
      CommentCreateModel model,
      User user
    ) {
      // Arrange

      // Act
      var result = service.Create(Guid.Empty, model, user.Id);

      // Assert
      result.Should().BeNull();
    }
  }
}
