namespace API.Services {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using AutoMapper;
  using AutoMapper.QueryableExtensions;
  using DB;
  using DB.Entities;
  using Microsoft.EntityFrameworkCore;
  using Models.Post;

  public class PostService {

    private readonly DataContext dataContext;
    private readonly IMapper mapper;

    public PostService(DataContext dataContext, IMapper mapper) {
      this.dataContext = dataContext;
      this.mapper = mapper;
    }

    public PostReadModel Create(PostCreateModel model, Guid userId) {
      var post = mapper.Map<Post>(model);
      post.CreatedById = userId;

      dataContext.Add(post);
      dataContext.SaveChanges();

      return dataContext.Posts
        .AsNoTracking()
        .ProjectTo<PostReadModel>(mapper.ConfigurationProvider)
        .First(x => x.Id == post.Id);
    }

    public PostReadModel? Read(Guid id) {
      return dataContext.Posts
        .AsNoTracking()
        .ProjectTo<PostReadModel>(mapper.ConfigurationProvider)
        .FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<PostReadAllModel> ReadAll() {
      return dataContext.Posts
        .Include(x => x.CreatedBy)
        .AsNoTracking()
        .ProjectTo<PostReadAllModel>(mapper.ConfigurationProvider);
    }

    public PostReadModel? Update(Guid id, PostCreateModel model) {
      var post = dataContext.Posts
        .Include(x => x.CreatedBy)
        .FirstOrDefault(x => x.Id == id);

      if (post == null) return null;

      mapper.Map(model, post);
      dataContext.Update(post);
      dataContext.SaveChanges();

      return mapper.Map<PostReadModel>(post);
    }

    public bool Delete(Guid id) {
      var post = dataContext.Posts.Find(id);
      if (post == null) return false;

      dataContext.Entry(post).State = EntityState.Deleted;
      dataContext.SaveChanges();

      return true;
    }

    public Guid? GetCreatedById(Guid postId) {
      return dataContext.Posts.Find(postId)?.CreatedById;
    }
  }
}
