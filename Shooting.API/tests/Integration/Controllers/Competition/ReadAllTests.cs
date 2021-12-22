namespace Integration.Controllers.Competition {
  using System.Threading.Tasks;
  using Clients;
  using FluentAssertions;
  using Generators;
  using Xunit;

  public class ReadAllTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly CompetitionClient client;

    public ReadAllTests(TestFixture fixture) {
      this.fixture = fixture;
      client = new CompetitionClient(fixture.Client);
    }

    [Fact]
    public async Task TwoCompetitionsExist_ReturnsBoth() {
      // Arrange
      var admin = fixture.Admin;
      var model1 = CompetitionGenerator.CreateModel(x => x.Name = "First competition");
      var model2 = CompetitionGenerator.CreateModel(x => x.Name = "Second competition");
      await client.CreateAsync(model1, admin.Token);
      await client.CreateAsync(model2, admin.Token);

      // Act
      var result = await client.ReadAllAsync();

      // Assert
      result.Should().HaveCount(2);
    }

    [Fact]
    public async Task ZeroCompetitionsExist_ReturnsEmptyList() {
      // Arrange

      // Act
      var result = await client.ReadAllAsync();

      // Assert
      result.Should().BeEmpty();
    }
  }
}
