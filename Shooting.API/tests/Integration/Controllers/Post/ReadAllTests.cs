namespace Integration.Controllers.Post {
  using System.Threading.Tasks;
  using Clients;
  using FluentAssertions;
  using Xunit;

  public class ReadAllTests : IClassFixture<TestFixture> {

    private readonly TestFixture fixture;
    private readonly PostClient postClient;

    public ReadAllTests(TestFixture fixture) {
      this.fixture = fixture;
      postClient = new PostClient(fixture.Client);
    }

    [Fact]
    public async Task TwoPostsExist_ReturnsPostList() {
      // Arrange
      var user = fixture.Admin;
      var existing1 = await postClient.CreateAsync(user.Token);
      var existing2 = await postClient.CreateAsync(user.Token);

      // Act
      var result = await postClient.ReadAllAsync();

      // Assert
      result.Should()
        .HaveCount(2)
        .And
        .ContainEquivalentOf(existing1,
          options => options
            .Excluding(x => x.Comments)
            .Excluding(x => x.Body))
        .And
        .ContainEquivalentOf(existing2,
          options => options
            .Excluding(x => x.Comments)
            .Excluding(x => x.Body));
    }
  }
}
