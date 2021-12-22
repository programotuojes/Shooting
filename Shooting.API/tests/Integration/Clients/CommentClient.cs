namespace Integration.Clients {
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using API.Models.Comment;
  using API.Models.Post;
  using Generators;
  using Utils;

  public class CommentClient {

    private readonly HttpClient client;

    public CommentClient(HttpClient client) {
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

    public async Task<CommentReadModel> CreateAsync(Guid postId, CommentCreateModel model, string token) {
      var request = HttpUtils.CreateRequest(
        $"API/posts/{postId}/comments",
        HttpMethod.Post,
        new JsonContent(model),
        token);

      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<CommentReadModel>();
    }

    public async Task<CommentReadModel> ReadAsync(Guid postId, Guid commentId) {
      var request = HttpUtils.CreateRequest(
        $"API/posts/{postId}/comments/{commentId}",
        HttpMethod.Get);

      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<CommentReadModel>();
    }

    public async Task<IEnumerable<CommentReadModel>> ReadAllAsync(Guid postId) {
      var request = HttpUtils.CreateRequest($"API/posts/{postId}/comments", HttpMethod.Get);
      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<IEnumerable<CommentReadModel>>();
    }

    public async Task<PostReadModel> UpdateAsync(Guid postId, Guid commentId, CommentCreateModel model, string token) {
      var request = HttpUtils.CreateRequest($"API/posts/{postId}/comments/{commentId}",
        HttpMethod.Put,
        new JsonContent(model),
        token);

      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<PostReadModel>();
    }

    public async Task<HttpStatusCode> DeleteAsync(Guid postId, Guid commentId, string token) {
      var request = HttpUtils.CreateRequest(
        $"API/posts/{postId}/comments/{commentId}",
        HttpMethod.Delete,
        token: token);

      var response = await client.SendAsync(request);
      return response.StatusCode;
    }
  }
}
