namespace Unit.Customizations {
  using System.Collections.Generic;
  using API.Models.Competitor;
  using AutoFixture;
  using DB.Entities;

  public class EmptyEntityListCustomization : ICustomization {

    public void Customize(IFixture fixture) {
      fixture.Register<ICollection<Comment>>(() => new List<Comment>());
      fixture.Register<ICollection<Competitor>>(() => new List<Competitor>());

      fixture.Register<ICollection<CompetitorCreateModel>>(() => new List<CompetitorCreateModel>());
    }
  }
}
