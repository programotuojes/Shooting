namespace Unit.Customizations {
  using System;
  using API;
  using AutoFixture;
  using AutoMapper;
  using Microsoft.Extensions.DependencyInjection;

  public class AutoMapperCustomization : ICustomization {

    private static readonly Lazy<IConfigurationProvider> configProvider = new(CreateAutoMapperConfig, true);

    public void Customize(IFixture fixture) {
      var provider = configProvider.Value;
      var mapper = provider.CreateMapper();

      fixture.Inject(provider);
      fixture.Inject(mapper);
    }

    private static IConfigurationProvider CreateAutoMapperConfig() {
      var services = new ServiceCollection();
      services.AddAutoMapper(typeof(Startup));

      var config = services
        .BuildServiceProvider()
        .GetRequiredService<IConfigurationProvider>();

      config.CompileMappings();
      return config;
    }
  }
}
