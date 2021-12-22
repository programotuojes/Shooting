namespace Integration.Controllers.Competition {
  using System;
  using System.Net;
  using System.Threading.Tasks;
  using Clients;
  using FluentAssertions;
  using Generators;
  using Xunit;

  public class DeleteTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly CompetitionClient client;

    public DeleteTests(TestFixture fixture) {
      this.fixture = fixture;
      client = new CompetitionClient(fixture.Client);
    }

    [Fact]
    public async Task CompetitionExists_ReturnsNoContent() {
      // Arrange
      var admin = fixture.Admin;
      var model = CompetitionGenerator.CreateModel();
      var competition = await client.CreateAsync(model, admin.Token);

      // Act
      var result = await client.DeleteAsync(competition.Id, admin.Token);

      // Assert
      result.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task CompetitionDoesNotExist_ReturnsNotFound() {
      // Arrange
      var admin = fixture.Admin;

      // Act
      var result = await client.DeleteAsync(Guid.NewGuid(), admin.Token);

      // Assert
      result.Should().Be(HttpStatusCode.NotFound);
    }
  }
}
