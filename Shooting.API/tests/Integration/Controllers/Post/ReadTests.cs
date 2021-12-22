namespace Integration.Controllers.Post {
  using System;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using Clients;
  using FluentAssertions;
  using Xunit;

  public class ReadTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly PostClient postClient;

    public ReadTests(TestFixture fixture) {
      this.fixture = fixture;
      postClient = new PostClient(fixture.Client);
    }

    [Fact]
    public async Task PostExists_ReturnsPost() {
      // Arrange
      var user = fixture.Admin;
      var existing = await postClient.CreateAsync(user.Token);

      // Act
      var result = await postClient.ReadAsync(existing.Id);

      // Assert
      result.Should().BeEquivalentTo(existing);
    }

    [Fact]
    public async Task PostDoesNotExist_ReturnsNotFound() {
      // Arrange

      // Act
      var result = await postClient.SendAsync($"API/posts/{Guid.NewGuid()}", HttpMethod.Get);

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
  }
}
