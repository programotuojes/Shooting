namespace Unit.Services.CommentService {
  using System;
  using System.Linq;
  using API.Models.Comment;
  using API.Services;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Xunit;

  public class ReadAllTests {

    [Theory]
    [CustomAutoData]
    public void PostExists_ReturnsCreatedComment(
      DataContext dataContext,
      CommentService service,
      Post post,
      Comment comment
    ) {
      // Arrange
      post.Comments.Add(comment);
      dataContext.Posts.Add(post);
      dataContext.SaveChanges();

      // Act
      var result = service.ReadAll(post.Id)?.ToList();

      // Assert
      result.Should().NotBeNull();
      result.Should()
        .HaveCount(1)
        .And
        .ContainEquivalentOf(new CommentReadModel {
          Id = comment.Id,
          Body = comment.Body,
          CreatedBy = comment.CreatedBy.Username,
          CreatedOn = comment.CreatedOn
        });
    }

    [Theory]
    [CustomAutoData]
    public void PostDoesNotExist_ReturnsNull(CommentService service) {
      // Arrange

      // Act
      var result = service.ReadAll(Guid.Empty);

      // Assert
      result.Should().BeNull();
    }
  }
}
