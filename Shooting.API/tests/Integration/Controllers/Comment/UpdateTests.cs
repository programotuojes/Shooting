namespace Integration.Controllers.Comment {
  using System;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using API.Models.Comment;
  using Clients;
  using Customizations;
  using FluentAssertions;
  using Utils;
  using Xunit;

  public class UpdateTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly PostClient postClient;
    private readonly CommentClient commentClient;

    public UpdateTests(TestFixture fixture) {
      this.fixture = fixture;
      postClient = new PostClient(fixture.Client);
      commentClient = new CommentClient(fixture.Client);
    }

    [Theory]
    [CustomAutoData]
    public async Task CommentExists_ReturnsUpdatedComment(CommentCreateModel model) {
      // Arrange
      var admin = fixture.Admin;
      var post = await postClient.CreateAsync(admin.Token);
      var comment = await commentClient.CreateAsync(post.Id, model, admin.Token);
      model.Body = "Updated comment";

      // Act
      var result = await commentClient.UpdateAsync(post.Id, comment.Id, model, admin.Token);

      // Assert
      result.Should()
        .BeEquivalentTo(new CommentReadModel {
          Id = comment.Id,
          Body = model.Body,
          CreatedBy = comment.CreatedBy,
          CreatedOn = comment.CreatedOn
        });
    }

    [Theory]
    [CustomAutoData]
    public async Task CommentDoesNotExist_ReturnsNotFound(CommentCreateModel model) {
      // Arrange
      var admin = fixture.Admin;
      var post = await postClient.CreateAsync(admin.Token);

      // Act
      var result = await commentClient.SendAsync(
        $"API/posts/{post.Id}/comments/{Guid.NewGuid()}",
        HttpMethod.Put,
        new JsonContent(model),
        admin.Token);

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [CustomAutoData]
    public async Task CommentCreatedByOtherUser_ReturnsForbidden(CommentCreateModel model) {
      // Arrange
      var admin = fixture.Admin;
      var post = await postClient.CreateAsync(admin.Token);
      var comment = await commentClient.CreateAsync(post.Id, model, admin.Token);
      var otherUser = fixture.User;

      // Act
      var result = await commentClient.SendAsync(
        $"API/posts/{post.Id}/comments/{comment.Id}",
        HttpMethod.Put,
        new JsonContent(model),
        otherUser.Token);

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
  }
}
