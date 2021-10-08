namespace Shooting.API.Models.Profiles {
  using AutoMapper;
  using Comment;
  using Database.Entities;

  public class CommentProfile : Profile{
    public CommentProfile() {
      CreateMap<Comment, CommentReadModel>()
        .ForMember(
          dest => dest.CreatedBy,
          act => act.MapFrom(src => src.CreatedBy.Username));
    }
  }
}
