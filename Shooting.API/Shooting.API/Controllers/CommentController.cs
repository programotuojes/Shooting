namespace Shooting.API.Controllers {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using AutoMapper;
  using Database;
  using Database.Entities;
  using Microsoft.AspNetCore.Mvc;
  using Models.Comment;
  using Models.Post;

  [ApiController]
  [Route("API/posts/{postId:guid}/comments")]
  public class CommentController : ControllerBase {
    private readonly IMapper mapper;

    public CommentController(IMapper mapper) {
      this.mapper = mapper;
    }

    [HttpPost]
    public ActionResult<CommentReadModel> Create(Guid postId, [FromBody] CommentCreateModel commentModel) {
      var post = MockData.Posts.FirstOrDefault(x => x.Id == postId);
      if (post == null) return NotFound();

      var comment = new Comment {
        Id = Guid.NewGuid(),
        Content = commentModel.Content,
        CreatedById = MockData.User1.Id,
        CreatedBy = MockData.User1,
        CreatedOn = DateTime.UtcNow
      };

      post.Comments.Add(comment);
      var result = mapper.Map<CommentReadModel>(comment);
      return Created($"API/posts/{postId}/comments/{comment.Id}", result);
    }

    [HttpGet("{commentId:guid}")]
    public ActionResult<CommentReadModel> Read(Guid postId, Guid commentId) {
      var post = MockData.Posts.FirstOrDefault(x => x.Id == postId);
      if (post == null)
        return NotFound();

      var comment = post.Comments.FirstOrDefault(x => x.Id == commentId);
      if (comment == null)
        return NotFound();

      return Ok(mapper.Map<CommentReadModel>(comment));
    }

    [HttpGet]
    public ActionResult<CommentReadModel> ReadAll(Guid postId) {
      var post = MockData.Posts.FirstOrDefault(x => x.Id == postId);
      if (post == null)
        return NotFound();

      return Ok(mapper.Map<ICollection<CommentReadModel>>(post.Comments));
    }

    [HttpPut("{commentId:guid}")]
    public ActionResult<CommentReadModel> Update(Guid postId, Guid commentId, [FromBody] CommentCreateModel commentModel) {
      var post = MockData.Posts.FirstOrDefault(x => x.Id == postId);
      if (post == null) return NotFound();

      var comment = post.Comments.FirstOrDefault(x => x.Id == commentId);
      if (comment == null) return NotFound();

      comment.Content = commentModel.Content;
      return Ok(mapper.Map<CommentReadModel>(comment));
    }

    [HttpDelete("{commentId:guid}")]
    public ActionResult Delete(Guid postId, Guid commentId) {
      var post = MockData.Posts.FirstOrDefault(x => x.Id == postId);
      if (post == null) return NotFound();

      var comment = post.Comments.FirstOrDefault(x => x.Id == commentId);
      if (comment == null) return NotFound();

      post.Comments = post.Comments.Where(x => x.Id != commentId).ToList();
      return NoContent();
    }
  }
}
