namespace Integration.Clients {
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using API.Models.Competition;
  using Utils;

  public class CompetitionClient {

    private readonly HttpClient client;

    public CompetitionClient(HttpClient client) {
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

    public async Task<CompetitionReadModel> CreateAsync(CompetitionCreateModel model, string token) {
      var request = HttpUtils.CreateRequest(
        "API/competitions",
        HttpMethod.Post,
        new JsonContent(model),
        token);

      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<CompetitionReadModel>();
    }

    public async Task<CompetitionReadModel> ReadAsync(Guid id) {
      var request = HttpUtils.CreateRequest($"API/competitions/{id}", HttpMethod.Get);
      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<CompetitionReadModel>();
    }

    public async Task<IEnumerable<CompetitionReadAllModel>> ReadAllAsync() {
      var request = HttpUtils.CreateRequest("API/competitions", HttpMethod.Get);
      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<IEnumerable<CompetitionReadAllModel>>();
    }

    public async Task<CompetitionReadModel> UpdateAsync(
      Guid competitionId,
      CompetitionCreateModel model,
      string token
    ) {
      var request = HttpUtils.CreateRequest($"API/competitions/{competitionId}",
        HttpMethod.Put,
        new JsonContent(model),
        token);

      var response = await client.SendAsync(request);
      return await response.ReadJsonAs<CompetitionReadModel>();
    }

    public async Task<HttpStatusCode> DeleteAsync(Guid competitionId, string token) {
      var request = HttpUtils.CreateRequest(
        $"API/competitions/{competitionId}",
        HttpMethod.Delete,
        token: token);

      var response = await client.SendAsync(request);
      return response.StatusCode;
    }
  }
}
