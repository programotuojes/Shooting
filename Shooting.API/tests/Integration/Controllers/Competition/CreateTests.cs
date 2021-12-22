namespace Integration.Controllers.Competition {
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using Clients;
  using FluentAssertions;
  using Generators;
  using Utils;
  using Xunit;

  public class CreateTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly CompetitionClient client;

    public CreateTests(TestFixture fixture) {
      this.fixture = fixture;
      client = new CompetitionClient(fixture.Client);
    }

    [Fact]
    public async Task CreateCompetition_ReturnsCreatedCompetition() {
      // Arrange
      var admin = fixture.Admin;
      var model = CompetitionGenerator.CreateModel();

      // Act
      var result = await client.CreateAsync(model, admin.Token);

      // Assert
      result.Should().BeEquivalentTo(model);
    }

    [Fact]
    public async Task UserNotAdmin_ReturnsForbidden() {
      // Arrange
      var user = fixture.User;
      var model = CompetitionGenerator.CreateModel();

      // Act
      var result = await client.SendAsync(
        "API/competitions",
        HttpMethod.Post,
        new JsonContent(model),
        user.Token);

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
  }
}
