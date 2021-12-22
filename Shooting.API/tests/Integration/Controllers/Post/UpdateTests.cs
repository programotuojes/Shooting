namespace Integration.Controllers.Post {
  using System;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using API.Models.Post;
  using Clients;
  using Customizations;
  using FluentAssertions;
  using Utils;
  using Xunit;

  public class UpdateTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly PostClient postClient;

    public UpdateTests(TestFixture fixture) {
      this.fixture = fixture;
      postClient = new PostClient(fixture.Client);
    }

    [Theory]
    [CustomAutoData]
    public async Task PostExists_ReturnsUpdatedPost(PostCreateModel updatedModel) {
      // Arrange
      var user = fixture.Admin;
      var post = await postClient.CreateAsync(user.Token);

      // Act
      var result = await postClient.UpdateAsync(post.Id, updatedModel, user.Token);

      // Assert
      result.Should().BeEquivalentTo(updatedModel);
    }

    [Theory]
    [CustomAutoData]
    public async Task PostDoesNotExist_ReturnsNotFound(PostCreateModel model) {
      // Arrange
      var user = fixture.Admin;

      // Act
      var result = await postClient.SendAsync($"API/posts/{Guid.NewGuid()}",
        HttpMethod.Put,
        new JsonContent(model),
        user.Token);

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [CustomAutoData]
    public async Task PostCreatedByOtherUser_ReturnsForbidden(PostCreateModel model) {
      // Arrange
      var user = fixture.Admin;
      var post = await postClient.CreateAsync(user.Token);
      var otherUser = await fixture.CreateAdminUserAsync("Other admin user");

      // Act
      var result = await postClient.SendAsync($"API/posts/{post.Id}",
        HttpMethod.Put,
        new JsonContent(model),
        otherUser.Token);

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Theory]
    [CustomAutoData]
    public async Task UserNotAdmin_ReturnsForbidden(PostCreateModel model) {
      // Arrange
      var admin = fixture.Admin;
      var post = await postClient.CreateAsync(admin.Token);
      var user = fixture.User;

      // Act
      var result = await postClient.SendAsync($"API/posts/{post.Id}",
        HttpMethod.Put,
        new JsonContent(model),
        user.Token);

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
  }
}
