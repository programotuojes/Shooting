namespace Unit.Services.PostService {
  using API.Services;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Xunit;

  public class GetCreatedByIdTests {

    [Theory]
    [CustomAutoData]
    public void PostExists_ReturnsUserId(
      DataContext dataContext,
      PostService service,
      Post post
    ) {
      // Arrange
      dataContext.Posts.Add(post);
      dataContext.SaveChanges();

      // Act
      var result = service.GetCreatedById(post.Id);

      // Assert
      result.Should().Be(post.CreatedBy.Id);
    }
  }
}
