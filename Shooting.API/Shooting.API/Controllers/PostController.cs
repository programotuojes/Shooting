namespace Shooting.API.Controllers {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using AutoMapper;
  using Database;
  using Database.Entities;
  using Microsoft.AspNetCore.Mvc;
  using Models.Post;

  [ApiController]
  [Route("API/posts")]
  public class PostController : ControllerBase {
    private readonly IMapper mapper;

    public PostController(IMapper mapper) {
      this.mapper = mapper;
    }

    [HttpPost]
    public ActionResult<PostReadModel> Create(PostCreateModel postModel) {
      var post = new Post {
        Id = Guid.NewGuid(),
        Title = postModel.Title,
        Body = postModel.Body,
        CreatedById = MockData.User1.Id,
        CreatedBy = MockData.User1,
        CreatedOn = DateTime.UtcNow
      };

      MockData.Posts.Add(post);
      var result = mapper.Map<PostReadModel>(post);
      return Created($"API/posts/{result.Id}", result);
    }

    [HttpGet("{postId:guid}")]
    public ActionResult<PostReadModel> Read(Guid postId) {
      var post = MockData.Posts.FirstOrDefault(x => x.Id == postId);
      if (post == null) return NotFound();

      return Ok(mapper.Map<PostReadModel>(post));
    }

    [HttpGet]
    public ActionResult<PostReadModel> ReadAll() {
      var posts = mapper.Map<ICollection<PostReadAllModel>>(MockData.Posts);
      return Ok(posts);
    }

    [HttpPut("{postId:guid}")]
    public ActionResult<PostReadModel> Update(Guid postId, [FromBody] PostCreateModel postModel) {
      var originalPost = MockData.Posts.FirstOrDefault(x => x.Id == postId);
      if (originalPost == null) return NotFound();

      originalPost.Title = postModel.Title;
      originalPost.Body = postModel.Body;
      return Ok(mapper.Map<PostReadModel>(originalPost));
    }

    [HttpDelete("{postId:guid}")]
    public ActionResult Delete(Guid postId) {
      var originalPost = MockData.Posts.FirstOrDefault(x => x.Id == postId);
      if (originalPost == null) return NotFound();

      MockData.Posts = MockData.Posts.Where(x => x.Id != postId).ToList();
      return NoContent();
    }
  }
}
