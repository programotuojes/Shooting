namespace API.Controllers {
  using System;
  using Authorization;
  using DB.Entities;
  using Microsoft.AspNetCore.Mvc;
  using Models.Competition;
  using Services;

  [ApiController]
  [Route("API/competitions")]
  public class CompetitionController : ControllerBase {

    private readonly CompetitionService competitionService;

    public CompetitionController(CompetitionService competitionService) {
      this.competitionService = competitionService;
    }

    [Authorize(Role.Admin)]
    [HttpPost]
    public ActionResult<CompetitionReadModel> Create(CompetitionCreateModel model) {
      var competition = competitionService.Create(model);

      return Created($"API/competitions/{competition.Id}", competition);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public ActionResult<CompetitionReadModel> Read(Guid id) {
      var competition = competitionService.Read(id);
      if (competition == null) return NotFound();

      return Ok(competition);
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult<CompetitionReadAllModel> ReadAll() {
      var competitions = competitionService.ReadAll();

      return Ok(competitions);
    }

    [Authorize(Role.Admin)]
    [HttpPut("{id:guid}")]
    public ActionResult<CompetitionReadModel> Update(Guid id, [FromBody] CompetitionCreateModel model) {
      var competition = competitionService.Update(id, model);
      if (competition == null) return NotFound();

      return Ok(competition);
    }

    [Authorize(Role.Admin)]
    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id) {
      var deleted = competitionService.Delete(id);
      if (!deleted) return NotFound();

      return NoContent();
    }
  }
}
