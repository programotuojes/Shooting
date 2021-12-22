namespace Integration.Controllers.Comment {
  using System;
  using System.Net;
  using System.Threading.Tasks;
  using API.Models.Comment;
  using Clients;
  using Customizations;
  using FluentAssertions;
  using Xunit;

  public class DeleteAsync : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly PostClient postClient;
    private readonly CommentClient commentClient;

    public DeleteAsync(TestFixture fixture) {
      this.fixture = fixture;
      postClient = new PostClient(fixture.Client);
      commentClient = new CommentClient(fixture.Client);
    }

    [Theory]
    [CustomAutoData]
    public async Task CommentExists_ReturnsNoContent(CommentCreateModel model) {
      // Arrange
      var admin = fixture.Admin;
      var post = await postClient.CreateAsync(admin.Token);
      var comment = await commentClient.CreateAsync(post.Id, model, admin.Token);

      // Act
      var result = await commentClient.DeleteAsync(post.Id, comment.Id, admin.Token);

      // Assert
      result.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task CommentDoesNotExist_ReturnsNotFound() {
      // Arrange
      var admin = fixture.Admin;
      var post = await postClient.CreateAsync(admin.Token);

      // Act
      var result = await commentClient.DeleteAsync(post.Id, Guid.NewGuid(), admin.Token);

      // Assert
      result.Should().Be(HttpStatusCode.NotFound);
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
      var result = await commentClient.DeleteAsync(post.Id, comment.Id, otherUser.Token);

      // Assert
      result.Should().Be(HttpStatusCode.Forbidden);
    }
  }
}
