namespace Integration.Customizations {
  using API.Models.Post;
  using AutoFixture;

  public class CreatePostModelCustomizations : ICustomization {

    public void Customize(IFixture fixture) {
      fixture.Customize<PostCreateModel>(composer => composer
        .With(x => x.ImageUrl, "https://example.com/image.png"));
    }
  }
}
