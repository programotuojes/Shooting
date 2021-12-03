namespace Unit.Services.PostService {
  using API.Models.Post;
  using API.Services;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Xunit;

  public class ReadTests {

    [Theory]
    [CustomAutoData]
    public void PostExists_PostReturned(
      DataContext dataContext,
      PostService service,
      Post post
    ) {
      // Arrange
      dataContext.Posts.Add(post);
      dataContext.SaveChanges();

      // Act
      var result = service.Read(post.Id);

      // Assert
      result.Should()
        .BeEquivalentTo(new PostReadModel {
          Id = post.Id,
          Title = post.Title,
          Body = post.Body,
          CreatedBy = post.CreatedBy.Username,
          CreatedOn = post.CreatedOn
        });
    }
  }
}
