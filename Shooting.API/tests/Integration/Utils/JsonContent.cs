namespace Integration.Utils {
  using System.Net.Http;
  using System.Text;
  using Newtonsoft.Json;

  public class JsonContent : StringContent {

    public JsonContent(object content) :
      base(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json") { }
  }
}
