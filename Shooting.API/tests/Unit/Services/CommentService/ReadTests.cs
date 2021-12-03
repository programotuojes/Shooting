namespace Unit.Services.CommentService {
  using API.Models.Comment;
  using API.Services;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Xunit;

  public class ReadTests {

    [Theory]
    [CustomAutoData]
    public void CommentExists_CommentReturned(
      DataContext dataContext,
      CommentService service,
      Comment comment
    ) {
      // Arrange
      dataContext.Comments.Add(comment);
      dataContext.SaveChanges();

      // Act
      var result = service.Read(comment.PostId, comment.Id);

      // Assert
      result.Should().NotBeNull();
      result.Should()
        .BeEquivalentTo(new CommentReadModel {
          Id = comment.Id,
          Body = comment.Body,
          CreatedBy = comment.CreatedBy.Username,
          CreatedOn = comment.CreatedOn
        });
    }
  }
}
