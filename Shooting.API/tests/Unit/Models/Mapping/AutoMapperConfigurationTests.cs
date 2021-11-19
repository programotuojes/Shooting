namespace Unit.Models.Mapping {
  using API;
  using AutoMapper;
  using Microsoft.Extensions.DependencyInjection;
  using Xunit;

  public class AutoMapperConfigurationTests {

    [Fact]
    public void AddAutoMapperConfiguration_AssertAllConfigurationsValid() {
      // Arrange
      var services = new ServiceCollection();
      services.AddAutoMapper(typeof(Startup));

      var autoMapperConfig = services
        .BuildServiceProvider()
        .GetRequiredService<IConfigurationProvider>();

      // Act & assert
      autoMapperConfig.AssertConfigurationIsValid();
    }
  }
}
