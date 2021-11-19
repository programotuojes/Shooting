namespace Unit.Customizations {
  using AutoFixture;
  using DB;
  using DB.Entities;
  using Microsoft.EntityFrameworkCore;
  using Moq;

  public class DataContextCustomization : ICustomization {

    public void Customize(IFixture fixture) {
      var dataContext = new Mock<DataContext>();
      // dataContext.Posts = Mock.Of<DbSet<Post>>();
      // dataContext.Comments = Mock.Of<DbSet<Comment>>();
      // dataContext.Users = Mock.Of<DbSet<User>>();
      // dataContext.Competitions = Mock.Of<DbSet<Competition>>();

      dataContext.Setup(x => x.Add(It.IsAny<object>()))
        .Callback(() => { });
      dataContext.Setup(x => x.SaveChanges())
        .Callback(() => { });

      fixture.Inject(dataContext);
    }
  }
}
