namespace API.Models.Mapping {
  using AutoMapper;
  using Competitor;
  using DB.Entities;

  public class CompetitorProfile : Profile {
    public CompetitorProfile() {
      CreateMap<CompetitorCreateModel, Competitor>(MemberList.Source);

      CreateMap<Competitor, CompetitorReadModel>();
    }
  }
}
