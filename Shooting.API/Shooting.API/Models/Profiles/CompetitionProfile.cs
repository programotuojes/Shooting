namespace Shooting.API.Models.Profiles {
  using AutoMapper;
  using Competition;
  using Database.Entities;

  public class CompetitionProfile : Profile {
    public CompetitionProfile() {
      CreateMap<Competition, CompetitionReadModel>();
      CreateMap<Competition, CompetitionReadAllModel>();
    }
  }
}
