namespace Unit.Services.PostService {
  using System;
  using API.Models.Post;
  using API.Services;
  using Customizations;
  using DB;
  using DB.Entities;
  using FluentAssertions;
  using Xunit;

  public class UpdateTests {

    [Theory]
    [CustomAutoData]
    public void PostExists_ReturnsUpdatedPost(
      DataContext dataContext,
      PostService service,
      Post post,
      PostCreateModel model
    ) {
      // Arrange
      var titleBefore = post.Title;
      var bodyBefore = post.Body;
      dataContext.Posts.Add(post);
      dataContext.SaveChanges();

      // Act
      var result = service.Update(post.Id, model);

      // Assert
      result.Should().NotBeNull();
      result!.Title.Should().NotBe(titleBefore);
      result.Body.Should().NotBe(bodyBefore);
      result.Should()
        .BeEquivalentTo(new PostReadModel {
          Id = post.Id,
          Title = model.Title,
          Description = model.Description,
          Body = model.Body,
          ImageUrl = model.ImageUrl,
          ImageLabel = model.ImageLabel,
          CreatedBy = post.CreatedBy.Username,
          CreatedOn = post.CreatedOn
        });
    }

    [Theory]
    [CustomAutoData]
    public void PostDoesNotExist_ReturnsNull(
      PostService service,
      PostCreateModel model
    ) {
      // Arrange

      // Act
      var result = service.Update(Guid.Empty, model);

      // Assert
      result.Should().BeNull();
    }
  }
}
