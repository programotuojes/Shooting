namespace DB {
  using Entities;
  using Microsoft.EntityFrameworkCore;

  public class DataContext : DbContext {

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Competition> Competitions => Set<Competition>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      // optionsBuilder.UseNpgsql("Host=localhost; Database=ShootingDB; Username=postgres; Password=pass");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Post>()
        .HasMany(x => x.Comments)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
