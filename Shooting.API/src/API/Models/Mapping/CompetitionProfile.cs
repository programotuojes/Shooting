namespace API.Models.Mapping {
  using AutoMapper;
  using Competition;
  using DB.Entities;

  public class CompetitionProfile : Profile {
    public CompetitionProfile() {
      CreateMap<CompetitionCreateModel, Competition>(MemberList.Source);

      CreateMap<Competition, CompetitionReadModel>();

      CreateMap<Competition, CompetitionReadAllModel>();
    }
  }
}
