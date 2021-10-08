namespace Shooting.API.Models.Profiles {
  using AutoMapper;
  using Competitor;
  using Database.Entities;

  public class CompetitorProfile : Profile {
    public CompetitorProfile() {
      CreateMap<CompetitorCreateModel, Competitor>();
      CreateMap<Competitor, CompetitorReadModel>();
    }
  }
}
