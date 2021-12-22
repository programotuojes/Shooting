namespace Integration.Controllers.Post {
  using System;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using API.Models.Post;
  using Clients;
  using FluentAssertions;
  using Generators;
  using Utils;
  using Xunit;

  public class CreateTests : IClassFixture<TestFixture> {

    private readonly PostClient postClient;
    private readonly TestFixture fixture;

    public CreateTests(TestFixture fixture) {
      this.fixture = fixture;
      postClient = new PostClient(fixture.Client);
    }

    [Fact]
    public async Task CreatePost_ReturnsCreatedPost() {
      // Arrange
      var user = fixture.Admin;
      var postModel = PostGenerator.CreateModel();
      var dateTimeBefore = DateTime.UtcNow;

      // Act
      var createdPost = await postClient.CreateAsync(user.Token);

      // Assert
      createdPost.CreatedOn.Should().BeAfter(dateTimeBefore).And.BeBefore(DateTime.UtcNow);

      createdPost.Should()
        .BeEquivalentTo(new PostReadModel {
            Title = postModel.Title,
            Description = postModel.Description,
            Body = postModel.Body,
            ImageUrl = postModel.ImageUrl,
            ImageLabel = postModel.ImageLabel,
            CreatedBy = user.Username
          },
          options => options
            .Excluding(x => x.Id)
            .Excluding(x => x.CreatedOn));
    }

    [Fact]
    public async Task UserNotAdmin_Forbidden() {
      // Arrange
      var user = fixture.User;
      var model = PostGenerator.CreateModel();

      // Act
      var response = await postClient.SendAsync("API/posts", HttpMethod.Post, new JsonContent(model), user.Token);

      // Assert
      response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
  }
}
