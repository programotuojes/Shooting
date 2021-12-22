namespace API.Controllers {
  using System;
  using System.Linq;
  using System.Net;
  using Authorization;
  using DB.Entities;
  using Microsoft.AspNetCore.Mvc;
  using Models.Post;
  using Services;

  [ApiController]
  [Route("API/posts")]
  public class PostController : ControllerBase {

    private readonly PostService postService;

    public PostController(PostService postService) {
      this.postService = postService;
    }

    [HttpPost]
    [Authorize(Role.Admin)]
    public ActionResult<PostReadModel> Create(PostCreateModel model) {
      var user = (User) HttpContext.Items["User"]!;
      var post = postService.Create(model, user.Id);

      return Created($"API/posts/{post.Id}", post);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public ActionResult<PostReadModel> Read(Guid id) {
      var post = postService.Read(id);
      if (post == null) return NotFound();

      post.Comments = post.Comments.OrderBy(x => x.CreatedOn).ToList();

      return Ok(post);
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult<PostReadModel> ReadAll() {
      var posts = postService.ReadAll().OrderByDescending(x => x.CreatedOn);

      return Ok(posts);
    }

    [Authorize(Role.Admin)]
    [HttpPut("{id:guid}")]
    public ActionResult<PostReadModel> Update(Guid id, [FromBody] PostCreateModel model) {
      var user = (User) HttpContext.Items["User"]!;

      var createdById = postService.GetCreatedById(id);
      if (createdById == null) return NotFound();

      if (user.Id != createdById)
        return StatusCode((int)HttpStatusCode.Forbidden);

      var post = postService.Update(id, model);

      return Ok(post);
    }

    [Authorize(Role.Admin)]
    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id) {
      var user = (User) HttpContext.Items["User"]!;

      var createdById = postService.GetCreatedById(id);
      if (createdById == null) return NotFound();

      if (user.Id != createdById)
        return StatusCode((int)HttpStatusCode.Forbidden);

      postService.Delete(id);
      return NoContent();
    }
  }
}
