namespace Integration {
  using System;
  using System.Linq;
  using System.Net.Http;
  using System.Net.Http.Headers;
  using System.Reflection;
  using System.Threading.Tasks;
  using API;
  using API.Models.Users;
  using DB;
  using DB.Entities;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Mvc.ApplicationParts;
  using Microsoft.AspNetCore.Mvc.Controllers;
  using Microsoft.AspNetCore.Mvc.ViewComponents;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Utils;
  using Utils.Models;

  public class TestFixture : IDisposable {

    private DataContext DataContext { get; set; } = null!;
    private TestServer Server { get; }

    public HttpClient Client { get; }
    public TestUser User { get; }
    public TestUser Admin { get; }

    public TestFixture() {
      var configurationBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.Development.json");

      var webHostBuilder = new WebHostBuilder()
        .UseStartup(typeof(Startup))
        .ConfigureServices(InitializeServices)
        .UseConfiguration(configurationBuilder.Build())
        .UseEnvironment("Development");

      // Create instance of test server
      Server = new TestServer(webHostBuilder);

      // Add configuration for client
      Client = Server.CreateClient();
      Client.BaseAddress = new Uri("http://localhost:5000");
      Client.DefaultRequestHeaders.Accept.Clear();
      Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      User = CreateRegularUserAsync("Regular user").Result;
      Admin = CreateAdminUserAsync("Admin user").Result;
    }

    public void Dispose() {
      GC.SuppressFinalize(this);
      DataContext.Database.EnsureDeleted();

      DataContext.Dispose();
      Server.Dispose();
      Client.Dispose();
    }

    protected virtual void InitializeServices(IServiceCollection services) {
      var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;

      var manager = new ApplicationPartManager {
        ApplicationParts = { new AssemblyPart(startupAssembly) },
        FeatureProviders = {
          new ControllerFeatureProvider(),
          new ViewComponentFeatureProvider()
        }
      };

      services.AddSingleton(manager);

      var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<DataContext>));
      if (descriptor != null) services.Remove(descriptor);

      var randomInt = new Random().Next(0, 10000);
      services.AddDbContext<DataContext>(options =>
        options.UseNpgsql($"Host=localhost; Database=ShootingDB_test_{randomInt}; Username=postgres; Password=pass"));

      var sp = services.BuildServiceProvider();
      var scope = sp.CreateScope();
      DataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
      DataContext.Database.Migrate();
    }

    public async Task<TestUser> CreateRegularUserAsync(string username) {
      var registerModel = new AuthRequestModel {
        Username = username,
        Password = "pass"
      };

      var request = HttpUtils.CreateRequest("API/users", HttpMethod.Post, new JsonContent(registerModel));
      var response = await Client.SendAsync(request);
      var createdUser = await response.ReadJsonAs<AuthResponseModel>();

      return new TestUser {
        Id = createdUser.Id,
        Username = createdUser.Username,
        Password = "pass",
        Role = Role.User,
        Token = createdUser.Token
      };
    }

    public async Task<TestUser> CreateAdminUserAsync(string username) {
      var registerModel = new AuthRequestModel {
        Username = username,
        Password = "pass"
      };

      var request = HttpUtils.CreateRequest("API/users", HttpMethod.Post, new JsonContent(registerModel));
      var response = await Client.SendAsync(request);
      var createdUser = await response.ReadJsonAs<AuthResponseModel>();

      var testUser = new TestUser {
        Id = createdUser.Id,
        Username = createdUser.Username,
        Password = "pass",
        Role = Role.Admin,
        Token = createdUser.Token
      };

      // DataContext = Server.Services.GetRequiredService<DataContext>();
      await DataContext.Database.ExecuteSqlRawAsync($"UPDATE \"Users\" SET \"Role\" = {(int) Role.Admin} WHERE \"Id\" = '{createdUser.Id}'");

      return testUser;
    }
  }
}
