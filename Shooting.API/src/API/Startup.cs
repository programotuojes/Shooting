namespace API {
  using System.Text.Json.Serialization;
  using Authorization;
  using DB;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Services;

  public class Startup {

    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration) {
      this.configuration = configuration;
    }

    // Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services) {
      services.AddDbContext<DataContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

      services.AddControllers()
        .AddJsonOptions(options =>
          options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

      services.AddAutoMapper(typeof(Startup));
      services.AddTransient(typeof(PostService));
      services.AddTransient(typeof(CommentService));
      services.AddTransient(typeof(CompetitionService));

      // Auth
      services.Configure<JwtConfig>(configuration.GetSection("Jwt"));
      services.AddScoped(typeof(JwtUtils));
      services.AddScoped(typeof(UserService));
    }

    // Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext) {
      app.UseMiddleware<ErrorHandlerMiddleware>();
      app.UseMiddleware<JwtMiddleware>();

      app.UseRouting();
      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}
