namespace Integration.Controllers.Competition {
  using System;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using Clients;
  using FluentAssertions;
  using Generators;
  using Utils;
  using Xunit;

  public class UpdateTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly CompetitionClient client;

    public UpdateTests(TestFixture fixture) {
      this.fixture = fixture;
      client = new CompetitionClient(fixture.Client);
    }

    [Fact]
    public async Task CompetitionExists_ReturnsUpdatedCompetition() {
      // Arrange
      var admin = fixture.Admin;
      var model = CompetitionGenerator.CreateModel();
      var competition = await client.CreateAsync(model, admin.Token);
      var updatedModel = CompetitionGenerator.CreateModel(x => x.Name = "Updated name");

      // Act
      var result = await client.UpdateAsync(competition.Id, updatedModel, admin.Token);

      // Assert
      result.Should().BeEquivalentTo(updatedModel);
    }

    [Fact]
    public async Task CompetitionDoesNotExists_ReturnsNotFound() {
      // Arrange
      var admin = fixture.Admin;
      var model = CompetitionGenerator.CreateModel();

      // Act
      var result = await client.SendAsync(
        $"API/competitions/{Guid.NewGuid()}",
        HttpMethod.Put,
        new JsonContent(model),
        admin.Token);

      // Assert
      result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
  }
}
