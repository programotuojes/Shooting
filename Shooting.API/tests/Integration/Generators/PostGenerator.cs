namespace Integration.Generators {
  using System;
  using API.Models.Post;

  public static class PostGenerator {

    public static PostCreateModel CreateModel(Action<PostCreateModel>? creator = null) {
      var model = new PostCreateModel {
        Title = "Test post",
        Description = "Test description",
        Body = "Test body",
        ImageUrl = "https://example.com/image",
        ImageLabel = "Example image"
      };

      creator?.Invoke(model);
      return model;
    }
  }
}
