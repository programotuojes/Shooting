namespace Integration.Clients {
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using API.Models.Post;
  using Generators;
  using Utils;

  public class PostClient {

    private readonly HttpClient client;

    public PostClient(HttpClient client) {
      this.client = client;
    }

    public Task<HttpResponseMessage> SendAsync(
      string path,
      HttpMethod method,
      HttpContent? content = null,
      string token = ""
    ) {
      var request = HttpUtils.CreateRequest(path, method, content, token);
      return client.SendAsync(request);
    }

    public async Task<PostReadModel> CreateAsync(string token) {
      var model = PostGenerator.CreateModel();
      var request = HttpUtils.CreateRequest("API/posts", HttpMethod.Post, new JsonContent(model), token);
      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<PostReadModel>();
    }

    public async Task<PostReadModel> ReadAsync(Guid id) {
      var request = HttpUtils.CreateRequest($"API/posts/{id}", HttpMethod.Get);
      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<PostReadModel>();
    }

    public async Task<IEnumerable<PostReadAllModel>> ReadAllAsync() {
      var request = HttpUtils.CreateRequest("API/posts", HttpMethod.Get);
      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<IEnumerable<PostReadAllModel>>();
    }

    public async Task<PostReadModel> UpdateAsync(Guid postId, PostCreateModel model, string token) {
      var request = HttpUtils.CreateRequest($"API/posts/{postId}", HttpMethod.Put, new JsonContent(model), token);
      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<PostReadModel>();
    }

    public async Task<HttpStatusCode> DeleteAsync(Guid postId, string token) {
      var request = HttpUtils.CreateRequest($"API/posts/{postId}", HttpMethod.Delete, token: token);
      var response = await client.SendAsync(request);
      return response.StatusCode;
    }
  }
}
