namespace Unit.Services.PostService {
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
    public void PostExists_DeletedAndReturnsTrue(
      DataContext dataContext,
      PostService service,
      Post post
    ) {
      // Arrange
      dataContext.Posts.Add(post);
      dataContext.SaveChanges();

      // Act
      var result = service.Delete(post.Id);

      // Assert
      result.Should().BeTrue();
      dataContext.Posts.Find(post.Id).Should().BeNull();
    }

    [Theory]
    [CustomAutoData]
    public void PostDoesNotExist_ReturnsFalse(PostService service) {
      // Arrange

      // Act
      var result = service.Delete(Guid.Empty);

      // Assert
      result.Should().BeFalse();
    }
  }
}
