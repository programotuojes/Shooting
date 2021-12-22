namespace Integration.Controllers.Post {
  using System;
  using System.Net;
  using System.Threading.Tasks;
  using Clients;
  using FluentAssertions;
  using Xunit;

  public class DeleteTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly PostClient postClient;

    public DeleteTests(TestFixture fixture) {
      this.fixture = fixture;
      postClient = new PostClient(fixture.Client);
    }

    [Fact]
    public async Task DeleteExistingPost_PostDeleted() {
      // Arrange
      var user = fixture.Admin;
      var createdPost = await postClient.CreateAsync(user.Token);

      // Act
      var statusCode = await postClient.DeleteAsync(createdPost.Id, user.Token);

      // Assert
      statusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteOtherUserPost_Forbidden() {
      // Arrange
      var user = fixture.Admin;
      var otherUser = await fixture.CreateAdminUserAsync("Other admin user");
      var createdPost = await postClient.CreateAsync(user.Token);

      // Act
      var statusCode = await postClient.DeleteAsync(createdPost.Id, otherUser.Token);

      // Assert
      statusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task PostDoesNotExist_NotFound() {
      // Arrange
      var user = fixture.Admin;

      // Act
      var statusCode = await postClient.DeleteAsync(Guid.NewGuid(), user.Token);

      // Assert
      statusCode.Should().Be(HttpStatusCode.NotFound);
    }
  }
}
