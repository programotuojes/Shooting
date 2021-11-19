namespace Unit.Customizations {
  using AutoFixture;
  using AutoFixture.AutoMoq;
  using AutoFixture.Xunit2;

  public class CustomAutoDataAttribute : AutoDataAttribute {
    public CustomAutoDataAttribute() :
      base(() => new Fixture()
        .Customize(new AutoMoqCustomization { ConfigureMembers = true })
        .Customize(new AutoMapperCustomization())
        .Customize(new DataContextCustomization())
      ) { }
  }
}
