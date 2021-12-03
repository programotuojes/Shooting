namespace Unit.Services.CommentService {
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
    public void CommentExists_DeletedAndReturnsTrue(
      DataContext dataContext,
      CommentService service,
      Comment comment
    ) {
      // Arrange
      dataContext.Comments.Add(comment);
      dataContext.SaveChanges();

      // Act
      var result = service.Delete(comment.PostId, comment.Id);

      // Assert
      result.Should().BeTrue();
      dataContext.Comments.Find(comment.Id).Should().BeNull();
    }

    [Theory]
    [CustomAutoData]
    public void CommentDoesNotExist_ReturnsFalse(CommentService service) {
      // Arrange

      // Act
      var result = service.Delete(Guid.Empty, Guid.Empty);

      // Assert
      result.Should().BeFalse();
    }
  }
}
