namespace Unit.Services.CommentService {
  using System;
  using API.Models.Comment;
  using API.Services;
  using AutoFixture;
  using AutoFixture.Xunit2;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Microsoft.EntityFrameworkCore;
  using Moq;
  using Xunit;

  public class CreateTests {

    [Theory]
    [CustomAutoData]
    public void PostExists_ReturnsCreatedComment(
      [Frozen] DataContext dataContext,
      CommentService service,
      CommentCreateModel model,
      Post post
    ) {
      // Arrange
      Mock.Get(dataContext.Posts)
        .Setup(x => x.Find(post.Id))
        .Returns(post);

      // Act
      var created = service.Create(post.Id, model, Guid.Empty);

      // Assert
      created.Should().NotBeNull();
      created!.Body.Should().Be(model.Body);
    }
  }
}
