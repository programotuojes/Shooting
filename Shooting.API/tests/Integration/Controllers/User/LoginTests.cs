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

  public class LoginTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly UserClient client;

    public LoginTests(TestFixture fixture) {
      this.fixture = fixture;
      client = new UserClient(fixture.Client);
    }

    [Fact]
    public async Task CorrectCredentials_TokenReturned() {
      // Arrange
      var user = fixture.User;
      var model = new AuthRequestModel { Username = user.Username, Password = user.Password };

      // Act
      var response = await client.Login(model);

      // Assert
      response.Role.Should().Be(Role.User);
      response.Token.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [CustomAutoData]
    public async Task BadCredentials_TokenReturned(AuthRequestModel model) {
      // Arrange

      // Act
      var response = await client.SendAsync("API/users/login", HttpMethod.Post, new JsonContent(model));

      // Assert
      response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
  }
}
