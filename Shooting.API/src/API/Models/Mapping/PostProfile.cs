namespace API.Models.Mapping {
  using AutoMapper;
  using DB.Entities;
  using Post;

  public class PostProfile : Profile {
    public PostProfile() {
      CreateMap<PostCreateModel, Post>(MemberList.Source);

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
