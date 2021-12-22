namespace Integration.Clients {
  using System.Net.Http;
  using System.Threading.Tasks;
  using API.Models.Users;
  using Utils;

  public class UserClient {

    private readonly HttpClient client;

    public UserClient(HttpClient client) {
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

    public async Task<AuthResponseModel> Register(AuthRequestModel model) {
      var request = HttpUtils.CreateRequest("API/users", HttpMethod.Post, new JsonContent(model));
      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<AuthResponseModel>();
    }

    public async Task<AuthResponseModel> Login(AuthRequestModel model) {
      var request = HttpUtils.CreateRequest("API/users/login", HttpMethod.Post, new JsonContent(model));
      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<AuthResponseModel>();
    }
  }
}
