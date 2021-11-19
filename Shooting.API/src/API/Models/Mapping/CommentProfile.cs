namespace API.Models.Mapping {
  using AutoMapper;
  using Comment;
  using DB.Entities;

  public class CommentProfile : Profile{
    public CommentProfile() {
      CreateMap<CommentCreateModel, Comment>(MemberList.Source);

      CreateMap<Comment, CommentReadModel>()
        .ForMember(
          dest => dest.CreatedBy,
          act => act.MapFrom(src => src.CreatedBy.Username));
    }
  }
}
