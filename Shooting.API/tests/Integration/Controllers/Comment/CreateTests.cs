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

  public class CreateTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly PostClient postClient;
    private readonly CommentClient commentClient;

    public CreateTests(TestFixture fixture) {
      this.fixture = fixture;
      postClient = new PostClient(fixture.Client);
      commentClient = new CommentClient(fixture.Client);
    }

    [Theory]
    [CustomAutoData]
    public async Task PostExists_ReturnsCreatedComment(CommentCreateModel model) {
      // Arrange
      var admin = fixture.Admin;
      var post = await postClient.CreateAsync(admin.Token);
      var dateTimeBefore = DateTime.UtcNow;

      // Act
      var result = await commentClient.CreateAsync(post.Id, model, admin.Token);

      // Assert
      result.Body.Should().Be(model.Body);
      result.CreatedOn.Should().BeAfter(dateTimeBefore).And.BeBefore(DateTime.UtcNow);
      result.CreatedBy.Should().Be(admin.Username);
    }

    [Theory]
    [CustomAutoData]
    public async Task PostDoesNotExist_ReturnsNotFound(CommentCreateModel model) {
      // Arrange
      var admin = fixture.Admin;

      // Act
      var result = await commentClient.SendAsync(
        $"API/posts/{Guid.NewGuid()}/comments",
        HttpMethod.Post,
        new JsonContent(model),
        admin.Token);

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [CustomAutoData]
    public async Task NoToken_ReturnsForbidden(CommentCreateModel model) {
      // Arrange

      // Act
      var result = await commentClient.SendAsync(
        $"API/posts/{Guid.NewGuid()}/comments",
        HttpMethod.Post,
        new JsonContent(model));

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
  }
}
