namespace Integration.Controllers.Competition {
  using System;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using Clients;
  using FluentAssertions;
  using Generators;
  using Xunit;

  public class ReadTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly CompetitionClient client;

    public ReadTests(TestFixture fixture) {
      this.fixture = fixture;
      client = new CompetitionClient(fixture.Client);
    }

    [Fact]
    public async Task CompetitionExists_ReturnsCompetition() {
      // Arrange
      var admin = fixture.Admin;
      var model = CompetitionGenerator.CreateModel();
      var competition = await client.CreateAsync(model, admin.Token);

      // Act
      var result = await client.ReadAsync(competition.Id);

      // Assert
      result.Should().BeEquivalentTo(competition);
    }

    [Fact]
    public async Task CompetitionDoesMotExist_ReturnsNotFound() {
      // Arrange

      // Act
      var result = await client.SendAsync($"API/competitions/{Guid.NewGuid()}", HttpMethod.Get);

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
  }
}
