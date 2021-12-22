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

  public class ReadAllTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly PostClient postClient;
    private readonly CommentClient commentClient;

    public ReadAllTests(TestFixture fixture) {
      this.fixture = fixture;
      postClient = new PostClient(fixture.Client);
      commentClient = new CommentClient(fixture.Client);
    }

    [Theory]
    [CustomAutoData]
    public async Task TwoCommentsExist_ReturnsList(CommentCreateModel model1, CommentCreateModel model2) {
      // Arrange
      var admin = fixture.Admin;
      var post = await postClient.CreateAsync(admin.Token);
      var comment1 = await commentClient.CreateAsync(post.Id, model1, admin.Token);
      var comment2 = await commentClient.CreateAsync(post.Id, model2, admin.Token);

      // Act
      var result = await commentClient.ReadAllAsync(post.Id);

      // Assert
      result.Should()
        .HaveCount(2).And
        .ContainEquivalentOf(comment1).And
        .ContainEquivalentOf(comment2);
    }

    [Fact]
    public async Task CommentDoesNotExist_ReturnsEmptyList() {
      // Arrange
      var admin = fixture.Admin;
      var post = await postClient.CreateAsync(admin.Token);

      // Act
      var result = await commentClient.ReadAllAsync(post.Id);

      // Assert
      result.Should().BeEmpty();
    }

    [Fact]
    public async Task PostDoesNotExist_ReturnsNotFound() {
      // Arrange

      // Act
      var result = await commentClient.SendAsync($"API/posts/{Guid.NewGuid()}/comments", HttpMethod.Get);

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
  }
}
