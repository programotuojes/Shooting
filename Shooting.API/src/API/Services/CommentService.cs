namespace API.Services {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using AutoMapper;
  using AutoMapper.QueryableExtensions;
  using DB;
  using DB.Entities;
  using Microsoft.EntityFrameworkCore;
  using Models.Comment;

  public class CommentService {

    private readonly DataContext dataContext;
    private readonly IMapper mapper;

    public CommentService(DataContext dataContext, IMapper mapper) {
      this.dataContext = dataContext;
      this.mapper = mapper;
    }

    public CommentReadModel? Create(Guid postId, CommentCreateModel model, Guid userId) {
      var post = dataContext.Posts.Find(postId);
      if (post == null) return null;

      var comment = mapper.Map<Comment>(model);
      comment.CreatedById = userId;
      comment.PostId = post.Id;

      dataContext.Add(comment);
      dataContext.SaveChanges();

      return dataContext.Comments
        .Where(x => x.Id == comment.Id && x.PostId == postId)
        .AsNoTracking()
        .ProjectTo<CommentReadModel>(mapper.ConfigurationProvider)
        .First();
    }

    public CommentReadModel? Read(Guid postId, Guid commentId) {
      return dataContext.Comments
        .Where(x => x.Id == commentId && x.PostId == postId)
        .AsNoTracking()
        .ProjectTo<CommentReadModel>(mapper.ConfigurationProvider)
        .FirstOrDefault();
    }

    public IEnumerable<CommentReadModel>? ReadAll(Guid postId) {
      var post = dataContext.Posts.Find(postId);
      if (post == null) return null;

      return dataContext.Comments
        .Where(x => x.PostId == post.Id)
        .AsNoTracking()
        .ProjectTo<CommentReadModel>(mapper.ConfigurationProvider);
    }

    public CommentReadModel? Update(Guid postId, Guid commentId, CommentCreateModel model) {
      var comment = dataContext.Comments
        .Include(x => x.CreatedBy)
        .FirstOrDefault(x => x.Id == commentId && x.PostId == postId);

      if (comment == null) return null;

      mapper.Map(model, comment);
      dataContext.Update(comment);
      dataContext.SaveChanges();

      return mapper.Map<CommentReadModel>(comment);
    }

    public bool Delete(Guid postId, Guid commentId) {
      var comment = dataContext.Comments.FirstOrDefault(x => x.Id == commentId && x.PostId == postId);
      if (comment == null) return false;

      dataContext.Entry(comment).State = EntityState.Deleted;
      dataContext.SaveChanges();

      return true;
    }

    public Guid? GetCreatedById(Guid commentId) {
      return dataContext.Comments.Find(commentId)?.CreatedById;
    }
  }
}
