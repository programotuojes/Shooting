namespace Integration.Controllers.User {
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using API.Models.Users;
  using Clients;
  using Customizations;
  using DB.Entities;
  using FluentAssertions;
  using Utils;
  using Xunit;

  public class RegisterTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly UserClient client;

    public RegisterTests(TestFixture fixture) {
      this.fixture = fixture;
      client = new UserClient(fixture.Client);
    }

    [Theory]
    [CustomAutoData]
    public async Task CreateNewUser_ReturnsCreatedUser(AuthRequestModel model) {
      // Arrange

      // Act
      var result = await client.Register(model);

      // Assert
      result.Username.Should().Be(model.Username);
      result.Role.Should().Be(Role.User);
      result.Token.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [CustomAutoData]
    public async Task UsernameTaken_ReturnsBadRequest(AuthRequestModel model) {
      // Arrange
      var user = fixture.User;
      model.Username = user.Username;

      // Act
      var result = await client.SendAsync("API/users", HttpMethod.Post, new JsonContent(model));

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
  }
}
