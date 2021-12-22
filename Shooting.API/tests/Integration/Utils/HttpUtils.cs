namespace Integration.Utils {
  using System.Net.Http;
  using System.Net.Http.Headers;
  using System.Threading.Tasks;
  using Newtonsoft.Json;

  public static class HttpUtils {

    public static HttpRequestMessage CreateRequest(
      string path,
      HttpMethod method,
      HttpContent? content = null,
      string token = ""
    ) {
      var request = new HttpRequestMessage(method, path);

      if (content != null)
        request.Content = content;

      if (token != string.Empty)
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

      return request;
    }

    public static async Task<T> ReadJsonAs<T>(this HttpResponseMessage httpResponse) {
      var content = await httpResponse.Content.ReadAsStringAsync();
      var settings = new JsonSerializerSettings {
        MissingMemberHandling = MissingMemberHandling.Error
      };

      return JsonConvert.DeserializeObject<T>(content, settings);
    }
  }
}
