namespace Unit.Services.CommentService {
  using System;
  using API.Models.Comment;
  using API.Services;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Xunit;

  public class UpdateTests {

    [Theory]
    [CustomAutoData]
    public void CommentExists_ReturnsUpdatedComment(
      DataContext dataContext,
      CommentService service,
      Comment comment,
      CommentCreateModel model
    ) {
      // Arrange
      var bodyBefore = comment.Body;
      dataContext.Comments.Add(comment);
      dataContext.SaveChanges();

      // Act
      var result = service.Update(comment.PostId, comment.Id, model);

      // Assert
      result.Should().NotBeNull();
      result!.Should().NotBe(bodyBefore);
      result.Should()
        .BeEquivalentTo(new CommentReadModel {
          Id = comment.Id,
          Body = model.Body,
          CreatedBy = comment.CreatedBy.Username,
          CreatedOn = comment.CreatedOn
        });
    }

    [Theory]
    [CustomAutoData]
    public void CommentDoesNotExist_ReturnsNull(
      CommentService service,
      Comment comment,
      CommentCreateModel model
    ) {
      // Arrange

      // Act
      var result = service.Update(comment.PostId, Guid.Empty, model);

      // Assert
      result.Should().BeNull();
    }
  }
}
