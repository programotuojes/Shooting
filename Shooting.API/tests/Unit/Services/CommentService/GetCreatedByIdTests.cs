namespace Unit.Services.CommentService {
  using API.Services;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Xunit;

  public class GetCreatedByIdTests {

    [Theory]
    [CustomAutoData]
    public void CommentExists_ReturnsUserId(
      DataContext dataContext,
      CommentService service,
      Comment comment
    ) {
      // Arrange
      dataContext.Comments.Add(comment);
      dataContext.SaveChanges();

      // Act
      var result = service.GetCreatedById(comment.Id);

      // Assert
      result.Should().Be(comment.CreatedBy.Id);
    }
  }
}
