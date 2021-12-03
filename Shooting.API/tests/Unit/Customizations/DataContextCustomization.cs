namespace Unit.Customizations {
  using System;
  using API.Models.Competition;
  using AutoFixture;
  using DB;
  using DB.Entities;
  using FluentAssertions.Extensions;
  using Microsoft.EntityFrameworkCore;

  public class DataContextCustomization : ICustomization {

    public void Customize(IFixture fixture) {
      var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase("Test DB")
        .Options;

      var dataContext = new DataContext(options);
      var user = new User {
        Username = "Test user",
        Password = "Test password",
        Role = Role.User
      };

      dataContext.Users.Add(user);
      dataContext.SaveChanges();

      fixture.Customize<Post>(composer => composer.With(x => x.CreatedBy, user));
      fixture.Customize<Comment>(composer => composer.With(x => x.CreatedBy, user));
      fixture.Customize<CompetitionCreateModel>(composer => composer
        .With(x => x.DateFrom, DateTime.UtcNow)
        .With(x => x.DateTo, DateTime.UtcNow.Add(10.Days())));

      fixture.Inject(user);
      fixture.Inject(dataContext);
    }
  }
}
