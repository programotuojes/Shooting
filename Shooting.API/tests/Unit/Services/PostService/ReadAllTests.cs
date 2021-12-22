namespace Unit.Services.PostService {
  using API.Models.Post;
  using API.Services;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Xunit;

  public class ReadAllTests {

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
      var result = service.ReadAll();

      // Assert
      result.Should()
        .ContainEquivalentOf(new PostReadAllModel {
          Id = post.Id,
          Title = post.Title,
          Description = post.Description,
          ImageUrl = post.ImageUrl,
          ImageLabel = post.ImageLabel,
          CreatedBy = post.CreatedBy.Username,
          CreatedOn = post.CreatedOn
        });
    }
  }
}
