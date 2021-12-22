namespace Integration.Controllers.Comment {
  using System;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using API.Models.Comment;
  using Clients;
  using Customizations;
  using FluentAssertions;
  using Xunit;

  public class ReadTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly PostClient postClient;
    private readonly CommentClient commentClient;

    public ReadTests(TestFixture fixture) {
      this.fixture = fixture;
      postClient = new PostClient(fixture.Client);
      commentClient = new CommentClient(fixture.Client);
    }

    [Theory]
    [CustomAutoData]
    public async Task CommentExists_ReturnsComment(CommentCreateModel model) {
      // Arrange
      var admin = fixture.Admin;
      var post = await postClient.CreateAsync(admin.Token);
      var comment = await commentClient.CreateAsync(post.Id, model, admin.Token);

      // Act
      var result = await commentClient.ReadAsync(post.Id, comment.Id);

      // Assert
      result.Should().BeEquivalentTo(comment);
    }

    [Fact]
    public async Task CommentDoesNotExist_ReturnsNotFound() {
      // Arrange
      var admin = fixture.Admin;
      var post = await postClient.CreateAsync(admin.Token);

      // Act
      var result = await commentClient.SendAsync(
        $"API/posts/{post.Id}/comments/{Guid.NewGuid()}",
        HttpMethod.Get);

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
  }
}
