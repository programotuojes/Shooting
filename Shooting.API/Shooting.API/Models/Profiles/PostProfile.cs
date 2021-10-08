namespace Shooting.API.Models.Profiles {
  using AutoMapper;
  using Database.Entities;
  using Post;

  public class PostProfile : Profile {
    public PostProfile() {
      CreateMap<Post, PostReadModel>()
        .ForMember(
          dest => dest.CreatedBy,
          act => act.MapFrom(src => src.CreatedBy.Username));

      CreateMap<Post, PostReadAllModel>()
        .ForMember(
          dest => dest.CreatedBy,
          act => act.MapFrom(src => src.CreatedBy.Username));
    }
  }
}
