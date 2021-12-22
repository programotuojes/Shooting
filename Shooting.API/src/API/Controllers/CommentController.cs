namespace API.Controllers {
  using System;
  using System.Collections.Generic;
  using System.Net;
  using Authorization;
  using DB.Entities;
  using Microsoft.AspNetCore.Mvc;
  using Models.Comment;
  using Services;

  [Authorize(Role.User, Role.Admin)]
  [ApiController]
  [Route("API/posts/{postId:guid}/comments")]
  public class CommentController : ControllerBase {

    private readonly CommentService commentService;

    public CommentController(CommentService commentService) {
      this.commentService = commentService;
    }

    [HttpPost]
    public ActionResult<CommentReadModel> Create(Guid postId, [FromBody] CommentCreateModel model) {
      var user = (User) HttpContext.Items["User"]!;
      var comment = commentService.Create(postId, model, user.Id);
      if (comment == null) return NotFound();

      return Created($"API/posts/{postId}/comments/{comment.Id}", comment);
    }

    [AllowAnonymous]
    [HttpGet("{commentId:guid}")]
    public ActionResult<CommentReadModel> Read(Guid postId, Guid commentId) {
      var comment = commentService.Read(postId, commentId);
      if (comment == null) return NotFound();

      return Ok(comment);
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<CommentReadModel>> ReadAll(Guid postId) {
      var comments = commentService.ReadAll(postId);
      if (comments == null) return NotFound();

      return Ok(comments);
    }

    [HttpPut("{commentId:guid}")]
    public ActionResult<CommentReadModel> Update(Guid postId, Guid commentId, [FromBody] CommentCreateModel model) {
      var createdById = commentService.GetCreatedById(commentId);
      if (createdById == null) return NotFound();

      var user = (User) HttpContext.Items["User"]!;
      if (user.Id != createdById) return StatusCode((int)HttpStatusCode.Forbidden);

      var comment = commentService.Update(postId, commentId, model);

      return Ok(comment);
    }

    [HttpDelete("{commentId:guid}")]
    public ActionResult Delete(Guid postId, Guid commentId) {
      var createdById = commentService.GetCreatedById(commentId);
      if (createdById == null) return NotFound();

      var user = (User) HttpContext.Items["User"]!;
      if (user.Id != createdById) return StatusCode((int)HttpStatusCode.Forbidden);

      commentService.Delete(postId, commentId);

      return NoContent();
    }
  }
}
